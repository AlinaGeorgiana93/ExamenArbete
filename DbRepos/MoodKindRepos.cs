
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;
using Microsoft.Extensions.Logging;

namespace DbRepos
{
    public class MoodKindDbRepos
    {
        private readonly ILogger<MoodKindDbRepos> _logger;
        private readonly MainDbContext _dbContext;

        #region Constructors
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
                    .Include(i => i.MoodDbM) // Assuming there is a relationship
                    .Where(i => i.MoodKindId == id);  // Use correct property MoodKindId
            }
            else
            {
                query = _dbContext.MoodKinds.AsNoTracking()
                    .Where(i => i.MoodKindId == id);  // Use correct property MoodKindId
            }

          
        var resp = await query.FirstOrDefaultAsync<IMoodKind>();
        return new ResponseItemDto<IMoodKind>()
            {
                DbConnectionKeyUsed = _dbContext.dbConnection,
                Item = resp
            };
        }

        public async Task<ResponsePageDto<IMoodKind>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<MoodKindDbM> query;

        if (flat)
        {
            query = _dbContext.MoodKinds.AsNoTracking();
        }
        else
        {
            query = _dbContext.MoodKinds.AsNoTracking()
                .Include(i => i.MoodDbM);
        }

        var ret = new ResponsePageDto<IMoodKind>()
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

                .ToListAsync<IMoodKind>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

        public async Task<ResponseItemDto<IMoodKind>> DeleteItemAsync(Guid id)
        {
            var query1 = _dbContext.MoodKinds
                .Where(i => i.MoodKindId == id);

            var item = await query1.FirstOrDefaultAsync<MoodKindDbM>();

            // If the item does not exist
            if (item == null)
                throw new ArgumentException($"Item {id} is not existing");

            // Delete the item from the MoodKinds table (not Mood)
            _dbContext.MoodKinds.Remove(item);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            return new ResponseItemDto<IMoodKind>()
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
                    .Include(i => i.MoodDbM) // Include related entities if needed
                    .FirstOrDefaultAsync<MoodKindDbM>();

            // If the item does not exist
            if (item == null) throw new ArgumentException($"Item {itemDto.MoodKindId} is not existing");

            // Transfer any changes from DTO to database object
            item.UpdateFromDTO(itemDto);

            // Save the updated entity in the database
            _dbContext.MoodKinds.Update(item);

            await _dbContext.SaveChangesAsync();

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

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.MoodKindId, false);    
    }

    }
}
