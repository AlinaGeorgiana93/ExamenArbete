using Configuration;

namespace Models;

public class Staff : IStaff
{
    public virtual Guid StaffId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }


    public virtual List<IPatient> Patients{ get; set; }
    public static List<Staff> GetSeedStaffData()
        {
            return
            [
                new() { StaffId = Guid.NewGuid(), FirstName = "Moris", LastName = "Andre" , PersonalNumber = "19750105-1111" },
                new() { StaffId = Guid.NewGuid(), FirstName = "Madi", LastName = "Alabama" , PersonalNumber = "19800613-1111" },
                new() { StaffId = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" , PersonalNumber = "19610228-1212"},
                new() { StaffId = Guid.NewGuid(), FirstName = "Alice", LastName = "Johnson" , PersonalNumber = "19931001-4444"},
                new() { StaffId = Guid.NewGuid(), FirstName = "John", LastName = "Doe" , PersonalNumber = "19900516-2222"},
                

            ];
        }
     


}
