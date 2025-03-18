using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Graphs", Schema = "supusr")]
public class GraphDbM : Graph
{
    [Key]
    public override Guid GraphId { get; set; }

    //Fix: Link Graph to Patient
    [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public PatientDbM PatientDbM { get; set; }  // Graph now belongs to a Patient


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


    // public override GraphDbM Seed (SeedGenerator _seeder)
    // {
    //     base.Seed (_seeder);
    //     return this;
    // }

    public GraphDbM UpdateFromDTO(GraphCuDto org)
    {
        if (org == null) return null;

        Date = org.Date;


        return this;
    }

    public GraphDbM() { }
    public GraphDbM(GraphCuDto org)
    {
        GraphId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}