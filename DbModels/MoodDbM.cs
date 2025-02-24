using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Moods", Schema = "supusr")]
public class MoodDbM : Mood, ISeed<MoodDbM>
{
    [Key]
    public override Guid MoodId { get; set; }

    [Required]
    public virtual MoodKind Kind { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    
    public DayOfWeek Day { get; set; }
    

  
}