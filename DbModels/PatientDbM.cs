using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;

namespace DbModels;

[Table("Patients", Schema = "supusr")]
public class PatientDbM : Patient
{
    [Key]
    public override Guid PatientId { get; set; }


    // public override string FirstName {get;set; }
    // public override string LastName { get; set; }
    // public override string PersonalNumber { get; set; }

    [JsonIgnore]
    public virtual Guid? GraphId { get; set; }

    [NotMapped]
    public override IGraph Graph { get => GraphDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]

    [ForeignKey("GraphId")] //Connecting FK above with GraphDbM.GraphId
    public GraphDbM GraphDbM { get; set; } = null;

    [NotMapped]
    public override List<IAppetiteLevel> AppetiteLevels { get => AppetiteLevelsDbM?.ToList<IAppetiteLevel>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<AppetiteLevelDbM> AppetiteLevelsDbM { get; set; }


    [NotMapped]
    public override List<IMoodKind> MoodKinds { get => MoodKindsDbM?.ToList<IMoodKind>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<MoodKindDbM> MoodKindsDbM { get; set; }

    [NotMapped]
    public override List<IActivityLevel> ActivityLevels { get => ActivityLevelsDbM?.ToList<IActivityLevel>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<ActivityLevelDbM> ActivityLevelsDbM { get; set; }


    [NotMapped]
    public override List<ISleepLevel> SleepLevels { get => SleepLevelsDbM?.ToList<ISleepLevel>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    // [Required]
    public List<SleepLevelDbM> SleepLevelsDbM { get; set; }
    public static new List<PatientDbM> GetSeedPatientsData()
    {
        return [.. Patient.GetSeedPatientsData()
            .Select(p => new PatientDbM
            {
                PatientId = p.PatientId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PersonalNumber = p.PersonalNumber
            })];
    }

    public PatientDbM UpdateFromDTO(PatientCuDto org)
    {
        if (org == null) return null;

        FirstName = org.FirstName;
        LastName = org.LastName;
        PersonalNumber = org.PersonalNumber;


        return this;
    }

    public PatientDbM() { }
    public PatientDbM(PatientCuDto org)
    {
        PatientId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}