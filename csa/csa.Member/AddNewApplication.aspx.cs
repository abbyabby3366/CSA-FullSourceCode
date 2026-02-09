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
    public partial class AddNewApplication : BaseMemberPage
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
            if(!IsPostBack)
            {
                var member = MemberBiz.Get(SessionManager.CurrentLoginMember.MemberId);
                var sectors = SectorBiz.Gets();
                var jobPositions = JobPositionBiz.Gets();
                var banks = BankBiz.GetActiveDropdown();
                banks.Insert(0, new Model.DataObject.DropdownItem("0", "Select an option"));


                var fileIc = FileBiz.Get(member.ICFileId);
                var fileOfferLetter = FileBiz.Get(member.OfferLetterFileId);
                var filePayslip = FileBiz.Get(member.PayslipFileId);
                //FileDisplay fdIc = null;
                //FileDisplay fdPayslip = null;
                //FileDisplay fdOfferLetter = null;
                //if (fileIc != null)
                //{
                //    fdIc = fileIc.ToDisplay();
                //    fdIc.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.IcFileDir));
                //}

                //if (fileOfferLetter != null)
                //{
                //    fdOfferLetter = fileOfferLetter.ToDisplay();
                //    fdOfferLetter.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.MemberDir));
                //}
                //if (filePayslip != null)
                //{
                //    fdPayslip = filePayslip.ToDisplay();
                //    fdPayslip.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.MemberDir));
                //}

                var vm = new AddNewApplicationByMemberViewModel(member.ProgramEventId, member.FullName, member.Birthdate, member.GenderId, member.ICNumber, member.StreetAddress1, member.PhoneNumber, member.Email, member.TaxNumber, member.RaceId, member.ReligionId, member.HighestLevelOfEducationId, member.MaritalStatusId, "todo", new Model.DataObject.RequestNewSurveySpouse(member.SpouseFullName, member.SpouseIdentificationNumber, member.SpouseContactInformation, member.SpouseOccupation, member.SpouseCompanyAddress, member.SpouseSalary), new Model.DataObject.RequestNewSurveyFamily(member.NumberOfDependent, member.IsHaveOKU, member.FatherName, member.FatherContactNumber, member.FatherAddress, member.MotherName, member.MotherConcatNumber, member.MotherAddress), new Model.DataObject.RequestNewSurveyCompany(member.CompanyEmployerTypeId, member.CompanyName, member.CompanyJobTitle, member.CompanySectorId, member.CompanyDepartmentId, member.CompanyAddress, member.CompanyOfficeContactNumber, member.CompanyEmploymentStatusId, member.RetirementAge, member.CompanyYearOfService,member.CompanyEmployerTypeOther,member.CompanyEmploymentStatusOther,member.CompanySectorOther,member.CompanyDepartementOther,member.CompanyEmployerName), new Model.DataObject.RequestNewSurveyEmergency(member.EmergencyConcatPerson, member.EmergencyRelationId, member.EmergencyConcatNumber, member.EmergencyPersonICNumber, member.EmergencyPersonOccupation, member.EmergencyPersonAddress), new Model.DataObject.RequestNewSurveyBank(member.BankId, member.BankAccountNumber, member.Salary, member.SalaryRangeId, member.BankAccountName,member.BankOther), new Model.DataObject.RequestNewSurveyOther(member.OtherPreferredLanguage, member.OtherHobbies, member.OtherSocialMediaHandles, member.OtherFBName)
                    , sectors.Select(x => new DropdownItem(x.SectorId.ToString(), x.Name)).ToList(), jobPositions.Select(x => new Dropdown3(x.JobPositionId.ToString(), x.Name, x.SectorId.ToString())).ToList(),banks,
                    BuildFile(fileIc), BuildFile(fileOfferLetter), BuildFile(filePayslip));
                hfModelView.Value = JsonConvert.SerializeObject(vm);
            }
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