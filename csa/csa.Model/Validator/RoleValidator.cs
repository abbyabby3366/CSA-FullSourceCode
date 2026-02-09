using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class NewRoleValidator : AbstractValidator<RequestNewRole>
    {
        public NewRoleValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull().WithMessage("name_required");
        }
    }

    public class UpdateRoleValidator : AbstractValidator<RequestUpdateRole>
    {
        public UpdateRoleValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull().WithMessage("name_required");
        }
    }
}
