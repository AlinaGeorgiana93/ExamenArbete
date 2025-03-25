using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DTO
{
    public class PatientCuDto
    {
        public Guid? PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }

        public List<Guid> GraphsId { get; set; } = null;
        public List<Guid> ActivitiesId { get; set; } =null;
        public List<Guid> AppetitesId { get; set; } = null;
        public List<Guid> MoodsId { get; set; } = null;
        public List<Guid> SleepsId { get; set; } =null;

        public PatientCuDto() { }

        public PatientCuDto(IPatient org)
        {
            if (org != null)
            {
                PatientId = org.PatientId;
                FirstName = org.FirstName;
                LastName = org.LastName;
                PersonalNumber = org.PersonalNumber;

                GraphsId = org.Graphs?.Select(i => i.GraphId).ToList() ?? new List<Guid>();
                MoodsId = org.Moods?.Select(i => i.MoodId).ToList() ?? new List<Guid>();
                ActivitiesId = org.Activities?.Select(e => e.ActivityId).ToList() ?? new List<Guid>();
                AppetitesId = org.Appetites?.Select(e => e.AppetiteId).ToList() ?? new List<Guid>();
                SleepsId = org.Sleeps?.Select(e => e.SleepId).ToList() ?? new List<Guid>();
            }
        }
    }
}
