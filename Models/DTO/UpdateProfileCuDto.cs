using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Models.DTO
{
    public class ProfileUpdateCuDto
{
    public Guid? StaffId { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }

    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword {get; set;}
}

}