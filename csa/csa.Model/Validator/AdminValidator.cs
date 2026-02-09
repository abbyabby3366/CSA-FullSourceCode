using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class AdminChangePasswordValidator : AbstractValidator<RequestAdminChangePassword>
    {
        public AdminChangePasswordValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Current password required");
            RuleFor(x => x.NewPassword).NotNull().WithMessage("New password required");
            RuleFor(x => x.ConfirmNewPassword).NotNull().WithMessage("Confirm new password required").Equal(x => x.NewPassword).WithMessage("Password not same");
        }
    }

    public class NewAdminValidator : AbstractValidator<RequestNewAdmin>
    {
        public NewAdminValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull().WithMessage("Name required");
            RuleFor(x => x.Email).NotNull().WithMessage("Email required").EmailAddress().WithMessage("Email invalid format");
            RuleFor(x => x.Password).NotNull().WithMessage("New password required");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Confirm new password required").Equal(x => x.Password).WithMessage("Password not same");
        }
    }

    public class UpdateAdminValidator : AbstractValidator<RequestUpdateAdmin>
    {
        public UpdateAdminValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull().WithMessage("Name required");
            RuleFor(x => x.Email).NotNull().WithMessage("Email required").EmailAddress().WithMessage("Email invalid format");
        }
    }

    public class AdminChangePasswordByAdminValidator : AbstractValidator<RequestAdminChangePasswordByAdmin>
    {
        public AdminChangePasswordByAdminValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Current password required");
            RuleFor(x => x.NewPassword).NotNull().WithMessage("New password required");
            RuleFor(x => x.ConfirmNewPassword).NotNull().WithMessage("Confirm new password required").Equal(x => x.NewPassword).WithMessage("Password not same");
        }
    }
}
