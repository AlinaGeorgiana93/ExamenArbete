using System;
using System.Collections.Generic; // Required for List

namespace Models.DTO
{
    // Main summary information for guest user access
    public class GstUsrInfoDbDto
    {
        public string Title { get; set; } // Title for the section (e.g., "Guest Access Overview")
        
        // Count of unseeded data (no distinction of seeded/unseeded, just unseeded counts)
        public int NrMoods { get; set; } = 0; 
        public int NrStaffs { get; set; } = 0; 
        public int NrActivities { get; set; } = 0; 
        public int NrAppetites { get; set; } = 0; 
        public int NrGraphs { get; set; } = 0; 
        public int NrPatients { get; set; } = 0; 
        public int NrSleeps { get; set; } = 0; 


    }

    // Represents mood information for the guest user
    public class GstUsrInfoMoodsDto
    {
        public string MoodKind { get; set; } = null; // Type of mood (e.g., happy, sad, etc.)
        public int NrMoods { get; set; } = 0; // Number of moods of this kind available to the guest
    }

    // Represents staff details for the guest user (basic info like name and personal number)
    public class GstUsrInfoStaffsDto
    {
        public string FirstName { get; set; } = null; // Staff first name
        public string LastName { get; set; } = null; // Staff last name
        public int NrPatients { get; set; }    // Number of patients assigned to the staff
     
    }

    // Represents the number of activities for the guest user
    public class GstUsrInfoActivitiesDto
    {
        public int NrActivities { get; set; } = 0; // Number of activities available to the guest
    }

    // Combines all information for the guest user
    public class GstUsrInfoAllDto
    {
        public GstUsrInfoDbDto Db { get; set; } = null; // General database info for guest
        public List<GstUsrInfoActivitiesDto> Activities { get; set; } = new List<GstUsrInfoActivitiesDto>(); // List of activities info
        public List<GstUsrInfoStaffsDto> Staffs { get; set; } = new List<GstUsrInfoStaffsDto>(); // List of staff info
        public List<GstUsrInfoMoodsDto> Moods { get; set; } = new List<GstUsrInfoMoodsDto>(); // List of moods info
    }
}
