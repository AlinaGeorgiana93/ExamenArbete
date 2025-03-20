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
                .Include(i => i.MoodsDbM)
               .Include(i => i.ActivitiesDbM)
                .Include(i=> i.AppetitesDbM)
                .Include(i => i.SleepsDbM)
               .Include(i => i.PatientsDbM)
                .Where(i => i.StaffId == id);
        }
        else
        {
            query = _dbContext.Staffs.AsNoTracking()
                .Where(i => i.StaffId == id);
        }  
 
        var resp =  await query.FirstOrDefaultAsync<IStaff>();
        return new ResponseItemDto<IStaff>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

     public async Task<ResponsePageDto<IStaff>> ReadItemsAsync (bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<StaffDbM> query = _dbContext.Staffs.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.Staffs.AsNoTracking()
             .Include(i => i.MoodsDbM)
                .Include(i => i.ActivitiesDbM)
                .Include(i=> i.AppetitesDbM)
                 .Include(i => i.SleepsDbM)
               .Include(i => i.PatientsDbM);
         }
         

        query = query.Where(i => i.Seeded == seeded &&
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
        var query = _dbContext.Staffs
        .Where(a => a.StaffId == id);
        var item = await query.FirstOrDefaultAsync();

        if(item == null) throw new ArgumentException($"Item: {id} is not existing");

        _dbContext.Staffs.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IStaff> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }
 public async Task<ResponseItemDto<IStaff>> CreateItemAsync(StaffCuDto itemDto)
    {
        if (itemDto.StaffId != null) 
          throw new ArgumentException($"{nameof(itemDto.StaffId)} must be null when creating a new object");

          var item = new StaffDbM(itemDto);

          UpdateNavigationProp(itemDto, item);

          _dbContext.Add(item);

          await _dbContext.SaveChangesAsync();

          return await ReadItemAsync(item.StaffId, true);
    }

    private void UpdateNavigationProp(StaffCuDto itemDto, StaffDbM item)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseItemDto<IStaff>> UpdateItemAsync(StaffCuDto itemDto)
    {
        var query1 = _dbContext.Staffs
            .Where(i => i.StaffId == itemDto.StaffId);
        var item = await query1
            .Include(i => i.MoodsDbM)
            .Include(i => i.ActivitiesDbM)
            .Include(i => i.AppetitesDbM)
            .FirstOrDefaultAsync<StaffDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.StaffId} is not existing");

        item.UpdateFromDTO(itemDto);

        UpdateNavigationProp(itemDto, item);

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.StaffId, true);
    }

    // public async Task UpdateNavigationProp(StaffCuDto itemDto, StaffDbM item)
    // {
      
    //     // Update Address
    //     var updatedAddress = await _dbContext.Moods
    //         .FirstOrDefaultAsync(a => a.MoodId == itemDto.MoodId);
    //     if (updatedMood == null)
    //         throw new ArgumentException($"Address with id {itemDto.Moodd} does not exist");
    //     item.MoodDbM = updatedMood;
    // }


}


