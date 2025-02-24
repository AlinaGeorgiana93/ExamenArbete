using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class AppetiteCuDto
{
    public virtual Guid?  AppetiteId { get; set; }
    public AppetiteLevel AppetiteLevel { get; set; } // e.g., Low, Medium, High
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } // Additional notes about the appetite
    public virtual Guid? PatientId { get; set; } = null;
    public AppetiteCuDto() { }
    public AppetiteCuDto(IAppetite org)
    {
        AppetiteId = org.AppetiteId;


        AppetiteLevel = org.AppetiteLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

        PatientId = org?.Patient?.PatientId;
    }
}