using Configuration;
using Microsoft.Identity.Client;


namespace Models;

public class Patient : IPatient
{
    public virtual Guid PatientId { get ; set ; }
    public virtual string FirstName { get; set ; }
    public virtual string LastName { get ; set; }
    public virtual string PersonalNumber { get ; set ; }


    public virtual Guid? GraphId { get ; set ; }  //FK

    public  virtual IGraph Graph { get; set; }
    public virtual List<IMood> Moods {get; set;}
    public virtual List<ISleep> Sleeps {get; set;}
    public virtual List <IActivity> Activities{ get; set; }
    public virtual List <IAppetite> Appetites{ get; set; }
     
}