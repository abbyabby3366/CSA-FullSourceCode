using csa.DataLogic;
using csa.Model.DataObject;
using csa.Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Member
{
    public partial class Survey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Prepare();
            }
        }

        private void Prepare()
        {
            if(Helpers.SessionManager.CurrentLoginMember != null)
            {
                //exist survey goto details
                if(SurveyBiz.ExistSurvey(Helpers.SessionManager.CurrentLoginMember.MemberId,(int)Library.SurveyType.YABAM,out long? surveyId))
                {
                    Response.StatusCode = 301;
                    Response.Redirect($"SurveyDetails?id={surveyId}", false);
                    Response.End();               
                }
                //set account from login
                Helpers.SessionManager.SetAccountMemberSurvey(new ResponseNewMemberSurvey(Helpers.SessionManager.CurrentLoginMember.FullName, Helpers.SessionManager.CurrentLoginMember.PhoneNumber, Helpers.SessionManager.CurrentLoginMember.MemberId));
            }

            var settingSurveyInformation = SettingBiz.Get("Survey1Information");
            var banks = BankBiz.GetActiveDropdown();
            banks.Insert(0, new Model.DataObject.DropdownItem("0", "Sila pilih jawapan anda"));

            long memberId = 0;
            ValueText<long> referralMember = null;
            if(Request.Params["ref"] != null)
            {
                string memberIdText = Library.SecurityLibrary.Decrypt(Request.Params["ref"]);
                if (long.TryParse(memberIdText, out memberId))
                {
                    var refMember = MemberBiz.Get(memberId);
                    if(refMember != null)
                    {
                        referralMember = new ValueText<long>(refMember.MemberId, refMember.FullName);
                    }
                }
            }

            var states = StateBiz.GetAllDisplay();
            states = states.Where(x => x.CountryId == 1).ToList();//malaysia

            var sectors = SectorBiz.Gets();
            var jobPositions = JobPositionBiz.Gets();

            var vm = new MemberSurvey1ViewModel(settingSurveyInformation.TextValue,banks, Helpers.SessionManager.AccountMemberSurvey, referralMember, Helpers.SessionManager.CurrentLoginMember, states, sectors.Select(x=> new DropdownItem(x.SectorId.ToString(),x.MalayName)).ToList(),jobPositions.Select(x=> new Dropdown3(x.JobPositionId.ToString(),x.MalayName,x.SectorId.ToString())).ToList());
            hfModelView.Value = JsonConvert.SerializeObject(vm);
        }
    }
}