// using Configuration;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Models;
// using Models.DTO;
// using Services;
// using System;
// using System.Security.Claims;
// using System.Threading.Tasks;

// namespace AppWebApi.Controllers
// {
//     [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Policy = null, Roles = "usr, sysadmin")]
//     [ApiController]
//     [Route("api/[controller]/[action]")]
   
//     public class PasswordResetTokenController : Controller
//     {
//         private readonly IPasswordResetTokenService _service;
//         private readonly ILogger<PasswordResetTokenController> _logger;
//         private readonly Encryptions _encryptionService;

//         public PasswordResetTokenController(IPasswordResetTokenService service, ILogger<PasswordResetTokenController> logger, Encryptions encryptions)
//         {
//             _service = service;
//             _logger = logger;
//             _encryptionService = encryptions;
//         }

//         #region Create Token
//         [HttpPost]
//         [ProducesResponseType(200, Type = typeof(ResponseItemDto<PasswordResetToken>))]
//         [ProducesResponseType(400, Type = typeof(string))]
//         public async Task<IActionResult> CreateToken([FromBody] PasswordResetTokenDto tokenDto)
//         {
//             try
//             {
//                 if (string.IsNullOrWhiteSpace(tokenDto.Email))
//                     return BadRequest("Email is required.");

//                 _logger.LogInformation($"Creating token for {tokenDto.Email}");

//                 var token = await _service.CreatePasswordResetTokenAsync(tokenDto);
//                 if (token?.Item == null)
//                     return BadRequest("Failed to create password reset token.");

//                 _logger.LogInformation($"Password reset token created for {tokenDto.Email}");
//                 return Ok(token);
//             }
//             catch (ArgumentException ex)
//             {
//                 _logger.LogError($"Error in CreateToken: {ex.Message}");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Unexpected error in CreateToken: {ex.Message}");
//                 return StatusCode(500, "Internal server error.");
//             }
//         }
//         #endregion

//         #region Read Token
//         [HttpGet("{id}")]
//         [ProducesResponseType(200, Type = typeof(ResponseItemDto<PasswordResetToken>))]
//         [ProducesResponseType(400, Type = typeof(string))]
//         [ProducesResponseType(404, Type = typeof(string))]
//         public async Task<IActionResult> ReadToken(string id)
//         {
//             try
//             {
//                 var idArg = Guid.Parse(id);

//                 _logger.LogInformation($"Reading token with ID: {idArg}");

//                 var item = await _service.ReadPasswordResetTokenAsync(idArg, false);
//                 if (item?.Item == null)
//                     return NotFound($"Token with ID {id} not found.");

//                 return Ok(item);
//             }
//             catch (ArgumentException ex)
//             {
//                 _logger.LogError($"Error in ReadToken: {ex.Message}");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Unexpected error in ReadToken: {ex.Message}");
//                 return StatusCode(500, "Internal server error.");
//             }
//         }
//         #endregion

//         #region Update Token
//         [HttpPut("{id}")]
//         [ProducesResponseType(200, Type = typeof(ResponseItemDto<PasswordResetToken>))]
//         [ProducesResponseType(400, Type = typeof(string))]
//         public async Task<IActionResult> UpdateToken(string id, [FromBody] PasswordResetTokenDto tokenDto)
//         {
//             try
//             {
//                 var idArg = Guid.Parse(id);
//                 if (tokenDto.PasswordResetTokenId != idArg)
//                     return BadRequest("ID mismatch.");

//                 _logger.LogInformation($"Updating token with ID: {idArg}");

//                 var updatedToken = await _service.UpdatePasswordResetTokenAsync(tokenDto);
//                 if (updatedToken?.Item == null)
//                     return BadRequest("Failed to update password reset token.");

//                 return Ok(updatedToken);
//             }
//             catch (ArgumentException ex)
//             {
//                 _logger.LogError($"Error in UpdateToken: {ex.Message}");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Unexpected error in UpdateToken: {ex.Message}");
//                 return StatusCode(500, "Internal server error.");
//             }
//         }
//         #endregion

