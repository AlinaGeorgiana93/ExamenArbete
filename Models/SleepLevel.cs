using Configuration;
using Microsoft.Identity.Client;
using Models;


namespace Models;

public class SleepLevel: ISleepLevel
{
    public virtual Guid SleepLevelId { get; set; }    
    public virtual string Name { get; set; }    
    public virtual string Label { get; set; }     
    public virtual int Rating { get; set; }       
 

    public virtual List <ISleep> Sleeps { get; set; }
}