using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class RegisterMemberValidator : AbstractValidator<RequestRegisterMember>
    {
        public RegisterMemberValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone required");
            RuleFor(x => x.Password).NotNull().WithMessage("Password required");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Confirm password required").Equal(x=>x.Password).WithMessage("Password not same");
        }
    }

    public class ChangePersonalDetailsValidator : AbstractValidator<RequestChangePersonalDetails>
    {
        public ChangePersonalDetailsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalid").When(x => !string.IsNullOrEmpty(x.Email));
        }
    }

    public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
    {
        public ChangePasswordValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Current password required");
            RuleFor(x => x.NewPassword).NotNull().WithMessage("New password required");
            RuleFor(x => x.ConfirmNewPassword).NotNull().WithMessage("Confirm password required").Equal(x => x.NewPassword).WithMessage("Password not same");
        }
    }

    public class ChangePasswordByAdminValidator : AbstractValidator<RequestChangePasswordByAdmin>
    {
        public ChangePasswordByAdminValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Current password required");
            RuleFor(x => x.NewPassword).NotNull().WithMessage("New password required");
            RuleFor(x => x.ConfirmNewPassword).NotNull().WithMessage("Confirm password required").Equal(x => x.NewPassword).WithMessage("Password not same");
        }
    }

    public class AgentByMemberValidator : AbstractValidator<RequestAgentByMember>
    {
        public AgentByMemberValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone required");
            RuleFor(x => x.ICNumber).NotEmpty().WithMessage("ICNumber required");
        }
    }

    public class NewMemberByAdminValidator : AbstractValidator<RequestNewMemberByAdmin>
    {
        public NewMemberByAdminValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone required");
            RuleFor(x => x.Password).NotNull().WithMessage("Password required");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Confirm password required").Equal(x => x.Password).WithMessage("Password not same");
        }
    } 
    
    public class UpdateMemberByAdminValidator : AbstractValidator<RequestUpdateMemberByAdminData>
    {
        public UpdateMemberByAdminValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalid").When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
