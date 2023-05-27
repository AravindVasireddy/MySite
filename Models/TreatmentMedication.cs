using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class TreatmentMedication
    {
        public int TreatmentId { get; set; }
        public string Din { get; set; }

        public virtual Medication DinNavigation { get; set; }
        public virtual Treatment Treatment { get; set; }
    }
}
