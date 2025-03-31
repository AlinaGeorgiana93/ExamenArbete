using Configuration;

namespace Models;

public interface IActivityLevel
{
    public Guid ActivityLevelId { get; set; }  // Primärnyckel
    public string Name { get; set; }      // e.g., "Low", "medel", "High", etc.
    public int Rating { get; set; }            // Betyg mellan 1-10


    public List<Activity> Activity { get; set; }



}
