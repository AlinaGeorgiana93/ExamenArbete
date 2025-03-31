using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{

    public interface IAppetite
    {

        public Guid AppetiteId { get; set; }
        
        public DateTime Date { get; set; }
        public DayOfWeek Day { get; set; }
        public string Notes { get; set; } // Additional notes about the appetite

        //Navigation properties
        public IPatient Patient { get; set; }
        public IGraph Graph { get; set; }
        public IAppetiteLevel AppetiteLevel { get; set; } // e.g., Low, Medium, High
    }
}