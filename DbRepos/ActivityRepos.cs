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

    #region contructors
    public ActivityDbRepos(ILogger<ActivityDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IActivity>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<ActivityDbM> query;
        if (!flat)
        {
            query = _dbContext.Activities.AsNoTracking()
                .Include(i => i.PatientDbM)
                .Where(i => i.ActivityId == id);
        }
        else
        {
            query = _dbContext.Activities.AsNoTracking()
                .Where(i => i.ActivityId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IActivity>();
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
            query = _dbContext.Activities.AsNoTracking()
            .Include(i => i.PatientDbM);

        }


        query = query.Where(i =>
                i.StrDate.ToLower().Contains(filter) ||
                i.StrDayOfWeek.ToLower().Contains(filter) ||
                i.PatientDbM.FirstName.ToLower().Contains(filter) ||
                i.Notes.ToLower().Contains(filter)
           );

        return new ResponsePageDto<IActivity>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<IActivity>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    }

    public async Task<ResponseItemDto<IActivity>> DeleteItemAsync(Guid id)
    {
        var query = _dbContext.Activities
        .Where(a => a.ActivityId == id);
        var item = await query.FirstOrDefaultAsync() ?? throw new ArgumentException($"Item: {id} is not existing");
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
        // Fetch the patient from the database using the PatientId
        var query1 = _dbContext.Activities
            .Where(i => i.ActivityId == itemDto.ActivityId);
        var item = await query1.FirstOrDefaultAsync<ActivityDbM>() ?? throw new ArgumentException($"Item {itemDto.ActivityId} is not existing");

        // Transfer changes from DTO to database object
        item.UpdateFromDTO(itemDto);

        // Update activities if provided
        // Update activity if provided
        if (itemDto.ActivityLevelId != null)
        {
            // Assuming itemDto.MoodKindId is a single ID, not a list
            var activityLevelId = itemDto.ActivityLevelId;  // Single ID instead of a list
            var activityLevel = await _dbContext.ActivityLevels.FirstOrDefaultAsync(a => a.ActivityLevelId == activityLevelId);

            if (activityLevel != null)
            {
                // moodKind.MoodsId = item.MoodId; // Ensure activity is linked to the mood
                item.ActivityLevelDbM = activityLevel;  // Update the single MoodKind for the item
            }
            else
            {
                _logger.LogError($"MoodKind with ID {activityLevelId} not found.");
                throw new ArgumentException($"MoodKind with ID {activityLevelId} not found.");
            }
        }

        // Save the updated mood and related entities to the database
        _dbContext.Activities.Update(item);
        await _dbContext.SaveChangesAsync();

        // Return the updated item (non-flat mode, including related entities)
        return await ReadItemAsync(item.ActivityId, false);
    }

    public async Task<ResponseItemDto<IActivity>> CreateItemAsync(ActivityCuDto itemDto)
    {
        if (itemDto.ActivityId != null)
            throw new ArgumentException($"{nameof(itemDto.ActivityId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new ActivityDbM(itemDto);

        //Update navigation properties
        //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Activities.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ActivityId, false);
    }

    public async Task UpdateNavigationProp(ActivityCuDto itemDto, ActivityDbM item)
    {

        // Update MoodKind
        var updatedActivityLevels = await _dbContext.ActivityLevels
            .FirstOrDefaultAsync(a => a.ActivityLevelId == itemDto.ActivityLevelId) ?? throw new ArgumentException($"Patient with id {itemDto.ActivityLevelId} does not exist");
        item.ActivityLevelDbM = updatedActivityLevels;


        // Update Patient
        var updatedPatients = await _dbContext.Patients
            .FirstOrDefaultAsync(a => a.PatientId == itemDto.PatientId) ?? throw new ArgumentException($"Patient with id {itemDto.PatientId} does not exist");
        item.PatientDbM = updatedPatients;

    }
}