//         #region Delete Token
//         [HttpDelete("{id}")]
//         [ProducesResponseType(200, Type = typeof(ResponseItemDto<PasswordResetToken>))]
//         [ProducesResponseType(400, Type = typeof(string))]
//         [ProducesResponseType(404, Type = typeof(string))]
//         public async Task<IActionResult> DeleteToken(string id)
//         {
//             try
//             {
//                 var idArg = Guid.Parse(id);

//                 _logger.LogInformation($"Deleting token with ID: {idArg}");

//                 var item = await _service.DeletePasswordResetTokenAsync(idArg);
//                 if (item?.Item == null)
//                     return NotFound($"Token with ID {id} not found.");

//                 return Ok(item);
//             }
//             catch (ArgumentException ex)
//             {
//                 _logger.LogError($"Error in DeleteToken: {ex.Message}");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Unexpected error in DeleteToken: {ex.Message}");
//                 return StatusCode(500, "Internal server error.");
//             }
//         }
//         #endregion

//         #region Reset Password
//         [HttpPost("ResetPassword")]
//         [AllowAnonymous]
//         [ProducesResponseType(200, Type = typeof(string))]
//         [ProducesResponseType(400, Type = typeof(string))]
//         [ProducesResponseType(500, Type = typeof(string))]
//         public async Task<IActionResult> ResetPassword([FromBody] PasswordResetTokenDto item)
//         {
//             try
//             {
//                 // Validate input fields
//                 if (string.IsNullOrWhiteSpace(item.Email) ||
//                     string.IsNullOrWhiteSpace(item.Token) ||
//                     string.IsNullOrWhiteSpace(item.NewPassword) ||
//                     string.IsNullOrWhiteSpace(item.ConfirmPassword))
//                 {
//                     return BadRequest("All fields are required.");
//                 }

//                 if (item.NewPassword != item.ConfirmPassword)
//                 {
//                     return BadRequest("Passwords do not match.");
//                 }

//                 _logger.LogInformation($"Attempting to reset password for {item.Email} with token {item.Token} ");

//                 var result = await _service.ResetPasswordAsync(item);

//                 // If an exception is thrown or the result isn't successful

//                 _logger.LogInformation($"Password reset successful for {item.Email}");
//                 return Ok("Password has been reset successfully.");
//             }
//             catch (ArgumentException ex)
//             {
//                 _logger.LogError($"Error in ResetPassword: {ex.Message}");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Unexpected error in ResetPassword: {ex.Message}");
//                 return StatusCode(500, "Internal server error.");
//             }
//         }

// [HttpPost("ChangePassword")]
// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "usr,sysadmin")]
// public async Task<IActionResult> ChangePassword([FromBody] PasswordResetTokenDto item)
// {
//     try
//     {
//         // Check if required fields are provided
//         if (string.IsNullOrWhiteSpace(item.CurrentPassword) ||
//             string.IsNullOrWhiteSpace(item.NewPassword) ||
//             string.IsNullOrWhiteSpace(item.ConfirmPassword))
//         {
//             return BadRequest("All fields are required.");
//         }

//         if (item.NewPassword != item.ConfirmPassword)
//         {
//             return BadRequest("Passwords do not match.");
//         }

//         // Extract user information from JWT claims
//         var email = User.FindFirst(ClaimTypes.Email)?.Value;
//         var userName = User.Identity?.Name;
//         var role = User.FindFirst(ClaimTypes.Role)?.Value;

//         if (string.IsNullOrWhiteSpace(email))
//         {
//             _logger.LogWarning("Email claim is missing in JWT.");
//             return Unauthorized("Invalid user context.");
//         }

//         _logger.LogInformation($"Authenticated user: {email}, role: {role}");

//         // Call service to change the password
//         var result = await _service.ChangePasswordAsync(email, item.CurrentPassword, item.NewPassword);

//         if (string.IsNullOrWhiteSpace(result?.Message))
//         {
//             return BadRequest("Unable to change password.");
//         }

//         return Ok("Password changed successfully.");
//     }
//     catch (Exception ex)
//     {
//         _logger.LogError($"Error changing password: {ex.Message}");
//         return StatusCode(500, "Internal server error.");
//     }
// }
//     }

//     #endregion
// }