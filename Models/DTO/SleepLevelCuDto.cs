using Models;

public class SleepLevelCuDto
{
    public virtual Guid? SleepLevelId { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public int Rating { get; set; } 

    
    public virtual Guid? SleepId{ get; set; }

    public SleepLevelCuDto() { }
    public SleepLevelCuDto(ISleepLevel org)
    {
        SleepLevelId = org.SleepLevelId;
  
        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;
        


      
    }
}