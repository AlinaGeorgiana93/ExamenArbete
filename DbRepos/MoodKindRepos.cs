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
                .Include(i => i.MoodDbM)
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

     public async Task<ResponsePageDto<IMoodKind>> ReadItemsAsync (bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<MoodKindDbM> query = _dbContext.MoodKinds.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.MoodKinds.AsNoTracking()
            .Include(i => i.MoodDbM);
            
         }
         

        query = query.Where(i => 
        
                i.Name.ToLower().Contains(filter) ||
                i.Label.ToLower().Contains(filter) ||
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
        var item = await query.FirstOrDefaultAsync();

        if(item == null) throw new ArgumentException($"Item: {id} is not existing");

        _dbContext.MoodKinds.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IMoodKind> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }
 public async Task<ResponseItemDto<IMoodKind>> CreateItemAsync(MoodKindCuDto itemDto)
    {
        if (itemDto.MoodKindId != null) 
          throw new ArgumentException($"{nameof(itemDto.MoodKindId)} must be null when creating a new object");

          var item = new MoodKindDbM(itemDto);

          await UpdateNavigationProp(itemDto, item);

          _dbContext.Add(item);

          await _dbContext.SaveChangesAsync();

          return await ReadItemAsync(item.MoodKindId, true);
    }
    public async Task<ResponseItemDto<IMoodKind>> UpdateItemAsync(MoodKindCuDto itemDto)
    {
        var query1 = _dbContext.MoodKinds
            .Where(i => i.MoodKindId == itemDto.MoodKindId);
        var item = await query1
            .Include(i => i.MoodDbM)
            .FirstOrDefaultAsync<MoodKindDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.MoodKindId} is not existing");

        item.UpdateFromDTO(itemDto);

        await UpdateNavigationProp(itemDto, item);

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.MoodKindId, true);
    }

    public async Task UpdateNavigationProp(MoodKindCuDto itemDto, MoodKindDbM item)
    {
      
        // Update  Mood
        var updatedMoods = await _dbContext.Moods
            .FirstOrDefaultAsync(a => a.MoodId == itemDto.MoodId);
        if (updatedMoods == null)
            throw new ArgumentException($"Activity with id {itemDto.MoodId} does not exist");
        item.MoodDbM = updatedMoods;
    }


}


