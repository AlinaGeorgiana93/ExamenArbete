using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("Appetites", Schema = "supusr")]
public class AppetiteDbM : Appetite // ISeed<AppetiteDbM>
{
    [Key]
    public override Guid AppetiteId { get; set; }




    #region adding more readability to an enum type in the database
    public virtual string strAppetiteLevel
    {
        get => AppetiteLevel.ToString();
        set { } 
    }
    
    public virtual string strDate
    {
        get => Date.ToString();
        set { } 
    }
     #endregion

   public AppetiteDbM UpdateFromDTO(AppetiteCuDto org)
    {
        if (org == null) return null;

        AppetiteLevel = org.AppetiteLevel;
        Date = org.Date;
        Day = org.Day;
        Notes = org.Notes;
       

        return this;
    }


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
 
 

    public AppetiteDbM() { }
    public AppetiteDbM(AppetiteCuDto org)
    {
        AppetiteId = Guid.NewGuid();
        UpdateFromDTO(org);
    }

  

    
}
