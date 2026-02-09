using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class NewTagValidator : AbstractValidator<RequestNewTag>
    {
        public NewTagValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Label).NotEmpty().WithMessage("label_required");
        }
    }

    public class UpdateTagValidator : AbstractValidator<RequestUpdateTag>
    {
        public UpdateTagValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Label).NotEmpty().WithMessage("label_required");
        }
    }
}
