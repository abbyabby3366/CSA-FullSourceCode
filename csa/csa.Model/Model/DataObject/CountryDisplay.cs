using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class CountryDisplay
    {
        public CountryDisplay(int countryId, string name, string iso2, string currency, int? phoneCode, int? isActive)
        {
            CountryId = countryId;
            Name = name;
            Iso2 = iso2;
            Currency = currency;
            PhoneCode = phoneCode;
            IsActive = isActive;
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Iso2 { get; set; }
        public string Currency { get; set; }
        public int? PhoneCode { get; set; }
        public int? IsActive { get; set; }
    }
}
