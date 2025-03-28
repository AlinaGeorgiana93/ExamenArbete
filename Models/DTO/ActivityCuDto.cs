using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class ActivityCuDto
{ 
    public Guid? ActivityId { get; set; }
    public ActivityLevel ActivityLevel { get; set; }
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } 
    
   public virtual Guid PatientId { get; set; } 
   //public virtual Guid? GraphId { get; set; } = null;


    public ActivityCuDto() { }
    public ActivityCuDto(IActivity org)
    {
        ActivityId = org.ActivityId;
        ActivityLevel = org.ActivityLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

       PatientId = org.Patient.PatientId;
      // GraphId = org?.Graph?.GraphId;
     
    }
}