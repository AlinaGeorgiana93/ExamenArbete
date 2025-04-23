namespace Models;


public interface IPatient
{
    public Guid PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }



    //Navigation properties
    public List<IMoodKind> MoodKinds {get; set;}
    public List <IActivityLevel> ActivityLevels{ get; set; }
    public List <ISleepLevel> SleepLevels{ get; set; }
    public List <IAppetiteLevel> AppetiteLevels{ get; set; }
    public IGraph Graph { get; set; }
}