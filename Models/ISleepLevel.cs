namespace Models;

public interface ISleepLevel
{

  public Guid SleepLevelId { get; set; }   
  public string Name { get; set; }   
  public string Label { get; set; }    
  public int Rating { get; set; }   


  public List<ISleep> Sleeps { get; set; }

}