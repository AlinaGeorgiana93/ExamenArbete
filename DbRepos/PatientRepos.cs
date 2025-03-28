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

   public async Task<ResponsePageDto<IPatient>> ReadItemsAsync (bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<PatientDbM> query = _dbContext.Patients.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.Patients.AsNoTracking()
               .Include(i => i.MoodsDbM)
                .Include(i => i.ActivitiesDbM)
                .Include(i => i.SleepsDbM)
                .Include(i => i.AppetitesDbM);
         }
         

        query = query.Where(i => 
           (
              i.FirstName.ToLower().Contains(filter) ||
              i.LastName.ToLower().Contains(filter) ||
              i.PersonalNumber.ToLower().Contains(filter) 
           ));

        return new ResponsePageDto<IPatient>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
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
    // Fetch the patient from the database using the PatientId
    var query1 = _dbContext.Patients
        .Where(i => i.PatientId == itemDto.PatientId);
    var item = await query1.FirstOrDefaultAsync<PatientDbM>();

    if (item == null) throw new ArgumentException($"Item {itemDto.PatientId} is not existing");

    // Transfer changes from DTO to database object
    item.UpdateFromDTO(itemDto);

    // Update activities if provided
    if (itemDto.ActivitiesId != null)
    {
        var activities = new List<ActivityDbM>();
        foreach (var activityId in itemDto.ActivitiesId)
        {
            var activity = await _dbContext.Activities.FirstOrDefaultAsync(a => a.ActivityId == activityId);
            if (activity != null)
            {
                activity.PatientId = item.PatientId; // Ensure activity is linked to the patient
                activities.Add(activity);
            }
            else
            {
                _logger.LogError($"Activity with ID {activityId} not found.");
                throw new ArgumentException($"Activity with ID {activityId} not found.");
            }
        }
        item.ActivitiesDbM = activities; // Update activities for the patient
    }

    // Update sleeps if provided
    if (itemDto.SleepsId != null)
    {
        var sleeps = new List<SleepDbM>();
        foreach (var sleepId in itemDto.SleepsId)
        {
            var sleep = await _dbContext.Sleeps.FirstOrDefaultAsync(s => s.SleepId == sleepId);
            if (sleep != null)
            {
                sleep.PatientId = item.PatientId; // Ensure sleep is linked to the patient
                sleeps.Add(sleep);
            }
            else
            {
                _logger.LogError($"Sleep with ID {sleepId} not found.");
                throw new ArgumentException($"Sleep with ID {sleepId} not found.");
            }
        }
        item.SleepsDbM = sleeps; // Update sleeps for the patient
    }

    // Update moods if provided
    if (itemDto.MoodsId != null)
    {
        var moods = new List<MoodDbM>();
        foreach (var moodId in itemDto.MoodsId)
        {
            var mood = await _dbContext.Moods.FirstOrDefaultAsync(m => m.MoodId == moodId);
            if (mood != null)
            {
                mood.PatientId = item.PatientId; // Ensure mood is linked to the patient
                moods.Add(mood);
            }
            else
            {
                _logger.LogError($"Mood with ID {moodId} not found.");
                throw new ArgumentException($"Mood with ID {moodId} not found.");
            }
        }
        item.MoodsDbM = moods; // Update moods for the patient
    }

    // Update appetites if provided
    if (itemDto.AppetitesId != null)
    {
        var appetites = new List<AppetiteDbM>();
        foreach (var appetiteId in itemDto.AppetitesId)
        {
            var appetite = await _dbContext.Appetites.FirstOrDefaultAsync(a => a.AppetiteId == appetiteId);
            if (appetite != null)
            {
                appetite.PatientId = item.PatientId; // Ensure appetite is linked to the patient
                appetites.Add(appetite);
            }
            else
            {
                _logger.LogError($"Appetite with ID {appetiteId} not found.");
                throw new ArgumentException($"Appetite with ID {appetiteId} not found.");
            }
        }
        item.AppetitesDbM = appetites; // Update appetites for the patient
    }

    // Save the updated patient and related entities to the database
    _dbContext.Patients.Update(item);
    await _dbContext.SaveChangesAsync();

    // Return the updated patient in non-flat mode (including related entities)
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