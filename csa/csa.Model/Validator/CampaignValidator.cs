using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class NewCampaignValidator : AbstractValidator<RequestNewCampaign>
    {
        public NewCampaignValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty().WithMessage("name_required");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("subject_required");
        }
    }

    public class UpdateCampaignValidator : AbstractValidator<RequestUpdateCampaign>
    {
        public UpdateCampaignValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty().WithMessage("name_required");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("subject_required");
        }
    }
}
