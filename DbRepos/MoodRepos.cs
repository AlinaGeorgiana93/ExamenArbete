// using Microsoft.Extensions.Logging;
// using Microsoft.EntityFrameworkCore;
// using System.Data;

// using Models;
// using Models.DTO;
// using DbModels;
// using DbContext;

// namespace DbRepos;

// public class MoodDbRepos
// {
//     private readonly ILogger<MoodDbRepos> _logger;
//     private readonly MainDbContext _dbContext;

//     #region contructors
//     public MoodDbRepos(ILogger<MoodDbRepos> logger, MainDbContext context)
//     {
//         _logger = logger;
//         _dbContext = context;
//     }
//     #endregion

//     public async Task<ResponseItemDto<IMood>> ReadItemAsync(Guid id, bool flat)
//     {
//         IQueryable<MoodDbM> query;
//         if (!flat)
//         {
//             query = _dbContext.Mood.AsNoTracking()
//                 .Include(i => i.AnimalsDbM)
//                 .Include(i => i.EmployeesDbM)
//                 .Where(i => i.ZooId == id);
//         }
//         else
//         {
//             query = _dbContext.Zoos.AsNoTracking()
//                 .Where(i => i.ZooId == id);
//         }   

//         var resp =  await query.FirstOrDefaultAsync<IZoo>();
//         return new ResponseItemDto<IZoo>()
//         {
//             DbConnectionKeyUsed = _dbContext.dbConnection,
//             Item = resp
//         };
//     }

//     public async Task<ResponsePageDto<IMood>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
//     {
//         filter ??= "";
//         IQueryable<ZooDbM> query;
//         if (flat)
//         {
//             query = _dbContext.Zoos.AsNoTracking();
//         }
//         else
//         {
//             query = _dbContext.Zoos.AsNoTracking()
//                 .Include(i => i.AnimalsDbM)
//                 .Include(i => i.EmployeesDbM);
//         }

//         return new ResponsePageDto<IMood>()
//         {
//             DbConnectionKeyUsed = _dbContext.dbConnection,
//             DbItemsCount = await query

//             //Adding filter functionality
//             .Where(i => (i.Seeded == seeded) && 
//                         (i.Name.ToLower().Contains(filter) ||
//                          i.City.ToLower().Contains(filter) ||
//                          i.Country.ToLower().Contains(filter))).CountAsync(),

//             PageItems = await query

//             //Adding filter functionality
//             .Where(i => (i.Seeded == seeded) && 
//                         (i.Name.ToLower().Contains(filter) ||
//                          i.City.ToLower().Contains(filter) ||
//                          i.Country.ToLower().Contains(filter)))

//             //Adding paging
//             .Skip(pageNumber * pageSize)
//             .Take(pageSize)

//             .ToListAsync<IZoo>(),

//             PageNr = pageNumber,
//             PageSize = pageSize
//         };
//     }

//     public async Task<ResponseItemDto<IMood>> DeleteItemAsync(Guid id)
//     {
//         //Find the instance with matching id
//         var query1 = _dbContext.Zoos
//             .Where(i => i.ZooId == id);
//         var item = await query1.FirstOrDefaultAsync<ZooDbM>();

//         //If the item does not exists
//         if (item == null) throw new ArgumentException($"Item {id} is not existing");

//         //delete in the database model
//         _dbContext.Zoos.Remove(item);

//         //write to database in a UoW
//         await _dbContext.SaveChangesAsync();

//         return new ResponseItemDto<IMood>()
//         {
//             DbConnectionKeyUsed = _dbContext.dbConnection,
//             Item = item
//         };
//     }

//     public async Task<ResponseItemDto<IMood>> UpdateItemAsync(MoodCuDto itemDto)
//     {
//         //Find the instance with matching id and read the navigation properties.
//         var query1 = _dbContext.Zoos
//             .Where(i => i.MoodId == itemDto.MoodId);
//         var item = await query1
//             .Include(i => i.AnimalsDbM)
//             .Include(i => i.EmployeesDbM)
//             .FirstOrDefaultAsync<ZooDbM>();

//         //If the item does not exists
//         if (item == null) throw new ArgumentException($"Item {itemDto.MoodId} is not existing");

//         //transfer any changes from DTO to database objects
//         //Update individual properties
//         item.UpdateFromDTO(itemDto);

//         //Update navigation properties
//         await navProp_Itemdto_to_ItemDbM(itemDto, item);

//         //write to database model
//         _dbContext.Zoos.Update(item);

//         //write to database in a UoW
//         await _dbContext.SaveChangesAsync();

//         //return the updated item in non-flat mode
//         return await ReadItemAsync(item.ZooId, false);    
//     }

//     public async Task<ResponseItemDto<IMood>> CreateItemAsync(MoodCuDto itemDto)
//     {
//         if (itemDto.MoodId != null)
//             throw new ArgumentException($"{nameof(itemDto.MoodId)} must be null when creating a new object");

//         //transfer any changes from DTO to database objects
//         //Update individual properties Zoo
//         var item = new MoodDbM(itemDto);

//         //Update navigation properties
//         await navProp_Itemdto_to_ItemDbM(itemDto, item);

//         //write to database model
//         _dbContext.Zoos.Add(item);

//         //write to database in a UoW
//         await _dbContext.SaveChangesAsync();
        
//         //return the updated item in non-flat mode
//         return await ReadItemAsync(item.ZooId, false);
//     }

//     //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
//     //as navigation properties. Error is thrown if no object is found corresponing to an id.
//     private async Task navProp_Itemdto_to_ItemDbM(MoodCuDto itemDtoSrc, ZooDbM itemDst)
//     {
//         //update AnimalsDbM from list
//         List<AnimalDbM> Animals = null;
//         if (itemDtoSrc.AnimalsId != null)
//         {
//             Animals = new List<AnimalDbM>();
//             foreach (var id in itemDtoSrc.AnimalsId)
//             {
//                 var p = await _dbContext.Animals.FirstOrDefaultAsync(i => i.AnimalId == id);
//                 if (p == null)
//                     throw new ArgumentException($"Item id {id} not existing");

//                 Animals.Add(p);
//             }
//         }
//         itemDst.AnimalsDbM = Animals;

//         //update EmployeessDbM from list
//         List<EmployeeDbM> Employees = null;
//         if (itemDtoSrc.EmployeesId != null)
//         {
//             Employees = new List<EmployeeDbM>();
//             foreach (var id in itemDtoSrc.EmployeesId)
//             {
//                 var p = await _dbContext.Employees.FirstOrDefaultAsync(i => i.EmployeeId == id);
//                 if (p == null)
//                     throw new ArgumentException($"Item id {id} not existing");

//                 Employees.Add(p);
//             }
//         }
//         itemDst.EmployeesDbM = Employees;
//     }
// }
