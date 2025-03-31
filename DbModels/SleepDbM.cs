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
    public override DateTime Date {get;set; }
    public override DayOfWeek Day { get; set; }
    public override string Notes { get; set; }


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

    public SleepDbM UpdateFromDTO(SleepCuDto org)
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
    public override ISleepLevel SleepLevel { get => SleepLevelDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    
    public SleepLevelDbM SleepLevelDbM { get; set; }
        

    public SleepDbM() { }
    public SleepDbM(SleepCuDto org)
    {
        SleepId = Guid.NewGuid();
        UpdateFromDTO(org);
    }




}