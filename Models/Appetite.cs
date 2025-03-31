using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Appetite : IAppetite
    {

        public virtual Guid AppetiteId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DayOfWeek Day { get; set; }
        public virtual string Notes { get; set; } // Additional notes about the appetite


        public virtual Guid? PatientId { get; set; } //FK
    
        public virtual IPatient Patient { get; set; }
        public virtual IAppetiteLevel AppetiteLevel { get; set; }


    }
}