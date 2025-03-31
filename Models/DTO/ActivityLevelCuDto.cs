using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class ActivityLevelCuDto
{

  public virtual Guid ? ActivityLevelId { get; set; }     // Primary Key
  public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
  public virtual int Rating { get; set; }       // Rating from 1 to 10


  public virtual Guid? ActivityId { get; set; }



  public ActivityLevelCuDto() { }
  public ActivityLevelCuDto(IActivityLevel org)
  {
    ActivityLevelId = org.ActivityLevelId;
    Name = org.Name;
    Rating = org.Rating;
    ActivityId = org.Activity?.ActivityId;


  }
}


