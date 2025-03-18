namespace Models;


public interface IPatient
{
    public Guid PatientId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PersonalNumber { get; set; }
    
    

    //Navigation properties
    public List<IGraph> Graphs{ get; set; }
    public List<IMood> Moods {get; set;}
    public List <IActivity> Activities{ get; set; }
    public List <Isleep> Sleeps{ get; set; }
    public List <IApetite> Apetites{ get; set; }
}