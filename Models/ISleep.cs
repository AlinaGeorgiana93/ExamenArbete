namespace Models;
public interface ISleep
{
    public Guid SleepId { get; set; }
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public string Notes { get; set; }



    public IPatient Patient { get; set; }
    // public IGraph Graph { get; set; }
    public ISleepLevel SleepLevel { get; set; }

}