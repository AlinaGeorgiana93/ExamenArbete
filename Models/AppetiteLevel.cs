using Configuration;
using Microsoft.Identity.Client;
using Models;
//using Seido.Utilities.SeedGenerator;



public class AppetiteLevel: IAppetiteLevel
{
    public virtual Guid AppetiteLevelId { get; set; }           // Primary Key
    public virtual string Name { get; set; }      //Very Hungry	; Hungry ; Normal Appetite ; Slightly Full ; Full ; Overeaten
    public virtual string Label { get; set; }     // Label Very Hungry	🍽️	10; Hungry 😋 	8-9; Normal Appetite 🙂 6-7; Slightly Full 🤔	4-5; Full 😌 2-3; Overeaten 🤢 1
    public virtual int Rating { get; set; }       // Rating from 1 to 10
 
    public virtual Guid? AppetiteId { get; set; } //FK key -nullable

    public virtual List <IAppetite> Appetite { get; set; }
}