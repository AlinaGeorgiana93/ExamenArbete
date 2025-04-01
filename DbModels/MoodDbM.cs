using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Models.DTO;

namespace DbModels;

[Table("Moods", Schema = "supusr")]
public class MoodDbM : Mood {
    
    [Key]
    public override Guid MoodId { get; set; }
    public override DateTime Date {get;set; }
    public override DayOfWeek Day { get; set; }
    public override string Notes { get; set; }


    #region convert to string
    

     public virtual string StrDayOfWeek
        {
            get => Day.ToString();
            set { }
        }
        
        public virtual string StrDate
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
        [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    public PatientDbM PatientDbM { get; set; }

    [NotMapped]
    public override IMoodKind MoodKind { get => MoodKindDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    
    public MoodKindDbM MoodKindDbM { get; set; }
        
    public MoodDbM() { }
    public MoodDbM(MoodCuDto org)
    {
        MoodId = Guid.NewGuid();
        UpdateFromDTO(org);
    }

}