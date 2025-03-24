using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;
public class SleepCuDto
{
    public virtual Guid?  SleepId { get; set; }
    public SleepLevel SleepLevel { get; set; } 
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } 
    public virtual Guid? GraphId { get; set; } = null;
    public virtual Guid? PatientId { get; set; } = null;


    public SleepCuDto() { }
    public SleepCuDto(ISleep org)
    {
        SleepId = org.SleepId;

        SleepLevel = org.SleepLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

        PatientId = org?.Patient?.PatientId;
        GraphId = org?.Graph?.GraphId;

    }
}