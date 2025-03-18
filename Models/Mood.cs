using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Mood : IMood
{

    public virtual Guid MoodId { get; set;}
   
    public  MoodKind MoodKind { get; set; }

    public DateTime Date { get; set; }

    public  DayOfWeek Day { get; set; }
    
    public  string Notes { get; set; } // Additional notes about the appetite


   //Navigation properties
   // public Virtual IPatient Patient { get; set; }
   // public Virtual IGraph Graph { get; set; }
}