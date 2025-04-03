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
    public override string Name { get; set; }

    public override string Label { get; set; }
    public override int Rating { get; set; }



    public ActivityLevelDbM UpdateFromDTO(ActivityLevelCuDto org)
    {
        if (org == null) return null;

        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;

        return this;
    }



    [NotMapped]
    public override List<Activity> Activities { get => ActivitiesDbM?.Cast<Activity>().ToList(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<ActivityDbM> ActivitiesDbM { get; set; }



    public static new List<ActivityLevelDbM> GetSeedActivityLevelsData()
    {
        return [.. ActivityLevel.GetSeedActivityLevelsData()
            .Select(a => new ActivityLevelDbM
            {
                ActivityLevelId = a.ActivityLevelId,
                Name = a.Name,
                Label = a.Label,
                Rating = a.Rating,

            })];
    }




    public ActivityLevelDbM() { }

    public ActivityLevelDbM(ActivityLevelCuDto org)
    {
        ActivityLevelId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}



