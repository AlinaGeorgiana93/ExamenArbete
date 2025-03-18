using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("Appetites", Schema = "supusr")]
public class AppetiteDbM : Appetite//, ISeed<AppetiteDbM>
{
    [Key]
    public override Guid AppetiteId { get; set; }

    #region adding more readability to an enum type in the database
    public virtual string strKind
    {
        get => AppetiteLevel.ToString();
        set { }  //set is needed by EFC to include in the database, so I make it to do nothing
    }
    public virtual string strMood
    {
        get => Date.ToString();
        set { } //set is needed by EFC to include in the database, so I make it to do nothing
    }
    #endregion
    
    // [NotMapped]
    // public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    // [JsonIgnore]
    // [Required]
    // public  PatientDbM PatientDbM { get; set; }

    [NotMapped]
    public override IGraph Graph { get => GraphDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public  GraphDbM? GraphDbM { get; set; }

    // public override AppetiteDbM Seed (SeedGenerator _seeder)
    // {
    //     base.Seed (_seeder);
    //     return this;
    // }

    public AppetiteDbM UpdateFromDTO(AppetiteCuDto org)
    {
        if (org == null) return null;

        AppetiteLevel = org.AppetiteLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;
       

        return this;
    }

    public AppetiteDbM() { }
    public AppetiteDbM(AppetiteCuDto org)
    {
        AppetiteId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}
