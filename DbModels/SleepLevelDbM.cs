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
    public override string Name {get;set; }
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
    public override List<ISleep> Sleep { get => new List<ISleep> { SleepDbM }; set => throw new NotImplementedException(); }
    [JsonIgnore]
    public SleepDbM SleepDbM { get; set; }  

    
    public SleepLevelDbM() { }
    public SleepLevelDbM(SleepLevelCuDto org)
    {
        SleepLevelId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
  
}