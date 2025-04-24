using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class SleepLevelCuDto
{
    public virtual Guid? SleepLevelId { get; set; }

    public virtual string Name { get; set; }
    public virtual string Label { get; set; }
    public virtual int Rating { get; set; }



    public SleepLevelCuDto() { }
    public SleepLevelCuDto(ISleepLevel org)
    {

        SleepLevelId = org.SleepLevelId;
        Name = org.Name;
        Rating = org.Rating;
    }

}