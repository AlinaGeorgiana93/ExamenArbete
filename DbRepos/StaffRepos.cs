// using Microsoft.Extensions.Logging;
// using Microsoft.EntityFrameworkCore;
// using System.Data;

// using Models;
// using Models.DTO;
// using DbModels;
// using DbContext;

// namespace DbRepos;

// public class StaffDbRepos
// {
//     private readonly ILogger<StaffDbRepos> _logger;
//     private readonly MainDbContext _dbContext;

//     #region contructors
//     public StaffDbRepos(ILogger<StaffDbRepos> logger, MainDbContext context)
//     {
//         _logger = logger;
//         _dbContext = context;
//     }
//     #endregion

//     public async Task<ResponseItemDto<IStaff>> ReadItemAsync(Guid id, bool flat)
//     {
//         IQueryable<StaffDbM> query;
//         if (!flat)
//         {
//             query = _dbContext.Staff.AsNoTracking()
//                 .Include(i => i.MoodsDbM)
//                 .Include(i => i.PatientsDbM)
 //                 .Include(i => i.ActivitiesDbM)
 //                 .Include(i => i.ApetitesDbM)
//                 .Where(i => i.StaffId == id);
//         }
//         else
//         {
//             query = _dbContext.Staffs.AsNoTracking()
//                 .Where(i => i.StaffId == id);
//         }   

//         var resp =  await query.FirstOrDefaultAsync<IStaff>();
//         return new ResponseItemDto<IStaff>()
//         {
//             DbConnectionKeyUsed = _dbContext.dbConnection,
//             Item = resp
//         };
//     }

//     public async Task<ResponsePageDto<IStaff>> ReadItemsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
//     {
//         filter ??= "";
//         IQueryable<StaffDbM> query;
//         if (flat)
//         {
//             query = _dbContext.Staffs.AsNoTracking();
//         }
//         else
//         {
//             query = _dbContext.Staffs.AsNoTracking()
//                 .Include(i => i.MoodsDbM)
//                 .Include(i => i.PatientsDbM);
//                 .Include(i => i.ActivitiesDbM)
 //                .Include(i => i.ApetitesDbM)
//                  
//
//         }

//         return new ResponsePageDto<IStaff>()
//         {
//             DbConnectionKeyUsed = _dbContext.dbConnection,
//             DbItemsCount = await query

//             //Adding filter functionality
//             .Where(i => (i.Seeded == seeded) && 
//                         (i.FirstName.ToLower().Contains(filter) ||
//                          i.LastName.ToLower().Contains(filter) ||
//                          i.PersonNumber.ToLower().Contains(filter))).CountAsync(),

//             PageItems = await query

//             //Adding filter functionality
//             .Where(i => (i.Seeded == seeded) && 
//                         (i.FirstName.ToLower().Contains(filter) ||
//                          i.LastName.ToLower().Contains(filter) ||
//                          i.PersonNumber.ToLower().Contains(filter)))

//             //Adding paging
//             .Skip(pageNumber * pageSize)
//             .Take(pageSize)

//             .ToListAsync<IStaff>(),

//             PageNr = pageNumber,
//             PageSize = pageSize
//         };
//     }

//     public async Task<ResponseItemDto<IStaff>> DeleteItemAsync(Guid id)
//     {
//         //Find the instance with matching id
//         var query1 = _dbContext.Staffs
//             .Where(i => i.StaffId == id);
//         var item = await query1.FirstOrDefaultAsync<StaffDbM>();

//         //If the item does not exists
//         if (item == null) throw new ArgumentException($"Item {id} is not existing");

//         //delete in the database model
//         _dbContext.Staffs.Remove(item);

//         //write to database in a UoW
//         await _dbContext.SaveChangesAsync();

//         return new ResponseItemDto<IStaff>()
//         {
//             DbConnectionKeyUsed = _dbContext.dbConnection,
//             Item = item
//         };
//     }

//     public async Task<ResponseItemDto<IStaff>> UpdateItemAsync(StaffCuDto itemDto)
//     {
//         //Find the instance with matching id and read the navigation properties.
//         var query1 = _dbContext.Staffs
//             .Where(i => i.StaffId == itemDto.StaffId);
//         var item = await query1
//             .Include(i => i.MoodsDbM)
//             .Include(i => i.ActivitesDbM)
//             .FirstOrDefaultAsync<StaffDbM>();

//         //If the item does not exists
//         if (item == null) throw new ArgumentException($"Item {itemDto.StaffId} is not existing");

//         //transfer any changes from DTO to database objects
//         //Update individual properties
//         item.UpdateFromDTO(itemDto);

//         //Update navigation properties
//         await navProp_Itemdto_to_ItemDbM(itemDto, item);

//         //write to database model
//         _dbContext.Staffs.Update(item);

//         //write to database in a UoW
//         await _dbContext.SaveChangesAsync();

//         //return the updated item in non-flat mode
//         return await ReadItemAsync(item.StaffId, false);    
//     }

//     public async Task<ResponseItemDto<IStaff>> CreateItemAsync(StaffCuDto itemDto)
//     {
//         if (itemDto.MoodId != null)
//             throw new ArgumentException($"{nameof(itemDto.StaffId)} must be null when creating a new object");

//         //transfer any changes from DTO to database objects
//         //Update individual properties Zoo
//         var item = new StaffDbM(itemDto);

//         //Update navigation properties
//         await navProp_Itemdto_to_ItemDbM(itemDto, item);

//         //write to database model
//         _dbContext.Staffs.Add(item);

//         //write to database in a UoW
//         await _dbContext.SaveChangesAsync();
        
//         //return the updated item in non-flat mode
//         return await ReadItemAsync(item.StaffId, false);
//     }

//     //from all Guid relationships in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst 
//     //as navigation properties. Error is thrown if no object is found corresponing to an id.
//     private async Task navProp_Itemdto_to_ItemDbM(StaffCuDto itemDtoSrc, ZooDbM itemDst)
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
