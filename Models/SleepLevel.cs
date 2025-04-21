namespace Models;


public class SleepLevel : ISleepLevel
{
    public virtual Guid SleepLevelId { get; set; }
    public virtual string Name { get; set; }
    public virtual string Label { get; set; }
    public virtual int Rating { get; set; }        //sleep between 5-10 hours

    public virtual List<ISleep> Sleeps { get; set; }

    public static List<SleepLevel> GetSeedSleepLevelsData()
    {
        return
        [
            new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Low",
            Rating = 5,
            Label = "Low Sleep Level 🙁"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Medium",
            Rating = 6,
            Label = "Medium Sleep Level 😐"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "OK",
            Rating = 8,
            Label = "OK Sleep Level 🙂"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Too much",
            Rating = 10,
            Label = "Too much Sleep Level 😃"
        },
        ];
    }


}