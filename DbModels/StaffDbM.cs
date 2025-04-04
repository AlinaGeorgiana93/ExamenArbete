using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;

namespace DbModels;

[Table("Staffs", Schema = "supusr")]
public class StaffDbM : Staff 
{
    [Key]
    public override Guid StaffId { get; set; }
 
    [NotMapped]
    public override List<IPatient> Patients { get => PatientsDbM?.ToList<IPatient>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
     [Required]
     public  List <PatientDbM> PatientsDbM { get; set; }

         public static new List<StaffDbM> GetSeedStaffData()
    {
        return [.. Staff.GetSeedStaffData()
            .Select(p => new StaffDbM
            {
                StaffId = p.StaffId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PersonalNumber = p.PersonalNumber
            })];
    }

    public StaffDbM UpdateFromDTO(StaffCuDto org)
    {
        if (org == null) return null;

        FirstName = org.FirstName;
        LastName = org.LastName;
        PersonalNumber = org.PersonalNumber;

        return this;
    }



    public StaffDbM() { }
    public StaffDbM(StaffCuDto org)
    {
        StaffId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}