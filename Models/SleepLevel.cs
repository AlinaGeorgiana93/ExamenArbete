using Configuration;
using Microsoft.Identity.Client;
using Models;


namespace Models;

public class SleepLevel: ISleepLevel
{
    public virtual Guid SleepLevelId { get; set; }    
    public virtual string Name { get; set; }    
    public virtual string Label { get; set; }     
    public virtual int Rating { get; set; }       

    public virtual List <ISleep> Sleeps { get; set; }
 public static List<SleepLevel> GetSeedSleepLevelsData()
    {
        return
        [
            new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Very Low",
            Rating = 1,
            Label = "Very Low Mood Level 😞"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Low",
            Rating = 3,
            Label = "Low Mood Level 🙁"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Medium",
            Rating = 5,
            Label = "Medium Mood Level 😐"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "High",
            Rating = 7,
            Label = "High Mood Level 🙂"
        },
        new SleepLevel {
            SleepLevelId = Guid.NewGuid(),
            Name = "Very High",
            Rating = 10,
            Label = "Very High Mood Level 😃"
        }
        ];
    }


}