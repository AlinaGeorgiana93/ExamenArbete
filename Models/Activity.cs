using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Activity:IActivity
{
    public virtual Guid ActivityId { get; set; }


    public ActivityLevel ActivityLevel { get; set; } // e.g., Low, Medium, High
    
    public virtual  DateTime Date { get; set; }

    public  virtual DayOfWeek Day { get; set; }

    public virtual string Notes { get; set; } // Additional notes about the appetite
    


    
    //Navigation properties
   public virtual IPatient Patient { get; set; }
   public  virtual IGraph Graph { get; set; }
 


}