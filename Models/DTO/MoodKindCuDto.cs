using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;


public class MoodKindCuDto
{

  public virtual Guid? MoodKindId { get; set; }     // Primary Key
  public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
  public virtual string Label { get; set; }
  public virtual int Rating { get; set; }       // Rating from 1 to 10

  // public virtual List<Guid> MoodsId { get; set; } = null;
  // public virtual Guid? MoodId { get; set; }

  public MoodKindCuDto() { }
  public MoodKindCuDto(IMoodKind org)
  {
    MoodKindId = org.MoodKindId;
    Name = org.Name;
    Rating = org.Rating;


    // MoodsId = org.Moods?.Select(i => i.MoodId).ToList(); 



  }
}