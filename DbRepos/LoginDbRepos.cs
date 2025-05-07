using Configuration;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace DbRepos;

public class LoginDbRepos
{
    private readonly ILogger<LoginDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    private Encryptions _encryptions;

    public LoginDbRepos(ILogger<LoginDbRepos> logger, Encryptions encryptions, MainDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _encryptions = encryptions;
    }



    public async Task<ResponseItemDto<LoginUserSessionDto>> LoginUserAsync(LoginCredentialsDto usrCreds)
    {
        using var cmd1 = _dbContext.Database.GetDbConnection().CreateCommand();
        //Notice how I use the efc Command to call sp as I do not return any dataset, only output parameters
        //Notice also how I encrypt the password, no coms to database with open password
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.CommandText = "gstusr.spLogin";
        cmd1.Parameters.Add(new SqlParameter("UserNameOrEmail", usrCreds.UserNameOrEmail));
        cmd1.Parameters.Add(new SqlParameter("Password", _encryptions.EncryptPasswordToBase64(usrCreds.Password)));

        int _usrIdIdx = cmd1.Parameters.Add(new SqlParameter("UserId", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output });
        int _usrIdx = cmd1.Parameters.Add(new SqlParameter("UserName", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });
        int _roleIdx = cmd1.Parameters.Add(new SqlParameter("Role", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });


        _dbContext.Database.OpenConnection();
        await cmd1.ExecuteScalarAsync();

                    var info = new LoginUserSessionDto
            {
                UserId = cmd1.Parameters[_usrIdIdx].Value as Guid?,
                UserName = cmd1.Parameters[_usrIdx].Value as string,
                UserRole = cmd1.Parameters[_roleIdx].Value as string,
               // Email = cmd1.Parameters[_emailIdx].Value as string
            };

            // Log or return user role
            if (info.UserRole == "sysadmin")
            {
                _logger.LogInformation($"Logged in as sysadmin: {info.UserName}");
            }
            else
            {
                _logger.LogInformation($"Logged in as user: {info.UserName}, Role: {info.UserRole}");
            }


        return new ResponseItemDto<LoginUserSessionDto>()
        {
            DbConnectionKeyUsed = _dbContext.dbConnection,
            Item = info
        };
    }
 public async Task<ResponseItemDto<LoginStaffSessionDto>> LoginStaffAsync(LoginCredentialsDto usrCreds)
{
    try
    {
        _logger.LogInformation("LoginStaffAsync started.");
        _logger.LogInformation($"INPUT: UsernameOrEmail = {usrCreds.UserNameOrEmail}");

            using var cmd1 = _dbContext.Database.GetDbConnection().CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "gstusr.spLoginStaff";

            cmd1.Parameters.Add(new SqlParameter("UserNameOrEmail", usrCreds.UserNameOrEmail));
            cmd1.Parameters.Add(new SqlParameter("Password", _encryptions.EncryptPasswordToBase64(usrCreds.Password)));

            int _staffIdIdx = cmd1.Parameters.Add(new SqlParameter("StaffId", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output });
            int _staffIdx = cmd1.Parameters.Add(new SqlParameter("UserName", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });
            int _roleIdx = cmd1.Parameters.Add(new SqlParameter("Role", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });
            int _emailIdx = cmd1.Parameters.Add(new SqlParameter("Email", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });

            _logger.LogInformation("Opening DB connection...");
            _dbContext.Database.OpenConnection();

            _logger.LogInformation("Executing stored procedure: gstusr.spLoginStaff");
            await cmd1.ExecuteScalarAsync();
            _logger.LogInformation("Stored procedure execution completed.");

                        var info = new LoginStaffSessionDto
            {
                StaffId = cmd1.Parameters[_staffIdIdx].Value as Guid?,
                UserName = cmd1.Parameters[_staffIdx].Value as string,
                UserRole = cmd1.Parameters[_roleIdx].Value as string,
                Email = cmd1.Parameters[_emailIdx].Value as string
            };

            _logger.LogInformation($"Login result: UserName = {info.UserName}, UserRole = {info.UserRole}");

            var encryptedPassword = _encryptions.EncryptPasswordToBase64(usrCreds.Password);
            _logger.LogInformation($"Encrypted Password: {encryptedPassword}");

            return new ResponseItemDto<LoginStaffSessionDto>
            {
                DbConnectionKeyUsed = _dbContext.dbConnection,
                Item = info
            };
        }
    catch (Exception ex)
    {
        _logger.LogError($"LoginStaffAsync error: {ex.Message}. StackTrace: {ex.StackTrace}");
        throw;
    }
}


}


