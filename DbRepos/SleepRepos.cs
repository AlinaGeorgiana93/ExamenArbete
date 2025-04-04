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
                .Include(i => i.SleepLevelDbM)
                .Include(i => i.PatientDbM)
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

   public async Task<ResponsePageDto<ISleep>> ReadItemsAsync ( bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<SleepDbM> query = _dbContext.Sleeps.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.Sleeps.AsNoTracking()
            .Include(i => i.PatientDbM)
            .Include(i => i.SleepLevelDbM);
            
         }
         

        query = query.Where(i => 
        
              i.StrDate.ToLower().Contains(filter) ||
              i.StrDayOfWeek.ToLower().Contains(filter) ||
              i.Notes.ToLower().Contains(filter) ||
              i.PatientDbM.FirstName.ToLower().Contains(filter) ||
              i.PatientDbM.LastName.ToLower().Contains(filter) ||
              i.PatientDbM.PersonalNumber.ToLower().Contains(filter)||
              i.SleepLevelDbM.Name.ToLower().Contains(filter) ||
              i.SleepLevelDbM.Rating.ToString().ToLower().Contains(filter) 
           );

        return new ResponsePageDto<ISleep>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<ISleep>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    } 

    public async Task<ResponseItemDto<ISleep>> DeleteItemAsync(Guid id)
    {  var query = _dbContext.Sleeps
        .Where(a => a.SleepId == id);
        var item = await query.FirstOrDefaultAsync();

        if(item == null) throw new ArgumentException($"Item: {id} is not existing");

        _dbContext.Sleeps.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<ISleep> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<ISleep>> CreateItemAsync(SleepCuDto itemDto)
    {
        if (itemDto.SleepId != null) 
          throw new ArgumentException($"{nameof(itemDto.SleepId)} must be null when creating a new object");

          var item = new SleepDbM(itemDto);

          await UpdateNavigationProp(itemDto, item);

          _dbContext.Add(item);

          await _dbContext.SaveChangesAsync();

          return await ReadItemAsync(item.SleepId, true);
    }
 public async Task<ResponseItemDto<ISleep>> UpdateItemAsync(SleepCuDto itemDto)
    {
        var query1 = _dbContext.Sleeps
            .Where(i => i.SleepId == itemDto.SleepId);
        var item = await query1
           .Include(i => i.SleepLevelDbM)
            .Include(i => i.PatientDbM)
           
            .FirstOrDefaultAsync<SleepDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.SleepId} is not existing");

        item.UpdateFromDTO(itemDto);

        await UpdateNavigationProp(itemDto, item);

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.SleepId, true);
    }

    public async Task UpdateNavigationProp(SleepCuDto itemDto, SleepDbM item)
    {

        // Update SleepLevel
        var updatedSleepLevels = await _dbContext.SleepLevels
            .FirstOrDefaultAsync(a => a.SleepLevelId == itemDto.SleepLevelId) ?? throw new ArgumentException($"Patient with id {itemDto.SleepLevelId} does not exist");
        item.SleepLevelDbM = updatedSleepLevels;


        // Update Patient
        var updatedPatients = await _dbContext.Patients
            .FirstOrDefaultAsync(a => a.PatientId == itemDto.PatientId) ?? throw new ArgumentException($"Patient with id {itemDto.PatientId} does not exist");
        item.PatientDbM = updatedPatients;

    }
}