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

   [Required]
    public ActivityLevel Level { get; set; }

    [Required]
    
    public DateTime Date { get; set; }

    [Required]

    public DayOfWeek Day { get; set; }
    

  

    
}
