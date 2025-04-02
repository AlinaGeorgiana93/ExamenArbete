namespace Models;



public class MoodKind : IMoodKind
{
    public virtual Guid MoodKindId { get; set; }     // Primary Key
    public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
    public virtual int Rating { get; set; }   // Rating from 1 to 10
    
    
    public virtual List <IMood> Moods{ get; set; }

 public static List<MoodKind> GetSeedMoodKindData()
        {
            return
            [
                new() { MoodKindId = Guid.NewGuid(), Name = "Very Happy", Rating = 10 },
                new() { MoodKindId = Guid.NewGuid(), Name = "Happy", Rating = 8 },
                new() { MoodKindId = Guid.NewGuid(), Name = "Neutral", Rating = 5 },
                new() { MoodKindId = Guid.NewGuid(), Name = "Sad", Rating = 5 },
                new() { MoodKindId = Guid.NewGuid(), Name = "Angry", Rating = 3 }
            ];
        }





}