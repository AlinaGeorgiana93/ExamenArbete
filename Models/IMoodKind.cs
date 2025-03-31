using Configuration;

namespace Models;

public interface IMoodKind
{
    public Guid MoodKindId { get; set; }  // Primärnyckel
    public string Name { get; set; }      
    public int Rating { get; set; }            // Betyg mellan 1-10


    public List<IMood> Moods {get; set;}



}