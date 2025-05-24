using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class MoodKindDbRepos
{
    private readonly ILogger<MoodKindDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public MoodKindDbRepos(ILogger<MoodKindDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

   public async Task<ResponseItemDto<IMoodKind>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<MoodKindDbM> query;
        if (!flat)
        {
            query = _dbContext.MoodKinds.AsNoTracking()
                .Include(i => i.MoodsDbM)
                .Where(i => i.MoodKindId == id);
        }
        else
        {
            query = _dbContext.MoodKinds.AsNoTracking()
                .Where(i => i.MoodKindId == id);
        }  
 
        var resp =  await query.FirstOrDefaultAsync<IMoodKind>();
        return new ResponseItemDto<IMoodKind>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

     public async Task<ResponsePageDto<IMoodKind>> ReadItemsAsync ( bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<MoodKindDbM> query = _dbContext.MoodKinds.AsNoTracking();

        if (!flat)
         {
            query = _dbContext.MoodKinds.AsNoTracking()
            .Include(i => i.MoodsDbM);
            
        }
         

        query = query.Where(i => 
        
                i.Name.ToLower().Contains(filter) ||
                i.Rating.ToString().ToLower().Contains(filter)

           );

        return new ResponsePageDto<IMoodKind>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<IMoodKind>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    } 

    public async Task<ResponseItemDto<IMoodKind>> DeleteItemAsync(Guid id)
    {
        var query = _dbContext.MoodKinds
        .Where(a => a.MoodKindId == id);
        var item = await query.FirstOrDefaultAsync() ?? throw new ArgumentException($"Item: {id} is not existing");
        _dbContext.MoodKinds.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IMoodKind> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }
   public async Task<ResponseItemDto<IMoodKind>> UpdateItemAsync(MoodKindCuDto itemDto)
        {
            var query1 = _dbContext.MoodKinds
                .Where(i => i.MoodKindId == itemDto.MoodKindId);

            var item = await query1
                    .Include(i => i.MoodsDbM) // Include related entities if needed
                    .FirstOrDefaultAsync() ?? throw new ArgumentException($"Item {itemDto.MoodKindId} is not existing");

        // Transfer any changes from DTO to database object
        item.UpdateFromDTO(itemDto);

            // Save the updated entity in the database
            _dbContext.MoodKinds.Update(item);

            await _dbContext.SaveChangesAsync();
             _logger.LogInformation($"UpdateItemAsync completed for MoodKind: {itemDto.MoodKindId}");

            // Return the updated item
        return await ReadItemAsync(item.MoodKindId, false);
        }

       public async Task<ResponseItemDto<IMoodKind>> CreateItemAsync(MoodKindCuDto itemDto)
    {
        if (itemDto.MoodKindId != null)
            throw new ArgumentException($"{nameof(itemDto.MoodKindId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new MoodKindDbM(itemDto);

        //Update navigation properties
     //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.MoodKinds.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
         _logger.LogInformation($"MoodKind item created with ID: {item.MoodKindId}");

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.MoodKindId, false);    
    }

    

}


