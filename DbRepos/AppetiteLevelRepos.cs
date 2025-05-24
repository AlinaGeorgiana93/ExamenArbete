
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos
{
    public class AppetiteLevelDbRepos(ILogger<AppetiteLevelDbRepos> logger, MainDbContext context)
    {
        private readonly ILogger<AppetiteLevelDbRepos> _logger = logger;
        private readonly MainDbContext _dbContext = context;

        #region Constructors
        #endregion

        public async Task<ResponseItemDto<IAppetiteLevel>> ReadItemAsync(Guid id, bool flat)
        {
            IQueryable<AppetiteLevelDbM> query;
            if (!flat)
            {
                query = _dbContext.AppetiteLevels.AsNoTracking()
                    .Include(i => i.AppetitesDbM) // Assuming there is a relationship
                    .Where(i => i.AppetiteLevelId == id);  // Use correct property MoodKindId
            }
            else
            {
                query = _dbContext.AppetiteLevels.AsNoTracking()
                    .Where(i => i.AppetiteLevelId == id);  // Use correct property MoodKindId
            }

            var resp = await query.FirstOrDefaultAsync();

            return new ResponseItemDto<IAppetiteLevel>()
            {
                DbConnectionKeyUsed = _dbContext.dbConnection,
                Item = resp
            };
        }

        public async Task<ResponsePageDto<IAppetiteLevel>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
        {
            filter ??= "";
            IQueryable<AppetiteLevelDbM> query;

            if (flat)
            {
                query = _dbContext.AppetiteLevels.AsNoTracking();
            }
            else
            {
                query = _dbContext.AppetiteLevels.AsNoTracking()
                    .Include(i => i.AppetitesDbM);
            }

            var ret = new ResponsePageDto<IAppetiteLevel>()
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
                    .ToListAsync<IAppetiteLevel>(),

                PageNr = pageNumber,
                PageSize = pageSize
            };
            return ret;
        }

        public async Task<ResponseItemDto<IAppetiteLevel>> DeleteItemAsync(Guid id)
        {
            var query1 = _dbContext.AppetiteLevels
                .Where(i => i.AppetiteLevelId == id);

            var item = await query1.FirstOrDefaultAsync();

            // If the item does not exist
            if (item == null)
                throw new ArgumentException($"Item {id} is not existing");

            // Delete the item from the MoodKinds table (not Mood)
            _dbContext.AppetiteLevels.Remove(item);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return new ResponseItemDto<IAppetiteLevel>()
            {
                DbConnectionKeyUsed = _dbContext.dbConnection,
                Item = item
            };
        }

        public async Task<ResponseItemDto<IAppetiteLevel>> UpdateItemAsync(AppetiteLevelCuDto itemDto)
        {
            var query1 = _dbContext.AppetiteLevels
                .Where(i => i.AppetiteLevelId == itemDto.AppetiteLevelId);

            var item = await query1
                    .Include(i => i.AppetitesDbM) // Include related entities if needed
                    .FirstOrDefaultAsync();

            // If the item does not exist
            if (item == null) throw new ArgumentException($"Item {itemDto.AppetiteLevelId} is not existing");

            // Transfer any changes from DTO to database object
            item.UpdateFromDTO(itemDto);

            // Save the updated entity in the database
            _dbContext.AppetiteLevels.Update(item);

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"UpdateItemAsync completed for AppetiteLevel: {itemDto.AppetiteLevelId}");

        
            // Return the updated item
            return await ReadItemAsync(item.AppetiteLevelId, false);
        }

        public async Task<ResponseItemDto<IAppetiteLevel>> CreateItemAsync(AppetiteLevelCuDto itemDto)
        {
            if (itemDto.AppetiteLevelId != null)
                throw new ArgumentException($"{nameof(itemDto.AppetiteLevelId)} must be null when creating a new object");

            //transfer any changes from DTO to database objects
            //Update individual properties
            var item = new AppetiteLevelDbM(itemDto);

            //Update navigation properties
            //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

            //write to database model
            _dbContext.AppetiteLevels.Add(item);

            //write to database in a UoW
            await _dbContext.SaveChangesAsync();
             _logger.LogInformation($"AppetiteLevel item created with ID: {item.AppetiteLevelId}");
            //return the updated item in non-flat mode
            return await ReadItemAsync(item.AppetiteLevelId, false);
        }

    }
}