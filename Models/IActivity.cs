namespace Models;

public enum ActivityLevel {Low, Medium, High};

public interface IActivity
{
    public Guid ActivityId { get; set; }
    public ActivityLevel Level { get; set; }

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    
   
    
    

    //Navigation properties
    //public virtual List<IPatient> Patients{ get; set; }
}