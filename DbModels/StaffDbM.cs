using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Configuration;

namespace DbModels
{
    [Table("Staffs", Schema = "supusr")]
    public class StaffDbM : Staff
    {
        [Key]
        public override Guid StaffId { get; set; }

        [NotMapped]
        public override List<IPatient> Patients
        {
            get => PatientsDbM?.ToList<IPatient>(); 
            set => throw new NotImplementedException(); // Avoid setting directly
        }

        [JsonIgnore]
        [Required]
        public List<PatientDbM> PatientsDbM { get; set; }

        // Static method to retrieve seed data
//        public static new List<StaffDbM> GetSeedStaffData(Encryptions encryptions)
// {
//     // Get the staff data and encrypt the passwords
//     return [.. Staff.GetSeedStaffData(encryptions)
//         .Select(p => new StaffDbM
//         {
//             StaffId = p.StaffId,
//             FirstName = p.FirstName,
//             LastName = p.LastName,
//             PersonalNumber = p.PersonalNumber,
//             Email = p.Email,
//             Role = p.Role,
//             UserName = p.UserName,
//             Password = p.Password,  // The password will already be encrypted here
//         })];  // Ensure the result is a list
// }
        // Method to update StaffDbM from StaffCuDto
        public StaffDbM UpdateFromDTO(StaffCuDto org)
        {
            if (org == null) return null;

            FirstName = org.FirstName;
            LastName = org.LastName;
            PersonalNumber = org.PersonalNumber;
            Email = org.Email;
            UserName = org.UserName;
            Password = org.Password; // In real applications, this should be hashed before storing
            // Add more properties here as necessary, such as Email, Password, etc.

            return this;
        }

        // Constructor
        public StaffDbM() { }

        // Constructor with DTO
        public StaffDbM(StaffCuDto org)
        {
            StaffId = Guid.NewGuid(); // Generate new ID for new staff
            UpdateFromDTO(org);
        }

        // Example password hashing implementation
        public string HashPassword(string password)
        {
            // In real-world applications, use BCrypt, Argon2, or a similar library
            var hashBytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
