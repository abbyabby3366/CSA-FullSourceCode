using csa.Model.DataObject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public class NewApplicationByMemberValidator : AbstractValidator<RequestNewApplicationByMember>
    {
        public NewApplicationByMemberValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull().WithMessage("name_required");
            RuleFor(x => x.ContactNo).NotNull().WithMessage("contact_required");
        }
    }   
    
    public interface ICustomValidator
    {
        RespArgs<bool> Validate();
    }

    public class ApplicationAdditionalDocumentCustomValidator : ICustomValidator
    {
        public ApplicationAdditionalDocumentCustomValidator(List<System.Web.HttpPostedFileBase> httpFiles, List<string> newMarks, List<string> updateMarks)
        {
            HttpFiles = httpFiles;
            NewMarks = newMarks;
            UpdateMarks = updateMarks;
        }

        public List<System.Web.HttpPostedFileBase> HttpFiles { get; set; }
        public List<string> NewMarks { get; set; }
        public List<string> UpdateMarks { get; set; }

        public RespArgs<bool> Validate()
        {
            if (HttpFiles != null && HttpFiles.Count == 0 && NewMarks.Count > 0) return RespArgs<bool>.CreateError("Additional document files are required");

            if (NewMarks.Concat(UpdateMarks).Any(x => string.IsNullOrWhiteSpace(x))) return RespArgs<bool>.CreateError("Additional document marks are required");

            return RespArgs<bool>.CreateSuccess(true);
        }
    }

    public class ApplicationConvertHeroCustomValidator : ICustomValidator
    {
        public ApplicationConvertHeroCustomValidator(long? referrerMemberId, int? aMAdminId, int? pFCAdminId, int? rMAdminId, int? uMAdminId, int? pAAdminId)
        {
            ReferrerMemberId = referrerMemberId;
            AMAdminId = aMAdminId;
            PFCAdminId = pFCAdminId;
            RMAdminId = rMAdminId;
            UMAdminId = uMAdminId;
            PAAdminId = pAAdminId;
        }

        public long? ReferrerMemberId { get; set; }
        public int? AMAdminId { get; set; }
        public int? PFCAdminId { get; set; }
        public int? RMAdminId { get; set; }
        public int? UMAdminId { get; set; }
        public int? PAAdminId { get; set; }
        public RespArgs<bool> Validate()
        {
            List<string> errorMessages = new List<string>();
            if (!ReferrerMemberId.HasValue) errorMessages.Add("Referrer has not been assigned yet");
            if (!PFCAdminId.HasValue) errorMessages.Add("PFC has not been assigned yet");
            if (!AMAdminId.HasValue) errorMessages.Add("AM has not been assigned yet");
            if (!RMAdminId.HasValue) errorMessages.Add("RM has not been assigned yet");
            if (!UMAdminId.HasValue) errorMessages.Add("UM has not been assigned yet");
            if (!PAAdminId.HasValue) errorMessages.Add("PA has not been assigned yet");

            if(errorMessages.Count > 0)
            {
                return RespArgs<bool>.CreateError(string.Join("", errorMessages.Select(x => $"<p>{x}</p>")));
            }
            return RespArgs<bool>.CreateSuccess(true);
        }
    }
}
