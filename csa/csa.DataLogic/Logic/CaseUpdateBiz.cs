using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class CaseUpdateBiz
    {
        public static List<ApplicationCaseUpdate> ListByApplicationId(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var list = db.Caseupdates.Where(x => x.ApplicationId == applicationId).ToList();
                List<ApplicationCaseUpdate> retVal = new List<ApplicationCaseUpdate>();
                foreach (var item in list)
                {
                    retVal.Add(new ApplicationCaseUpdate(
                        item.CaseUpdateId,
                        item.BankId,
                        item.LoanAmount,
                        item.SubmitDate,
                        item.Banker,
                        item.CompleteStatusId,
                        item.Consolidate,
                        item.CashNet,
                        item.Instalment,
                        item.ApprovedDate,
                        item.SignDate,
                        item.DisbursementDate,
                        item.UpdateDate,
                        item.LoanAccountNumber,
                        item.StDueDate,
                        item.Remarkds,
                        AdminBiz.Get(item.AdminId ?? 0)?.Name
                    ));
                }

                return retVal;
            }
        }
    }
}
