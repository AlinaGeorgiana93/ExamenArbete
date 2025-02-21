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
            query = _dbContext.Activitys.AsNoTracking()
                .Include(i => i.ActivityDbM)
                .Where(i => i.AnimalId == id);
        }
        else
        {
            query = _dbContext.Activitys.AsNoTracking()
                .Where(i => i.ActivityId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IActivity>();
        return new ResponseItemDto<IActivity>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IActivity>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<ActivityDbM> query;
        if (flat)
        {
            query = _dbContext.Activitys.AsNoTracking();
        }
        else
        {
            query = _dbContext.Activitys.AsNoTracking()
                .Include(i => i.ActivityDbM);
        }

        var ret = new ResponsePageDto<IActivity>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

            //Adding filter functionality
            .Where(i => (i.Seeded == seeded) && 
                        (i.Name.ToLower().Contains(filter) ||
                         i.strMood.ToLower().Contains(filter) ||
                         i.strKind.ToLower().Contains(filter) ||
                         i.Age.ToString().Contains(filter) ||
                         i.Description.ToLower().Contains(filter))).CountAsync(),

            PageItems = await query

            //Adding filter functionality
            .Where(i => (i.Seeded == seeded) && 
                        (i.Name.ToLower().Contains(filter) ||
                         i.strMood.ToLower().Contains(filter) ||
                         i.strKind.ToLower().Contains(filter) ||
                         i.Age.ToString().Contains(filter) ||
                         i.Description.ToLower().Contains(filter)))

            //Adding paging
            .Skip(pageNumber * pageSize)
            .Take(pageSize)

            .ToListAsync<IActivity>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<IActivity>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Animals
            .Where(i => i.AnimalId == id);

        var item = await query1.FirstOrDefaultAsync<ActivityDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Animals.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IActivity>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IActivity>> UpdateItemAsync(ActivityCuDto itemDto)
    {
        var query1 = _dbContext.Activitys
            .Where(i => i.ActivityId == itemDto.ActivityId);
        var item = await query1
                .Include(i => i.ActivityDbM)
                .FirstOrDefaultAsync<ActivityDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.ActivityId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Activitys.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
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
        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Activitys.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ActivityId, false);    
    }

    private async Task navProp_ItemCUdto_to_ItemDbM(ActivityCuDto itemDtoSrc, ActivityDbM itemDst)
    {
        //update zoo nav props
        var zoo = await _dbContext.Patients.FirstOrDefaultAsync(
            a => (a.ZooId == itemDtoSrc.PatientId));

        if (zoo == null)
            throw new ArgumentException($"Item id {itemDtoSrc.PatinetId} not existing");

        itemDst.patientDbM = patinet;
    }
}
