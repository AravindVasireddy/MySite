using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class Medication
    {
        public Medication()
        {
            PatientMedication = new HashSet<PatientMedication>();
            TreatmentMedication = new HashSet<TreatmentMedication>();
        }

        public string Din { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int MedicationTypeId { get; set; }
        public string DispensingCode { get; set; }
        public double Concentration { get; set; }
        public string ConcentrationCode { get; set; }

        public virtual ConcentrationUnit ConcentrationCodeNavigation { get; set; }
        public virtual DispensingUnit DispensingCodeNavigation { get; set; }
        public virtual MedicationType MedicationType { get; set; }
        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
        public virtual ICollection<TreatmentMedication> TreatmentMedication { get; set; }
    }
}
