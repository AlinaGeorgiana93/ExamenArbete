using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;


namespace DbModels;
[Table("MoodKinds", Schema = "supusr")]
public class MoodKindDbM : MoodKind
{
    [Key]
    public override Guid MoodKindId { get; set; }
    public override string Name {get;set; }
    public override string Label { get; set; }
    public override int Rating { get; set; }
   // public override Guid MoodId { get; set; }


      public MoodKindDbM UpdateFromDTO(MoodKindCuDto org)
    {
          if (org == null) return null;

        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;

        return this;

    }
[NotMapped]
public override IMood Mood { get => MoodDbM; set => throw new NotImplementedException(); }

[JsonIgnore]
[Required]
public MoodDbM MoodDbM { get; set; }  // This is a real entity




    public MoodKindDbM() { }
    public MoodKindDbM(MoodKindCuDto org)
    {
        MoodKindId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    
}

