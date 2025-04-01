using Configuration;

namespace Models;

public class Activity : IActivity
{
    public virtual Guid ActivityId { get; set; }
    public virtual DateTime Date { get; set; }
    public virtual DayOfWeek Day { get; set; }
    public virtual string Notes { get; set; } // Additional notes about the appetite

    public virtual Guid? PatientId { get; set; } 

    //Navigation properties
    public virtual IPatient Patient { get; set; }
   // public virtual IGraph Graph { get; set; }
    public virtual IActivityLevel ActivityLevel { get; set; }




}