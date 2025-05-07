using System;

namespace Models.DTO
{
    public class PasswordResetTokenDto
    {
        public Guid? PasswordResetTokenId { get; set; } 
        public Guid? StaffId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Fields for password operations

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }


        // Default constructor
        public PasswordResetTokenDto() { }

        // Constructor for mapping from entity
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
}
