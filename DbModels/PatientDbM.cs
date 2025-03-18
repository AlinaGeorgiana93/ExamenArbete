using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Patients", Schema = "supusr")]
public class PatientDbM : Patient
{
    [Key]
    public override Guid PatientId { get; set; }

    [NotMapped]
    public override List<IGraph> Graphs { get => GraphsDbM?.ToList<IGraph>(); set => throw new NotImplementedException(); }
    [JsonIgnore]
    [Required]
    public List<GraphDbM> GraphsDbM { get; set; }  


    [NotMapped]
    public override List<IAppetite> Appetites { get => AppetitesDbM?.ToList<IAppetite>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<AppetiteDbM> AppetitesDbM { get; set; }


    [NotMapped]
    public override List<IMood> Moods { get => MoodsDbM?.ToList<IMood>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<MoodDbM> MoodsDbM { get; set; }

        [NotMapped]
    public override List<IActivity> Activities { get => ActivitiesDbM?.ToList<IActivity>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<ActivityDbM> ActivitiesDbM { get; set; }


    [NotMapped]
    public override List<ISleep> Sleeps { get => SleepsDbM?.ToList<ISleep>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<SleepDbM> SleepsDbM { get; set; }


    public PatientDbM UpdateFromDTO(PatientCuDto org)
    {
        if (org == null) return null;

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