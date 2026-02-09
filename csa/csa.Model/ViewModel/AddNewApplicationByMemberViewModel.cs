using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class AddNewApplicationByMemberViewModel
    {
        public AddNewApplicationByMemberViewModel(int? programEventId, string fullName, DateTime? dateOfBirth, int? gender, string iCNumber, string address, string phoneNumber, string email, string taxNumber, int? raceId, int? religionId, int? highestLevelOfEducationId, int? maritalStatusId, string referralName, RequestNewSurveySpouse spouse, RequestNewSurveyFamily family, RequestNewSurveyCompany company, RequestNewSurveyEmergency emergency, RequestNewSurveyBank bank, RequestNewSurveyOther other, List<DropdownItem> sectors, List<Dropdown3> jobPositions, List<DropdownItem> banks, FileUploadedBy<ValueText<string>> iCFile, FileUploadedBy<ValueText<string>> offerLetterFile, FileUploadedBy<ValueText<string>> payslipFile)
        {
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
            OfferLetterFile = offerLetterFile;
            PayslipFile = payslipFile;
        }

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
    }
}
