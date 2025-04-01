using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class ActivityCuDto
{


  public Guid? ActivityId { get; set; }
  public DateTime Date { get; set; }
  public DayOfWeek Day { get; set; }
  public string Notes { get; set; }

  public virtual Guid PatientId { get; set; }
  public virtual Guid? ActivityLevelId { get; set; }
  // public virtual Guid GraphId { get; set; }


  public ActivityCuDto() { }
  public ActivityCuDto(IActivity org)
  {
    ActivityId = org.ActivityId;
    Date = org.Date;
    Day = org.Day;
    Notes = org.Notes;

    PatientId = org.Patient.PatientId;
    ActivityLevelId = org?.ActivityLevel?.ActivityLevelId;
    //GraphId = org.Graph.GraphId;

  }
}




