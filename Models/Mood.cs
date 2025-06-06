using Configuration;

namespace Models;

public class Mood : IMood
{

    public virtual Guid MoodId { get; set;}
    public virtual DateTime Date { get; set; }
    public virtual DayOfWeek Day { get; set; }
    public virtual string Notes { get; set; } // Additional notes about the appetite


   //Navigation properties

     public virtual Guid? PatientId { get; set; } 
     public virtual IPatient Patient { get; set; }
     public virtual IMoodKind MoodKind { get; set; }

}