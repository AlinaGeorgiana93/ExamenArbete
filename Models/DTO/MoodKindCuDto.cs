using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class MoodKindCuDto
{ 
   public virtual Guid? MoodKindId { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public int Rating { get; set; } 
    public virtual Guid MoodId { get; set; }
    



    public MoodKindCuDto() { }
    public MoodKindCuDto(IMoodKind org)
    {
        MoodKindId = org.MoodKindId;
        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;
        
        MoodId = org.Mood.MoodId;
        

     
    }
}