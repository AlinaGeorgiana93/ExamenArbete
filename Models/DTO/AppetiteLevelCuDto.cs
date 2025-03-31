using Models;

public class AppetiteLevelCuDto
{
    public virtual Guid? AppetiteLevelId { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public int Rating { get; set; } 

    
    public virtual Guid? AppetiteId{ get; set; }

    public AppetiteLevelCuDto() { }
    public AppetiteLevelCuDto(IAppetiteLevel org)
    {
        AppetiteLevelId = org.AppetiteLevelId;
  
        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;
        


      
    }
}