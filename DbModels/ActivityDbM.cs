using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("Activity", Schema = "supusr")]
public class ActivityDbM : Activity //ISeed<ActivityDbM>
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
//     public override IStaff Staffs { get => StaffDbM; set => throw new NotImplementedException(); }

//     [JsonIgnore]
//     [Required]
//     public  StaffDbM StaffDbM { get; set; }


    public ActivityDbM UpdateFromDTO(ActivityCuDto org)
    {
        if (org == null) return null;

       Level= org.Level;
      // Mood = org.Mood;

        return this;
    }

    public ActivityDbM() { }
    public ActivityDbM(ActivityCuDto org)
    {
        ActivityId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}
