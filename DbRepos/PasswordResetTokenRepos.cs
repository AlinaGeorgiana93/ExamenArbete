using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using DbModels;
using DbContext;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PasswordResetTokenRepos
{
    private readonly ILogger<PasswordResetTokenRepos> _logger;
    private readonly MainDbContext _dbContext;

    #region Constructors
    public PasswordResetTokenRepos(ILogger<PasswordResetTokenRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    #endregion

    #region Read Methods

    // Retrieve a single password reset token based on its ID
    public async Task<ResponseItemDto<IPasswordResetToken>> ReadItemAsync(Guid id, bool flat)
    {
        IQueryable<PasswordResetTokenDbM> query;

        if (!flat)
        {
            query = _dbContext.PasswordResetTokens.AsNoTracking()
                .Include(i => i.StaffDbM)
                .Where(i => i.PasswordResetTokenId == id);
        }
        else
        {
            query = _dbContext.PasswordResetTokens.AsNoTracking()
                .Where(i => i.PasswordResetTokenId == id);
        }

        var resp = await query.FirstOrDefaultAsync();
        return new ResponseItemDto<IPasswordResetToken>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = resp
        };
    }

    // Get a paginated list of password reset tokens, filtered by a string
    public async Task<ResponsePageDto<IPasswordResetToken>> ReadItemsAsync(bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";

        IQueryable<PasswordResetTokenDbM> query = _dbContext.PasswordResetTokens.AsNoTracking();

        if (!flat)
        {
            query = query.Include(i => i.StaffDbM);
        }

        query = query.Where(i =>
            i.Token.ToLower().Contains(filter) ||
            i.ExpiryDateStr.ToLower().Contains(filter) ||
            i.CreatedAtStr.ToLower().Contains(filter) ||
            i.Email.ToLower().Contains(filter) ||
            i.StaffDbM.FirstName.ToLower().Contains(filter) ||
            i.StaffDbM.LastName.ToLower().Contains(filter));

        return new ResponsePageDto<IPasswordResetToken>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            DbItemsCount = await query.CountAsync(),
            PageItems = [.. (await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync())
                .Cast<IPasswordResetToken>()],
            PageNr = pageNumber,
            PageSize = pageSize
        };
    }

    #endregion

    #region Create / Update Methods

    // Create a new password reset token
    public async Task<ResponseItemDto<IPasswordResetToken>> CreateItemAsync(PasswordResetTokenDto itemDto)
    {
        if (itemDto.PasswordResetTokenId != null)
            throw new ArgumentException($"{nameof(itemDto.PasswordResetTokenId)} must be null when creating a new token");

        var item = new PasswordResetTokenDbM(itemDto);

        _dbContext.PasswordResetTokens.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.PasswordResetTokenId, true);
    }

    // Update an existing password reset token (if needed)
    public async Task<ResponseItemDto<IPasswordResetToken>> UpdateItemAsync(PasswordResetTokenDto itemDto)
    {
        var item = await _dbContext.PasswordResetTokens
            .Where(i => i.PasswordResetTokenId == itemDto.PasswordResetTokenId)
            .Include(i => i.StaffDbM)
            .FirstOrDefaultAsync();

        if (item == null)
            throw new ArgumentException($"Token {itemDto.PasswordResetTokenId} does not exist");

        item.Token = itemDto.Token;
        item.ExpiryDate = itemDto.ExpiryDate;

        _dbContext.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadItemAsync(item.PasswordResetTokenId, true);
    }

    #endregion

    #region Delete Methods

    // Delete a password reset token by ID
    public async Task<ResponseItemDto<IPasswordResetToken>> DeleteItemAsync(Guid id)
    {
        var item = await _dbContext.PasswordResetTokens
            .Where(a => a.PasswordResetTokenId == id)
            .FirstOrDefaultAsync();

        if (item == null) throw new ArgumentException($"Item: {id} does not exist");

        _dbContext.PasswordResetTokens.Remove(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IPasswordResetToken>
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = item
        };
    }

    public async Task<ResponseItemDto<IStaff>> ResetPasswordAsync(PasswordResetTokenDto item)
{
    // Validate password confirmation
    if (item.NewPassword != item.ConfirmPassword)
        throw new ArgumentException("Passwords do not match.");

    // Find token
    var token = await _dbContext.PasswordResetTokens
        .Include(t => t.StaffDbM)
        .FirstOrDefaultAsync(t => t.Token == item.Token && t.Email == item.Email);

    if (token == null)
        throw new ArgumentException("Invalid or expired token.");

    // Check if token is expired
    if (token.ExpiryDate < DateTime.UtcNow)
        throw new ArgumentException("Token has expired.");

    // Hash the new password
    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(item.NewPassword); // Make sure to install BCrypt.Net-Next

    // Update staff's password
    token.StaffDbM.Password = hashedPassword;

    // Optionally remove or mark the token as used
    _dbContext.PasswordResetTokens.Remove(token);

    await _dbContext.SaveChangesAsync();

    return new ResponseItemDto<IStaff>
    {
        DbConnectionKeyUsed = _dbContext.dbConnection,
        Item = token.StaffDbM
    };
}

public async Task<ResponseItemDto<IPasswordResetToken>> RequestPasswordResetAsync(string email)
{
    var staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.Email == email);
    if (staff == null)
        throw new ArgumentException("Staff not found");

    var token = new PasswordResetTokenDbM
    {
        PasswordResetTokenId = Guid.NewGuid(),
        Token = Guid.NewGuid().ToString(),
        StaffId = staff.StaffId,
        Email = email,
        ExpiryDate = DateTime.UtcNow.AddHours(1),
        ExpiryDateStr = DateTime.UtcNow.AddHours(1).ToString("o"), // Optional if you store this
        CreatedAt = DateTime.UtcNow,
        CreatedAtStr = DateTime.UtcNow.ToString("o") // Optional
    };

    _dbContext.PasswordResetTokens.Add(token);
    await _dbContext.SaveChangesAsync();

    return new ResponseItemDto<IPasswordResetToken>
    {
        DbConnectionKeyUsed = _dbContext.dbConnection,
        Item = token
    };
}
public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
{
    // Find the staff user by email
    var staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.Email == email);
    if (staff == null)
        return false; // User not found

    // Verify current password using BCrypt
    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(currentPassword, staff.Password);
    if (!isPasswordCorrect)
        return false; // Current password is wrong

    // Hash the new password
    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

    // Update the staff's password
    staff.Password = hashedPassword;

    // Save changes
    await _dbContext.SaveChangesAsync();

    return true;
}
}
#endregion