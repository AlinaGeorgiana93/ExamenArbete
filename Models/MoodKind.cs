namespace Models;



public class MoodKind : IMoodKind
{
    public virtual Guid MoodKindId { get; set; }     // Primary Key
    public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
    public virtual string Label { get; set; }
    public virtual int Rating { get; set; }   // Rating from 1 to 10( for that mood type, e.g., 1 = deppresed, 10 =  Happy)


    public virtual List<IMood> Moods { get; set; }

    public static List<MoodKind> GetSeedMoodKindsData()
    {
        return
        [
            new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Happy",
            Rating = 10,
            Label = "Happy 😃"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Sad",
            Rating = 2,
            Label = "Sad 🙁"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Angry",
            Rating = 3,
            Label = "Angry 😡"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Lovely",
            Rating = 7,
            Label = "Lovely 😍"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Depressed",
            Rating = 1,
            Label = "Depressed 😢"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Excited",
            Rating = 9,
            Label = "Excited 🤩"
        },
        new MoodKind {
            MoodKindId = Guid.NewGuid(),
            Name = "Bored",
            Rating = 4,
            Label = "Bored 😒"
        },
        
        ];
    }


}

