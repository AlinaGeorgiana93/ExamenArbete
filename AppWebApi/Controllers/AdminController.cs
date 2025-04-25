using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

using Models.DTO;
using Services;
using Configuration;


namespace AppWebApi.Controllers
{
#if !DEBUG    
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Policy = null, Roles = "sysadmin")]
#endif
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        readonly DatabaseConnections _dbConnections;
        readonly IAdminService _adminService;
        readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger, DatabaseConnections dbConnections)
        {
            _adminService = adminService;
            _logger = logger;
            _dbConnections = dbConnections;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(DatabaseConnections.SetupInformation))]
        public IActionResult Info()
        {
            try
            {
                var info = _dbConnections.SetupInfo;

                _logger.LogInformation($"{nameof(Info)}:\n{JsonConvert.SerializeObject(info)}");
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Info)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

#if DEBUG
      
        //You need to run this with sysadmin connection string
       [HttpPost()]
public async Task<IActionResult> SeedDefaultUsersAsync()
{
    try
    {
        _logger.LogInformation("Seeding default users (sysadmin1, staff1) if not already present.");

        await _adminService.SeedDefaultUsersAsync(); // assumes this checks if users already exist
        return Ok("Default users created or already exist.");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while seeding default users.");
        return BadRequest($"{ex.Message} {ex.InnerException?.Message}");
    }
}

#endif

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogMessage>))]
        public async Task<IActionResult> Log([FromServices] ILoggerProvider _loggerProvider)
        {
            //Note the way to get the LoggerProvider, not the logger from Services via DI
            if (_loggerProvider is InMemoryLoggerProvider cl)
            {
                return Ok(await cl.MessagesAsync);
            }
            return Ok("No messages in log");
        }
    }
}

