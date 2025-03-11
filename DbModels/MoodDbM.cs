using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Moods", Schema = "supusr")]
public class MoodDbM : Mood //ISeed<MoodDbM>
{
    [Key]
    public override Guid MoodId { get; set; }


    [NotMapped]
    //public override List<IStaff> Staffs { get => StaffsDbM?.ToList<IStaff>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<StaffDbM> StaffsDbM { get; set; }


//     [NotMapped]
//    public override List<IPatient> Patients { get => PatientsDbM?.ToList<IPatient>(); set => throw new NotImplementedException(); }

//     [JsonIgnore]
//     public List<PatientDbM> PatientsDbM { get; set; }


    // public override MoodDbM Seed (SeedGenerator _seeder)
    // {
    //     base.Seed (_seeder);
    //     return this;
    // }

   public Mood UpdateFromDTO(MoodCuDto org)
{
    if (org == null) return null;

    // Update properties from the DTO
    Kind = org.Kind;                // Assuming Kind is part of DTO (of type MoodKind)
   // Date = org.DateTime;            // Assuming DateTime in DTO is Date for the Mood
   // Day = org.DateTime.DayOfWeek;   // Setting Day based on the DateTime in DTO

    return this;
}
    public MoodDbM() { }
    public MoodDbM(MoodCuDto org)
    {
        MoodId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}