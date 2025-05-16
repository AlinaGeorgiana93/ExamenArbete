using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Models;
using Models.DTO;
using Services;



namespace AppWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Policy = null, Roles = "usr, sysadmin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StaffController : Controller
    {
        readonly IStaffService _service = null;
        readonly ILogger<StaffController> _logger = null;

        public StaffController(IStaffService service, ILogger<StaffController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IStaff>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadItems(string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSizeArg = int.Parse(pageSize);

                _logger.LogInformation($"{nameof(ReadItems)}:  {nameof(flatArg)}: {flatArg}, " +
                    $"{nameof(pageNrArg)}: {pageNrArg}, {nameof(pageSizeArg)}: {pageSizeArg}");

                var resp = await _service.ReadStaffsAsync(flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItems)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IStaff>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadItem)}: {nameof(idArg)}: {idArg}, {nameof(flatArg)}: {flatArg}");

                var item = await _service.ReadStaffAsync(idArg, flatArg);
                if (item?.Item == null) throw new ArgumentException($"Item with id {id} does not exist");

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
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IStaff>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(DeleteItem)}: {nameof(idArg)}: {idArg}");

                var item = await _service.DeleteStaffAsync(idArg);
                if (item?.Item == null) throw new ArgumentException($"Item with id {id} does not exist");

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
            Policy = null, Roles = "usr, sysadmin")]
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<StaffCuDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(ReadItemDto)}: {nameof(idArg)}: {idArg}");

                var item = await _service.ReadStaffAsync(idArg, false);
                if (item?.Item == null) throw new ArgumentException($"Item with id {id} does not exist");

                return Ok(
                    new ResponseItemDto<StaffCuDto>()
                    {
                        DbConnectionKeyUsed = item.DbConnectionKeyUsed,
                        Item = new StaffCuDto(item.Item)
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItemDto)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = "usr, sysadmin")]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IStaff>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] StaffCuDto item)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(UpdateItem)}: {nameof(idArg)}: {idArg}");

                if (item.StaffId != idArg) throw new ArgumentException("Id mismatch");

                var _item = await _service.UpdateStaffAsync(item);
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
            Policy = null, Roles = "usr, sysadmin")]

            [HttpPut("{id}")]
            [ProducesResponseType(200, Type = typeof(ResponseItemDto<IStaff>))]
            [ProducesResponseType(400, Type = typeof(string))]
            public async Task<IActionResult> UpdateProfile(string id, [FromBody] ProfileUpdateCuDto staff) 
            {
                if (!Guid.TryParse(id?.Trim(), out var idArg))
                {
                    _logger.LogError($"{nameof(UpdateProfile)}: Invalid GUID format for id '{id}'");
                    return BadRequest("Invalid staff ID format.");
                }

                try
                {
                    _logger.LogInformation($"{nameof(UpdateProfile)}: {nameof(idArg)}: {idArg}");

                    if (staff.StaffId != idArg) 
                        throw new ArgumentException("Id mismatch");

                    // Trim inputs to avoid trailing/leading space issues
                    staff.Email = staff.Email?.Trim();
                    staff.UserName = staff.UserName?.Trim();
                    staff.CurrentPassword = staff.CurrentPassword?.Trim();
                    staff.NewPassword = staff.NewPassword?.Trim();
                    staff.ConfirmPassword= staff.ConfirmPassword?.Trim();

                    var _item = await _service.UpdateProfileAsync(staff);
                    _logger.LogInformation($"item {idArg} updated");

                    return Ok(_item);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{nameof(UpdateProfile)}: {ex.InnerException?.Message ?? ex.Message}");
                    return BadRequest($"Could not update. Error {ex.InnerException?.Message ?? ex.Message}");
                }
            }



        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
            Policy = null, Roles = " sysadmin")]
        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IStaff>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] StaffCuDto item)
        {
           
            try
            {                
                _logger.LogInformation($"{nameof(CreateItem)}:");

                var _item = await _service.CreateStaffAsync(item);
                _logger.LogInformation($"item {_item.Item.StaffId} created");

                if (string.IsNullOrWhiteSpace(item.Password))
              throw new ArgumentException("Password is required.");

                return Ok(_item);
            }
                catch (Exception ex)
    {
        _logger.LogError(ex, $"{nameof(CreateItem)} failed: {ex.Message}");
        return BadRequest($"Could not create. Error: {ex.Message}");
    }
            }
    }

}

