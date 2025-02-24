using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Appetite: IAppetite
    {
        
        public Guid AppetiteId { get; set; }
        public string AppetiteLevel { get; set; } // e.g., Low, Medium, High
        public DateTime Date { get; set; }
        public DayOfWeek Day { get; set; }
        public string Notes { get; set; } // Additional notes about the appetite

        // Navigation property to Patient
          //public virtual IPatient Patient { get; set; }
    }
}