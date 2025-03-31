namespace Models;

public interface IMood
{
    public Guid MoodId { get; set; }

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }

    public string Notes { get; set; } // Additional notes about the appetite

    //Navigation properties
    public IPatient Patient { get; set; }
    public IGraph Graph { get; set; }

}