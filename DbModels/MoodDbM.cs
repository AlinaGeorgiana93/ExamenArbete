using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Moods", Schema = "supusr")]
public class MoodDbM : Mood//ISeed<MoodDbM>
{
    [Key]
    public override Guid MoodId { get; set; }

    public override DateTime Date {get;set; }
    public override DayOfWeek Day { get; set; }

    public override string Notes { get; set; }




    #region adding more readability to an enum type in the database
    public virtual string strMoodKind
    {
        get => MoodKind.ToString();
        set { } 
    }
     #endregion

   public MoodDbM UpdateFromDTO(MoodCuDto org)
    {
        if (org == null) return null;

        MoodKind = org.MoodKind;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;
       

        return this;
    }

  /*  [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public  PatientDbM PatientDbM { get; set; }
 */

    public MoodDbM() { }
    public MoodDbM(MoodCuDto org)
    {
        MoodId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
  
}