using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class MoodCuDto
{
    public virtual Guid? MoodId { get; set; }

    public MoodKind Kind { get; set; }

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }


    //public virtual List<Guid> PatientId { get; set; } = null;


    public MoodCuDto() { }
    public MoodCuDto(IMood org)
    {
        MoodId = org.MoodId;
        Kind = org.Kind;
        Date = org.Date;
        Day = org.Day;
        
       // AnimalsId = org.Animals?.Select(i => i.AnimalId).ToList();
       // EmployeesId = org.Employees?.Select(e => e.EmployeeId).ToList();
    }
}