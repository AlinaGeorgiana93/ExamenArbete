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
                .Include(i => i.AppetiteLevelDbM)
                .Include(i => i.PatientDbM)
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

   public async Task<ResponsePageDto<IAppetite>> ReadItemsAsync ( bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<AppetiteDbM> query = _dbContext.Appetites.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.Appetites.AsNoTracking()
            .Include(i => i.PatientDbM)
            .Include(i => i.AppetiteLevelDbM);
            
         }
         

        query = query.Where(i => 
        
              i.StrDate.ToLower().Contains(filter) ||
              i.StrDayOfWeek.ToLower().Contains(filter) ||
              i.Notes.ToLower().Contains(filter) ||
              i.PatientDbM.FirstName.ToLower().Contains(filter) ||
              i.PatientDbM.LastName.ToLower().Contains(filter) ||
              i.PatientDbM.PersonalNumber.ToLower().Contains(filter)||
              i.AppetiteLevelDbM.Name.ToLower().Contains(filter) ||
              i.AppetiteLevelDbM.Rating.ToString().ToLower().Contains(filter) 
           );

        return new ResponsePageDto<IAppetite>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<IAppetite>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    } 

    public async Task<ResponseItemDto<IAppetite>> DeleteItemAsync(Guid id)
    {  var query = _dbContext.Appetites
        .Where(a => a.AppetiteId == id);
        var item = await query.FirstOrDefaultAsync();

        if(item == null) throw new ArgumentException($"Item: {id} is not existing");

        _dbContext.Appetites.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IAppetite> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IAppetite>> CreateItemAsync(AppetiteCuDto itemDto)
    {
        if (itemDto.AppetiteId != null) 
          throw new ArgumentException($"{nameof(itemDto.AppetiteId)} must be null when creating a new object");

          var item = new AppetiteDbM(itemDto);

          await UpdateNavigationProp(itemDto, item);

          _dbContext.Add(item);

          await _dbContext.SaveChangesAsync();

          return await ReadItemAsync(item.AppetiteId, true);
    }
 public async Task<ResponseItemDto<IAppetite>> UpdateItemAsync(AppetiteCuDto itemDto)
    {
        var query1 = _dbContext.Appetites
            .Where(i => i.AppetiteId == itemDto.AppetiteId);
        var item = await query1
           .Include(i => i.AppetiteLevelDbM)
            .Include(i => i.PatientDbM)
           
            .FirstOrDefaultAsync<AppetiteDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.AppetiteId} is not existing");

        item.UpdateFromDTO(itemDto);

        await UpdateNavigationProp(itemDto, item);

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.AppetiteId, true);
    }

    public async Task UpdateNavigationProp(AppetiteCuDto itemDto, AppetiteDbM item)
    {

        // Update AppetiteLevel
        var updatedAppetiteLevels = await _dbContext.AppetiteLevels
            .FirstOrDefaultAsync(a => a.AppetiteLevelId == itemDto.AppetiteLevelId) ?? throw new ArgumentException($"Patient with id {itemDto.AppetiteLevelId} does not exist");
        item.AppetiteLevelDbM = updatedAppetiteLevels;


        // Update Patient
        var updatedPatients = await _dbContext.Patients
            .FirstOrDefaultAsync(a => a.PatientId == itemDto.PatientId) ?? throw new ArgumentException($"Patient with id {itemDto.PatientId} does not exist");
        item.PatientDbM = updatedPatients;

    }
}