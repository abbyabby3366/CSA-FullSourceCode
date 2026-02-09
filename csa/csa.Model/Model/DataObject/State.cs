using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class StateDisplay
    {
        public StateDisplay(int stateId, string code, string name, int? countryId)
        {
            StateId = stateId;
            Code = code;
            Name = name;
            CountryId = countryId;
        }

        public int StateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }
}
