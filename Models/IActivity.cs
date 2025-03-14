namespace Models;

public enum ActivityLevel {Low, Medium, High};

public interface IActivity
{
    public Guid ActivityId { get; set; }

    public ActivityLevel Level  { get; set; } // e.g., Low, Medium, High

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite
    
   
    
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int? GraphId { get; set; }
    public Graph Graph { get; set; }

    public int? StaffId { get; set; }  // Koppling till Staff
    public Staff Staff { get; set; }
}