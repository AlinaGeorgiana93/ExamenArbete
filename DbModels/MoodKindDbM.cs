using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Models;
using Models.DTO;

namespace DbModels;

[Table("MoodKinds", Schema = "supusr")]
public class MoodKindDbM : MoodKind
{
    [Key]
    public override Guid MoodKindId { get; set; }
    public override string Name { get; set; }
    public override int Rating { get; set; }



    public IMoodKind UpdateFromDTO(MoodKindCuDto org)
    {
        if (org == null) return null;

        Name = org.Name;
        Rating = org.Rating;

        return this;
    }


    [NotMapped]
    public override List<IMood>Moods { get => MoodsDbM?.ToList<IMood>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<MoodDbM> MoodsDbM { get; set; }


public static new List<MoodKindDbM> GetSeedMoodKindData()
{
    return [..MoodKind.GetSeedMoodKindData()
        .Select(m => new MoodKindDbM
        {
            MoodKindId = m.MoodKindId,
            Name = m.Name,
            Rating = m.Rating
        })];
}


    public MoodKindDbM() { }

    public MoodKindDbM(MoodKindCuDto org)
    {
        MoodKindId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}