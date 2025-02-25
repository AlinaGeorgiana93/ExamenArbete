using System;
using Configuration;

namespace Models.DTO;

public class GstUsrInfoDbDto
{
    public string Title {get;  set;}
    public int NrSeededMoods { get; set; } = 0;
    public int NrUnseededMoods { get; set; } = 0;

    public int NrSeededStaffs { get; set; } = 0;
    public int NrUnseededStaffs{ get; set; } = 0;

    public int NrSeededActivities { get; set; } = 0;
    public int NrUnseededActivities{ get; set; } = 0;

    public int NrSeededCreditCards { get; set; } = 0;
    public int NrUnseededCreditCards { get; set; } = 0;
}

public class GstUsrInfoMoodDto
{
    public string MoodKind { get; set; } = null;
    public int NrMoods { get; set; } = 0;
   
}

public class GstUsrInfoStaffsDto
{
    public string FirstName { get; set; } = null;
    public string LastName { get; set; } = null;
    public string PersonalNumber { get; set; } = null;
    public int NrAnimals { get; set; } = 0;
}

public class GstUsrInfoActivitiesDto
{
 
    public int NrActivities { get; set; } = 0;
}

public class GstUsrInfoAllDto
{
    public GstUsrInfoDbDto Db { get; set; } = null;
    public List<GstUsrInfoActivitiesDto> Activities { get; set; } = null;
    public List<GstUsrInfoStaffsDto> Staffs { get; set; } = null;
   // public List<GstUsrInfoMoodsDto> Moods{ get; set; } = null;
}


