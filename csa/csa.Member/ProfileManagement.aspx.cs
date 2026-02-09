using csa.DataLogic;
using csa.DataLogic.Library;
using csa.Member.Helpers;
using csa.Model.DataObject;
using csa.Model.ViewModel;
using CsaModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Member
{
    public partial class ProfileManagement : BaseMemberPage
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
            var member = MemberBiz.Get(SessionManager.CurrentLoginMember.MemberId);
            var fileIc = FileBiz.Get(member.ICFileId);
            var fileProfile = FileBiz.Get(member.ProfileFileId);
            var filePayslip = FileBiz.Get(member.PayslipFileId);
            //FileDisplay fdIc = null;
            FileDisplay fdProfile = null;
            //FileDisplay fdPayslip = null;
            //if(fileIc != null)
            //{
            //    fdIc = fileIc.ToDisplay();
            //    fdIc.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.IcFileDir));
            //}

            if (fileProfile != null)
            {
                fdProfile = fileProfile.ToDisplay();
                fdProfile.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.ProfileFileDir));
            }
            //if (filePayslip != null)
            //{
            //    fdPayslip = filePayslip.ToDisplay();
            //    fdPayslip.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.MemberDir));
            //}

            var profileManagement = new ProfileManagementViewModel(profileManagementModel: new ProfileManagementModel(new MemberPersonalDetails(member.MemberId,member.FirstName,member.LastName,member.Email,member.PhoneNumber, BuildFile(fileIc), fdProfile, BuildFile(filePayslip)),new BankDetails(member.BankId,member.BankAccountName,member.BankAccountNumber,member.BankOther)), bankDropdown: BankBiz.GetActiveDropdown());
            hfModelView.Value = JsonConvert.SerializeObject(profileManagement);
        }

        FileUploadedBy<ValueText<string>> BuildFile(File file)
        {
            if (file == null) return null;
            return new FileUploadedBy<ValueText<string>>(new ValueText<string>(file?.FileId + file?.Extension, file?.Filename), "", null);
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