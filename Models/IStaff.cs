namespace Models;


public interface IStaff
{
    public Guid StaffId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }



    //Navigation properties
  
         public List <IPatient> Patients { get; set; }
     //    public IPasswordResetToken PasswordResetToken { get; set; }
         



}