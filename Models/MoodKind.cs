using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class MoodKind: IMoodKind
{
    public virtual Guid MoodKindId { get; set; }
    public virtual string Name { get; set; } // e.g., Low, Medium, High
    public virtual  string Label { get; set; }
    public  virtual int Rating { get; set; }

   
   public virtual Guid MoodId { get; set; }  //FK
   public virtual IMood Mood { get; set; }

 


}