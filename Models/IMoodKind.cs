namespace Models;

public interface IMoodKind
{
    public Guid MoodKindId { get; set; }
    public string Name  { get; set; } // e.g., Low, Medium, High
    public string Label { get; set; }
    public int Rating { get; set; }

    //Navigation properties
    public IMood Mood { get; set; }
}