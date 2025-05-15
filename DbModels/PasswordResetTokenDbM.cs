// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using Newtonsoft.Json;
// using Models;
// using Models.DTO;
// using System.Linq;

// namespace DbModels;

// [Table("PasswordResetToken", Schema = "supusr")]

// public class PasswordResetTokenDbM : PasswordResetToken 
// {
//     [Key]
//     public override Guid PasswordResetTokenId { get; set; }  
//     public override Guid ?StaffId { get; set; }   
//     public override string Token { get; set; }
//     public override string Email { get; set; } // Initialize to an empty string to avoid null reference issues
//     public override DateTime ExpiryDate { get; set; }
//     public override DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Initialize to the current UTC time

//     // Optional navigation property


//      public virtual string ExpiryDateStr
//         {
//             get => ExpiryDate.ToString("yyyy-MM-dd"); // To always get the format "2025-03-21"
//             set { }
//         }
//     public virtual string CreatedAtStr
//         {
//             get => CreatedAt.ToString("yyyy-MM-dd"); // To always get the format "2025-03-21"
//             set { }
//         }

//     [NotMapped]
//     public override IStaff Staff { get => StaffDbM; set => throw new NotImplementedException(); }

//     [ForeignKey("StaffId")] //Connecting FK above with StaffDbM.StaffId
//     public StaffDbM StaffDbM { get; set; }


//  public PasswordResetTokenDbM UpdateFromDTO(PasswordResetTokenDto org)
//     {
//         if (org == null) return null;

//         ExpiryDate = org.ExpiryDate;
//         CreatedAt = org.CreatedAt;
//         Token = org.Token;
//         Email = org.Email;
//        // StaffId = org.StaffId;

//         return this;
//     }
//       public PasswordResetTokenDbM() { }
//     public PasswordResetTokenDbM(PasswordResetTokenDto org)
//     {
//         PasswordResetTokenId = Guid.NewGuid();
//         UpdateFromDTO(org);
//     }


// }