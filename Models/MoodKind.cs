using Configuration;
using Microsoft.Identity.Client;
using Models;
using Seido.Utilities.SeedGenerator;



public class MoodKind: IMoodKind
{
    public virtual Guid MoodKindId { get; set; }           // Primary Key
    public virtual string Name { get; set; }      // e.g., "Happy", "Sad", etc.
    public virtual string Label { get; set; }     // Label (e.g., "😊 Happy", "😢 Sad")
    public virtual int Rating { get; set; }       // Rating from 1 to 10
 
    public virtual Guid? MoodId { get; set; } //FK key -nullable

    public virtual IMood Mood { get; set; }
}
