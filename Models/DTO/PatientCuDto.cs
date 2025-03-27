using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;
public class PatientCuDto
{
    public Guid ?PatientId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }



    
    // public virtual List<Guid> ActivitiesId { get; set; } = null;
   // public virtual List<Guid> AppetitesId { get; set; } = null;
   // public virtual List<Guid> MoodsId { get; set; } = null;
   // public virtual List<Guid> SleepsId { get; set; } = null;

    public virtual Guid? GraphId { get; set; } // ✅ Store Patient as a GUID instead of `IPatient`

    public PatientCuDto() { }
    public PatientCuDto(IPatient org)
    {
        PatientId = org.PatientId;
        FirstName = org.FirstName;
        LastName= org.LastName;
        PersonalNumber = org.PersonalNumber;
        
       GraphId = org.Graph?.GraphId; // ✅ Assign PatientId correctly
        // MoodsId = org.Moods?.Select(i => i.MoodId).ToList();
        // ActivitiesId  = org.Activities?.Select(e => e.ActivityId).ToList();
        // AppetitesId = org.Appetites?.Select(e => e.AppetiteId).ToList();

    

    }
}