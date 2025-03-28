using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class GraphCuDto
{
    public virtual Guid? GraphId { get; set; }

    public DateTime Date { get; set; }

    // public virtual List<Guid> ActivitiesId { get; set; } = null;
    // public virtual List<Guid> AppetitesId { get; set; } = null;
    // public virtual List<Guid> MoodsId { get; set; } = null;
    // public virtual List<Guid> SleepsId { get; set; } = null;
    public virtual Guid? PatientId { get; set; } // ✅ Store Patient as a GUID instead of `IPatient`
   

    public GraphCuDto() { }
    public GraphCuDto(IGraph org)
    {
        GraphId = org.GraphId;
        Date = org.Date;
        
        
    //     MoodsId = org.Moods?.Select(i => i.MoodId).ToList();
    //    ActivitiesId = org.Activities?.Select(e => e.ActivityId).ToList();
    //     SleepsId = org.Sleeps?.Select(i => i.SleepId).ToList();
    //    AppetitesId = org.Appetites?.Select(e => e.AppetiteId).ToList();

       PatientId = org.Patient?.PatientId; // ✅ Assign PatientId correctly
    }
}