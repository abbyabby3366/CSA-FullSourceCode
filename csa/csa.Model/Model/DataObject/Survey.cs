using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace csa.Model.DataObject
{
    public class RequestNewSurveyProfile
    {
        public int? ProgramEventId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderId { get; set; }
        public string ICNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string TaxNumber { get; set; }
        public int? RaceId { get; set; }
        public int? ReligionId { get; set; }
        public int? HighLevelOfEducationId { get; set; }
        public int? MaritalStatusId { get; set; }
    }

    public class RequestNewSurveySpouse
    {
        public RequestNewSurveySpouse()
        {
        }

        public RequestNewSurveySpouse(string fullName, string iCNumber, string contactInformation, string occupation, string companyAddress, decimal? salary)
        {
            FullName = fullName;
            ICNumber = iCNumber;
            ContactInformation = contactInformation;
            Occupation = occupation;
            CompanyAddress = companyAddress;
            Salary = salary;
        }

        public string FullName { get; set; }
        public string ICNumber { get; set; }
        public string ContactInformation { get; set; }
        public string Occupation { get; set; }
        public string CompanyAddress { get; set; }
        public decimal? Salary { get; set; }
    }

    public class RequestNewSurveyFamily
    {
        public RequestNewSurveyFamily()
        {
        }

        public RequestNewSurveyFamily(int? numberOfDependent, int? isHasOKU, string fatherName, string fatherContactNumber, string fatherAddress, string motherName, string motherContactNumber, string motherAddress)
        {
            NumberOfDependent = numberOfDependent;
            IsHasOKU = isHasOKU;
            FatherName = fatherName;
            FatherContactNumber = fatherContactNumber;
            FatherAddress = fatherAddress;
            MotherName = motherName;
            MotherContactNumber = motherContactNumber;
            MotherAddress = motherAddress;
        }

        public int? NumberOfDependent { get; set; }
        public int? IsHasOKU { get; set; }
        public string FatherName { get; set; }
        public string FatherContactNumber { get; set; }
        public string FatherAddress { get; set; }
        public string MotherName { get; set; }
        public string MotherContactNumber { get; set; }
        public string MotherAddress { get; set; }
    }

    public class RequestNewSurveyCompany
    {
        public RequestNewSurveyCompany()
        {
        }

        public RequestNewSurveyCompany(int? employerTypeId, string companyName, string jobTitle, int? sectorId, int? departmentId, string companyAddress, string officeContactNumber, int? employmentStatusId, int? retirementAge, int? yearOfService, string employerTypeOther, string employmentStatusOther, string sectorOther, string departmentOther, string employerName)
        {
            EmployerTypeId = employerTypeId;
            CompanyName = companyName;
            JobTitle = jobTitle;
            SectorId = sectorId;
            DepartmentId = departmentId;
            CompanyAddress = companyAddress;
            OfficeContactNumber = officeContactNumber;
            EmploymentStatusId = employmentStatusId;
            RetirementAge = retirementAge;
            YearOfService = yearOfService;
            EmployerTypeOther = employerTypeOther;
            EmploymentStatusOther = employmentStatusOther;
            SectorOther = sectorOther;
            DepartmentOther = departmentOther;
            EmployerName = employerName;
        }

        public int? EmployerTypeId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public int? SectorId { get; set; }
        public int? DepartmentId { get; set; }
        public string CompanyAddress { get; set; }
        public string OfficeContactNumber { get; set; }
        public int? EmploymentStatusId { get; set; }
        public int? RetirementAge { get; set; }
        public int? YearOfService { get; set; }
        public string EmployerTypeOther { get; set; }
        public string EmploymentStatusOther { get; set; }
        public string SectorOther { get; set; }
        public string DepartmentOther { get; set; }
        public string EmployerName { get; set; }
    }

    public class RequestNewSurveyEmergency
    {
        public RequestNewSurveyEmergency()
        {
        }

        public RequestNewSurveyEmergency(string contactPerson, int? relationShipId, string contactNumber, string iCNumber, string occupation, string address)
        {
            ContactPerson = contactPerson;
            RelationShipId = relationShipId;
            ContactNumber = contactNumber;
            ICNumber = iCNumber;
            Occupation = occupation;
            Address = address;
        }

        public string ContactPerson { get; set; }
        public int? RelationShipId { get; set; }
        public string ContactNumber { get; set; }
        public string ICNumber { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
    }

    public class RequestNewSurveyBank
    {
        public RequestNewSurveyBank()
        {
        }

        public RequestNewSurveyBank(int? bankId, string accountNumber, decimal? grossSalary, int? salaryRangeId, string accountName, string bankOther)
        {
            BankId = bankId;
            AccountNumber = accountNumber;
            GrossSalary = grossSalary;
            SalaryRangeId = salaryRangeId;
            AccountName = accountName;
            BankOther = bankOther;
        }

        public int? BankId { get; set; }
        public string BankOther { get; set; }
        public string AccountNumber { get; set; }
        public decimal? GrossSalary { get; set; }
        public int? SalaryRangeId { get; set; }
        public string AccountName { get; set; }
    }

    public class RequestNewSurveyLogin
    {
        public bool IsLogged { get; set; } 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RequestNewSurveyOther
    {
        public RequestNewSurveyOther()
        {
        }

        public RequestNewSurveyOther(string preferredLanguage, string hobbies, string socialMediaHandles, string fBName)
        {
            PreferredLanguage = preferredLanguage;
            Hobbies = hobbies;
            SocialMediaHandles = socialMediaHandles;
            FBName = fBName;
        }

        public string PreferredLanguage { get; set; }
        public string Hobbies { get; set; }
        public string SocialMediaHandles { get; set; }
        public string FBName { get; set; }
    }

    public class RequestNewSurvey1ByMember
    {
        public HttpPostedFileBase PayslipFile { get; set; }
        public HttpPostedFileBase IcFile { get; set; }
        public string Json { get; set; }
    }

    public class RequestNewSurvey1DataByMember
    {
        public long? ReferralMemberId { get; set; }
        public long MemberId { get; set; }
        public string SurveyJson { get; set; }
        public string ICNumber { get; set; }
        public int? BankId { get; set; }
        public string BankOther { get; set; }
        public string BankAccountNumber { get; set; }
        public bool IsLogged { get; set; }
        public int? StateId { get; set; }
        public int? EmployerTypeId { get; set; }
        public string EmployerTypeOther { get; set; }
        public int? EmploymentStatusId { get; set; }
        public string EmploymentStatusOther { get; set; }
        public int? RetirementAge { get; set; }
        public int? YearOfService { get; set; }
        public string ReferrerName { get; set; }
        public string CompanyEmployerName { get; set; }
        public int? CompanySectorId { get; set; }
        public string CompanySectorOther { get; set; }
        public int? CompanyDepartmentId { get; set; }
        public string CompanyDepartmentOther { get; set; }
        public int? GenderId { get; set; }
        public string Email { get; set; }
    }

    public class RequestNewSurveyByMember
    {
        public HttpPostedFileBase PayslipFile { get; set; }
        public HttpPostedFileBase IcFile { get; set; }
        public HttpPostedFileBase OfferLetterFile { get; set; }
        public string Json { get; set; }
    }
    public class RequestNewSurveyDataByMember
    {
        public long? ReferralMemberId { get; set; }
        public long MemberId { get; set; }
        public string SurveyJson { get; set; }
        public RequestNewSurveyProfile Profile { get; set; }
        public RequestNewSurveySpouse Spouse { get; set; }
        public RequestNewSurveyFamily Family { get; set; }
        public RequestNewSurveyCompany Company { get; set; }
        public RequestNewSurveyEmergency Emergency { get; set; }
        public RequestNewSurveyBank Bank { get; set; }
        public RequestNewSurveyLogin Login { get; set; }
        public RequestNewSurveyOther Other { get; set; }
    }

    public class RequestNewMemberSurvey
    {
        public RequestNewMemberSurvey()
        {
        }

        public RequestNewMemberSurvey(string fullName, string phoneNumber)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ResponseNewMemberSurvey : RequestNewMemberSurvey
    {
        public ResponseNewMemberSurvey(string fullname, string phoneNumber, long memberId) : base(fullname, phoneNumber)
        {
            MemberId = memberId;
        }
        public long MemberId { get; set; }
    }

    public class SurveyData
    {
        public SurveyDataBagA bagA { get; set; }
        public SurveyDataBagB bagB { get; set; }
        public SurveyDataBagC bagC { get; set; }
    }

    public class SurveyDataBagA
    {
        public string a1 { get; set; }
        public string a2 { get; set; }
        public string a2a { get; set; }
        public string a2b { get; set; }
        public string a2c { get; set; }
        public string a3 { get; set; }
        public string a4 { get; set; }
        public string a5 { get; set; }
        public string a6 { get; set; }
        public string a7 { get; set; }
        public string a8 { get; set; }
        public string a9 { get; set; }
        public List<string> a10 { get; set; }
        public string a10a { get; set; }
    }

    public class SurveyDataBagB
    {
        public string b1 { get; set; }
        public string b2 { get; set; }
        public string b3 { get; set; }
        public List<string> b4 { get; set; }
        public List<string> b5 { get; set; }
        public string b5a { get; set; }
        public string b6 { get; set; }
        public string b7 { get; set; }
        public List<string> b8 { get; set; }
        public string b8a { get; set; }
        public List<string> b9 { get; set; }
        public string b9a { get; set; }
        public string b10 { get; set; }
    }

    public class SurveyDataBagC
    {
        public string c1 { get; set; }
        public string c1a { get; set; }
        public string c2 { get; set; }
    }
}
