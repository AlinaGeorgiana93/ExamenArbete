using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Activity:IActivity
{
    public Guid ActivityId { get; set; }


    public ActivityLevel Level { get; set; } // e.g., Low, Medium, High
    
    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite
    
    //Navigation properties
    //public virtual List<IPatient> Patients{ get; set; }
    
}