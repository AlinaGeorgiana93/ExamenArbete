using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class ActivityCuDto
{

    
    public Guid? ActivityId { get; set; }
    public ActivityLevel Level { get; set; }
    
    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    
    public virtual Guid? ZooId { get; set; } = null;
    
    public ActivityCuDto() { }
    public ActivityCuDto(IActivity org)
    {
        ActivityId = org.ActivityId;
        Level = org.Level;
        Date = org.Date;
        Day = org.Day;
       
        // PatientsId = org.Patients?.Select(i => i.PatientId).ToList();
    }
}