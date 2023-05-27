using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class DiagnosisCategory
    {
        public DiagnosisCategory()
        {
            Diagnosis = new HashSet<Diagnosis>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
    }
}
