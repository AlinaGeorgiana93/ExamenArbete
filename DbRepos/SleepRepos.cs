using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class SleepDbRepos
{
    private readonly ILogger<SleepDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public SleepDbRepos(ILogger<SleepDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<ISleep>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<SleepDbM> query;
        if (!flat)
        {
            query = _dbContext.Sleeps.AsNoTracking()
                .Include(i => i.PatientDbM)
                .Include(i => i.GraphDbM)
                .Where(i => i.SleepId == id);
        }
        else
        {
            query = _dbContext.Sleeps.AsNoTracking()
                .Where(i => i.SleepId == id);
        }

        var resp = await query.FirstOrDefaultAsync<ISleep>();
        return new ResponseItemDto<ISleep>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<ISleep>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<SleepDbM> query;
        if (flat)
        {
            query = _dbContext.Sleeps.AsNoTracking();
        }
        else
        {
            query = _dbContext.Sleeps.AsNoTracking()
                .Include(i => i.PatientDbM)
                .Include(i => i.GraphDbM);
        }

        var ret = new ResponsePageDto<ISleep>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

                // Adding filter functionality
                .Where(i =>
                 i.strSleepLevel.ToLower().Contains(filter) ||
                 i.strDate.ToLower().Contains(filter) ||
                 i.strDayOfWeek.ToLower().Contains(filter) ||
                 i.Notes.ToLower().Contains(filter))
                .CountAsync(),

            PageItems = await query

                     // Adding filter functionality
                     .Where(i =>
                    i.strSleepLevel.ToLower().Contains(filter) ||
                    i.strDate.ToLower().Contains(filter) ||
                    i.strDayOfWeek.ToLower().Contains(filter) ||
                    i.Notes.ToLower().Contains(filter))

                // Adding paging
                .Skip(pageNumber * pageSize)
                .Take(pageSize)

                .ToListAsync<ISleep>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<ISleep>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Sleeps
            .Where(i => i.SleepId == id);

        var item = await query1.FirstOrDefaultAsync<SleepDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Sleeps.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<ISleep>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<ISleep>> UpdateItemAsync(SleepCuDto itemDto)
    {
        var query1 = _dbContext.Sleeps
            .Where(i => i.SleepId == itemDto.SleepId);
        var item = await query1
                .Include(i => i.PatientDbM)
                .Include(i => i.GraphDbM)
                .FirstOrDefaultAsync<SleepDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.SleepId} is not existing");


        item.UpdateFromDTO(itemDto);

        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        _dbContext.Sleeps.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.SleepId, false);
    }

    public async Task<ResponseItemDto<ISleep>> CreateItemAsync(SleepCuDto itemDto)
    {
        if (itemDto.SleepId != null)
            throw new ArgumentException($"{nameof(itemDto.SleepId)} must be null when creating a new object");


        var item = new SleepDbM(itemDto);

        await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        _dbContext.Sleeps.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.SleepId, false);
    }

    private async Task navProp_ItemCUdto_to_ItemDbM(SleepCuDto itemDtoSrc, SleepDbM itemDst)
    {
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(
            a => a.PatientId == itemDtoSrc.PatientId);

        if (patient == null)
            throw new ArgumentException($"Item id {itemDtoSrc.PatientId} not existing");

        itemDst.PatientDbM = patient;

        var graph = await _dbContext.Graphs.FirstOrDefaultAsync(
       g => g.GraphId == itemDtoSrc.GraphId);

        if (graph == null)
            throw new ArgumentException($"Graph ID {itemDtoSrc.GraphId} does not exist");

        itemDst.GraphDbM = graph;
    }
}