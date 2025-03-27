using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class StaffDbRepos
{
    private readonly ILogger<StaffDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public StaffDbRepos(ILogger<StaffDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IStaff>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<StaffDbM> query;
        if (!flat)
        {
            query = _dbContext.Staffs.AsNoTracking()
               .Include(i => i.PatientsDbM)
                .Where(i => i.StaffId == id);
        }
        else
        {
            query = _dbContext.Staffs.AsNoTracking()
                .Where(i => i.StaffId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IStaff>();
        return new ResponseItemDto<IStaff>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IStaff>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<StaffDbM> query = _dbContext.Staffs.AsNoTracking();

        if (!flat)
        {
            query = _dbContext.Staffs.AsNoTracking()
               .Include(i => i.PatientsDbM);
        }


        query = query.Where(i =>
           (
              i.FirstName.ToLower().Contains(filter) ||
              i.LastName.ToLower().Contains(filter) ||
              i.PersonalNumber.ToLower().Contains(filter)
           ));

        return new ResponsePageDto<IStaff>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<IStaff>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    }

    public async Task<ResponseItemDto<IStaff>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Staffs
            .Where(i => i.StaffId == id);

        var item = await query1.FirstOrDefaultAsync<StaffDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Staffs.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IStaff>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IStaff>> UpdateItemAsync(StaffCuDto itemDto)
    {
        var query1 = _dbContext.Staffs
            .Where(i => i.StaffId == itemDto.StaffId);
        var item = await query1
                //.Include(i => i.AttractionDbM) // Commented out Attraction
                .FirstOrDefaultAsync<StaffDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.StaffId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDto);

        //Update navigation properties
        //await navProp_ItemCUdto_to_ItemDbM(itemDto, item); // Commented out navProp_ItemCUdto_to_ItemDbM

        //write to database model
        _dbContext.Staffs.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.StaffId, false);
    }

    public async Task<ResponseItemDto<IStaff>> CreateItemAsync(StaffCuDto itemDto)
    {
        if (itemDto.StaffId != null)
            throw new ArgumentException($"{nameof(itemDto.StaffId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new StaffDbM(itemDto);

        //Update navigation properties
        //await navProp_ItemCUdto_to_ItemDbM(itemDto, item); // Commented out navProp_ItemCUdto_to_ItemDbM

        //write to database model
        _dbContext.Staffs.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.StaffId, false);
    }

    //private async Task navProp_ItemCUdto_to_ItemDbM(AddressCuDto itemDtoSrc, AddressDbM itemDst) // Commented out method
    //{
    //    //update attraction nav props
    //    var attraction = await _dbContext.Attractions.FirstOrDefaultAsync(
    //        a => (a.AttractionId == itemDtoSrc.AttractionId));

    //    if (attraction == null)
    //        throw new ArgumentException($"Item id {itemDtoSrc.AttractionId} not existing");

    //    itemDst.AttractionDbM = attraction;
    //}
}