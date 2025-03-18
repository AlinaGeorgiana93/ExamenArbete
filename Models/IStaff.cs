namespace Models;


public interface IStaff
{
    public Guid StaffId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PersonalNumber { get; set; }
    
    

    //Navigation properties
  
     public  List<IMood> Moods {get; set;}
     public List <IActivity> Activities{ get; set; }
     public List <IAppetite> Appetites{ get; set; }
     public List <Patient> Patients { get; set; }
}