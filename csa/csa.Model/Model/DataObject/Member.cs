using csa.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace csa.Model.DataObject
{
    public class RequestLoginMember : BaseReqDTO
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class RequestRegisterMember : BaseReqDTO
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class RequestForgotPasswordMember : BaseReqDTO
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class RequestChangePersonalDetails : BaseReqDTO
    {
        [JsonProperty("memberId")]
        public int MemberId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("icFile")]
        public HttpPostedFileBase IcFile { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
    }

    public class RequestChangeProfilePicture : BaseReqDTO
    {
        [JsonProperty("memberId")]
        public long MemberId { get; set; }
        [JsonProperty("imageFile")]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class RequestChangeBankDetails : BaseReqDTO
    {
        [JsonProperty("memberId")]
        public int MemberId { get; set; }
        [JsonProperty("bankId")]
        public int? BankId { get; set; }
        [JsonProperty("bankAccountName")]
        public string BankAccountName { get; set; }
        [JsonProperty("bankAccountNumber")]
        public string BankAccountNumber { get; set; }
        [JsonProperty("bankOther")]
        public string BankOther { get; set; }
    }

    public class RequestChangePassword : BaseReqDTO
    {
        [JsonProperty("memberId")]
        public int MemberId { get; set; }
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
        [JsonProperty("confirmNewPassword")]
        public string ConfirmNewPassword { get; set; }
    }

    public class RequestChangePasswordByAdmin 
    {
        public int AdminId { get; set; }
        public long MemberId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }

    public class LoginMember
    {
        public LoginMember(long memberId, string memberCode, string firstName, string lastName, string phoneNumber, string profileFileId)
        {
            MemberId = memberId;
            MemberCode = memberCode;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            ProfileFileId = profileFileId;
        }

        public long MemberId { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string PhoneNumber { get; set; }
        public string ProfileFileId { get; set; }
    }

    public class BankDetails
    {
        public BankDetails()
        {

        }
        public BankDetails(int? bankId, string bankAccountName, string bankAccountNumber, string bankOther)
        {
            BankId = bankId;
            BankAccountName = bankAccountName;
            BankAccountNumber = bankAccountNumber;
            BankOther = bankOther;
        }

        public int? BankId { get; set; }
        public string BankOther { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class MyProfileBankDetails
    {
        public MyProfileBankDetails(string bankName, string bankAccountName, string bankAccountNumber)
        {
            BankName = bankName;
            BankAccountName = bankAccountName;
            BankAccountNumber = bankAccountNumber;
        }

        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class MemberPersonalDetails
    {
        public MemberPersonalDetails(long memberId, string firstName, string lastName, string email, string phoneNumber, FileUploadedBy<ValueText<string>> icFile, FileDisplay profileFile, FileUploadedBy<ValueText<string>> payslipFile)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            ICFile = icFile;
            ProfileFile = profileFile;
            PayslipFile = payslipFile;
        }

        public long MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public FileUploadedBy<ValueText<string>> ICFile { get; set; }
        public FileDisplay ProfileFile { get; set; }
        public FileUploadedBy<ValueText<string>> PayslipFile { get; set; }
        
    }

    public class MemberProfileDetails
    {
        public MemberProfileDetails(long memberId, string memberCode, int statusId, string firstName, string lastName, string email, string phoneNumber, long? referralId, string referralName, string iCNumber, decimal? salary, int? genderId, string companyName, string streetAddress1, FileDisplay profileFile, FileDisplay iCFile, FileDisplay payslipFile, int? salaryRangeId, string fileNumber, string referralFileNumber)
        {
            MemberId = memberId;
            MemberCode = memberCode;
            StatusId = statusId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            ReferralId = referralId;
            ReferralName = referralName;
            ICNumber = iCNumber;
            Salary = salary;
            GenderId = genderId;
            CompanyName = companyName;
            StreetAddress1 = streetAddress1;
            ProfileFile = profileFile;
            ICFile = iCFile;
            PayslipFile = payslipFile;
            SalaryRangeId = salaryRangeId;
            FileNumber = fileNumber;
            ReferralFileNumber = referralFileNumber;
        }

        public long MemberId { get; set; }
        public string MemberCode { get; set; }
        public int StatusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? ReferralId { get; set; }
        public string ReferralName { get; set; }
        public string ICNumber { get; set; }
        public decimal? Salary { get; set; }
        public int? GenderId { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress1 { get; set; }
        public FileDisplay ProfileFile { get; set; }
        public FileDisplay ICFile { get; set; }
        public FileDisplay PayslipFile { get; set; }
        public int? SalaryRangeId { get; set; }
        public string FileNumber { get; set; }
        public string ReferralFileNumber { get; set; }
        public string SalaryRange
        {
            get
            {
                try
                {                    
                    return EnumHelper.GetDescription((SalaryRange)Enum.ToObject(typeof(SalaryRange), SalaryRangeId));
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public string FullName => $"{FirstName} {LastName}";
        public string Gender
        {
            get
            {
                if(GenderId.HasValue)
                {
                    return Enum.GetName(typeof(Gender), GenderId);
                }

                return "";
            }
        }


        public string Status
        {
            get
            {
                try
                {
                    return Enum.GetName(typeof(MemberStatus), StatusId);
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }

    }

    public class ProfileManagementModel
    {
        public ProfileManagementModel(MemberPersonalDetails personalDetails, BankDetails bankDetails)
        {
            PersonalDetails = personalDetails;
            BankDetails = bankDetails;
        }

        public MemberPersonalDetails PersonalDetails { get; set; }
        public BankDetails BankDetails { get; set; }
    }

    public class ReferralGVByMember
    {
        public long MemberId { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime CreateDate { get; set; }
        public int? ReferralTypeId { get; set; }
        public string ReferralType
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription((AgentType)Enum.ToObject(typeof(AgentType), ReferralTypeId));
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public int StatusId { get; set; }
        public string Status
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription((AgentStatus)Enum.ToObject(typeof(AgentStatus), StatusId));
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public decimal? ReferralAmount { get; set; }
        public string Remark { get; set; }
        public int SequenceId { get; set; }
    }

    public class DashboardReferralGVByMember
    {
        public long MemberId { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string PhoneNumber { get; set; }
        public int StatusId { get; set; }
        public string Status
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription((AgentStatus)Enum.ToObject(typeof(AgentStatus), StatusId));
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public int SequenceId { get; set; }
    }

    public class RequestAgentByMember
    {
        public int CreatorMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ICNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
    }

    public class RequestNewMemberByAdmin
    {
        public string MemberCode { get; set; }
        public int StatusId { get; set; }
        public int? ReferrerMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
        public int? GenderId { get; set; }
        public string Address { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string CompanyName { get; set; }
        public string Occupation { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public BankDetails Bank { get; set; }
        public HttpPostedFileBase IcFile { get; set; }
        public HttpPostedFileBase ProfileFile { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
        public string ICNumber { get; set; }
        public int? CompanySectorId { get; set; }
        public int? CompanyEmploymentStatusId { get; set; }
        public int? RetirementAge { get; set; }
        public int? YearOfService { get; set; }
        public int? CompanyTypeId { get; set; }
        public int? SalaryRangeId { get; set; }
    }

    public class MemberDetailsByAdmin
    {
        public MemberDetailsByAdmin(long memberId, int statusId, ValueText<long> referrerMember, string firstName, string lastName, int? stateId, int? countryId, string occupation, int companyTypeId, string fileNumber, int? programEventId, string fullName, DateTime? dateOfBirth, int? gender, string iCNumber, string address, string phoneNumber, string email, string taxNumber, int? raceId, int? religionId, int? highestLevelOfEducationId, int? maritalStatusId, string referralName, RequestNewSurveySpouse spouse, RequestNewSurveyFamily family, RequestNewSurveyCompany company, RequestNewSurveyEmergency emergency, RequestNewSurveyBank bank, RequestNewSurveyOther other, List<DropdownItem> sectors, List<Dropdown3> jobPositions, List<DropdownItem> banks, FileUploadedBy<ValueText<string>> iCFile, FileUploadedBy<ValueText<string>> payslipFile, FileUploadedBy<ValueText<string>> offerLetterFile, FileDisplay profileFile, string adminRemark, decimal walletCash, DateTime createDate, decimal walletSavings)
        {
            MemberId = memberId;
            StatusId = statusId;
            ReferrerMember = referrerMember;
            FirstName = firstName;
            LastName = lastName;
            StateId = stateId;
            CountryId = countryId;
            Occupation = occupation;
            CompanyTypeId = companyTypeId;
            FileNumber = fileNumber;
            ProgramEventId = programEventId;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ICNumber = iCNumber;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            TaxNumber = taxNumber;
            RaceId = raceId;
            ReligionId = religionId;
            HighestLevelOfEducationId = highestLevelOfEducationId;
            MaritalStatusId = maritalStatusId;
            ReferralName = referralName;
            Spouse = spouse;
            Family = family;
            Company = company;
            Emergency = emergency;
            Bank = bank;
            Other = other;
            Sectors = sectors;
            JobPositions = jobPositions;
            Banks = banks;
            ICFile = iCFile;
            PayslipFile = payslipFile;
            OfferLetterFile = offerLetterFile;
            ProfileFile = profileFile;
            AdminRemark = adminRemark;
            WalletCash = walletCash;
            CreateDate = createDate;
            WalletSavings = walletSavings;
        }

        public long MemberId { get; set; }
        public int StatusId { get; set; }
        public ValueText<long> ReferrerMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string Occupation { get; set; }
        public int CompanyTypeId { get; set; }
        public string FileNumber { get; set; }
        public int? ProgramEventId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string ICNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public int? RaceId { get; set; }
        public int? ReligionId { get; set; }
        public int? HighestLevelOfEducationId { get; set; }
        public int? MaritalStatusId { get; set; }
        public string ReferralName { get; set; }
        public RequestNewSurveySpouse Spouse { get; set; }
        public RequestNewSurveyFamily Family { get; set; }
        public RequestNewSurveyCompany Company { get; set; }
        public RequestNewSurveyEmergency Emergency { get; set; }
        public RequestNewSurveyBank Bank { get; set; }
        public RequestNewSurveyOther Other { get; set; }
        public List<DropdownItem> Sectors { get; set; }
        public List<Dropdown3> JobPositions { get; set; }
        public List<DropdownItem> Banks { get; set; }
        public FileUploadedBy<ValueText<string>> ICFile { get; set; }
        public FileUploadedBy<ValueText<string>> PayslipFile { get; set; }
        public FileUploadedBy<ValueText<string>> OfferLetterFile { get; set; }
        public FileDisplay ProfileFile { get; set; }
        public string AdminRemark { get; set; }
        public decimal WalletCash { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal WalletSavings { get; set; }
    }

    public class RequestUpdateMemberByAdmin
    {
        public HttpPostedFileBase IcFile { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
        public HttpPostedFileBase OfferLetterFile { get; set; }
        public string Json { get; set; }
    }

    public class RequestUpdateMemberByAdminData
    {
        public long MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StatusId { get; set; }
        public long? ReferrerMemberId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? ProgramEventId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string ICNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public int? RaceId { get; set; }
        public int? ReligionId { get; set; }
        public int? HighestLevelOfEducationId { get; set; }
        public int? MaritalStatusId { get; set; }
        public RequestNewSurveySpouse Spouse { get; set; }
        public RequestNewSurveyFamily Family { get; set; }
        public RequestNewSurveyCompany Company { get; set; }
        public RequestNewSurveyEmergency Emergency { get; set; }
        public RequestNewSurveyBank Bank { get; set; }
        public RequestNewSurveyOther Other { get; set; }
    }

    public class MemberGVByAdmin
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ICNumber { get; set; }
        public int? GenderId { get; set; }
        public string Gender
        {
            get
            {
                try
                {
                    return ((Gender)Enum.ToObject(typeof(Gender), GenderId)).GetDescription();
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public string ReferrerName { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string Occupation { get; set; }
        public int CompanyTypeId { get; set; }
        public int MemberTypeId { get; set; }   
        public long? ApplicationId { get; set; }   
        public DateTime? RejectedDate { get; set; }
        public int? CustomerStatusId { get; set; }
        public string FileNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public string MemberType {
            get
            {
                try
                {
                    return ((MemberType)Enum.ToObject(typeof(MemberType), MemberTypeId)).GetDescription();
                }
                catch (Exception)
                {
                    return MemberTypeId.ToString();
                }
            }
        }     
        public string EmployerName { get; set; }
        public string State { get; set; }
        public string Sector { get; set; }
        public int? SalaryRangeId { get; set; }
        public string SalaryRange { 
            get
            {
                try
                {
                    return ((SalaryRange)Enum.ToObject(typeof(SalaryRange), SalaryRangeId)).GetDescription();
                }
                catch (Exception)
                {
                    return SalaryRangeId.ToString();
                }
            }
        }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string Bank { get; set; }
        public string ReferralLink { get; set; }        
    }

    public class AgentGVByAdmin
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ICNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public string State { get; set; }
        public string Sector { get; set; }
        public string Position { get; set; }
        public string NoReferralCustomer { get; set; }
        public string Payslip { get; set; }
        public string FileNumber { get; set; }
        public string ReferralLink { get; set; }
    }

    public class RequestApproveMember
    {
        public RequestApproveMember()
        {

        }

        public RequestApproveMember(int memberId, int adminId)
        {
            MemberId = memberId;
            AdminId = adminId;
        }

        public int MemberId { get; set; }
        public int AdminId { get; set; }
    }

    public class RequestRejectMember : RequestApproveMember
    {
        public RequestRejectMember()
        {

        }

        public RequestRejectMember(int memberId, int adminId) : base(memberId, adminId)
        {
        }
    }

    public class TopReferrals { 
        public long MemberId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class RequestSaveMemberBeforeApplicationCreate
    {
        public HttpPostedFileBase IcFile { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
        public HttpPostedFileBase OfferLetterFile { get; set; }
        public string Json { get; set; }
    }

    public class RequestSaveMemberBeforeApplicationCreateData
    {
        public long MemberId { get; set; }
        public int? ProgramEventId { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string ICNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public int? RaceId { get; set; }
        public int? ReligionId { get; set; }
        public int? HighestLevelOfEducationId { get; set; }
        public int? MaritalStatusId { get; set; }
        public RequestNewSurveySpouse Spouse { get; set; }
        public RequestNewSurveyFamily Family { get; set; }
        public RequestNewSurveyCompany Company { get; set; }
        public RequestNewSurveyEmergency Emergency { get; set; }
        public RequestNewSurveyBank Bank { get; set; }
        public RequestNewSurveyOther Other { get; set; }
    }

    public class RequestWalletChangesByAdmin
    {
        public long MemberId { get; set; }
        public int AdminId { get; set; }
        public decimal Amount { get; set; }
    }

    public abstract class BaseRequestExport
    {
        public BaseRequestExport(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class RequestExportSurvey : BaseRequestExport
    {
        public RequestExportSurvey(DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
        {

        }   
    }

    public class RequestExportFinance : BaseRequestExport
    {
        public RequestExportFinance(DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
        {

        }
    }

    public class RequestExportDemographicMarketAnalysis : BaseRequestExport
    {
        public RequestExportDemographicMarketAnalysis(DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
        {

        }
    }

    public class RequestExportRM : BaseRequestExport
    {
        public RequestExportRM(DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
        {

        }
    }
    
    public class RequestExportOperation : BaseRequestExport
    {
        public RequestExportOperation(DateTime? startDate, DateTime? endDate) : base(startDate, endDate)
        {

        }
    }

    public class ExportSurvey
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ICNumber { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public string ICFileId { get; set; }
        public string PayslipFileId { get; set; }        
        public string Answer { get; set; }    
        private SurveyData SurveyData
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<SurveyData>(Answer);
                }
                catch (Exception ex)
                {
                    return new SurveyData();
                }
            }
        }
        public string GetA1(List<StateDisplay> states)
        {
            try
            {
                return states.Find(x=>x.StateId == SurveyData.bagA.a1.ToInt())?.Name;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA2(List<DropdownItem> sector, List<Dropdown3> job)
        {
            try
            {
                string sectorName = "";
                string jobName = "";

                if(SurveyData.bagA.a2 == "999")
                {
                    sectorName = $"Lain-lain: {SurveyData.bagA.a2a}";
                }
                else
                {
                    sectorName = sector.Find(x => x.Key == SurveyData.bagA.a2)?.Text;
                }

                if (SurveyData.bagA.a2b == "999")
                {
                    jobName = $"Lain-lain: {SurveyData.bagA.a2c}";
                }
                else
                {
                    jobName = job.Find(x => x.Value == SurveyData.bagA.a2b)?.Text;
                }

                return $"{sectorName} {jobName}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA3()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"PMR"),
                    new ValueText<int>(2,"SPM"),
                    new ValueText<int>(3,"Diploma"),
                    new ValueText<int>(4,"Ijazah"),
                    new ValueText<int>(5,"Ijazah sarjana"),
                    new ValueText<int>(6,"PhD"),
                };

                return list.Find(x => x.Value == SurveyData.bagA.a3.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA4()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Berkahwin"),
                    new ValueText<int>(2,"Bujang"),
                    new ValueText<int>(3,"Janda"),
                    new ValueText<int>(4,"Duda"),
                };

                return list.Find(x => x.Value == SurveyData.bagA.a4.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA5()
        {
            try
            {
                return SurveyData.bagA.a5;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA6()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Ya"),
                    new ValueText<int>(2,"Tidak"),
                };

                return list.Find(x => x.Value == SurveyData.bagA.a6.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA7()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Memiliki rumah sendiri"),
                    new ValueText<int>(2,"Menyewa"),
                    new ValueText<int>(3,"Rumah ibu bapa"),
                };

                return list.Find(x => x.Value == SurveyData.bagA.a7.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA8()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Dalam Bandar"),
                    new ValueText<int>(2,"Luar Bandar"),
                };

                return list.Find(x => x.Value == SurveyData.bagA.a8.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA9()
        {
            try
            {
                return SurveyData.bagA.a9;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetA10()
        {
            try
            {
                int OtherId = 16;
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Menonton TV atau filem"),
                    new ValueText<int>(2,"Cafe hopping"),
                    new ValueText<int>(3,"Shopping atau online shopping"),
                    new ValueText<int>(4,"Karaoke dan muzik"),
                    new ValueText<int>(5,"Membaca atau menulis"),
                    new ValueText<int>(6,"Bersenam atau mengikuti kelas kecergasan"),
                    new ValueText<int>(7,"Melancong atau berkelah"),
                    new ValueText<int>(8,"Memasak"),
                    new ValueText<int>(9,"Kraf dan seni"),
                    new ValueText<int>(10,"Berkebun atau menjaga tanaman"),
                    new ValueText<int>(11,"Meluangkan masa untuk aktiviti keluarga"),
                    new ValueText<int>(12,"Berkunjung ke pasar malam atau bazar"),
                    new ValueText<int>(13,"Menyertai aktiviti sukarelawan di komuniti"),
                    new ValueText<int>(14,"Berlepak dengan rakan-rakan"),
                    new ValueText<int>(15,"Bermain permainan video atau mobile game"),
                };

                List<string> values = new List<string>();
                foreach (var item in SurveyData.bagA.a10)
                {
                    if(item == OtherId.ToString())
                    {
                        values.Add($"Lain-lain: {SurveyData.bagA.a10a}");
                    }
                    else
                    {
                        values.Add(list.Find(x => x.Value == item.ToInt())?.Text);
                    }
                }

                return string.Join(", ", values);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB1()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"< RM3,000"),
                    new ValueText<int>(2,"RM3,001 - RM5,000"),
                    new ValueText<int>(3,"RM5,001 - RM10,000"),
                    new ValueText<int>(4,"RM10,001 - RM15,000"),
                    new ValueText<int>(5,"> RM15,000"),
                };

                return list.Find(x => x.Value == SurveyData.bagB.b1.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB2()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"< RM100,000"),
                    new ValueText<int>(2,"RM100,001 - RM200,000"),
                    new ValueText<int>(3,"> RM200,000"),
                };

                return list.Find(x => x.Value == SurveyData.bagB.b2.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB3()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"< RM400"),
                    new ValueText<int>(2,"RM401 - RM700"),
                    new ValueText<int>(3,"RM701 - RM1000"),
                    new ValueText<int>(4,"> RM1000"),
                };

                return list.Find(x => x.Value == SurveyData.bagB.b3.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB4()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Menyediakan dana kecemasan"),
                    new ValueText<int>(2,"Mencapai impian hidup (membeli kereta, percutian, perkahwinan)"),
                    new ValueText<int>(3,"Persediaan untuk persaraan"),
                    new ValueText<int>(4,"Menjamin masa depan pendidikan anak-anak"),
                    new ValueText<int>(5,"Mengurangkan beban hutang"),
                    new ValueText<int>(6,"Menghadapi ketidakpastian ekonomi"),
                    new ValueText<int>(7,"Sebagai modal perniagaan atau pelaburan"),
                    new ValueText<int>(8,"Menambah simpanan untuk perbelanjaan kesihatan"),
                    new ValueText<int>(9,"Menyediakan simpanan untuk keluarga atau tanggungan"),
                    new ValueText<int>(10,"Mencapai kestabilan kewangan dan ketenangan fikiran"),
                };

                List<string> values = new List<string>();
                foreach (var item in SurveyData.bagB.b4)
                {
                    values.Add(list.Find(x => x.Value == item.ToInt())?.Text);
                }

                return string.Join(", ", values);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB5()
        {
            try
            {
                int OtherId = 10;
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Kos sara hidup yang tinggi"),
                    new ValueText<int>(2,"Pendapatan yang tidak mencukupi"),
                    new ValueText<int>(3,"Komitmen hutang yang tinggi (kad kredit, pinjaman)"),
                    new ValueText<int>(4,"Gaya hidup atau keperluan harian yang mahal"),
                    new ValueText<int>(5,"Kurang disiplin dalam mengawal perbelanjaan"),
                    new ValueText<int>(6,"Kecemasan atau perbelanjaan tidak dijangka"),
                    new ValueText<int>(7,"Keinginan untuk berbelanja lebih pada hiburan dan hobi"),
                    new ValueText<int>(8,"Sokongan kewangan kepada keluarga atau tanggungan"),
                    new ValueText<int>(9,"Kekurangan pengetahuan atau kemahiran pengurusan kewangan"),
                };

                List<string> values = new List<string>();
                foreach (var item in SurveyData.bagB.b5)
                {
                    if (item == OtherId.ToString())
                    {
                        values.Add($"Lain-lain: {SurveyData.bagB.b5a}");
                    }
                    else
                    {
                        values.Add(list.Find(x => x.Value == item.ToInt())?.Text);
                    }
                }

                return string.Join(", ", values);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB6()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Tidak pernah"),
                    new ValueText<int>(2,"Sekali atau dua kali"),
                    new ValueText<int>(3,"Beberapa kali"),
                    new ValueText<int>(4,"Secara berkala"),
                };

                return list.Find(x => x.Value == SurveyData.bagB.b6.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB7()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Tidak mampu membayar balik hutang"),
                    new ValueText<int>(2,"Beban kewangan yang bertambah dan menjejaskan gaya hidup"),
                    new ValueText<int>(3,"Risiko muflis atau kehabisan wang simpanan"),
                    new ValueText<int>(4,"Keperluan untuk mengorbankan matlamat kewangan lain (contohnya, pendidikan anak atau persaraan)"),
                };

                return list.Find(x => x.Value == SurveyData.bagB.b7.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB8()
        {
            try
            {
                int OtherId = 13;
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Membayar hutang peribadi (kad kredit, pinjaman)"),
                    new ValueText<int>(2,"Membayar pinjaman perumahan (gadai janji, pinjaman penambahbaikan rumah)"),
                    new ValueText<int>(3,"Melabur dalam saham, dana bersama, atau instrumen kewangan lain"),
                    new ValueText<int>(4,"Memulakan atau mengembangkan perniagaan"),
                    new ValueText<int>(5,"Menyimpan untuk pendidikan anak-anak"),
                    new ValueText<int>(6,"Membeli atau memperbaiki rumah"),
                    new ValueText<int>(7,"Membina tabung kecemasan"),
                    new ValueText<int>(8,"Melancong bersama keluarga atau aktiviti rekreasi"),
                    new ValueText<int>(9,"Membeli kereta baru atau barangan rumah utama"),
                    new ValueText<int>(10,"Menampung perbelanjaan perubatan atau keperluan berkaitan kesihatan"),
                    new ValueText<int>(11,"Menderma atau menyokong badan amal"),
                    new ValueText<int>(12,"Menyimpan untuk persaraan"),
                };

                List<string> values = new List<string>();
                foreach (var item in SurveyData.bagB.b8)
                {
                    if (item == OtherId.ToString())
                    {
                        values.Add($"Lain-lain: {SurveyData.bagB.b8a}");
                    }
                    else
                    {
                        values.Add(list.Find(x => x.Value == item.ToInt())?.Text);
                    }
                }

                return string.Join(", ", values);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB9()
        {
            try
            {
                int OtherId = 10;
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Hartanah"),
                    new ValueText<int>(2,"Emas"),
                    new ValueText<int>(3,"Saham atau pasaran modal"),
                    new ValueText<int>(4,"Forex atau Crypto"),
                    new ValueText<int>(5,"Trust Fund atau pelaburan amanah"),
                    new ValueText<int>(6,"Perniagaan F&B (makanan dan minuman)"),
                    new ValueText<int>(7,"Perniagaan E-Dagang"),
                    new ValueText<int>(8,"Perniagaan kecantikan dan kesihatan"),
                    new ValueText<int>(9,"MLM atau Direct Sales"),
                };

                List<string> values = new List<string>();
                foreach (var item in SurveyData.bagB.b9)
                {
                    if (item == OtherId.ToString())
                    {
                        values.Add($"Lain-lain: {SurveyData.bagB.b9a}");
                    }
                    else
                    {
                        values.Add(list.Find(x => x.Value == item.ToInt())?.Text);
                    }
                }

                return string.Join(", ", values);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetB10()
        {
            try
            {
                return SurveyData.bagB.b10;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetC1()
        {
            try
            {
                int otherId = 6;
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Tiada"),
                    new ValueText<int>(2,"Financial Faiz"),
                    new ValueText<int>(3,"Dr Adam Zubir"),
                    new ValueText<int>(4,"Faizul Ridzuan"),
                    new ValueText<int>(5,"Afyan Mat Rawi"),
                };

                if(SurveyData.bagC.c1 == otherId.ToString())
                {
                    return SurveyData.bagC.c1a;
                }

                return list.Find(x => x.Value == SurveyData.bagC.c1.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetC2()
        {
            try
            {
                var list = new List<ValueText<int>>
                {
                    new ValueText<int>(1,"Pagi"),
                    new ValueText<int>(2,"Tengah Hari"),
                    new ValueText<int>(3,"Petang"),
                    new ValueText<int>(4,"Malam"),
                };

                return list.Find(x => x.Value == SurveyData.bagC.c2.ToInt())?.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public class ExportFinance
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ICNumber { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public int? StatusId { get; set; }
        public decimal? Amount { get; set; }
        public string Status
        {
            get
            {
                return hudanLibrary.EnumHelper.GetDescription(typeof(WithdrawStatus), StatusId, StatusId.ToString());
            }
        }
    }

    public class ExportDemographicMarketAnalysis
    {
        public int? ProgramEventId { get; set; }
        public string ProgramEvent
        {
            get
            {
                return hudanLibrary.EnumHelper.GetDescription(typeof(MemberProgramEvent), ProgramEventId, ProgramEventId.ToString());
            }
        }
        public int? RaceId { get; set; }
        public string Race
        {
            get
            {
                return hudanLibrary.EnumHelper.GetDescription(typeof(Race), RaceId, RaceId.ToString());
            }
        }
        public DateTime? Birthdate { get; set; }
        public string Age
        {
            get
            {
                if (!Birthdate.HasValue) return "";
                DateTime currentDate = DateTime.Today;

                int age = currentDate.Year - Birthdate.Value.Year;

                if (currentDate.Month < Birthdate.Value.Month || (currentDate.Month == Birthdate.Value.Month && currentDate.Day < Birthdate.Value.Day))
                {
                    age--;
                }

                return age.ToString();
            }
        }
        public int? GenderId { get; set; }
        public string Gender => GenderId == 1 ? "Male" : "Female";
        public decimal? Salary { get; set; }
        public int? SalaryRangeId { get; set; }
        public string State { get; set; }
        public string Sector { get; set; }
        public string SalaryRange
        {
            get
            {
                return hudanLibrary.EnumHelper.GetDescription(typeof(SalaryRange), SalaryRangeId, SalaryRangeId.ToString());
            }
        }
    }

    public class ExportRM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int? ReferralCount { get; set; }        
        public string TierRank { get; set; }
        public void CalcTierRank(int index)
        {
            if (!ReferralCount.HasValue) return;

            if(ReferralCount.Value >= 1 && ReferralCount.Value < 4)
            {
                TierRank = "Wira Plus";
            }
            else if (ReferralCount.Value >= 4 && ReferralCount.Value < 8)
            {
                TierRank = "Wira Silver";
            }
            else if (ReferralCount.Value >= 9 && ReferralCount.Value < 12)
            {
                TierRank = "Wira Gold";
            }
            else if (ReferralCount.Value > 12 && index < 2)
            {
                TierRank = "Wira Legend";
            }
            else if (ReferralCount.Value > 12)
            {
                TierRank = "Wira Platinum";
            }
        }
    }

    public class ExportOperation
    {
        public int No { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Consultant { get; set; }
        public string FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public decimal? Amount { get; set; }
        public decimal? AmountFacilities { get; set; }
        public decimal TotalAB => (Amount.HasValue ? Amount.Value : 0) + (AmountFacilities.HasValue ? AmountFacilities.Value : 0);
        public decimal? Pct { get; set; }
        public decimal? Stli { get; set; }
        public decimal Total => TotalAB + (Stli.HasValue ? Stli.Value : 0);
        public string SettlementInvoice { get; set; }
    }

    public class ExportCredit
    {
        public int PreCheckingStage { get; set; }
        public int ProposalPreparationStage { get; set; }
        public int ProposalPresentationStage { get; set; }
        public int QueueForReloanStage { get; set; }
        public int ReloanSubmissionStage { get; set; }
        public int TotalApprovesCases { get; set; }
        public int TotalBurstCases { get; set; }
        public int TotalDroppedCases { get; set; }
        public int TotalMIACases { get; set; }
        public int TotalSingleCases { get; set; }
        public int TotalRNRCases { get; set; }
    }
}
