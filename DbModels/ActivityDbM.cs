﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("Activities", Schema = "supusr")]
public class ActivityDbM : Activity
{
    [Key]
    public override Guid ActivityId { get; set; }
    public override DateTime Date {get;set;}
    public override DayOfWeek Day { get; set; }
    public override string Notes { get; set; }




    #region adding more readability to an enum type in the database
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
      [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    public  PatientDbM PatientDbM { get; set; }

       [NotMapped]
    public override IGraph Graph { get => GraphDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
   
    public  GraphDbM GraphDbM { get; set; }


    public ActivityDbM() { }
    public ActivityDbM(ActivityCuDto org)
    {
        ActivityId = Guid.NewGuid();
        UpdateFromDTO(org);
    }

  

    
}
