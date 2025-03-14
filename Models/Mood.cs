using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Mood : IMood
{

    public virtual Guid MoodId { get; set;}
   
    public virtual MoodKind Kind { get; set; }

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