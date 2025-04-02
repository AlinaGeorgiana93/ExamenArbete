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

   public async Task<ResponsePageDto<IMood>> ReadItemsAsync ( bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<MoodDbM> query = _dbContext.Moods.AsNoTracking();

         if (!flat)
         {
            query = _dbContext.Moods.AsNoTracking()
            .Include(i => i.PatientDbM)
            .Include(i => i.MoodKindDbM);
            
         }
         

        query = query.Where(i => 
        
              i.StrDate.ToLower().Contains(filter) ||
              i.StrDayOfWeek.ToLower().Contains(filter) ||
              i.Notes.ToLower().Contains(filter) ||
              i.PatientDbM.FirstName.ToLower().Contains(filter) ||
              i.PatientDbM.LastName.ToLower().Contains(filter) ||
              i.PatientDbM.PersonalNumber.ToLower().Contains(filter)||
              i.MoodKindDbM.Name.ToLower().Contains(filter) ||
              i.MoodKindDbM.Rating.ToString().ToLower().Contains(filter) 
           );

        return new ResponsePageDto<IMood>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync<IMood>(),
            PageNr = pageNumber,
            PageSize = pageSize
        };

    } 

    public async Task<ResponseItemDto<IMood>> DeleteItemAsync(Guid id)
    {  var query = _dbContext.Moods
        .Where(a => a.MoodId == id);
        var item = await query.FirstOrDefaultAsync();

        if(item == null) throw new ArgumentException($"Item: {id} is not existing");

        _dbContext.Moods.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IMood> 
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IMood>> CreateItemAsync(MoodCuDto itemDto)
    {
        if (itemDto.MoodId != null) 
          throw new ArgumentException($"{nameof(itemDto.MoodId)} must be null when creating a new object");

          var item = new MoodDbM(itemDto);

          await UpdateNavigationProp(itemDto, item);

          _dbContext.Add(item);

          await _dbContext.SaveChangesAsync();

          return await ReadItemAsync(item.MoodId, true);
    }
 public async Task<ResponseItemDto<IMood>> UpdateItemAsync(MoodCuDto itemDto)
    {
        var query1 = _dbContext.Moods
            .Where(i => i.MoodId == itemDto.MoodId);
        var item = await query1
           .Include(i => i.MoodKindDbM)
            .Include(i => i.PatientDbM)
           
            .FirstOrDefaultAsync<MoodDbM>();

        if (item == null) throw new ArgumentException($"Item {itemDto.MoodId} is not existing");

        item.UpdateFromDTO(itemDto);

        await UpdateNavigationProp(itemDto, item);

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.MoodId, true);
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