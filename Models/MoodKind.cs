namespace Models;



public class MoodKind : IMoodKind
{
    public virtual Guid MoodKindId { get; set; }     // Primary Key
    public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
    public virtual string Label { get; set; }
    public virtual int Rating { get; set; }   // Rating from 1 to 10


    public virtual List<IMood> Moods { get; set; }

    public static List<MoodKind> GetSeedMoodKindsData()
    {
        return
        [
            new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Very Low",
            Rating = 1,
            Label = "Very Low Mood Level 😞"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Low",
            Rating = 3,
            Label = "Low Mood Level 🙁"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Medium",
            Rating = 5,
            Label = "Medium Mood Level 😐"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "High",
            Rating = 7,
            Label = "High Mood Level 🙂"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Very High",
            Rating = 10,
            Label = "Very High Mood Level 😃"
        }
        ];
    }


}

