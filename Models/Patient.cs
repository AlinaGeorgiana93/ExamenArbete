using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class Patient : IPatient
{
    public Guid PatientId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }



    //public virtual List<IPatient> Patients{ get; set; }
    //public virtual List<IMood> Moods {get; set;}
    //public virtual List <IActivity> Activities{ get; set; }
   //public virtual List <IApetite> Apetites{ get; set; }
     
}