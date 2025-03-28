namespace Models;

public enum ActivityLevel {Low, Medium, High};

public interface IActivity
{
    public Guid ActivityId { get; set; }

    public ActivityLevel ActivityLevel  { get; set; } // e.g., Low, Medium, High

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite
    
    //Navigation properties
    public IPatient Patient { get; set; }
   // public IGraph Graph { get; set; }
 
 
}