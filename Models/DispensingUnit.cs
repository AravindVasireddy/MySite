using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class DispensingUnit
    {
        public DispensingUnit()
        {
            Medication = new HashSet<Medication>();
        }

        public string DispensingCode { get; set; }

        public virtual ICollection<Medication> Medication { get; set; }
    }
}
