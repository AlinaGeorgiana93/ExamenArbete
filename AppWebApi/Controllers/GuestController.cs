using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Models.DTO;
using Services;
using System.Text.RegularExpressions;


namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GuestController : Controller
    {
        readonly IAdminService _adminService;
        readonly ILoginService _loginService;
        readonly ILogger<GuestController> _logger;

        public GuestController(IAdminService adminService, ILoginService loginService, ILogger<GuestController> logger)
        {
            _adminService = adminService;
            _loginService = loginService;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<GstUsrInfoAllDto>))]
        public async Task<IActionResult> Info()
        {
            try
            {
                var info = await _adminService.InfoAsync();

                _logger.LogInformation($"{nameof(Info)}:\n{JsonConvert.SerializeObject(info)}");
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Info)}: {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message}.{ex.InnerException?.Message}");
            }
        }


       
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<LoginUserSessionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> LoginUser([FromBody] LoginCredentialsDto userCreds)
        {
            _logger.LogInformation("LoginUser initiated");

            try
            {
                // Log username and password (only for debugging purposes, not recommended in production)
                _logger.LogInformation($"Attempting login with Username or Email: {userCreds.UserNameOrEmail} and Password: {userCreds.Password}");

                // Validate credentials
               if (string.IsNullOrEmpty(userCreds.UserNameOrEmail) || string.IsNullOrEmpty(userCreds.Password))
                {
                    _logger.LogWarning("Login credentials are empty or null.");
                    return BadRequest("Username or Password cannot be null or empty.");
                }

                            // Regex checks for username/email and password format
                var pSimple = @"^([a-z]|[A-Z]|[0-9]){4,12}$";
                var pEmail = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                var pUNoE = @$"({pSimple})|({pEmail})";

                Regex r = new Regex(pUNoE, RegexOptions.IgnoreCase);
                if (!r.Match(userCreds.UserNameOrEmail).Success) throw new ArgumentException("Wrong username format");

                r = new Regex(pSimple, RegexOptions.IgnoreCase);
                if (!r.Match(userCreds.Password).Success) throw new ArgumentException("Wrong password format");

                // With validated credentials, proceed to login
                var resp = await _loginService.LoginUserAsync(userCreds);

                // Log successful login (password is not typically logged in production)
                _logger.LogInformation($"{resp.Item.UserName} logged in");

                return Ok(resp);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Login Error: {ex.Message}");
                return BadRequest($"Login Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Login Error: {ex.Message}.{ex.InnerException?.Message}");
                return BadRequest($"Login Error: {ex.Message}.{ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<LoginStaffSessionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> LoginStaff([FromBody] LoginCredentialsDto userCreds)
        {
            _logger.LogInformation("LoginStaff initiated");

            try
            {
                // Log username and password (only for debugging purposes, not recommended in production)
                _logger.LogInformation($"Attempting login with Username or Email: {userCreds.UserNameOrEmail} and Password: {userCreds.Password}");

                // Validate credentials
               if (string.IsNullOrEmpty(userCreds.UserNameOrEmail) || string.IsNullOrEmpty(userCreds.Password))
                {
                    _logger.LogWarning("Login credentials are empty or null.");
                    return BadRequest("Username or Password cannot be null or empty.");
                }

              // Regex checks for username/email and password format
             // Proper regex definitions
        var pUsername = @"^[a-zA-Z0-9._-]{4,20}$"; // usernames: allow letters, numbers, dots, underscores, dashes
        var pEmail = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        var pPassword = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$"; // 6+ chars, 1 uppercase, 1 digit, 1 special

        // Check if username or email is valid
        bool isUsernameValid = Regex.IsMatch(userCreds.UserNameOrEmail, pUsername, RegexOptions.IgnoreCase);
        bool isEmailValid = Regex.IsMatch(userCreds.UserNameOrEmail, pEmail, RegexOptions.IgnoreCase);

        if (!isUsernameValid && !isEmailValid)
            throw new ArgumentException("Wrong username/email format");

        // Check password format
        if (!Regex.IsMatch(userCreds.Password, pPassword))
            throw new ArgumentException("Wrong password format");

        // With validated credentials, proceed to login
        var resp = await _loginService.LoginStaffAsync(userCreds);
        _logger.LogInformation($"{resp.Item.UserName} logged in");

                return Ok(resp);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Login Error: {ex.Message}");
                return BadRequest($"Login Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Login Error: {ex.Message}.{ex.InnerException?.Message}");
                return BadRequest($"Login Error: {ex.Message}.{ex.InnerException?.Message}");
            }
        }
    
    }
}

