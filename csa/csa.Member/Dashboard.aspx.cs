using csa.DataLogic;
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
    public partial class Dashboard : BaseMemberPage
    {
        protected override void InitializeCulture()
        {
            if (!string.IsNullOrEmpty(Request["lang"]))
            {
                switch (Request.Params["lang"])
                {
                    case "en":
                        HttpContext.Current.Session["Language"] = "en";
                        break;
                    case "zh":
                    case "cn":
                        HttpContext.Current.Session["Language"] = "zh";
                        break;
                    case "ms":
                        HttpContext.Current.Session["Language"] = "ms";
                        break;

                    default:
                        break;
                }
            }

            string culture = (HttpContext.Current.Session["Language"] == null) ? "en" : HttpContext.Current.Session["Language"].ToString();

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);

            base.InitializeCulture();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var member = MemberBiz.Get(CurrentLoginMember.MemberId);
            var vm = new MemberDashboardViewModel(member.WalletCash ?? 0,0,MemberBiz.CountReferral(CurrentLoginMember.MemberId), MemberBiz.CountReferralYesterday(CurrentLoginMember.MemberId,DateTime.Now),MemberBiz.GetDashboardReferralGVByMember(CurrentLoginMember.MemberId,5).ObjVal, HistoryBiz.GetGVByMember(CurrentLoginMember.MemberId,0,5).ObjVal, AnnouncementBiz.GetGVByMember(DateTime.Now).ObjVal, member.WalletSavings ?? 0);
            hfModelView.Value = JsonConvert.SerializeObject(vm);
        }
        //Events
        #region Events

        #endregion

        //Functions
        #region Functions

        #endregion

        //Properties
        #region Properties

        #endregion

        //Utilities

        #region Utilities
        protected string GetLangText(string key)
        {
            var globalresourcestring = (String)GetGlobalResourceObject("Lang", key);
            return globalresourcestring;
        }
        #endregion
    }
}