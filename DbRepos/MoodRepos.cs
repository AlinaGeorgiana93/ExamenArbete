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
                .Include(i => i.MoodKindDbM)
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

    public async Task<ResponsePageDto<IMood>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
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
                .Include(i => i.MoodKindDbM);
        }

        var ret = new ResponsePageDto<IMood>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query

                // Adding filter functionality
                .Where(i =>
                 i.StrDayOfWeek.ToLower().Contains(filter) ||
                 i.StrDate.ToLower().Contains(filter) ||
                 i.Notes.ToLower().Contains(filter))
                .CountAsync(),

            PageItems = await query

                // Adding filter functionality
                .Where(i =>
                    i.StrDayOfWeek.ToLower().Contains(filter) ||
                    i.StrDate.ToLower().Contains(filter) ||
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

        var item = await query1.FirstOrDefaultAsync<MoodDbM>() ?? throw new ArgumentException($"Item {id} is not existing");

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
        // Update activity if provided
        if (itemDto.MoodKindId != null)
        {
            // Assuming itemDto.MoodKindId is a single ID, not a list
            var moodKindId = itemDto.MoodKindId;  // Single ID instead of a list
            var moodKind = await _dbContext.MoodKinds.FirstOrDefaultAsync(a => a.MoodKindId == moodKindId);

            if (moodKind != null)
            {
                // moodKind.MoodsId = item.MoodId; // Ensure activity is linked to the mood
                item.MoodKindDbM = moodKind;  // Update the single MoodKind for the item
            }
            else
            {
                _logger.LogError($"MoodKind with ID {moodKindId} not found.");
                throw new ArgumentException($"MoodKind with ID {moodKindId} not found.");
            }
        }

        // Save the updated mood and related entities to the database
        _dbContext.Moods.Update(item);
        await _dbContext.SaveChangesAsync();

        // Return the updated item (non-flat mode, including related entities)
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

    public async Task UpdateNavigationProp(MoodCuDto itemDto, MoodDbM item)
    {

        // Update MoodKind
        var updatedMoodKinds = await _dbContext.MoodKinds
            .FirstOrDefaultAsync(a => a.MoodKindId == itemDto.MoodKindId) ?? throw new ArgumentException($"Patient with id {itemDto.MoodKindId} does not exist");
        item.MoodKindDbM = updatedMoodKinds;


        // Update Patient
        var updatedPatients = await _dbContext.Patients
            .FirstOrDefaultAsync(a => a.PatientId == itemDto.PatientId) ?? throw new ArgumentException($"Patient with id {itemDto.PatientId} does not exist");
        item.PatientDbM = updatedPatients;

    }
}