using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

using Models.DTO;
using DbModels;
using DbContext;
using Configuration;
using Models;
using System.Security;

namespace DbRepos;

public class AdminDbRepos
{

    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    #region contructors
    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
    #endregion

    public async Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync()
    {
        var info = new GstUsrInfoAllDto();
        info.Db = await _dbContext.InfoDbView.FirstAsync();
        info.Staffs = await _dbContext.InfoStaffsView.ToListAsync();


        return new ResponseItemDto<GstUsrInfoAllDto>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = info
        };
    }

   public async Task SeedDefaultUsersAsync()
{
    if (!_dbContext.Users.Any())
    {
        _dbContext.Users.AddRange(new List<UserDbM>
        {
            new() {
                UserId = Guid.NewGuid(),
                UserName = "sysadmin1",
                Email = "sysadmin1@gmail.com",
                Password = _encryptions.EncryptPasswordToBase64("sysadmin1"),
                Role = "sysadmin"
            }   
        });

    if (!_dbContext.Staffs.Any())
{
    var testStaff1 = new StaffDbM
    {
        StaffId = Guid.NewGuid(),
        FirstName = "Charlie",
        LastName = "Doddie",
        PersonalNumber = _encryptions.EncryptLast4Digits("199001011234"),
        Email = "charlie@mail.com",
        Role = "usr",
        UserName = "Charlie1",
        Password = _encryptions.EncryptPasswordToBase64("1234") // encrypt it same as login
    };

    var testStaff2 = new StaffDbM
    {
        StaffId = Guid.NewGuid(),
        FirstName = "Alice",
        LastName = "Maor",
        PersonalNumber = _encryptions.EncryptLast4Digits("19900104567"),
        Email = "alice@mail.com",
        Role = "usr",
        UserName = "Alice1",
        Password = _encryptions.EncryptPasswordToBase64("1234") // encrypt it same as login
    };

    var testStaff3 = new StaffDbM
    {
        StaffId = Guid.NewGuid(),
        FirstName = "Bob",
        LastName = "Tom",
        PersonalNumber = _encryptions.EncryptLast4Digits("199001014567"),
        Email = "bob@mail.com",
        Role = "usr",
        UserName = "Bob1",
        Password = _encryptions.EncryptPasswordToBase64("1234") // encrypt it same as login
    };
    var testStaff4 = new StaffDbM
    {
        StaffId = Guid.NewGuid(),
        FirstName = "Mike",
        LastName = "Smith",
        PersonalNumber = _encryptions.EncryptLast4Digits("199001012345"),
        Email = "mike@mail.com",
        Role = "sysadmin",
        UserName = "Mike1",
        Password = _encryptions.EncryptPasswordToBase64("1234") // encrypt it same as login
    };
    var testStaff5 = new StaffDbM
    {
        StaffId = Guid.NewGuid(),
        FirstName = "John",
        LastName = "Doe",
        PersonalNumber  = _encryptions.EncryptLast4Digits("19560937474"),
        Email = "john-doe@yahoo.com",
        Role = "usr",
        UserName = "user1",     
        Password = _encryptions.EncryptPasswordToBase64("user1") // encrypt it same as login
    };
    

    _dbContext.Staffs.AddRange(testStaff1, testStaff2, testStaff3, testStaff4, testStaff5);
    await _dbContext.SaveChangesAsync();
}


        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Default users and staffs seeded.");
    }
}

}
