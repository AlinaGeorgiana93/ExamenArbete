using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;
public class SleepCuDto
{
    public virtual Guid?  SleepId { get; set; }
    
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } 

   
     public virtual Guid? PatientId { get; set; } = null;
    public virtual Guid? SleepLevelId { get; set; } // ✅ Store Patient as a GUID instead of `IPatient`

    public SleepCuDto() { }
    public SleepCuDto(ISleep org)
    {
        SleepId = org.SleepId;

        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

          SleepLevelId = org?.SleepLevel?.SleepLevelId;
  

    }
}