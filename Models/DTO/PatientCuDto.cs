using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.DTO;
public class PatientCuDto
{
    public Guid? PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }


    public PatientCuDto() { }
    public PatientCuDto(IPatient org)
    {
        PatientId = org.PatientId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        PersonalNumber = org.PersonalNumber;




    }
}