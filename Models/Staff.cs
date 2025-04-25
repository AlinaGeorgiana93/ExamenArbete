using Configuration;
using static BCrypt.Net.BCrypt;

namespace Models;

public class Staff : IStaff
{
    public virtual Guid StaffId { get ; set ; }
    public string FirstName { get; set ; }
    public string LastName { get ; set; }
    public string PersonalNumber { get ; set ; }
    public string Email { get ; set ; }
    public string Role { get ; set ; }
    public string UserName { get ; set ; }
    public string Password { get ; set ; }


     public virtual List<IPatient> Patients{ get; set; } 
    // public static List<Staff> GetSeedStaffData(Encryptions encryptions)
    // {
    //     return
    //     [
    //         new() { StaffId = Guid.NewGuid(), FirstName = "Moris", LastName = "Andre", PersonalNumber = "19750105-1111", Email = "Moris@mail.com", Role = "usr", UserName = "moris", Password = encryptions.EncryptPasswordToBase64("1234") },
    //         new() { StaffId = Guid.NewGuid(), FirstName = "Martha", LastName = "Stewart", PersonalNumber = "19850505-2222", Email = "Martha@mail.com", Role = "usr", UserName = "martha", Password = encryptions.EncryptPasswordToBase64("1234") },
    //         new() { StaffId = Guid.NewGuid(), FirstName = "Madi", LastName = "Alabama", PersonalNumber = "19800613-1111", Email = "madi@mail.com", Role = "usr", UserName = "madi", Password = encryptions.EncryptPasswordToBase64("1234") },
    //         new() { StaffId = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", PersonalNumber = "19610228-1212", Email = "jane@mail.com", Role = "usr", UserName = "jane", Password = encryptions.EncryptPasswordToBase64("1234") },
    //         new() { StaffId = Guid.NewGuid(), FirstName = "Bob", LastName = "Brown", PersonalNumber = "19880505-3333", Email = "bob@mail.com", Role = "usr", UserName = "bob", Password = encryptions.EncryptPasswordToBase64("1234") },
    //         new() { StaffId = Guid.NewGuid(), FirstName = "Alice", LastName = "Johnson", PersonalNumber = "19931001-4444", Email = "alice@mail.com", Role = "usr", UserName = "alice", Password = encryptions.EncryptPasswordToBase64("1234") },
    //         new() { StaffId = Guid.NewGuid(), FirstName = "John", LastName = "Doe", PersonalNumber = "19900516-2222", Email = "john@mail.com", Role = "usr", UserName = "john", Password = encryptions.EncryptPasswordToBase64("1234") }
    //     ];
    // }
     
}
