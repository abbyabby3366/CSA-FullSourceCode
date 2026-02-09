using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class MemberDetailsByAdminViewModel
    {
        public MemberDetailsByAdminViewModel(MemberDetailsByAdmin member, List<DropdownItem> banks, List<CountryDisplay> countries, List<StateDisplay> states, string referralLink)
        {
            Member = member;
            Banks = banks;
            Countries = countries;
            States = states;
            ReferralLink = referralLink;
        }

        public MemberDetailsByAdmin Member { get; set; }
        public List<DropdownItem> Banks { get; set; }
        public List<CountryDisplay> Countries { get; set; }
        public List<StateDisplay> States { get; set; }
        public string ReferralLink { get; set; }
    }
}
