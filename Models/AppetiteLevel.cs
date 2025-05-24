namespace Models;



public class AppetiteLevel : IAppetiteLevel
{
    public virtual Guid AppetiteLevelId { get; set; }           // Primary Key
    public virtual string Name { get; set; }      //Very Hungry	; Hungry ; Normal Appetite ; Slightly Full ; Full ; Overeaten
    public virtual string Label { get; set; }     // Label Very Hungry	🍽️	10; Hungry 😋 	8-9; Normal Appetite 🙂 6-7; Slightly Full 🤔	4-5; Full 😌 2-3; Overeaten 🤢 1
    public virtual int Rating { get; set; }       // Rating from 1 to 10
 

    public virtual List <IAppetite> Appetites { get; set; }

     public static List<AppetiteLevel> GetSeedAppetiteLevelsData()
    {
        return
        [
            new AppetiteLevel { 
                AppetiteLevelId = Guid.NewGuid(), 
                Name = "Didn't Eat At All", 
                Rating = 1, 
                Label = "🤢"
            },
            new AppetiteLevel { 
                AppetiteLevelId = Guid.NewGuid(), 
                Name = "Little", 
                Rating = 3, 
                Label = "🍽️"
            },
            new AppetiteLevel { 
                AppetiteLevelId = Guid.NewGuid(), 
                Name = "Normal", 
                Rating = 5, 
                Label = " 🙂"
            },
            new AppetiteLevel { 
                AppetiteLevelId = Guid.NewGuid(), 
                Name = "Medium", 
                Rating = 7, 
                Label = " 😋"
            },
            new AppetiteLevel { 
                AppetiteLevelId = Guid.NewGuid(), 
                Name = "Very Much", 
                Rating = 10, 
                Label = " 🍴"
            }
        ];
}
}