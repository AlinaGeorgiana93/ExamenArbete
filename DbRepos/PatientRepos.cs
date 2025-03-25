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
                .Include(i => i.MoodsDbM)
                .Include(i => i.ActivitiesDbM)
                .Include(i => i.SleepsDbM)
                .Include(i => i.AppetitesDbM)

                .Where(i => i.PatientId == id);
        }
        else
        {
            query = _dbContext.Patients.AsNoTracking()
                .Where(i => i.PatientId == id);
        }   

        var resp =  await query.FirstOrDefaultAsync<IPatient>();
        return new ResponseItemDto<IPatient>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IPatient>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<PatientDbM> query;
        if (flat)
        {
            query = _dbContext.Patients.AsNoTracking();
        }
        else
        {
            query = _dbContext.Patients.AsNoTracking()
                .Include(i => i.MoodsDbM)
                .Include(i => i.ActivitiesDbM)
                .Include(i => i.SleepsDbM)
                .Include(i => i.AppetitesDbM);
         
        }

        return new ResponsePageDto<IPatient>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

            //Adding filter functionality
            .Where(i => //(i.Seeded == seeded) && 
                        // (i.Date.ToLower().Contains(filter) ||
                        //  i..ToLower().Contains(filter) ||
                         i.PersonalNumber.ToString().ToLower().Contains(filter)).CountAsync(),

            PageItems = await query

            //Adding filter functionality
            .Where(i => //(i.Seeded == seeded) && 
                        // (i.Date.ToLower().Contains(filter) ||
                        //  i..ToLower().Contains(filter) ||
                         i.PersonalNumber.ToString().ToLower().Contains(filter))

            //Adding paging
            .Skip(pageNumber * pageSize)
            .Take(pageSize)

            .ToListAsync<IPatient>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<ResponseItemDto<IPatient>> DeleteItemAsync(Guid id)
    {
        //Find the instance with matching id
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

        //Update navigation properties
        //await navProp_ItemCUdto_to_ItemDbM(itemDto, item); // Commented out navProp_ItemCUdto_to_ItemDbM

        //write to database model
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

    // Create the patient entity
    var patient = new PatientDbM(itemDto);

    // Save the patient first without the related entities
    _dbContext.Patients.Add(patient);
    await _dbContext.SaveChangesAsync();

    // Return the newly created patient
    return await ReadItemAsync(patient.PatientId, false);
}



    //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
    //as navigation properties. Error is thrown if no object is found corresponing to an id.
    // private async Task navProp_Itemdto_to_ItemDbM(PatientCuDto itemDtoSrc, PatientDbM itemDst)
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

