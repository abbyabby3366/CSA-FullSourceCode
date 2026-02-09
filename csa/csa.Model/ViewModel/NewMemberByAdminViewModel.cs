
using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class NewMemberByAdminViewModel
    {
        public NewMemberByAdminViewModel(List<DropdownItem> banks, List<CountryDisplay> countries, List<StateDisplay> states)
        {
            Banks = banks;
            Countries = countries;
            States = states;
        }

        public List<DropdownItem> Banks { get; set; }
        public List<CountryDisplay> Countries { get; set; }
        public List<StateDisplay> States { get; set; }
    }
}
