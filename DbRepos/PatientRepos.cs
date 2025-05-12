using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;
using Configuration;
using Utils;

namespace DbRepos;

public class PatientDbRepos
{
    private readonly ILogger<PatientDbRepos> _logger;
    private readonly MainDbContext _dbContext;
     private Encryptions _encryptions;

    #region contructors
    public PatientDbRepos(ILogger<PatientDbRepos> logger, MainDbContext context,  Encryptions encryptions)
    {
        _logger = logger;
        _dbContext = context;
        _encryptions = encryptions;
    }
    #endregion
public async Task<ResponseItemDto<IPatient>> ReadItemAsync(Guid id, bool flat)
{
    IQueryable<PatientDbM> query;

    if (!flat)
    {
        query = _dbContext.Patients.AsNoTracking().Where(i => i.PatientId == id);
    }
    else
    {
        query = _dbContext.Patients.AsNoTracking().Where(i => i.PatientId == id);
    }

    var resp = await query.FirstOrDefaultAsync<IPatient>();

    // Ensure the PersonalNumber is decrypted here
    if (resp != null && !string.IsNullOrWhiteSpace(resp.PersonalNumber))
    {
        _logger.LogInformation($"Decrypting PersonalNumber for Patient ID {id}: {resp.PersonalNumber}");
        resp.PersonalNumber = _encryptions.DecryptLast4Digits(resp.PersonalNumber);  // Decrypt personal number
    }
    else
    {
        _logger.LogWarning($"PersonalNumber is missing or empty for Patient ID {id}");
    }

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

    // Filter query for the database count
    var filteredQuery = query.Where(i =>
        i.FirstName.ToLower().Contains(filter) ||
        i.LastName.ToLower().Contains(filter) ||
        i.PersonalNumber.ToLower().Contains(filter)
    );

    var patientsDbMList = await filteredQuery
        .Skip(pageNumber * pageSize)
        .Take(pageSize)
        .ToListAsync();

    // Decrypt the PersonalNumber for each patient in the list
    var patientsList = patientsDbMList.Select(patient =>
    {
        // Decrypt PersonalNumber here
        if (!string.IsNullOrWhiteSpace(patient.PersonalNumber))
        {
            patient.PersonalNumber = _encryptions.DecryptLast4Digits(patient.PersonalNumber);
        }

        return (IPatient)patient; // Cast the PatientDbM object to IPatient
    }).ToList();

    return new ResponsePageDto<IPatient>
    {
        DbConnectionKeyUsed = _dbContext.dbConnection,
        DbItemsCount = await filteredQuery.CountAsync(),
        PageItems = patientsList,  // The decrypted list of patients
        PageNr = pageNumber,
        PageSize = pageSize
    };
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
      var item = await _dbContext.Patients
        .FirstOrDefaultAsync(i => i.PatientId == itemDto.PatientId)
        ?? throw new ArgumentException($"Item {itemDto.PatientId} does not exist");

    // Preserve existing values if not provided in DTO
    item.FirstName = string.IsNullOrWhiteSpace(itemDto.FirstName) ? item.FirstName : itemDto.FirstName;
    item.LastName = string.IsNullOrWhiteSpace(itemDto.LastName) ? item.LastName : itemDto.LastName;

    if (!string.IsNullOrWhiteSpace(itemDto.PersonalNumber))
    {
        var normalizedPn = PersonalNumberUtils.Normalize(itemDto.PersonalNumber);
        item.PersonalNumber = _encryptions.EncryptLast4Digits(normalizedPn);
    }



    _dbContext.Patients.Update(item);
    await _dbContext.SaveChangesAsync();

    return await ReadItemAsync(item.PatientId, false);
}
    public async Task<ResponseItemDto<IPatient>> CreateItemAsync(PatientCuDto itemDto)
    {
        if (itemDto.PatientId != null)
            throw new ArgumentException($"{nameof(itemDto.PatientId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
       
          var normalizedPn = PersonalNumberUtils.Normalize(itemDto.PersonalNumber);
          var encryptedPersonalNumber = _encryptions.EncryptLast4Digits(normalizedPn);

        var item = new PatientDbM(itemDto)
        {
            PersonalNumber = encryptedPersonalNumber
        };
        _dbContext.Patients.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.PatientId, false);    
    }

    }
