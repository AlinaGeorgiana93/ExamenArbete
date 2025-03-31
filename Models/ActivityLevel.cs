namespace Models;



public class ActivityLevel : IActivityLevel
{
    public virtual Guid ActivityLevelId { get; set; }     // Primary Key
    public virtual string Name { get; set; }      // e.g., "Hig", "Medel", Low.
    public virtual int Rating { get; set; }       // Rating from 1 to 10
    public virtual List<Activity> Activity { get; set; }






}