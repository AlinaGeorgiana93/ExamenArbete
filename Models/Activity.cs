using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Activity:IActivity
{
    public Guid ActivityId { get; set; }


    public ActivityLevel Level { get; set; }
    
    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    
    //Navigation properties
    //public virtual List<IPatient> Patients{ get; set; }
    
}