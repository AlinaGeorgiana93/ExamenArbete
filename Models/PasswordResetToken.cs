using System;

namespace Models
{
    public interface IPasswordResetToken
    {
        public Guid PasswordResetTokenId { get; set; }
        public Guid ?StaffId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public IStaff Staff { get; set; }
    }

    public class PasswordResetToken : IPasswordResetToken
    {
        public virtual Guid PasswordResetTokenId { get; set; }
        public virtual string Token { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime ExpiryDate { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional navigation property
        public virtual IStaff Staff { get; set; }
        public virtual Guid? StaffId { get; set; } 
    }
}
