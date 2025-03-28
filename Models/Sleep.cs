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
   
    public virtual Guid? PatientId { get; set; } 
   // public virtual Guid? GraphId { get; set; }

     public virtual IPatient Patient { get; set; }
    // public  virtual IGraph Graph { get; set; }
    
}