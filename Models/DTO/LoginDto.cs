﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class LoginCredentialsDto
{
    public string UserNameOrEmail { get; set; } 
    public string Password { get; set; }
}

public class LoginUserSessionDto
{   public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public string Email { get; set; }
    public JwtUserToken JwtToken { get; set; }
}

public class LoginStaffSessionDto
{
    public Guid? StaffId { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public string Email { get; set; }
    public JwtUserToken JwtToken { get; set; }
}



