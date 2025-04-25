using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DTO
{
    public class StaffCuDto
    {
     
        public Guid? StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }



         // FK for Patients (One-to-Many Relationship)
        public virtual List<Guid> PatientsId { get; set; }
        // Default constructor
        public StaffCuDto() { }

        // Constructor to map from a staff entity to a DTO (used in services or controllers)
        public StaffCuDto(IStaff org)
    {
        StaffId = org.StaffId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        PersonalNumber = org.PersonalNumber;
        Email = org.Email;
        UserName = org.UserName;
        Password = org.Password; // In real applications, this should be hashed before storing
        Role = org.Role;

        
        
         PatientsId = org.Patients?.Select(i => i.PatientId).ToList();
        }
    }
}
