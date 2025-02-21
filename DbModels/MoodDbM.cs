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


    [NotMapped]
    public override List<IAnimal> Animals { get => AnimalsDbM?.ToList<IAnimal>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<AnimalDbM> AnimalsDbM { get; set; }


    [NotMapped]
    public override List<IEmployee> Employees { get => EmployeesDbM?.ToList<IEmployee>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<EmployeeDbM> EmployeesDbM { get; set; }


    public override MoodDbM Seed (SeedGenerator _seeder)
    {
        base.Seed (_seeder);
        return this;
    }

    public MoodDbM UpdateFromDTO(MoodCuDto org)
    {
        if (org == null) return null;

        City = org.City;
        Country = org.Country;
        Name = org.Name;

        return this;
    }

    public MoodDbM() { }
    public MoodDbM(MoodCuDto org)
    {
        MoodId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}