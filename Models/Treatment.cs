using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class Treatment
    {
        public Treatment()
        {
            PatientTreatment = new HashSet<PatientTreatment>();
            TreatmentMedication = new HashSet<TreatmentMedication>();
        }

        public int TreatmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DiagnosisId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }
        public virtual ICollection<PatientTreatment> PatientTreatment { get; set; }
        public virtual ICollection<TreatmentMedication> TreatmentMedication { get; set; }
    }
}
