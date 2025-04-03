namespace Models;



public class ActivityLevel : IActivityLevel
{
    public virtual Guid ActivityLevelId { get; set; }     // Primary Key
    public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
    public virtual int Rating { get; set; }       // Rating from 1 to 10
    public virtual string Label { get; set; }
    public virtual List<Activity> Activities { get; set; }





public static List<ActivityLevel> GetSeedActivityLevelsData()
    {
        return
        [
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Very Low", 
                Rating = 1, 
                Label = "Very Low Activity Level 🛌"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Low", 
                Rating = 3, 
                Label = "Low Activity Level 🚶‍♂️"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Medium", 
                Rating = 5, 
                Label = "Medium Activity Level 🏃‍♂️"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "High", 
                Rating = 7, 
                Label = "High Activity Level 🏋️‍♂️"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Very High", 
                Rating = 10, 
                Label = "Very High Activity Level 🏆"
            }
        ];
    }



    
}


    



