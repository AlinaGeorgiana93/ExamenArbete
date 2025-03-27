using Configuration;

namespace Models;

public interface IActivityLevel
{
    Guid ActivityLevelId { get; set; }  // Primärnyckel
    public string Name { get; set; }      // e.g., "Low", "medel", "High", etc.
    int Rating { get; set; }            // Betyg mellan 1-10



    Guid ActivityId { get; set; }      // FK till Activity
}
