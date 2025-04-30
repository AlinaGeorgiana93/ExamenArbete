using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
//using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Graphs", Schema = "supusr")]
public class GraphDbM : Graph
{
    [Key]
    public override Guid GraphId { get; set; }


    [NotMapped]
    public override IPatient Patient { get => PatientDbM; set => throw new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    public PatientDbM PatientDbM { get; set; }  // Graph now belongs to a Patient


       public GraphDbM UpdateFromDTO(GraphCuDto org)
    {
        if (org == null) return null;

        Date = org.Date;


        return this;
    }

    public GraphDbM() { }
    public GraphDbM(GraphCuDto org)
    {
        GraphId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}