using Configuration;

namespace Models;

public class Graph : IGraph
{
    public virtual Guid GraphId { get; set; }
    public virtual DateTime Date { get; set; }


    public virtual Guid? PatientId { get; set; } 
    public virtual IPatient Patient { get; set; } // Navigation property without Foreign Key
    
}