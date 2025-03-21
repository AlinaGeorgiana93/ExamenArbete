namespace Models;


public interface IStaff
{
    public Guid StaffId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PersonalNumber { get; set; }
    
    

    //Navigation properties
  
     public List <IPatient> Patients { get; set; }
  

}