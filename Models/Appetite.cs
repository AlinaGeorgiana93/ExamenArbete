using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Appetite: IAppetite
    {
        
        public virtual Guid AppetiteId { get; set; }
        public AppetiteLevel AppetiteLevel { get; set; } // e.g., Low, Medium, High
        public DateTime Date { get; set; }
        public DayOfWeek Day { get; set; }
        public string Notes { get; set; } // Additional notes about the appetite

        // Navigation property to Patient
          public virtual IPatient Patient { get; set; }
          public virtual IGraph Graph { get; set; }
    }
}