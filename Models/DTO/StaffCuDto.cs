using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class StaffCuDto
{
    public virtual Guid? StaffId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }

    // FK for Patients (One-to-Many Relationship)
    public virtual List<Guid>? PatientsId { get; set; }

    public StaffCuDto() { }

    public StaffCuDto(IStaff org)
    {
        StaffId = org.StaffId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        PersonalNumber = org.PersonalNumber;
        
        // Ensure Patients list is not null before selecting IDs
       PatientsId = org.Patients?.Select(i => i.PatientId).ToList();
    }
}
