using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class SettlementBiz
    {
        public static List<ApplicationSettlementDetails> ListByApplicationId(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var list = db.Settlements.Where(x => x.ApplicationID == applicationId).ToList();
                List<ApplicationSettlementDetails> retVal = new List<ApplicationSettlementDetails>();
                foreach (var item in list)
                {
                    retVal.Add(new ApplicationSettlementDetails(item.SettlementId,item.Amount,item.TotalPct.HasValue ? item.TotalPct.Value * 100 : (decimal?)null, item.TotalPctAmount,item.PaymentDate,item.BankId,item.BankAccountNumber,item.DueDate,item.FacilitiesOther,item.AmountFacilities,item.FlexyCampaignId,item.TotalCampaign,item.RedemptionLetterDate,item.RedemptionAmount,item.LoanReleaseDate,item.SettlementStatusId,item.Remark,AdminBiz.Get(item.CreateAdminId ?? 0)?.Name,item.FacilitiesId,item.FlexyCampaignOther));
                }

                return retVal;
            }
        }
    }
}
