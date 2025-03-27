using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;

namespace DbModels;

[Table("Activities", Schema = "supusr")]
public class ActivityDbM : Activity
{
    [Key]
    public override Guid ActivityId { get; set; }


    #region Adding more readability to an enum type in the database
    public virtual string strActivityLevel
    {
        get => ActivityLevel.ToString();
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

    public ActivityDbM UpdateFromDTO(ActivityCuDto org)
    {
        if (org == null) return null;

        ActivityLevel = org.ActivityLevel;
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

    public ActivityDbM() { }

    public ActivityDbM(ActivityCuDto org)
    {
        ActivityId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}

