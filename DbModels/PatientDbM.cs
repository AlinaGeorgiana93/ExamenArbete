// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using Newtonsoft.Json;

// using Models;
// using Seido.Utilities.SeedGenerator;
// using Models.DTO;

// namespace DbModels;
// [Table("Patients", Schema = "supusr")]
// public class PatientDbM : Patient ,ISeed<PatientDbM>
// {
//     [Key]
//     public override Guid PatientId { get; set; }

//     public virtual string strCategory
//     {
//         get=>Category.ToString();
//         set{}
//     }

//      public virtual string strAttractionName
//     {
//         get=>PatientName.ToString();
//         set{}
//     }
//      [NotMapped]
//     public override IMood Mood { get => AddressDbM; set => throw new NotImplementedException(); }

//     [JsonIgnore]
//     [Required]
//     public MoodDbM AddressDbM { get; set; }
    

//     [NotMapped]
//     public override List<IActivity> Activitities { get => ActivititiesDbM?.ToList<IReview>(); set => throw new NotImplementedException(); }

//     [JsonIgnore]
//     public List<ReviewDbM> ReviewsDbM { get; set; }
//     public override AttractionDbM Seed (SeedGenerator _seeder)
//     {
//         base.Seed (_seeder);
//         return this;
//     }

//     [NotMapped]
//     public override List<IEmployee> Employees { get => EmployeesDbM?.ToList<IEmployee>(); set => throw new NotImplementedException(); }

//     [JsonIgnore]
//     public List<EmployeeDbM> EmployeesDbM { get; set; }

//     public AttractionDbM UpdateFromDTO(AttractionCuDto org)
//     {
//         if (org == null) return null;
//         AttractionName=org.AttractionName;
//         Category = org.Category;
//         Description= org.Description;



//         return this;
//     }

//     public AttractionDbM() { }
//     public AttractionDbM(AttractionCuDto org)
//     {
//         AttractionId = Guid.NewGuid();
//         UpdateFromDTO(org);
//     }
// }
