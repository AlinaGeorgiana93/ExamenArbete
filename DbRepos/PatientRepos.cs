using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class PatientDbRepos
{
    private readonly ILogger<PatientDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public PatientDbRepos(ILogger<PatientDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion
 public async Task<ResponseItemDto<IPatient>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<PatientDbM> query;
        if (!flat)
        {
            query = _dbContext.Patients.AsNoTracking()
                .Where(i => i.PatientId == id);
        }
        else
        {
            query = _dbContext.Patients.AsNoTracking()
                .Where(i => i.PatientId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IPatient>();
        return new ResponseItemDto<IPatient>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IPatient>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<PatientDbM> query;
        if (flat)
        {
            query = _dbContext.Patients.AsNoTracking();
        }
        else
        {
            query = _dbContext.Patients.AsNoTracking();
               
        }

        var ret = new ResponsePageDto<IPatient>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

            //Adding filter functionality
            .Where(i => 
                        i.FirstName.ToLower().Contains(filter) ||
                        i.LastName.ToLower().Contains(filter) ||
                        i.PersonalNumber.ToLower().Contains(filter)).CountAsync(),
 
            PageItems = await query

            //Adding filter functionality
             .Where(i => 
                        
                         i.FirstName.ToLower().Contains(filter) ||
                         i.LastName.ToLower().Contains(filter) ||
                         i.PersonalNumber.ToLower().Contains(filter))
 
            //Adding paging
            .Skip(pageNumber * pageSize)
            .Take(pageSize)

            .ToListAsync<IPatient>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }


  public async Task<ResponseItemDto<IPatient>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Patients
            .Where(i => i.PatientId == id);

        var item = await query1.FirstOrDefaultAsync<PatientDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Patients.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IPatient>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IPatient>> UpdateItemAsync(PatientCuDto itemDto)
    {
        var query1 = _dbContext.Patients
            .Where(i => i.PatientId == itemDto.PatientId);
        var item = await query1
                //.Include(i => i.AttractionDbM) // Commented out Attraction
                .FirstOrDefaultAsync<PatientDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.PatientId} is not existing");

        //transfer any changes from DTO to database objects
        //Update individual properties 
        item.UpdateFromDTO(itemDto);

        _dbContext.Patients.Update(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.PatientId, false);    
    }

    public async Task<ResponseItemDto<IPatient>> CreateItemAsync(PatientCuDto itemDto)
    {
        if (itemDto.PatientId != null)
            throw new ArgumentException($"{nameof(itemDto.PatientId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new PatientDbM(itemDto);

        _dbContext.Patients.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.PatientId, false);    
    }

    }
