using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class ActivityLevelDbRepos
{
    private readonly ILogger<ActivityLevelDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region Constructors
    public ActivityLevelDbRepos(ILogger<ActivityLevelDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IActivityLevel>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<ActivityLevelDbM> query;
        if (!flat)
        {
            query = _dbContext.ActivityLevels.AsNoTracking()
                .Include(i => i.ActivitiesDbM) // Assuming there is a relationship
                .Where(i => i.ActivityLevelId == id);
        }
        else
        {
            query = _dbContext.ActivityLevels.AsNoTracking()
                .Where(i => i.ActivityLevelId == id);
        }

        var resp = await query.FirstOrDefaultAsync();

        return new ResponseItemDto<IActivityLevel>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IActivityLevel>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<ActivityLevelDbM> query;

        if (flat)
        {
            query = _dbContext.ActivityLevels.AsNoTracking();
        }
        else
        {
            query = _dbContext.ActivityLevels.AsNoTracking()
                .Include(i => i.ActivitiesDbM);
        }

        var ret = new ResponsePageDto<IActivityLevel>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query
                .Where(i =>
                    i.Name.ToLower().Contains(filter) ||
                    i.Label.ToLower().Contains(filter) ||
                    i.Rating.ToString().ToLower().Contains(filter)
                ).CountAsync(),

            PageItems = await query
                .Where(i =>
                    i.Name.ToLower().Contains(filter) ||
                    i.Label.ToLower().Contains(filter) ||
                    i.Rating.ToString().ToLower().Contains(filter)
                )
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<IActivityLevel>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<IActivityLevel>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.ActivityLevels
            .Where(i => i.ActivityLevelId == id);

        var item = await query1.FirstOrDefaultAsync();

        // If the item does not exist
        if (item == null)
            throw new ArgumentException($"Item {id} is not existing");

        // Delete the item from the ActivityLevels table 
        _dbContext.ActivityLevels.Remove(item);

        // Save the changes to the database
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IActivityLevel>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IActivityLevel>> UpdateItemAsync(ActivityLevelCuDto itemDto)
    {
        var query1 = _dbContext.ActivityLevels
            .Where(i => i.ActivityLevelId == itemDto.ActivityLevelId);

        var item = await query1
                .Include(i => i.ActivitiesDbM) // Include related entities if needed
                .FirstOrDefaultAsync();

        // If the item does not exist
        if (item == null) throw new ArgumentException($"Item {itemDto.ActivityLevelId} is not existing");

        // Transfer any changes from DTO to database object
        item.UpdateFromDTO(itemDto);

        // Save the updated entity in the database
        _dbContext.ActivityLevels.Update(item);

        await _dbContext.SaveChangesAsync();

        // Return the updated item
        return await ReadItemAsync(item.ActivityLevelId, false);
    }

    public async Task<ResponseItemDto<IActivityLevel>> CreateItemAsync(ActivityLevelCuDto itemDto)
    {
        if (itemDto.ActivityLevelId != null)
            throw new ArgumentException($"{nameof(itemDto.ActivityLevelId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new ActivityLevelDbM(itemDto);

        //Update navigation properties
        //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.ActivityLevels.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.ActivityLevelId, false);
    }

    // public static implicit operator ActivityLevelDbRepos(ActivityDbRepos v)
    // {
    //     throw new NotImplementedException();
    // }
}
