using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class AppetiteCuDto
{
    public virtual Guid?  AppetiteId { get; set; }
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } // Additional notes about the appetite

    public virtual Guid? PatientId { get; set; }
    public virtual Guid? AppetiteLevelId { get; set; }

    public AppetiteCuDto() { }
    public AppetiteCuDto(IAppetite org)
    {
        AppetiteId = org.AppetiteId;


        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

        PatientId = org.Patient.PatientId;
         AppetiteLevelId = org?.AppetiteLevel?.AppetiteLevelId;
    }
}