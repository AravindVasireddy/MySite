using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class PatientTreatment
    {
        public PatientTreatment()
        {
            PatientMedication = new HashSet<PatientMedication>();
        }

        public int PatientTreatmentId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime DatePrescribed { get; set; }
        public string Comments { get; set; }
        public int PatientDiagnosisId { get; set; }

        public virtual PatientDiagnosis PatientDiagnosis { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
    }
}
