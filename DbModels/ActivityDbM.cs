using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("Acticity", Schema = "supusr")]
public class ActivityDbM : Activity, ISeed<ActivityDbM>
{
    [Key]
    public override Guid ActivityId { get; set; }

    #region adding more readability to an enum type in the database
    public virtual string strLevel
    {
        get => Level.ToString();
        set { }  //set is needed by EFC to include in the database, so I make it to do nothing
    }
   

    #endregion
    
 //   [NotMapped]
    //public override IZoo Zoo { get => ZooDbM; set => throw new NotImplementedException(); }

    //[JsonIgnore]
    //[Required]
    //public  ZooDbM ZooDbM { get; set; }


    public ActivityDbM UpdateFromDTO(ActivityCuDto org)
    {
        if (org == null) return null;

        Kind = org.Kind;
        Mood = org.Mood;
        Age = org.Age;
        Name = org.Name;
        Description = org.Description;

        return this;
    }

    public ActivityDbM() { }
    public ActivityDbM(AnimalCuDto org)
    {
        ActivityId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}
