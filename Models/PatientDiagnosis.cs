using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class PatientDiagnosis
    {
        public PatientDiagnosis()
        {
            PatientTreatment = new HashSet<PatientTreatment>();
        }

        public int PatientDiagnosisId { get; set; }
        public int PatientId { get; set; }
        public int DiagnosisId { get; set; }
        public string Comments { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<PatientTreatment> PatientTreatment { get; set; }
    }
}
