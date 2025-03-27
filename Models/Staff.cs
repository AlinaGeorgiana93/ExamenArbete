using Configuration;

namespace Models;

public class Staff : IStaff
{
    public virtual Guid StaffId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }

    public void Seed(object seeder)
    {
        throw new NotImplementedException();
    }

    public virtual List<IPatient> Patients{ get; set; }
  
}
