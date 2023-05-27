using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class Diagnosis
    {
        public Diagnosis()
        {
            PatientDiagnosis = new HashSet<PatientDiagnosis>();
            Treatment = new HashSet<Treatment>();
        }

        public int DiagnosisId { get; set; }
        public string Name { get; set; }
        public int DiagnosisCategoryId { get; set; }

        public virtual DiagnosisCategory DiagnosisCategory { get; set; }
        public virtual ICollection<PatientDiagnosis> PatientDiagnosis { get; set; }
        public virtual ICollection<Treatment> Treatment { get; set; }
    }
}
