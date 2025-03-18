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
    public virtual Guid? StaffId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }



    //public virtual List<Guid> PatientId { get; set; } = null;
    //public virtual List<Guid> MoodId { get; set; } = null;
    //public virtual List<Guid> ActivityId { get; set; } = null;
    //public virtual List<Guid> ApetiteId { get; set; } = null;
   


    public StaffCuDto() { }
    public StaffCuDto(IStaff org)
    {
        StaffId = org.StaffId;
        FirstName = org.FirstName;
        LastName= org.LastName;
        PersonalNumber = org.PersonalNumber;
        
       // MoodsId = org.Moods?.Select(i => i.MoodId).ToList();
       // ActivitysId = org.Activities?.Select(e => e.ActivityId).ToList();
       //ApetitesId = org.Apetites?.Select(e => e.ApetiteId).ToList();
       //PacientsId = org.Pacientes?.Select(e => e.PatientId).ToList();

    }
}