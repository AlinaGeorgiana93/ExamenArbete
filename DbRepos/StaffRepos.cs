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

    // First name
    if (!string.IsNullOrWhiteSpace(itemDto.FirstName))
    {
        var normalizedFirst = InputValidationUtils.NormalizeName(itemDto.FirstName);
        if (!InputValidationUtils.IsValidName(normalizedFirst))
            throw new ArgumentException("Invalid first name.");
        item.FirstName = normalizedFirst;
    }

    // Last name
    if (!string.IsNullOrWhiteSpace(itemDto.LastName))
    {
        var normalizedLast = InputValidationUtils.NormalizeName(itemDto.LastName);
        if (!InputValidationUtils.IsValidName(normalizedLast))
            throw new ArgumentException("Invalid last name.");
        item.LastName = normalizedLast;
    }
    
    // Email
    if (!string.IsNullOrWhiteSpace(itemDto.Email))
    {
        if (!InputValidationUtils.IsValidEmail(itemDto.Email))
            throw new ArgumentException("Invalid email format.");
        item.Email = itemDto.Email;
    }

    // Username
    if (!string.IsNullOrWhiteSpace(itemDto.UserName))
    {
        if (!InputValidationUtils.IsValidUsername(itemDto.UserName))
            throw new ArgumentException("Invalid username.");
        item.UserName = itemDto.UserName;
    }

    // Role
    if (!string.IsNullOrWhiteSpace(itemDto.Role))
        item.Role = itemDto.Role;

    // Personal Number
    if (!string.IsNullOrWhiteSpace(itemDto.PersonalNumber))
    {
        var normalizedPn = PersonalNumberUtils.Normalize(itemDto.PersonalNumber);
        if (string.IsNullOrEmpty(normalizedPn) || !PersonalNumberUtils.IsValid(normalizedPn))
            throw new ArgumentException("Invalid personal number.");

        var encryptedPn = _encryptions.EncryptLast4Digits(normalizedPn);
        var duplicate = await _dbContext.Staffs
            .AsNoTracking()
            .AnyAsync(s => s.PersonalNumber == encryptedPn && s.StaffId != item.StaffId);

        if (duplicate)
            throw new InvalidOperationException("Another staff member with this personal number already exists.");

        item.PersonalNumber = encryptedPn;
    }

    // Password
    if (!string.IsNullOrWhiteSpace(itemDto.Password))
    {
        if (!InputValidationUtils.IsStrongPassword(itemDto.Password))
            throw new ArgumentException("Password must contain at least 6 characters, one uppercase, one digit, and one special character.");
        item.Password = _encryptions.EncryptPasswordToBase64(itemDto.Password);
    }

    _dbContext.Staffs.Update(item);
    await _dbContext.SaveChangesAsync();

    return await ReadItemAsync(item.StaffId, false);
}
 public async Task<ResponseItemDto<IStaff>> UpdateProfileAsync(ProfileUpdateCuDto profileDto)
{
    _logger.LogInformation($"UpdateProfileAsync started for StaffId: {profileDto.StaffId}");

    var staff = await _dbContext.Staffs
        .FirstOrDefaultAsync(i => i.StaffId == profileDto.StaffId)
        ?? throw new ArgumentException($"Staff with ID {profileDto.StaffId} does not exist.");

    // Update Email
    if (!string.IsNullOrWhiteSpace(profileDto.Email))
    {
        _logger.LogInformation("Attempting to update email...");
        if (!InputValidationUtils.IsValidEmail(profileDto.Email))
        {
            _logger.LogWarning("Invalid email format detected.");
            throw new ArgumentException("Invalid email format.");
        }
        staff.Email = profileDto.Email;
        _logger.LogInformation("Email updated.");
    }

    // Update Username
    if (!string.IsNullOrWhiteSpace(profileDto.UserName))
    {
        _logger.LogInformation("Attempting to update username...");
        if (!InputValidationUtils.IsValidUsername(profileDto.UserName))
        {
            _logger.LogWarning("Invalid username detected.");
            throw new ArgumentException("Invalid username.");
        }
        staff.UserName = profileDto.UserName;
        _logger.LogInformation("Username updated.");
    }

    // Update Password if new password provided
    if (!string.IsNullOrWhiteSpace(profileDto.NewPassword))
    {
        _logger.LogInformation("Attempting to update password...");
        if (string.IsNullOrWhiteSpace(profileDto.CurrentPassword))
        {
            _logger.LogWarning("Current password is missing.");
            throw new ArgumentException("Current password is required to change password.");
        }

        var hashedCurrentPassword = _encryptions.EncryptPasswordToBase64(profileDto.CurrentPassword);
        if (staff.Password != hashedCurrentPassword)
        {
            _logger.LogWarning("Current password does not match.");
            throw new Exception("Current password is incorrect.");
        }

        if (!InputValidationUtils.IsStrongPassword(profileDto.NewPassword))
        {
            _logger.LogWarning("New password does not meet strength requirements.");
            throw new ArgumentException("Password must contain at least 6 characters, one uppercase, one digit, and one special character.");
        }

        staff.Password = _encryptions.EncryptPasswordToBase64(profileDto.NewPassword);
        _logger.LogInformation("Password updated.");
    }
            if (!InputValidationUtils.IsStrongPassword(profileDto.ConfirmPassword))
            {
                    _logger.LogWarning("New password does not meet strength requirements.");
                    throw new ArgumentException("Password must contain at least 6 characters, one uppercase, one digit, and one special character.");
                }

                staff.Password = _encryptions.EncryptPasswordToBase64(profileDto.ConfirmPassword);
                _logger.LogInformation("Password updated.");

            _dbContext.Staffs.Update(staff);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"UpdateProfileAsync completed for StaffId: {profileDto.StaffId}");

            return await ReadItemAsync(staff.StaffId, false);
        }


        public async Task<bool> IsEmailOrUserNameExistAsync(string email, string userName)
    {
        return await _dbContext.Staffs.AnyAsync(s => s.Email == email || s.UserName == userName);
    }

