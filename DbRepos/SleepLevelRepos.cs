
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos
{
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

            var resp = await query.FirstOrDefaultAsync();

            return new ResponseItemDto<ISleepLevel>()
            {
                DbConnectionKeyUsed = _dbContext.dbConnection,
                Item = resp
            };
        }

        public async Task<ResponsePageDto<ISleepLevel>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<SleepLevelDbM> query;

        if (flat)
        {
            query = _dbContext.SleepLevels.AsNoTracking();
        }
        else
        {
            query = _dbContext.SleepLevels.AsNoTracking()
                .Include(i => i.SleepsDbM);
        }

        var ret = new ResponsePageDto<ISleepLevel>()
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
                    i.Name.ToLower().Contains(filter)||
                    i.Label.ToLower().Contains(filter)||
                    i.Rating.ToString().ToLower().Contains(filter)
                )
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<ISleepLevel>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

        public async Task<ResponseItemDto<ISleepLevel>> DeleteItemAsync(Guid id)
        {
            var query1 = _dbContext.SleepLevels
                .Where(i => i.SleepLevelId == id);

            var item = await query1.FirstOrDefaultAsync();

            if (item == null)
                throw new ArgumentException($"Item {id} is not existing");

            _dbContext.SleepLevels.Remove(item);

            await _dbContext.SaveChangesAsync();

            return new ResponseItemDto<ISleepLevel>()
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
                    .Include(i => i.SleepsDbM) 
                    .FirstOrDefaultAsync();

            if (item == null) throw new ArgumentException($"Item {itemDto.SleepLevelId} is not existing");

            item.UpdateFromDTO(itemDto);

            _dbContext.SleepLevels.Update(item);

            await _dbContext.SaveChangesAsync();

            return await ReadItemAsync(item.SleepLevelId, false);
        }

        public async Task<ResponseItemDto<ISleepLevel>> CreateItemAsync(SleepLevelCuDto itemDto)
    {
        if (itemDto.SleepLevelId != null)
            throw new ArgumentException($"{nameof(itemDto.SleepLevelId)} must be null when creating a new object");

    
        var item = new SleepLevelDbM(itemDto);

        _dbContext.SleepLevels.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.SleepLevelId, false);    
    }

    }
}