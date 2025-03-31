using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Models.DTO;


namespace DbModels;
[Table("Sleeps", Schema = "supusr")]
public class SleepDbM : Sleep 

{
    [Key]
    public override Guid SleepId { get; set; }




    #region adding more readability to an enum type in the database
    public virtual string strSleepLevel
    {
        get => SleepLevel.ToString();
        set { }
    }

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

    public SleepDbM UpdateFromDTO(SleepCuDto org)
    {
        if (org == null) return null;

        SleepLevel = org.SleepLevel;
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
    public GraphDbM GraphDbM { get; set; } 

    [NotMapped]
    public override IGraph Graph
    {
        get => GraphDbM;
        set => throw new NotImplementedException();
    }




    public SleepDbM() { }
    public SleepDbM(SleepCuDto org)
    {
        SleepId = Guid.NewGuid();
        UpdateFromDTO(org);
    }




}