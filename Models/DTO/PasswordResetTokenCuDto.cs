using System.ComponentModel;
using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;


    public class PasswordResetTokenDto
    {
        public virtual Guid? PasswordResetTokenId { get; set; } 
        public virtual Guid? StaffId { get; set; }
        public virtual string Token { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime ExpiryDate { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual string NewPassword { get; set; }
        public virtual string ConfirmPassword { get; set; }
        public virtual string CurrentPassword { get; set; }

        
    

    public PasswordResetTokenDto() { }
    public PasswordResetTokenDto(IPasswordResetToken org)
     {
    PasswordResetTokenId = org.PasswordResetTokenId;
    Token = org.Token;
    Email = org.Email;
    ExpiryDate = org.ExpiryDate;
    CreatedAt = org.CreatedAt;
    StaffId = org?.Staff?.StaffId;

      }


    }

