namespace Models;
public enum MoodKind {Happy, sad, mad, calm, Stressd, Sleepy};

public interface IMood
{
    public Guid MoodId { get; set; }
    public MoodKind MoodKind { get; set; } // e.g., Happy, Sad, Mad, Calm, Stressed, Sleepy

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite
    
    //Navigation properties
   // public virtual IPatient Patient { get; set; }
   // public List<IPatient> IPatients { get; set; }
}