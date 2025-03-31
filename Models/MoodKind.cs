namespace Models;



public class MoodKind : IMoodKind
{
    public virtual Guid MoodKindId { get; set; }     // Primary Key
    public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
    public virtual int Rating { get; set; }   // Rating from 1 to 10
    

    
    public virtual List <IMood> Moods{ get; set; }







}