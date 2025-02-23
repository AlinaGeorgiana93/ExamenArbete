namespace Models;


public interface IStaff
{
    public Guid StaffId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PersonalNumber { get; set; }
    
    

    //Navigation properties
    //public virtual List<IPatient> Patients{ get; set; }
    //public virtual List<IMood> Moods {get; set;}
   // public virtual List <IActivity> Activities{ get; set; }
   // public virtual List <IApetite> Apetites{ get; set; }
}