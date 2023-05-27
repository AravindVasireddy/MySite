using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AVPatients.Models
{
    public partial class Province
    {
        public Province()
        {
            Patient = new HashSet<Patient>();
        }

        public string ProvinceCode { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string SalesTaxCode { get; set; }
        public double SalesTax { get; set; }
        public bool IncludesFederalTax { get; set; }
        public string FirstPostalLetter { get; set; }

        public virtual Country CountryCodeNavigation { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
