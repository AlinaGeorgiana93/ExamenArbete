using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models;
using Models.DTO;
using Services;

namespace AppWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Policy = null, Roles = "staff, sysadmin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MoodKindController : Controller
    {
        readonly IMoodKindService _service = null;
        readonly ILogger<MoodKindController> _logger = null;

        public MoodKindController(IMoodKindService service, ILogger<MoodKindController> logger)
        {
            _service = service;
            _logger = logger;
        }

    
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IMoodKind>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadItems(string seeded = "true", string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool seededArg = bool.Parse(seeded);
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSizeArg = int.Parse(pageSize);

                _logger.LogInformation($"{nameof(ReadItems)}: {nameof(seededArg)}: {seededArg}, {nameof(flatArg)}: {flatArg}, " +
                    $"{nameof(pageNrArg)}: {pageNrArg}, {nameof(pageSizeArg)}: {pageSizeArg}");

                var resp = await _service.ReadMoodKindsAsync(seededArg, flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);     
                return Ok(resp);     
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItems)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IMoodKind>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadItem)}: {nameof(idArg)}: {idArg}, {nameof(flatArg)}: {flatArg}");
                
                var item = await _service.ReadMoodKindAsync(idArg, flatArg);
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
            Policy = null, Roles = "sysadmin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IMoodKind>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(DeleteItem)}: {nameof(idArg)}: {idArg}");
                
                var item = await _service.DeleteMoodKindAsync(idArg);
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
             Policy = null, Roles = "staff, sysadmin")]
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<MoodKindCuDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(ReadItemDto)}: {nameof(idArg)}: {idArg}");

                var item = await _service.ReadMoodKindAsync(idArg, false);
                if (item?.Item == null) throw new ArgumentException ($"Item with id {id} does not exist");

                return Ok(
                    new ResponseItemDto<MoodKindCuDto>() {
                    DbConnectionKeyUsed = item.DbConnectionKeyUsed,
                    Item = new MoodKindCuDto(item.Item)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItemDto)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

       [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
             Policy = null, Roles = "sysadmin")]
         [HttpPut("{id}")]
         [ProducesResponseType(200, Type = typeof(ResponseItemDto<IMoodKind>))]
         [ProducesResponseType(400, Type = typeof(string))]
         public async Task<IActionResult> UpdateItem(string id, [FromBody] MoodKindCuDto item)
         {
            try
             {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(UpdateItem)}: {nameof(idArg)}: {idArg}");
                
                 if (item.MoodKindId != idArg) throw new ArgumentException("Id mismatch");

                var _item = await _service.UpdateMoodKindAsync(item);
                _logger.LogInformation($"item {idArg} updated");
               
                 return Ok(_item);             
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
         [ProducesResponseType(200, Type = typeof(ResponseItemDto<IMoodKind>))]
         [ProducesResponseType(400, Type = typeof(string))]
         public async Task<IActionResult> CreateItem([FromBody] MoodKindCuDto item)
         {
            try
            {
                _logger.LogInformation($"{nameof(CreateItem)}:");
                
                var _item = await _service.CreateMoodKindAsync(item);
                _logger.LogInformation($"item {_item.Item.MoodKindId} created");

                return Ok(_item);       
            }
            catch (Exception ex)
            {
                // Log the full exception first
                _logger.LogError($"{nameof(CreateItem)}: {ex.Message}, {ex.StackTrace}");

                // Then return the BadRequest response
                return BadRequest($"Could not create. Error {ex.Message}");
            }
                }

    }
}