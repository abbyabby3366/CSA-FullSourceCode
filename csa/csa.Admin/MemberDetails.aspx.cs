using csa.DataLogic;
using csa.DataLogic.Library;
using csa.Library;
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

namespace csa.Admin
{
    public partial class MemberDetails : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idText = Request.Params["id"];
                long id = 0;
                if (long.TryParse(idText, out id))
                {
                    LoadData(id);
                }
                else
                {
                    Response.Redirect("~/Members");
                }
            }
        }

        private void LoadData(long id)
        {
            var m = MemberBiz.Get(id);
            if(m == null) Response.Redirect("~/Members");

            ValueText<long> referrerMember = null;
            if(m.ReferrerMemberId.HasValue)
            {
                var referrer = MemberBiz.Get(m.ReferrerMemberId.Value);
                referrerMember = new ValueText<long>(referrer.MemberId, $"{referrer.FullName} ({referrer.PhoneNumber})");
            }
            var banks = BankBiz.GetActiveDropdown();
            var countries = CountryBiz.GetActiveDisplay();
            var states = StateBiz.GetAllDisplay();

            var fileIc = FileBiz.Get(m.ICFileId);
            var fileProfile = FileBiz.Get(m.ProfileFileId);
            var filePayslip = FileBiz.Get(m.PayslipFileId);
            var fileOfferLetter = FileBiz.Get(m.OfferLetterFileId);
            //FileDisplay fdIc = null;
            FileDisplay fdProfile = null;
            //FileDisplay fdPayslip = null;
            //FileDisplay fdOfferLetter = null;
            //if (fileIc != null)
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
            
            //if (fileOfferLetter != null)
            //{
            //    fdOfferLetter = fileOfferLetter.ToDisplay();
            //    fdOfferLetter.SetUploadPath(FileHelper.GetUploadFullPath(FileHelper.FileDir.MemberDir));
            //}

            var sectors = SectorBiz.Gets();
            var jobPositions = JobPositionBiz.Gets();

            string referralLink = Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(m.MemberId.ToString());

            var vm = new MemberDetailsByAdminViewModel(new Model.DataObject.MemberDetailsByAdmin(
                m.MemberId,m.StatusId,referrerMember,m.FirstName,m.LastName,m.StateId,m.CountryId,m.Occupation,m.CompanyTypeId, m.FileNumber,
                m.ProgramEventId, m.FullName, m.Birthdate, m.GenderId, m.ICNumber, m.StreetAddress1, m.PhoneNumber, m.Email, m.TaxNumber, m.RaceId, m.ReligionId, m.HighestLevelOfEducationId, m.MaritalStatusId, "todo", new Model.DataObject.RequestNewSurveySpouse(m.SpouseFullName, m.SpouseIdentificationNumber, m.SpouseContactInformation, m.SpouseOccupation, m.SpouseCompanyAddress, m.SpouseSalary), new Model.DataObject.RequestNewSurveyFamily(m.NumberOfDependent, m.IsHaveOKU, m.FatherName, m.FatherContactNumber, m.FatherAddress, m.MotherName, m.MotherConcatNumber, m.MotherAddress), new Model.DataObject.RequestNewSurveyCompany(m.CompanyEmployerTypeId, m.CompanyName, m.CompanyJobTitle, m.CompanySectorId, m.CompanyDepartmentId, m.CompanyAddress, m.CompanyOfficeContactNumber, m.CompanyEmploymentStatusId, m.RetirementAge, m.CompanyYearOfService,m.CompanyEmployerTypeOther,m.CompanyEmploymentStatusOther,m.CompanySectorOther,m.CompanyDepartementOther,m.CompanyEmployerName), new Model.DataObject.RequestNewSurveyEmergency(m.EmergencyConcatPerson, m.EmergencyRelationId, m.EmergencyConcatNumber, m.EmergencyPersonICNumber, m.EmergencyPersonOccupation, m.EmergencyPersonAddress), new Model.DataObject.RequestNewSurveyBank(m.BankId, m.BankAccountNumber, m.Salary, m.SalaryRangeId,m.BankAccountName,m.BankOther), new Model.DataObject.RequestNewSurveyOther(m.OtherPreferredLanguage, m.OtherHobbies, m.OtherSocialMediaHandles, m.OtherFBName)
                    , sectors.Select(x => new DropdownItem(x.SectorId.ToString(), x.Name)).ToList(), jobPositions.Select(x => new Dropdown3(x.JobPositionId.ToString(), x.Name, x.SectorId.ToString())).ToList(),banks,
                    BuildFile(fileIc), BuildFile(filePayslip), BuildFile(fileOfferLetter), fdProfile,m.AdminRemark,m.WalletCash.HasValue ? m.WalletCash.Value : 0,m.CreateDate,m.WalletSavings ?? 0),banks, countries, states,
                referralLink);

            banks.Insert(0, new Model.DataObject.DropdownItem("0", "Select an option"));
            hfModelView.Value = JsonConvert.SerializeObject(vm);

            var survey = SurveyBiz.Get(m.MemberId, (int)SurveyType.YABAM);
            if (survey != null)
            {
                SurveyYabamResult.SetEdit(survey);
                SurveyYabamResult.Visible = true;
            }
        }

        FileUploadedBy<ValueText<string>> BuildFile(File file)
        {
            if (file == null) return null;
            return new FileUploadedBy<ValueText<string>>(new ValueText<string>(file?.FileId + file?.Extension, file?.Filename), "", null);
        }
    }
}