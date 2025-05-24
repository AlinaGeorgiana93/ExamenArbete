using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class SleepLevelDbRepos
{
    private readonly ILogger<SleepLevelDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region Constructors
    public SleepLevelDbRepos(ILogger<SleepLevelDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<ISleepLevel>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<SleepLevelDbM> query;
        if (!flat)
        {
            query = _dbContext.SleepLevels.AsNoTracking()
                .Include(i => i.SleepsDbM)
                .Where(i => i.SleepLevelId == id);
        }
        else
        {
            query = _dbContext.SleepLevels.AsNoTracking()
                .Where(i => i.SleepLevelId == id);
        }

        var resp = await query.FirstOrDefaultAsync<ISleepLevel>();

        return new ResponseItemDto<ISleepLevel>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<ISleepLevel>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<SleepLevelDbM> query = _dbContext.SleepLevels.AsNoTracking();

        if (!flat)
        {
            query = _dbContext.SleepLevels.AsNoTracking()
            .Include(i => i.SleepsDbM);

        }


        query = query.Where(i =>

                i.Name.ToLower().Contains(filter) ||
                i.Rating.ToString().ToLower().Contains(filter)

           );

        return new ResponsePageDto<ISleepLevel>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<ISleepLevel>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    }


    public async Task<ResponseItemDto<ISleepLevel>> DeleteItemAsync(Guid id)
    {
        var query = _dbContext.SleepLevels
        .Where(a => a.SleepLevelId == id);
        var item = await query.FirstOrDefaultAsync() ?? throw new ArgumentException($"Item: {id} is not existing");
        _dbContext.SleepLevels.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<ISleepLevel>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<ISleepLevel>> UpdateItemAsync(SleepLevelCuDto itemDto)
    {
        var query1 = _dbContext.SleepLevels
            .Where(i => i.SleepLevelId == itemDto.SleepLevelId);

        var item = await query1
                .Include(i => i.SleepsDbM) // Include related entities if needed
                .FirstOrDefaultAsync() ?? throw new ArgumentException($"Item {itemDto.SleepLevelId} is not existing");

        // Transfer any changes from DTO to database object
        item.UpdateFromDTO(itemDto);

        // Save the updated entity in the database
        _dbContext.SleepLevels.Update(item);

        await _dbContext.SaveChangesAsync();
         _logger.LogInformation($"UpdateItemAsync completed for SleepLevel: {itemDto.SleepLevelId}");

        // Return the updated item
        return await ReadItemAsync(item.SleepLevelId, false);
    }

    public async Task<ResponseItemDto<ISleepLevel>> CreateItemAsync(SleepLevelCuDto itemDto)
    {
        if (itemDto.SleepLevelId != null)
            throw new ArgumentException($"{nameof(itemDto.SleepLevelId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new SleepLevelDbM(itemDto);

        //Update navigation properties
        //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.SleepLevels.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
          _logger.LogInformation($"SleepLevel item created with ID: {item.SleepLevelId}");

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.SleepLevelId, false);
    }


}


