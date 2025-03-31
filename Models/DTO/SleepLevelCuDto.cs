using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class SleepLevelCuDto
{
    public virtual Guid? SleepLevelId { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public int Rating { get; set; } 

    
  
  public virtual List<Guid> SleepsId { get; set; } = null;
  public virtual Guid? SleepId { get; set; }

    public SleepLevelCuDto() { }
    public SleepLevelCuDto(ISleepLevel org)
    {
        SleepLevelId = org.SleepLevelId;
  
        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;
        
     SleepsId = org.Sleeps?.Select(i => i.SleepId).ToList(); 

      
    }
}