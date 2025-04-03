using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class ActivityDbRepos
{
    private readonly ILogger<ActivityDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region constructors
    public ActivityDbRepos(ILogger<ActivityDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IActivity>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<ActivityDbM> query = _dbContext.Activities.AsNoTracking();

        if (!flat)
        {
            query = query.Include(i => i.ActivityLevelDbM)
                         .Include(i => i.PatientDbM);
        }

        var resp = await query.FirstOrDefaultAsync(i => i.ActivityId == id);
        return new ResponseItemDto<IActivity>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IActivity>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<ActivityDbM> query = _dbContext.Activities.AsNoTracking();

        if (!flat)
        {
            query = query.Include(i => i.PatientDbM)
                         .Include(i => i.ActivityLevelDbM);
        }

        query = query.Where(i =>
            i.StrDate.ToLower().Contains(filter) ||
            i.StrDayOfWeek.ToLower().Contains(filter) ||
            i.Notes.ToLower().Contains(filter) ||
            i.PatientDbM.FirstName.ToLower().Contains(filter) ||
            i.PatientDbM.LastName.ToLower().Contains(filter) ||
            i.PatientDbM.PersonalNumber.ToLower().Contains(filter) ||
            i.ActivityLevelDbM.Name.ToLower().Contains(filter) ||
            i.ActivityLevelDbM.Rating.ToString().ToLower().Contains(filter)
        );

        return new ResponsePageDto<IActivity>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync<IActivity>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDto<IActivity>> DeleteItemAsync(Guid id)
    {
        var item = await _dbContext.Activities.FirstOrDefaultAsync(a => a.ActivityId == id)
                    ?? throw new ArgumentException($"Item: {id} does not exist");

        _dbContext.Activities.Remove(item);
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IActivity>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IActivity>> UpdateItemAsync(ActivityCuDto itemDto)
    {
        var item = await _dbContext.Activities
            .Include(i => i.ActivityLevelDbM)
            .Include(i => i.PatientDbM)
            .FirstOrDefaultAsync(i => i.ActivityId == itemDto.ActivityId)
            ?? throw new ArgumentException($"Item {itemDto.ActivityId} does not exist");

        item.UpdateFromDTO(itemDto);
        await UpdateNavigationProp(itemDto, item);

        _dbContext.Activities.Update(item);
        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.ActivityId, false);
    }

    public async Task<ResponseItemDto<IActivity>> CreateItemAsync(ActivityCuDto itemDto)
    {
        if (itemDto.ActivityId != null)
            throw new ArgumentException($"{nameof(itemDto.ActivityId)} must be null when creating a new object");

        var item = new ActivityDbM(itemDto);
        await UpdateNavigationProp(itemDto, item);

        _dbContext.Activities.Add(item);
        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.ActivityId, false);
    }

    private async Task UpdateNavigationProp(ActivityCuDto itemDto, ActivityDbM item)
    {
        // Kontrollera att ActivityLevel finns
        var activityLevel = await _dbContext.ActivityLevels
            .FirstOrDefaultAsync(a => a.ActivityLevelId == itemDto.ActivityLevelId)
            ?? throw new ArgumentException($"ActivityLevel with ID {itemDto.ActivityLevelId} does not exist");

        item.ActivityLevelDbM = activityLevel;

        // Kontrollera att Patient finns
        var patient = await _dbContext.Patients
            .FirstOrDefaultAsync(a => a.PatientId == itemDto.PatientId)
            ?? throw new ArgumentException($"Patient with ID {itemDto.PatientId} does not exist");

        item.PatientDbM = patient;
    }
}
