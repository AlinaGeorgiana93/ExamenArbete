using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

[Table("Staffs", Schema = "supusr")]
public class StaffDbM : Staff, ISeed<StaffDbM>
{
    [Key]
    //public override Guid StaffId { get; set; }


    [NotMapped]
   // public override List<IMood> Moods { get => MoodsDbM?.ToList<IMood>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<MoodDbM> MoodsDbM { get; set; }

    [NotMapped]
    //public override List<IActivity> Activities { get => ActivitiesDbM?.ToList<IActivity>(); set => throw new NotImplementedException(); }

    [JsonIgnore]
    public List<ActivityDbM> ActivityDbM { get; set; }
    public bool Seeded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    // public override StaffDbM Seed (SeedGenerator _seeder)
    // {
    //     base.Seed (_seeder);
    //     return this;
    // }

    public StaffDbM UpdateFromDTO(StaffCuDto org)
    {
        if (org == null) return null;

        FirstName = org.FirstName;
        LastName = org.LastName;
        PersonalNumber = org.PersonalNumber;

        return this;
    }

    public StaffDbM Seed(SeedGenerator seedGenerator)
    {
        throw new NotImplementedException();
    }

    public StaffDbM() { }
    public StaffDbM(StaffCuDto org)
    {
        StaffId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
}