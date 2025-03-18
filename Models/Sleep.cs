using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Sleep : ISleep
{
    public virtual Guid SleepId { get; set; }
    public SleepLevel SleepLevel { get; set; }
    
    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    
    public string Notes { get; set; } 

    //Navigation properties
   
     public virtual IPatient Patient { get; set; }
    public  virtual IGraph Graph { get; set; }
    
}