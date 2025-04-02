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

    public static List<Patient> GetSeedPatientsData()
        {
            return
            [
                new() { PatientId = Guid.NewGuid(), FirstName = "Madi", LastName = "Alabama" , PersonalNumber = "19560831-1111" },
                new() { PatientId = Guid.NewGuid(), FirstName = "John", LastName = "Doe" , PersonalNumber = "19480516-2222"},
                new() { PatientId = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" , PersonalNumber = "19610228-1212"},
                new() { PatientId = Guid.NewGuid(), FirstName = "Alice", LastName = "Johnson" , PersonalNumber = "19450801-4444"},
                new() { PatientId = Guid.NewGuid(), FirstName = "Bob", LastName = "Brown" , PersonalNumber = "19501110-1331"},
                new() { PatientId = Guid.NewGuid(), FirstName = "Charlie", LastName = "Davis" , PersonalNumber = "19511231-16181"},

            ];
        }
     
}