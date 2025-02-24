using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum AppetiteLevel {Low, Medium, High};
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