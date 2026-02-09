using csa.Library;
using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    
    public class NewSurveyProfileValidator : AbstractValidator<RequestNewSurveyProfile>
    {       
        public NewSurveyProfileValidator()
        {
            RuleFor(x => x.GenderId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Gender required");
            RuleFor(x => x.ICNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("ICNumber required");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Invalid email format.").When(x => !string.IsNullOrEmpty(x.EmailAddress));
        }
    }

    public class NewSurveyCompanyValidator : AbstractValidator<RequestNewSurveyCompany>
    {
        public NewSurveyCompanyValidator()
        {
            RuleFor(x => x.EmployerTypeId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Employer type required");
            RuleFor(x => x.CompanyName).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Company name required");
            RuleFor(x => x.SectorId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Sector required");
            RuleFor(x => x.EmploymentStatusId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Employment status required");
            RuleFor(x => x.RetirementAge).NotNull().WithMessage("Retirement age required");
            RuleFor(x => x.YearOfService).NotNull().WithMessage("Year of service required");
        }
    }

    public class NewSurveyBankValidator : AbstractValidator<RequestNewSurveyBank>
    {
        public NewSurveyBankValidator()
        {
            RuleFor(x => x.BankId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Bank required");
            RuleFor(x => x.AccountNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Bank account number required");
            RuleFor(x => x.SalaryRangeId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Salary range required");
            //RuleFor(x => x.GrossSalary).NotNull().WithMessage("Gross salary required");
        }
    }
    
    public class NewSurveyLoginValidator : AbstractValidator<RequestNewSurveyLogin>
    {
        public NewSurveyLoginValidator()
        {
            RuleFor(x => x.Password).NotNull().WithMessage("Password required").When(x=> !x.IsLogged);
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Confirm password required").When(x => !x.IsLogged).Equal(x => x.Password).WithMessage("Password not same").When(x => !x.IsLogged);
        }
    }
    public class NewSurveyByMemberValidator : AbstractValidator<RequestNewSurveyDataByMember>
    {
        public NewSurveyByMemberValidator()
        {
            RuleFor(x => x.SurveyJson).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Survey required");
            RuleFor(x => x.Profile).SetValidator(new NewSurveyProfileValidator());
            RuleFor(x => x.Company).SetValidator(new NewSurveyCompanyValidator());
            RuleFor(x => x.Bank).SetValidator(new NewSurveyBankValidator());
            RuleFor(x => x.Login).SetValidator(new NewSurveyLoginValidator());
        }
    }

    public class NewSurveyAccountByMemberValidator : AbstractValidator<RequestNewMemberSurvey>
    {
        public NewSurveyAccountByMemberValidator()
        {
            RuleFor(x => x.FullName).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("FullName required");
            RuleFor(x => x.PhoneNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Phone Number required");
        }
    }

    public class NewSurvey1ByMemberValidator : AbstractValidator<RequestNewSurvey1DataByMember>
    {
        public NewSurvey1ByMemberValidator()
        {
            RuleFor(x => x.SurveyJson).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Survey required");
            RuleFor(x => x.ICNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("ICNumber required");
            RuleFor(x => x.BankId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Bank required");
            RuleFor(x => x.BankAccountNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Bank account number required");
            RuleFor(x => x.GenderId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Gender required");
            RuleFor(x => x.EmployerTypeId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Employer type required");
            RuleFor(x => x.EmployerTypeOther).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Employer type other required").When(x=>x.EmployerTypeId == 6); //6 is other
            RuleFor(x => x.CompanyEmployerName).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Employer name required");
            RuleFor(x => x.CompanySectorId).Must(HelperValidator.MustNotZero).WithMessage("Sector required");
            RuleFor(x => x.CompanySectorOther).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Sector other required").When(x => !x.CompanySectorId.HasValue); //null is other;
            RuleFor(x => x.EmploymentStatusId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Employment status required");
            RuleFor(x => x.EmploymentStatusOther).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Employment status other required").When(x => x.EmploymentStatusId == 4); //4 is other;
            RuleFor(x => x.RetirementAge).Must(HelperValidator.MustNumberPositiveNotNull).WithMessage("Retirement age required");
            RuleFor(x => x.YearOfService).Must(HelperValidator.MustNotNullAndZero).WithMessage("Year of service required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalid").When(x => x.Email.IsNotEmpty());
        }
    }
}
