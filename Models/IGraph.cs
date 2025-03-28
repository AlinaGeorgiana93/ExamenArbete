using System;
using System.Collections.Generic;

namespace Models
{
    public interface IGraph
    {
       public Guid GraphId { get; set; }
      public DateTime Date { get; set; }

        public IPatient Patient { get; set; }  // Patient linked to this Graph

        // public List<IActivity> Activities { get; set; }
        // public List<IAppetite> Appetites { get; set; }
        // public List<IMood> Moods { get; set; }
        // public List<ISleep> Sleeps { get; set; }
    }
}
