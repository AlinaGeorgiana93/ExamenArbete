using Configuration;
using Microsoft.Identity.Client;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Patient : IPatient
{
    public virtual Guid PatientId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }


    public virtual List<IPatient> Patients{ get; set; }

    public virtual List<IGraph> Graphs{ get; set; }
    public virtual List<IMood> Moods {get; set;}
    public virtual List<ISleep> Sleeps {get; set;}

    public virtual List <IActivity> Activities{ get; set; }
    public virtual List <IAppetite> Appetites{ get; set; }
     
}