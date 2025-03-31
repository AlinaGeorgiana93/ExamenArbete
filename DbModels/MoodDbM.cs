using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Models.DTO;

namespace DbModels;

[Table("Moods", Schema = "supusr")]
public class MoodDbM : Mood
{
    [Key]
    public override Guid MoodId { get; set; }


    #region adding more readability to an enum type in the database
   

    public virtual string strDayOfWeek
    {
        get => Day.ToString();
        set { }
    }


    public virtual string strDate
    {
        get => Date.ToString("yyyy-MM-dd"); // To always get the format "2025-03-21"
        set { }
    }

    #endregion

    public MoodDbM UpdateFromDTO(MoodCuDto org)
    {
        if (org == null) return null;

       
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;


        return this;
    }

    public Guid PatientDbMPatientId { get; set; }

    [NotMapped]
    public override IPatient Patient
    {
        get => PatientDbM; set => throw new NotImplementedException();
    }

    [JsonIgnore]
    [Required]
    public PatientDbM PatientDbM { get; set; }

    [JsonIgnore]
    [Required]
    public GraphDbM GraphDbM { get; set; }  // This represents the relationship with GraphDbM

    [NotMapped]
    public override IGraph Graph
    {
        get => GraphDbM;
        set => throw new NotImplementedException();
    }



    public MoodDbM() { }
    public MoodDbM(MoodCuDto org)
    {
        MoodId = Guid.NewGuid();
        UpdateFromDTO(org);
    }

}