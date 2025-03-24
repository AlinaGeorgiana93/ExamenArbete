using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class AppetiteCuDto
{
    public virtual Guid?  AppetiteId { get; set; }
    public AppetiteLevel AppetiteLevel { get; set; } // e.g., Low, Medium, High
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } // Additional notes about the appetite
    public virtual Guid? PatientId { get; set; } = null;
    public virtual Guid? GraphId { get; set; } = null;

    public AppetiteCuDto() { }
    public AppetiteCuDto(IAppetite org)
    {
        AppetiteId = org.AppetiteId;


        AppetiteLevel = org.AppetiteLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

        PatientId = org?.Patient?.PatientId;
        GraphId = org?.Graph?.GraphId;
    }
}