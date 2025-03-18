using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Appetite: IAppetite
    {
        
        public virtual Guid AppetiteId { get; set; }
        public virtual AppetiteLevel AppetiteLevel { get; set; } // e.g., Low, Medium, High
        public virtual DateTime Date { get; set; }
        public virtual DayOfWeek Day { get; set; }
        public virtual string Notes { get; set; } // Additional notes about the appetite

        // Navigation property to Patient
          public virtual IPatient Patient { get; set; }
          public virtual IGraph Graph { get; set; }
    }
}