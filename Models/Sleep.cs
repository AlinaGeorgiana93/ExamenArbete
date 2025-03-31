using Configuration;

namespace Models;

public class Sleep : ISleep
{
    public virtual Guid SleepId { get; set; }
    public virtual DateTime Date { get; set; }
    public virtual DayOfWeek Day { get; set; }
    public virtual string Notes { get; set; } 

   
    public virtual Guid? PatientId { get; set; }  //FK nullable 


    public virtual IPatient Patient { get; set; }
   // public virtual IGraph Graph { get; set; }
    public virtual ISleepLevel SleepLevel { get; set; } 
    
}