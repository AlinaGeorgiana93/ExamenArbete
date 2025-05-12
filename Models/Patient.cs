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
    
    public virtual List<IMoodKind> MoodKinds {get; set;}
    public virtual List<ISleepLevel> SleepLevels {get; set;}
    public virtual List <IActivityLevel> ActivityLevels{ get; set; }
    public virtual List <IAppetiteLevel> AppetiteLevels{ get; set; }
     
}