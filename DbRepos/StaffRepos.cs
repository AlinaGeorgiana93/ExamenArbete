using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbModels;
using DbContext;
using Configuration;
using Utils;



namespace DbRepos
{
    public class StaffDbRepos
    {
        private readonly ILogger<StaffDbRepos> _logger;
        private readonly MainDbContext _dbContext;
        private Encryptions _encryptions;

        #region constructors
        public StaffDbRepos(ILogger<StaffDbRepos> logger, MainDbContext context, Encryptions encryptions)
        {
            _encryptions = encryptions;
            _logger = logger;
            _dbContext = context;
        }
        
        #endregion

     
    public async Task<ResponseItemDto<IStaff>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<StaffDbM> query;
        if (!flat)
        {
            query = _dbContext.Staffs.AsNoTracking()
               .Include(i => i.PatientsDbM)
                .Where(i => i.StaffId == id);
        }
        else
        {
            query = _dbContext.Staffs.AsNoTracking()
                .Where(i => i.StaffId == id);
        }

        var resp = await query.FirstOrDefaultAsync<IStaff>();

        // Decrypt PersonalNumber here
        if (resp != null)
        {
            resp.PersonalNumber = _encryptions.DecryptLast4Digits(resp.PersonalNumber);  // Decrypt personal number
        }

        return new ResponseItemDto<IStaff>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    // Read list of staff with filtering and pagination
   public async Task<ResponsePageDto<IStaff>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
{
    filter ??= "";

    IQueryable<StaffDbM> query = _dbContext.Staffs.AsNoTracking();

    if (!flat)
    {
        query = _dbContext.Staffs.AsNoTracking()
           .Include(i => i.PatientsDbM);
    }

    query = query.Where(i =>
       (
          i.FirstName.ToLower().Contains(filter) ||
          i.LastName.ToLower().Contains(filter) ||
          i.PersonalNumber.ToLower().Contains(filter)
       ));

    var staffQuery = query.Skip(pageNumber * pageSize).Take(pageSize);

    // Get the list of StaffDbM
    var staffListDbM = await staffQuery.ToListAsync();

    // Convert the list of StaffDbM to a list of IStaff
    var staffList = staffListDbM.Select(staff => 
    {
        // Decrypt PersonalNumber for each staff item
        staff.PersonalNumber = _encryptions.DecryptLast4Digits(staff.PersonalNumber); 
        return (IStaff)staff; // Cast the StaffDbM object to IStaff
    }).ToList();

    return new ResponsePageDto<IStaff>
    {
        DbConnectionKeyUsed = _dbContext.dbConnection,
        DbItemsCount = await query.CountAsync(),
        PageItems = staffList,  // The decrypted and converted list
        PageNr = pageNumber,
        PageSize = pageSize
    };
}


        // Delete a staff item
        public async Task<ResponseItemDto<IStaff>> DeleteItemAsync(Guid id)
        {
            var query1 = _dbContext.Staffs
                .Where(i => i.StaffId == id);

            var item = await query1.FirstOrDefaultAsync<StaffDbM>() ?? throw new ArgumentException($"Item {id} is not existing");

            _dbContext.Staffs.Remove(item);
            await _dbContext.SaveChangesAsync();

            return new ResponseItemDto<IStaff>()
            {
                DbConnectionKeyUsed = _dbContext.dbConnection,
                Item = item
            };
        }

        // Update staff item
public async Task<ResponseItemDto<IStaff>> UpdateItemAsync(StaffCuDto itemDto)
{
    var item = await _dbContext.Staffs
        .FirstOrDefaultAsync(i => i.StaffId == itemDto.StaffId)
        ?? throw new ArgumentException($"Item {itemDto.StaffId} does not exist");

    // Preserve existing values if not provided in DTO
    item.FirstName = string.IsNullOrWhiteSpace(itemDto.FirstName) ? item.FirstName : itemDto.FirstName;
    item.LastName = string.IsNullOrWhiteSpace(itemDto.LastName) ? item.LastName : itemDto.LastName;
    item.Email = string.IsNullOrWhiteSpace(itemDto.Email) ? item.Email : itemDto.Email;
    item.UserName = string.IsNullOrWhiteSpace(itemDto.UserName) ? item.UserName : itemDto.UserName;
    item.Role = string.IsNullOrWhiteSpace(itemDto.Role) ? item.Role : itemDto.Role;

    if (!string.IsNullOrWhiteSpace(itemDto.PersonalNumber))
    {
        var normalizedPn = PersonalNumberUtils.Normalize(itemDto.PersonalNumber);
        item.PersonalNumber = _encryptions.EncryptLast4Digits(normalizedPn);
    }

    // Only update password if a new one is provided
    if (!string.IsNullOrWhiteSpace(itemDto.Password))
    {
        item.Password = _encryptions.EncryptPasswordToBase64(itemDto.Password);
    }

    _dbContext.Staffs.Update(item);
    await _dbContext.SaveChangesAsync();

    return await ReadItemAsync(item.StaffId, false);
}


        public async Task<bool> IsEmailOrUserNameExistAsync(string email, string userName)
    {
        return await _dbContext.Staffs.AnyAsync(s => s.Email == email || s.UserName == userName);
    }

public async Task<ResponseItemDto<IStaff>> CreateItemAsync(StaffCuDto itemDto)
{
    _logger.LogInformation("Starting CreateItemAsync");

    // Check if StaffId is null
    // if (itemDto.StaffId != null)
    // {
    //     _logger.LogWarning("StaffId should be null on creation");
    //     throw new ArgumentException($"{nameof(itemDto.StaffId)} must be null when creating a new object");
    // }

    // Check if the email or username already exists
    if (await IsEmailOrUserNameExistAsync(itemDto.Email, itemDto.UserName))
    {
        _logger.LogWarning("Email or Username already exists");
        throw new ArgumentException("Email or Username already exists.");
    }

    // Check if the password is provided
    if (string.IsNullOrWhiteSpace(itemDto.Password))
    {
        _logger.LogError("Password is missing");
        throw new ArgumentException("Password is required.");
    }

    _logger.LogInformation("Encrypting password");

    // Encrypt the password
    
    var encryptedPassword = _encryptions.EncryptPasswordToBase64(itemDto.Password);

    // Assign role: use default role if none is provided
    var role = string.IsNullOrEmpty(itemDto.Role) ? "usr" : itemDto.Role;

    // Create new staff item with encrypted password and role
    var normalizedPn = PersonalNumberUtils.Normalize(itemDto.PersonalNumber);
    var encryptedPersonalNumber = _encryptions.EncryptLast4Digits(normalizedPn);

        var item = new StaffDbM(itemDto)
        {
            UserName = itemDto.UserName,
            Email = itemDto.Email,
            Password = encryptedPassword,
            Role = role,
            PersonalNumber = encryptedPersonalNumber
        };

    // Add item to the database and save changes
    _dbContext.Staffs.Add(item);
    await _dbContext.SaveChangesAsync();

    _logger.LogInformation($"Staff item created with ID: {item.StaffId}");

    return await ReadItemAsync(item.StaffId, false);
}
    }
}
