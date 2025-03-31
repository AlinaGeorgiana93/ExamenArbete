using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class AppetiteLevelCuDto
{
    public virtual Guid? AppetiteLevelId { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public int Rating { get; set; } 

    
    public virtual List<Guid> AppetitesId { get; set; } = null;

    public AppetiteLevelCuDto() { }
    public AppetiteLevelCuDto(IAppetiteLevel org)
    {
        AppetiteLevelId = org.AppetiteLevelId;
  
        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;

          AppetitesId = org.Appetites?.Select(i => i.AppetiteId).ToList(); 
  
        


      
    }
}