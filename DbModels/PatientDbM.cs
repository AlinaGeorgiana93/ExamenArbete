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
  
    [ForeignKey ("GraphId")] //Connecting FK above with GraphDbM.GraphId
    public  GraphDbM  GraphDbM { get; set; } = null;

   [NotMapped]
    public override List<IAppetite> Appetites { get => AppetitesDbM?.ToList<IAppetite>(); set => throw new NotImplementedException(); }
 
    [JsonIgnore]
    [Required]
    public List<AppetiteDbM> AppetitesDbM { get; set; }


    [NotMapped]
    public override List<IMood> Moods { get => MoodsDbM?.ToList<IMood>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
     [Required]
    public List<MoodDbM> MoodsDbM { get; set; }

        [NotMapped]
    public override List<IActivity> Activities { get => ActivitiesDbM?.ToList<IActivity>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
      // [Required]
    public List<ActivityDbM> ActivitiesDbM { get; set; }


    [NotMapped]
    public override List<ISleep> Sleeps { get => SleepsDbM?.ToList<ISleep>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
      // [Required]
    public List<SleepDbM> SleepsDbM { get; set; }
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

        FirstName=org.FirstName;
        LastName=org.LastName;
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