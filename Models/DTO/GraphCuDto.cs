using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO;

public class GraphCuDto
{
    public virtual Guid? GraphId { get; set; }

    public DateTime Date { get; set; }
    public virtual Guid? PatientId { get; set; } // ✅ Store Patient as a GUID instead of `IPatient`
   

    public GraphCuDto() { }
    public GraphCuDto(IGraph org)
    {
        GraphId = org.GraphId;
        Date = org.Date;
        PatientId = org.Patient?.PatientId; // ✅ Assign PatientId correctly
    }
}