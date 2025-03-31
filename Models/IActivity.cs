namespace Models;

public interface IActivity
{
    public Guid ActivityId { get; set; }
    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite

    //Navigation properties
    public IPatient Patient { get; set; }
    //public IGraph Graph { get; set; }
    public IActivityLevel ActivityLevel { get; set; }



}