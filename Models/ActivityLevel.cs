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
                Name = "Resting", 
                Rating = 1, 
                Label = "Resting 🛌"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Reading", 
                Rating = 3, 
                Label = "Reading 📖"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Take a Walk", 
                Rating = 5, 
                Label = "Take a Walk 🚶‍♂️"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Jogging", 
                Rating = 10, 
                Label = "Jogging 🏃‍♂️"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Swimming", 
                Rating = 7, 
                Label = "Swimming 🏊‍♂️"
            },
            new ActivityLevel { 
                ActivityLevelId = Guid.NewGuid(), 
                Name = "Training", 
                Rating = 9, 
                Label = "Training 🏋️‍♂️"
            }
        ];
    }



    
}


    