public async Task<ResponseItemDto<IStaff>> CreateItemAsync(StaffCuDto itemDto)
{
    _logger.LogInformation("Starting CreateItemAsync");

    // Validate required fields
    if (!InputValidationUtils.IsValidName(itemDto.FirstName))
        throw new ArgumentException("Invalid first name.");
    if (!InputValidationUtils.IsValidName(itemDto.LastName))
        throw new ArgumentException("Invalid last name.");
    if (!InputValidationUtils.IsValidUsername(itemDto.UserName))
        throw new ArgumentException("Invalid username.");
    if (!InputValidationUtils.IsValidEmail(itemDto.Email))
        throw new ArgumentException("Invalid email format.");
    if (!InputValidationUtils.IsStrongPassword(itemDto.Password))
        throw new ArgumentException("Password must contain at least 6 characters, one uppercase, one digit, and one special character.");

    // Normalize names
    itemDto.FirstName = InputValidationUtils.NormalizeName(itemDto.FirstName);
    itemDto.LastName = InputValidationUtils.NormalizeName(itemDto.LastName);

    if (await IsEmailOrUserNameExistAsync(itemDto.Email, itemDto.UserName))
        throw new ArgumentException("Email or Username already exists.");

    var normalizedPn = PersonalNumberUtils.Normalize(itemDto.PersonalNumber);
    if (string.IsNullOrEmpty(normalizedPn) || !PersonalNumberUtils.IsValid(normalizedPn))
        throw new ArgumentException("Invalid personal number.");

    var encryptedPersonalNumber = _encryptions.EncryptLast4Digits(normalizedPn);

    var duplicatePn = await _dbContext.Staffs
        .AsNoTracking()
        .AnyAsync(s => s.PersonalNumber == encryptedPersonalNumber);

    if (duplicatePn)
        throw new InvalidOperationException("A staff member with this personal number already exists.");

    var encryptedPassword = _encryptions.EncryptPasswordToBase64(itemDto.Password);
    var role = itemDto.Role?.Trim().ToLower();

        if (string.IsNullOrEmpty(role) || (role != "usr" && role != "sysadmin"))
            throw new ArgumentException("Role must be either 'usr' or 'sysadmin'.");

        itemDto.Role = role;

    var item = new StaffDbM(itemDto)
    {
        UserName = itemDto.UserName,
        Email = itemDto.Email,
        Password = encryptedPassword,
        Role = role,
        PersonalNumber = encryptedPersonalNumber
    };

    _dbContext.Staffs.Add(item);
    await _dbContext.SaveChangesAsync();

    _logger.LogInformation($"Staff item created with ID: {item.StaffId}");

    return await ReadItemAsync(item.StaffId, false);
}

}
}