using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Graph: IGraph
{
    public virtual Guid GraphId { get; set;}
    public virtual DateTime Date { get; set; }

     public virtual Guid? PatientId { get; set; } //FK key -nullable

     public virtual IPatient Patient { get; set; } 
    public virtual List<IActivity> Activities { get; set; }
    public virtual List<IAppetite> Appetites { get; set; }
    public virtual List<IMood> Moods { get; set; }
    public virtual List<ISleep> Sleeps { get; set; }
   
}