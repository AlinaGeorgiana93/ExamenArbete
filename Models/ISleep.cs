namespace Models;

public enum SleepLevel {Low, Medium, High};

public interface ISleep
{
    public Guid SleepId { get; set; }
    public SleepLevel SleepLevel { get; set; }

    public DateTime Date { get; set; }

    public DayOfWeek Day { get; set; }
    
    public string Notes { get; set; } 

   
    
    

    //Navigation properties
    //public virtual List<IPatient> Patients{ get; set; }
}