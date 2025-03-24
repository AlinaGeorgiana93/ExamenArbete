using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("Sleeps", Schema = "supusr")]
public class SleepDbM : Sleep//, ISeed<SleepDbM>
{
    [Key]
    public override Guid SleepId { get; set; }

    #region adding more readability to an enum type in the database
    public virtual string strSleepLevel
    {
        get => SleepLevel.ToString();
        set { } 
    }
    public virtual string strDate
    {
        get => Date.ToString();
        set { } 
    }
    #endregion
    
   [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public  PatientDbM PatientDbM { get; set; }

         [NotMapped]
    public override IGraph Graph { get => GraphDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public  GraphDbM GraphDbM { get; set; }

    // public override SleepDbM Seed (SeedGenerator _seeder)
    // {
    //     base.Seed (_seeder);
    //     return this;
    // }

    public SleepDbM UpdateFromDTO(SleepCuDto org)
    {
        if (org == null) return null;

        SleepLevel = org.SleepLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;
       

        return this;
    }

    public SleepDbM() { }
    public SleepDbM(SleepCuDto org)
    {
        SleepId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}