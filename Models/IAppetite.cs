using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum AppetiteLevel {Low = 1, Medium = 2, High = 3}

    public interface IAppetite
    {
        
        public Guid AppetiteId { get; set; }
        public AppetiteLevel AppetiteLevel { get; set; } // e.g., Low, Medium, High
        public DateTime Date { get; set; }
        public DayOfWeek Day { get; set; }
        public string Notes { get; set; } // Additional notes about the appetite

    //Navigation properties
    //public IPatient Patient { get; set; }
    }
}