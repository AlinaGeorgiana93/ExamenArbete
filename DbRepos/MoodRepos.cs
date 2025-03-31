﻿using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

namespace DbRepos;

public class MoodDbRepos
{
    private readonly ILogger<MoodDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region contructors
    public MoodDbRepos(ILogger<MoodDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<IMood>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<MoodDbM> query;
        if (!flat)
        {
            query = _dbContext.Moods.AsNoTracking()
                .Include(i => i.MoodKindsDbM)
                .Include(i => i.PatientDbM)
                .Where(i => i.MoodId == id);
        }
        else
        {
            query = _dbContext.Moods.AsNoTracking()
                .Where(i => i.MoodId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IMood>();
        return new ResponseItemDto<IMood>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    public async Task<ResponsePageDto<IMood>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<MoodDbM> query;
        if (flat)
        {
            query = _dbContext.Moods.AsNoTracking();
        }
        else
        {
            query = _dbContext.Moods.AsNoTracking()
                .Include(i => i.MoodKindsDbM);
        }

        var ret = new ResponsePageDto<IMood>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

                // Adding filter functionality
                .Where(i => 
                 i.strDayOfWeek.ToLower().Contains(filter) ||
                 i.strDate.ToLower().Contains(filter) ||
                 i.Notes.ToLower().Contains(filter))
                .CountAsync(),

            PageItems = await query

                    // Adding filter functionality
                .Where(i => 
                    i.strDayOfWeek.ToLower().Contains(filter) ||
                    i.strDate.ToLower().Contains(filter) ||
                    i.Notes.ToLower().Contains(filter))

                // Adding paging
                .Skip(pageNumber * pageSize)
                .Take(pageSize)

                .ToListAsync<IMood>(),

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    public async Task<ResponseItemDto<IMood>> DeleteItemAsync(Guid id)
    {
        var query1 = _dbContext.Moods
            .Where(i => i.MoodId == id);

        var item = await query1.FirstOrDefaultAsync<MoodDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {id} is not existing");

        //delete in the database model
        _dbContext.Moods.Remove(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IMood>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IMood>> UpdateItemAsync(MoodCuDto itemDto)
{
    // Fetch the patient from the database using the PatientId
    var query1 = _dbContext.Moods
        .Where(i => i.MoodId == itemDto.MoodId);
    var item = await query1.FirstOrDefaultAsync<MoodDbM>() ?? throw new ArgumentException($"Item {itemDto.MoodId} is not existing");

        // Transfer changes from DTO to database object
        item.UpdateFromDTO(itemDto);

    // Update activities if provided
    if (itemDto.MoodKindsId != null)
    {
        var moodKinds = new List<MoodKindDbM>();
        foreach (var moodKindId in itemDto.MoodKindsId)
        {
            var moodKind = await _dbContext.MoodKinds.FirstOrDefaultAsync(a => a.MoodKindId == moodKindId);
            if (moodKind != null)
            {
                moodKind.MoodId = item.MoodId; // Ensure activity is linked to the patient
                moodKinds.Add(moodKind);
            }
            else
            {
                _logger.LogError($"Activity with ID {moodKindId} not found.");
                throw new ArgumentException($"Activity with ID {moodKind} not found.");
            }
        }
        item.MoodKindsDbM = moodKinds; // Update activities for the patient

    }
     // Save the updated patient and related entities to the database
    _dbContext.Moods.Update(item);
    await _dbContext.SaveChangesAsync();

    // Return the updated patient in non-flat mode (including related entities)
    return await ReadItemAsync(item.MoodId, false);
}

    public async Task<ResponseItemDto<IMood>> CreateItemAsync(MoodCuDto itemDto)
    {
        if (itemDto.MoodId != null)
            throw new ArgumentException($"{nameof(itemDto.MoodId)} must be null when creating a new object");

        //transfer any changes from DTO to database objects
        //Update individual properties
        var item = new MoodDbM(itemDto);

        //Update navigation properties
     //   await navProp_ItemCUdto_to_ItemDbM(itemDto, item);

        //write to database model
        _dbContext.Moods.Add(item);

        //write to database in a UoW
        await _dbContext.SaveChangesAsync();

        //return the updated item in non-flat mode
        return await ReadItemAsync(item.MoodId, false);    
    }

    // private async Task navProp_ItemCUdto_to_ItemDbM(MoodCuDto itemDtoSrc, MoodDbM itemDst)
    // {
       
    //     var patient = await _dbContext.Patients.FirstOrDefaultAsync(
    //         a => a.PatientId == itemDtoSrc.PatientId);

    //     if (patient == null)
    //         throw new ArgumentException($"Item id {itemDtoSrc.PatientId} not existing");

    //     itemDst.PatientDbM = patient;
    // }
}