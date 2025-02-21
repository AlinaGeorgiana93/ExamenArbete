namespace Models;
public enum MoodKind {Glad, Ledsen, Arg, Lugn, Stressad, Sömnig};

public interface IMood
{
    public Guid MoodId { get; set; }
    public MoodKind Kind { get; set; }

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    

    //Navigation properties
    // public Graph Graph { get; set; }
    // public List<IPatient> Patients { get; set; } 
}