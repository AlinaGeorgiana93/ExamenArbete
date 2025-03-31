using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
//using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("AppetiteLevels", Schema = "supusr")]
public class AppetiteLevelDbM : AppetiteLevel
{
    [Key]
    public override Guid AppetiteLevelId { get; set; }
    public override string Name {get;set; }
    public override string Label { get; set; }
    public override int Rating { get; set; }

   
   
    public AppetiteLevelDbM UpdateFromDTO(AppetiteLevelCuDto org)
    {
        if (org == null) return null;

        Name = org.Name;
        Label = org.Label;
        Rating = org.Rating;
       

        return this;
    }


    [NotMapped]
    public override List<IAppetite> Appetites { get => AppetitesDbM?.ToList<IAppetite>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public List<AppetiteDbM> AppetitesDbM { get; set; }


    
    public AppetiteLevelDbM() { }
    public AppetiteLevelDbM(AppetiteLevelCuDto org)
    {
        AppetiteLevelId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
  
}