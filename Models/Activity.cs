using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Activity:IActivity
{
    public virtual Guid ActivityId { get; set; }


    public ActivityLevel ActivityLevel { get; set; } // e.g., Low, Medium, High
    
    public  DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite
    


    
    //Navigation properties
   // public Virtual IPatient Patient { get; set; }
   // public  Virtual IGraph Graph { get; set; }
 


}