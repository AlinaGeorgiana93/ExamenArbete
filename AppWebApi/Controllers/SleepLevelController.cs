using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models;
using Models.DTO;
using Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Policy = null, Roles = "staff, sysadmin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SleepLevelController : Controller
    {
         readonly ISleepLevelService _service = null;
        readonly ILogger<SleepLevelController> _logger = null;

        public SleepLevelController(ISleepLevelService service, ILogger<SleepLevelController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<ISleepLevel>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadItems( string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSizeArg = int.Parse(pageSize);

                 _logger.LogInformation($"{nameof(ReadItems)}:{nameof(flatArg)}: {flatArg}, " +
                    $"{nameof(pageNrArg)}: {pageNrArg}, {nameof(pageSizeArg)}: {pageSizeArg}");
                
                var resp = await _service.ReadSleepLevelsAsync( flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);     
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItems)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<ISleepLevel>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadItem)}: {nameof(idArg)}: {idArg}, {nameof(flatArg)}: {flatArg}");
                
                var item = await _service.ReadSleepLevelAsync(idArg, flatArg);
                if (item?.Item == null) throw new ArgumentException ($"Item with id {id} does not exist");

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItem)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = " sysadmin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<ISleepLevel>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(DeleteItem)}: {nameof(idArg)}: {idArg}");
                
                var item = await _service.DeleteSleepLevelAsync(idArg);
                if (item?.Item == null) throw new ArgumentException ($"Item with id {id} does not exist");
        
                _logger.LogInformation($"item {idArg} deleted");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteItem)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "sysadmin")]
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<SleepLevelCuDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(ReadItemDto)}: {nameof(idArg)}: {idArg}");

                var item = await _service.ReadSleepLevelAsync(idArg, false);
                if (item?.Item == null) throw new ArgumentException ($"Item with id {id} does not exist");

                return Ok(
                    new ResponseItemDto<SleepLevelCuDto>() {
                    DbConnectionKeyUsed = item.DbConnectionKeyUsed,
                    Item = new SleepLevelCuDto(item.Item)
                });   
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItemDto)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = " sysadmin")]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<ISleepLevel>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] SleepLevelCuDto item)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(UpdateItem)}: {nameof(idArg)}: {idArg}");
                
                if (item.SleepLevelId != idArg) throw new ArgumentException("Id mismatch");

                var model = await _service.UpdateSleepLevelAsync(item);
                _logger.LogInformation($"item {idArg} updated");
               
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateItem)}: {ex.InnerException?.Message}");
                return BadRequest($"Could not update. Error {ex.InnerException?.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = " sysadmin")]
        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<ISleepLevel>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] SleepLevelCuDto item)
        {
            try
            {
              _logger.LogInformation($"{nameof(CreateItem)}: Creating sleeplevel for sleepId {item.SleepId}");
                
                var model = await _service.CreateSleepLevelAsync(item);
                 _logger.LogInformation($"Sleeplevel with ID {model.Item.SleepLevelId} created for the Sleep {item.SleepId}");

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateItem)}: {ex.InnerException?.Message}");
                return BadRequest($"Could not create. Error {ex.InnerException?.Message}");
            }
        }
    }
}