using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Mood : IMood
{

    public virtual Guid MoodId { get; set;}
   
    public virtual MoodKind Kind { get; set; }

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    
    //Navigation properties
    // public Graph Graph { get; set; }
    // public List<Patient> Patient { get; set; }   // public List<Patient> Patient { get; set; } 
    
}