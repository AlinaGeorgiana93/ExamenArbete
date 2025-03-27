using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;

namespace DbModels;

[Table("ActivityLevels", Schema = "supusr")]
public class ActivityLevelDbM : ActivityLevel
{
    [Key]
    public override Guid ActivityLevelId { get; set; }


    public override IActivityLevel UpdateFromDTO(ActivityLevelCuDto org)
    {
        if (org == null) return null;

        Name = org.Name;
        Rating = org.Rating;

        return this;
    }  
  


    //public Guid ActivityDbMActivityId { get; set; }

    [NotMapped]
    public override IActivity Activity
    {
        get => ActivityDbM;
        set => throw new NotImplementedException();
    }
    

    [JsonIgnore]
    [Required]
    public ActivityDbM ActivityDbM { get; set; }




    public ActivityLevelDbM() { }

    public ActivityLevelDbM(ActivityLevelCuDto org)
    {
        ActivityLevelId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}

