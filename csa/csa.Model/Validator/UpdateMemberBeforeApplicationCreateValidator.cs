using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class UpdateMemberBeforeApplicationCreateCompanyValidator : AbstractValidator<RequestNewSurveyCompany>
    {
        public UpdateMemberBeforeApplicationCreateCompanyValidator()
        {
            RuleFor(x => x.EmployerTypeId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Employer type required");
            //RuleFor(x => x.CompanyName).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Company name required");
            RuleFor(x => x.SectorId).Must(HelperValidator.MustNotZero).WithMessage("Sector required");
            RuleFor(x => x.EmploymentStatusId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Employment status required");
            RuleFor(x => x.RetirementAge).NotNull().WithMessage("Retirement age required");
            RuleFor(x => x.YearOfService).NotNull().WithMessage("Year of service required");
        }
    }

    public class UpdateMemberBeforeApplicationCreateBankValidator : AbstractValidator<RequestNewSurveyBank>
    {
        public UpdateMemberBeforeApplicationCreateBankValidator()
        {
            RuleFor(x => x.BankId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Bank required");
            RuleFor(x => x.AccountNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("Bank account number required");
            RuleFor(x => x.SalaryRangeId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Salary range required");
            //RuleFor(x => x.GrossSalary).NotNull().WithMessage("Gross salary required");
        }
    }

    public class UpdateMemberBeforeApplicationCreateValidator : AbstractValidator<RequestSaveMemberBeforeApplicationCreateData>
    {
        public UpdateMemberBeforeApplicationCreateValidator()
        {
            RuleFor(x => x.ProgramEventId).Must(HelperValidator.MustNotNullAndZero).WithMessage("Program required");
            RuleFor(x => x.Gender).Must(HelperValidator.MustNotNullAndZero).WithMessage("Gender required");
            RuleFor(x => x.ICNumber).Must(HelperValidator.MustNotNullAndEmpty).WithMessage("ICNumber required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.").When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Company).SetValidator(new UpdateMemberBeforeApplicationCreateCompanyValidator());
            RuleFor(x => x.Bank).SetValidator(new UpdateMemberBeforeApplicationCreateBankValidator());
        }
    }
}
