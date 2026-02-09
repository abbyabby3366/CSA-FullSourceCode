using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class MemberSurvey1ViewModel
    {
        public MemberSurvey1ViewModel(string surveyInformation, List<DropdownItem> vtBanks, ResponseNewMemberSurvey sessionAccountSurvey, ValueText<long> referralMember, LoginMember loginMember, List<StateDisplay> states, List<DropdownItem> sectors, List<Dropdown3> jobPositions)
        {
            SurveyInformation = surveyInformation;
            VtBanks = vtBanks;
            SessionAccountSurvey = sessionAccountSurvey;
            ReferralMember = referralMember;
            LoginMember = loginMember;
            States = states;
            Sectors = sectors;
            JobPositions = jobPositions;
        }

        public string SurveyInformation { get; set; }
        public List<DropdownItem> VtBanks { get; set; }
        public ResponseNewMemberSurvey SessionAccountSurvey { get; set; }
        public ValueText<long> ReferralMember { get; set; }
        public LoginMember LoginMember { get; set; }
        public List<StateDisplay> States { get; set; }
        public List<DropdownItem> Sectors { get; set; }
        public List<Dropdown3> JobPositions { get; set; }
    }
}
