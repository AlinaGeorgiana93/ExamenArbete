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
    public virtual List<Guid> ApetiteId { get; set; } = null;
   


    public PatientCuDto() { }
    public PatientCuDto(IPatient org)
    {
        PatientId = org.PatientId;
        FirstName = org.FirstName;
        LastName= org.LastName;
        PersonalNumber = org.PersonalNumber;
        
        GraphsId = org.Graphs?.Select(i => i.GraphId).ToList();
        MoodsId = org.Moods?.Select(i => i.MoodId).ToList();
        ActivitysId = org.Activities?.Select(e => e.ActivityId).ToList();
        ApetitesId = org.Apetites?.Select(e => e.ApetiteId).ToList();
        PatientsId = org.Patients?.Select(e => e.PatientId).ToList();

    }
}