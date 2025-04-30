using System;
using System.Collections.Generic;

namespace Models
{
    public interface IGraph
    {
      public Guid GraphId { get; set; }
      public DateTime Date { get; set; }

      public IPatient Patient { get; set; }  // Patient linked to this Graph

        
    }
}
