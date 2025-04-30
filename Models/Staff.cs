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
     public virtual IPasswordResetToken PasswordResetToken { get; set; }
     public virtual Guid? PasswordResetTokenId { get ; set ; }  //FK
  
    
     
}
