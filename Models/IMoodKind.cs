namespace Models;

public interface IMoodKind {

 public Guid MoodKindId { get; set; }           // Primary Key
public string Name { get; set; }      // e.g., "Happy", "Sad", etc.
public string Label { get; set; }     // Label (e.g., "😊 Happy", "😢 Sad")
public int Rating { get; set; }       // Rating from 1 to 10


  public IMood Mood { get; set; }



}