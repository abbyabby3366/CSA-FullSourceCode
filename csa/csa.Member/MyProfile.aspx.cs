using csa.DataLogic;
using csa.DataLogic.Library;
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
    public partial class MyProfile : BaseMemberPage
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
            var fileIc = FileBiz.Get(member.ICFileId);
            var fileProfile = FileBiz.Get(member.ProfileFileId);
            var filePayslip = FileBiz.Get(member.PayslipFileId);
            FileDisplay fdIc = null;
            FileDisplay fdProfile = null;
            FileDisplay fdPayslip = null;
            if (fileIc != null)
            {
                fdIc = fileIc.ToDisplay();
                fdIc.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.IcFileDir));
            }

            if (fileProfile != null)
            {
                fdProfile = fileProfile.ToDisplay();
                fdProfile.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.ProfileFileDir));
            }
             if (filePayslip != null)
            {
                fdPayslip = filePayslip.ToDisplay();
                fdPayslip.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.MemberDir));
            }

            string referralName = "";
            string referralFileNumber = "";
            if(member.ReferrerMemberId.HasValue)
            {
                var referrer = MemberBiz.Get(member.ReferrerMemberId.Value);
                referralName = referrer?.FullName;
                referralFileNumber = referrer?.FileNumber;
            }

            string bankName = "";
            if (member.BankId.HasValue) bankName = BankBiz.Get(member.BankId.Value)?.Name;
            MyProfileBankDetails bank = new MyProfileBankDetails(bankName, member.BankAccountName, member.BankAccountNumber);

            string referralLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(member.MemberId.ToString());

            var vm = new MyProfileViewModel(new MemberProfileDetails(member.MemberId,member.MemberCode,member.StatusId,member.FirstName,member.LastName,member.Email,member.PhoneNumber,member.ReferrerMemberId, referralName,member.ICNumber,member.Salary,member.GenderId,member.CompanyName,member.StreetAddress1,fdProfile,fdIc, fdPayslip, member.SalaryRangeId,member.FileNumber, referralFileNumber), bank,member.WalletCash ?? 0,MemberBiz.CountReferral(CurrentLoginMember.MemberId),
                referralLink);
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