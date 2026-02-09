using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class NewEmailTemplateValidator : AbstractValidator<RequestNewEmailTemplate>
    {
        public NewEmailTemplateValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty().WithMessage("name_required");
        }
    }

    public class UpdateEmailTemplateValidator : AbstractValidator<RequestUpdateEmailTemplate>
    {
        public UpdateEmailTemplateValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty().WithMessage("name_required");
        }
    }
}
