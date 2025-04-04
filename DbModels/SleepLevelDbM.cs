using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;

namespace DbModels;

[Table("SleepLevels", Schema = "supusr")]
public class SleepLevelDbM : SleepLevel
{
    [Key]
    public override Guid SleepLevelId { get; set; }
    public override string Name { get; set; }
    public override string Label { get; set; }
    public override int Rating { get; set; }



    public SleepLevelDbM UpdateFromDTO(SleepLevelCuDto org)
    {
        if (org == null) return null;

        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;

        return this;
    }


    [NotMapped]
    public override List<ISleep> Sleeps { get => SleepsDbM?.ToList<ISleep>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<SleepDbM> SleepsDbM { get; set; }

   
  public static new List<SleepLevelDbM> GetSeedSleepLevelsData()
    {
        return [.. SleepLevel.GetSeedSleepLevelsData()
            .Select(a => new SleepLevelDbM
            {
                SleepLevelId = a.SleepLevelId,
                Name = a.Name,
                Label = a.Label,
                Rating = a.Rating
            })];
    }

    public SleepLevelDbM() { }

    public SleepLevelDbM(SleepLevelCuDto org)
    {
        SleepLevelId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}