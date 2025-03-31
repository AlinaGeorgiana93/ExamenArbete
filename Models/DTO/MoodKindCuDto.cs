using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

//DTO is a DataTransferObject, can be instanstiated by the controller logic
//and represents a, fully instantiable, subset of the Database models
//for a specific purpose.

//These DTO are simplistic and used to Update and Create objects
public class MoodKindCuDto
{

  public virtual Guid ? MoodKindId { get; set; }     // Primary Key
  public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
  public virtual int Rating { get; set; }       // Rating from 1 to 10

  public virtual List<Guid> MoodsId { get; set; } = null;
  public virtual Guid? MoodId { get; set; }



  public MoodKindCuDto() { }
  public MoodKindCuDto(IMoodKind org)
  {
    MoodKindId = org.MoodKindId;
    Name = org.Name;
    Rating = org.Rating;

    
    MoodsId = org.Moods?.Select(i => i.MoodId).ToList(); 
  


  }
}