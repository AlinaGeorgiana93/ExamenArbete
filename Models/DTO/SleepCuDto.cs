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
    

    // public virtual Guid? GraphId { get; set; } = null;
    public virtual Guid? PatientId { get; set; } = null;
    public virtual Guid? SleepLevelId { get; set; }

    public SleepCuDto() { }
    public SleepCuDto(ISleep org)
    {
        SleepId = org.SleepId;
        Date = org.Date;
        Day = org.Day;

        PatientId = org?.Patient?.PatientId;
        SleepLevelId = org?.SleepLevel?.SleepLevelId;

    }
}