namespace Models;
public enum MoodKind {Happy, sad, mad, calm, Stressd, Sleepy};

public interface IMood
{
    public Guid MoodId { get; set; }
    public MoodKind Kind { get; set; } // e.g., Happy, Sad, Mad, Calm, Stressed, Sleepy

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