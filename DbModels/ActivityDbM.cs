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


        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;

        return this;
    }

    [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public PatientDbM PatientDbM { get; set; }




    [NotMapped]
    public override IActivityLevel ActivityLevel { get => ActivityLevelDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]

    public ActivityLevelDbM ActivityLevelDbM { get; set; }


    public ActivityDbM() { }
    public ActivityDbM(ActivityCuDto org)
    {
        ActivityId = Guid.NewGuid();
        UpdateFromDTO(org);
    }

}

