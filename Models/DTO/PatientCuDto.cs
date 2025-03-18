using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;
public class PatientCuDto
{
    public Guid PatientId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }



    public virtual List<Guid> GraphId { get; set; } = null;
    public virtual List<Guid> MoodId { get; set; } = null;
    public virtual List<Guid> ActivityId { get; set; } = null;
    public virtual List<Guid> AppetiteId { get; set; } = null;
   


    public PatientCuDto() { }
    public PatientCuDto(IPatient org)
    {
        PatientId = org.PatientId;
        FirstName = org.FirstName;
        LastName= org.LastName;
        PersonalNumber = org.PersonalNumber;
        
         GraphId = org.Graphs?.Select(i => i.GraphId).ToList();
         MoodId = org.Moods?.Select(i => i.MoodId).ToList();
         ActivityId = org.Activities?.Select(e => e.ActivityId).ToList();
         AppetiteId = org.Appetites?.Select(e => e.AppetiteId).ToList();
        // PatientId = org.Patients?.Select(e => e.PatientId).ToList();

    }
}