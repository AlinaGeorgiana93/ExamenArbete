using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class MoodCuDto
{
    public virtual Guid? MoodId { get; set; }
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; } 
 
    public virtual Guid? PatientId { get; set; } = null;
    public List<Guid> MoodKindsId { get; set; }

   


    public MoodCuDto() { }
    public MoodCuDto(IMood org)
    {
        MoodId = org.MoodId;
        Date = org.Date;
        Day = org.Day;
        
      PatientId = org?.Patient?.PatientId;
      MoodKindsId = org.MoodKinds?.Select(e => e.MoodKindId).ToList();

      
    }
}