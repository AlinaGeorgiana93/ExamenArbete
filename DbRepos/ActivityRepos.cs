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
                .Include(i => i.GraphDbM)
                .Where(i => i.ActivityId == id);
        }
        else
        {
            query = _dbContext.Activities.AsNoTracking()
                .Where(i => i.ActivityId == id);
        }  
 
        var resp =  await query.FirstOrDefaultAsync<IActivity>();
        return new ResponseItemDto<IActivity>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

     public async Task<ResponsePageDto<IActivity>> ReadItemsAsync (bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<ActivityDbM> query = _dbContext.Activities.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.Activities.AsNoTracking()
            .Include(i => i.PatientDbM);
            
         }
         

        query = query.Where(i => 
        
               i.strActivityLevel.ToLower().Contains(filter) ||
                i.strDate.ToLower().Contains(filter) ||
                i.strDayOfWeek.ToLower().Contains(filter) ||
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
        var item = await query.FirstOrDefaultAsync();

        if(item == null) throw new ArgumentException($"Item: {id} is not existing");

        _dbContext.Activities.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IActivity> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }
 public async Task<ResponseItemDto<IActivity>> CreateItemAsync(ActivityCuDto itemDto)
    {
        if (itemDto.ActivityId != null) 
          throw new ArgumentException($"{nameof(itemDto.ActivityId)} must be null when creating a new object");

          var item = new ActivityDbM(itemDto);

          await UpdateNavigationProp(itemDto, item);

          _dbContext.Add(item);

          await _dbContext.SaveChangesAsync();

          return await ReadItemAsync(item.ActivityId, true);
    }
    public async Task<ResponseItemDto<IActivity>> UpdateItemAsync(ActivityCuDto itemDto)
    {
        var query1 = _dbContext.Activities
            .Where(i => i.ActivityId == itemDto.ActivityId);
        var item = await query1
            .Include(i => i.PatientDbM)
            .FirstOrDefaultAsync<ActivityDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.ActivityId} is not existing");

        item.UpdateFromDTO(itemDto);

        await UpdateNavigationProp(itemDto, item);

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.ActivityId, true);
    }

    public async Task UpdateNavigationProp(ActivityCuDto itemDto, ActivityDbM item)
    {
      
        // Update Patient
        var updatedPatients = await _dbContext.Patients
            .FirstOrDefaultAsync(a => a.PatientId == itemDto.PatientId);
        if (updatedPatients == null)
            throw new ArgumentException($"Patient with id {itemDto.PatientId} does not exist");
        item.PatientDbM = updatedPatients;
    }


}


