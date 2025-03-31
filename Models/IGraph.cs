using System;
using System.Collections.Generic;

namespace Models
{
    public interface IGraph
    {
        Guid GraphId { get; set; }
        DateTime Date { get; set; }

        public IPatient Patient { get; set; }  // Patient linked to this Graph

        
    }
}
