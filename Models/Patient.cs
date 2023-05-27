using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class Patient
    {
        public Patient()
        {
            PatientDiagnosis = new HashSet<PatientDiagnosis>();
        }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string Ohip { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Deceased { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string HomePhone { get; set; }
        public string Gender { get; set; }

        public virtual Province ProvinceCodeNavigation { get; set; }
        public virtual ICollection<PatientDiagnosis> PatientDiagnosis { get; set; }
    }
}
