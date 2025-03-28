using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class AppetiteDbRepos
{
    private readonly ILogger<AppetiteDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public AppetiteDbRepos(ILogger<AppetiteDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IAppetite>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<AppetiteDbM> query;
        if (!flat)
        {
            query = _dbContext.Appetites.AsNoTracking()
                .Include(i => i.PatientDbM)
                //.Include(i => i.GraphDbM)
                .Where(i => i.AppetiteId == id);
        }
        else
        {
            query = _dbContext.Appetites.AsNoTracking()
                .Where(i => i.AppetiteId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IAppetite>();
        return new ResponseItemDto<IAppetite>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IAppetite>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<AppetiteDbM> query;
        if (flat)
        {
            query = _dbContext.Appetites.AsNoTracking();
        }
        else
        {
            query = _dbContext.Appetites.AsNoTracking()
                .Include(i => i.PatientDbM);
                //.Include(i => i.GraphDbM);
        }

        var ret = new ResponsePageDto<IAppetite>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

                // Adding filter functionality
                .Where(i => 
                i.strAppetiteLevel.ToLower().Contains(filter) ||
                 i.strDate.ToLower().Contains(filter) ||
                 i.strDayOfWeek.ToLower().Contains(filter) ||
                 i.Notes.ToLower().Contains(filter))
                .CountAsync(),

            PageItems = await query

                    // Adding filter functionality
                .Where(i => 
                    i.strAppetiteLevel.ToLower().Contains(filter) ||
                    i.strDate.ToLower().Contains(filter) ||
                    i.strDayOfWeek.ToLower().Contains(filter) ||
                    i.Notes.ToLower().Contains(filter))

                // Adding paging
                .Skip(pageNumber * pageSize)
                .Take(pageSize)

                .ToListAsync<IAppetite>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<IAppetite>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Appetites
            .Where(i => i.AppetiteId == id);

        var item = await query1.FirstOrDefaultAsync<AppetiteDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Appetites.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IAppetite>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IAppetite>> UpdateItemAsync(AppetiteCuDto itemDto)
    {
        var query1 = _dbContext.Appetites
            .Where(i => i.AppetiteId == itemDto.AppetiteId);
        var item = await query1
                .Include(i => i.PatientDbM)
                //.Include(i => i.GraphDbM)  // Include Graph
                .FirstOrDefaultAsync<AppetiteDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.AppetiteId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
     //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Appetites.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.AppetiteId, false);    
    }

    public async Task<ResponseItemDto<IAppetite>> CreateItemAsync(AppetiteCuDto itemDto)
    {
        if (itemDto.AppetiteId != null)
            throw new ArgumentException($"{nameof(itemDto.AppetiteId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new AppetiteDbM(itemDto);

        //Update navigation properties
      //  await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Appetites.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.AppetiteId, false);    
    }

    // private async Task navProp_ItemCUdto_to_ItemDbM(AppetiteCuDto itemDtoSrc, AppetiteDbM itemDst)
    // {
    //     //update Patient nav props
    //     var patient = await _dbContext.Patients.FirstOrDefaultAsync(
    //         a => (a.PatientId == itemDtoSrc.PatientId));

    //     if (patient == null)
    //         throw new ArgumentException($"Item id {itemDtoSrc.PatientId} not existing");

    //     itemDst.PatientDbM = patient;

    //      var graph = await _dbContext.Graphs.FirstOrDefaultAsync(
    //     g => (g.GraphId == itemDtoSrc.GraphId));

    // if (graph == null)
    //     throw new ArgumentException($"Graph ID {itemDtoSrc.GraphId} does not exist");

    // itemDst.GraphDbM = graph;
    // }
}
