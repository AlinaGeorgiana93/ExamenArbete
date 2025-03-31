using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class GraphDbRepos
{
    private readonly ILogger<GraphDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public GraphDbRepos(ILogger<GraphDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IGraph>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<GraphDbM> query;
        if (!flat)
        {
            query = _dbContext.Graphs.AsNoTracking()
                // .Include(i => i.MoodsDbM)
                // .Include(i => i.ActivitiesDbM)
                // .Include(i => i.SleepsDbM)
                // .Include(i => i.AppetitesDbM)
                .Include(i => i.PatientDbM)
                .Where(i => i.GraphId == id);
        }
        else
        {
            query = _dbContext.Graphs.AsNoTracking()
                .Where(i => i.GraphId == id);
        }   

        var resp =  await query.FirstOrDefaultAsync<IGraph>();
        return new ResponseItemDto<IGraph>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IGraph>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<GraphDbM> query;
        if (flat)
        {
            query = _dbContext.Graphs.AsNoTracking();
        }
        else
        {
            query = _dbContext.Graphs.AsNoTracking()
                // .Include(i => i.MoodsDbM)
                // .Include(i => i.ActivitiesDbM)
                // .Include(i => i.SleepsDbM)
                // .Include(i => i.AppetitesDbM)
                .Include(i => i.PatientDbM);
        }

        return new ResponsePageDto<IGraph>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

            //Adding filter functionality
            .Where(i => //(i.Seeded == seeded) && 
                        // (i.Date.ToLower().Contains(filter) ||
                        //  i..ToLower().Contains(filter) ||
                         i.Date.ToString().ToLower().Contains(filter)).CountAsync(),

            PageItems = await query

            //Adding filter functionality
            .Where(i => //(i.Seeded == seeded) && 
                        // (i.Date.ToLower().Contains(filter) ||
                        //  i..ToLower().Contains(filter) ||
                         i.Date.ToString().ToLower().Contains(filter))

            //Adding paging
            .Skip(pageNumber * pageSize)
            .Take(pageSize)

            .ToListAsync<IGraph>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDto<IGraph>> DeleteItemAsync(Guid id)
    {
        //Find the instance with matching id
        var query1 = _dbContext.Graphs
            .Where(i => i.GraphId == id);
        var item = await query1.FirstOrDefaultAsync<GraphDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Graphs.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IGraph>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IGraph>> UpdateItemAsync(GraphCuDto itemDto)
    {
        //Find the instance with matching id and read the navigation properties.
        var query1 = _dbContext.Graphs
            .Where(i => i.GraphId == itemDto.GraphId);
        var item = await query1
        //    .Include(i => i.MoodsDbM)
        //         .Include(i => i.ActivitiesDbM)
        //         .Include(i => i.SleepsDbM)
        //         .Include(i => i.AppetitesDbM)
             .Include(i => i.PatientDbM)
            .FirstOrDefaultAsync<GraphDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.GraphId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        //await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Graphs.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.GraphId, false);    
    }

    public async Task<ResponseItemDto<IGraph>> CreateItemAsync(GraphCuDto itemDto)
    {
        if (itemDto.GraphId != null)
            throw new ArgumentException($"{nameof(itemDto.GraphId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties Graph
        var item = new GraphDbM(itemDto);

        //Update navigation properties
        //await navProp_Itemdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Graphs.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();
        
        //return the updated item in non-flat mode
        return await ReadItemAsync(item.GraphId, false);
    }

    //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
    //as navigation properties. Error is thrown if no object is found corresponing to an id.
    // private async Task navProp_Itemdto_to_ItemDbM(GraphCuDto itemDtoSrc, GraphDbM itemDst)
    // {
    //     //update MoodsDbM from list
    //     List<MoodDbM> Moods = null;
    //     if (itemDtoSrc.MoodsId != null)
    //     {
    //         Moods = new List<MoodDbM>();
    //         foreach (var id in itemDtoSrc.MoodsId)
    //         {
    //             var p = await _dbContext.Moods.FirstOrDefaultAsync(i => i.MoodId == id);
    //             if (p == null)
    //                 throw new ArgumentException($"Item id {id} not existing");

    //             Moods.Add(p);
    //         }
    //     }
    //     itemDst.MoodsDbM = Moods;


    //     //update EmployeessDbM from list
    //     List<ActivityDbM> Activities = null;
    //     if (itemDtoSrc.ActivitiesId != null)
    //     {
    //         Activities = new List<ActivityDbM>();
    //         foreach (var id in itemDtoSrc.ActivitiesId)
    //         {
    //             var p = await _dbContext.Activities.FirstOrDefaultAsync(i => i.ActivityId == id);
    //             if (p == null)
    //                 throw new ArgumentException($"Item id {id} not existing");

    //             Activities.Add(p);
    //         }
    //     }
    //     itemDst.ActivitiesDbM = Activities;

    //         // ✅ Update SleepsDbM from list (Newly added)
    //     List<SleepDbM> Sleeps = null;
    //     if (itemDtoSrc.SleepsId != null)
    //     {
    //         Sleeps = new List<SleepDbM>();
    //         foreach (var id in itemDtoSrc.SleepsId)
    //         {
    //             var sleep = await _dbContext.Sleeps.FirstOrDefaultAsync(i => i.SleepId == id);
    //             if (sleep == null)
    //                 throw new ArgumentException($"Sleep item with id {id} not found");

    //             Sleeps.Add(sleep);
    //         }
    //     }
    //     itemDst.SleepsDbM = Sleeps;

    // // ✅ Update AppetitesDbM from list (Newly added)
    //     List<AppetiteDbM> Appetites = null;
    //     if (itemDtoSrc.AppetitesId != null)
    //     {
    //         Appetites = new List<AppetiteDbM>();
    //         foreach (var id in itemDtoSrc.AppetitesId)
    //         {
    //             var appetite = await _dbContext.Appetites.FirstOrDefaultAsync(i => i.AppetiteId == id);
    //             if (appetite == null)
    //                 throw new ArgumentException($"Appetite item with id {id} not found");

    //             Appetites.Add(appetite);
    //         }
    //     }
    //     itemDst.AppetitesDbM = Appetites;
    // }
}
