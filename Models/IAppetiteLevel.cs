namespace Models;

public interface IAppetiteLevel {

 public Guid AppetiteLevelId { get; set; }           // Primary Key
public string Name { get; set; }      //Very Hungry	; Hungry ; Normal Appetite ; Slightly Full ; Full ; Overeaten
public string Label { get; set; }     // Label Very Hungry	🍽️	10; Hungry 😋 	8-9; Normal Appetite 🙂 6-7; Slightly Full 🤔	4-5; Full 😌 2-3; Overeaten 🤢 1
public int Rating { get; set; }       // Rating from 1 to 10


  public IAppetite Appetite { get; set; }



}