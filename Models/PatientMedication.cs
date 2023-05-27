using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class PatientMedication
    {
        public int PatientMedicationId { get; set; }
        public int PatientTreatmentId { get; set; }
        public string Din { get; set; }
        public double? Dose { get; set; }
        public int? Frequency { get; set; }
        public string FrequencyPeriod { get; set; }
        public string ExactMinMax { get; set; }
        public string Comments { get; set; }

        public virtual Medication DinNavigation { get; set; }
        public virtual PatientTreatment PatientTreatment { get; set; }
    }
}
