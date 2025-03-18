using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

using Seido.Utilities.SeedGenerator;
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

    public async Task<UsrInfoDto> SeedUsersAsync(int nrOfUsers, int nrOfSysAdmin)
    {
            _logger.LogInformation($"Seeding {nrOfUsers} staff ");
            
            //First delete all existing users
            foreach (var u in _dbContext.Users)
                _dbContext.Users.Remove(u);

            //add users
            for (int i = 1; i <= nrOfUsers; i++)
            {
                _dbContext.Users.Add(new UserDbM
                {
                    UserId = Guid.NewGuid(),
                    UserName = $"staff{i}",
                    Email = $"staff{i}@gmail.com",
                    Password = _encryptions.EncryptPasswordToBase64($"staff{i}"),
                    Role = "staff"
                });
            }


            //add system adminitrators
            for (int i = 1; i <= nrOfSysAdmin; i++)
            {
                _dbContext.Users.Add(new UserDbM
                {
                    UserId = Guid.NewGuid(),
                    UserName = $"sysadmin{i}",
                    Email = $"sysadmin{i}@gmail.com",
                    Password = _encryptions.EncryptPasswordToBase64($"sysadmin{i}"),
                    Role = "sysadmin"
                });
            }
            await _dbContext.SaveChangesAsync();

            var _info = new UsrInfoDto
            {
                NrStaff = await _dbContext.Users.CountAsync(i => i.Role == "staff"),
                NrSystemAdmin = await _dbContext.Users.CountAsync(i => i.Role == "sysadmin")
            };

            return _info;
    }
}
