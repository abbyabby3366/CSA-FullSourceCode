using csa.Library;
using csa.Model.DataObject;
using csa.Model;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csa.Model.DataObject;

namespace csa.DataLogic
{
    public static class HistoryBiz
    {
        public static RespArgs<GridViewModel<HistoryGVByMember>> GetGVByMember(long memberId,int pageIndex, int pageSize)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("history h");
                sqlSelect.AddSelect("h.HistoryId,h.TransactionTypeId,h.TransactionAmount,h.CreateDate,m.MemberCode,m.MemberId");
                sqlSelect.AddLeftJoin("member m","h.MemberId","m.MemberId");
                sqlSelect.SetOrderBY("h.HistoryId", SQLSelect.OrderByEnum.DESC);
                sqlSelect.AddWhere($"h.MemberId={memberId}");
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                var list = db.ExecuteStoreQuery<HistoryGVByMember>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<HistoryGVByMember>>.CreateSuccess(new GridViewModel<HistoryGVByMember>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static History Factory(BaseHistory req)
        {
            if(req is HistoryWalletMemberChangesByAdmin)
            {
                var payload = req as HistoryWalletMemberChangesByAdmin;
                return new History {
                    MemberId = payload.MemberId,
                    AdminId = payload.AdminId,
                    TransactionAmount = payload.TransactionAmount,
                    TransactionTypeId = payload.TransactionTypeId,
                    CreateDate = DateTime.Now,
                    StatusId = 1,           
                    InitAmount = payload.InitAmount
                };
            }
            else if (req is HistoryCommissionRegisterNewMember)
            {
                var payload = req as HistoryCommissionRegisterNewMember;
                return new History
                {
                    MemberId = payload.MemberId,
                    AdminId = payload.AdminId,
                    TransactionAmount = payload.TransactionAmount,
                    TransactionTypeId = payload.TransactionTypeId,
                    CreateDate = DateTime.Now,
                    StatusId = 1,
                    InitAmount = payload.InitAmount
                };
            }
            else if (req is HistoryCommissionRegisterReferrer)
            {
                var payload = req as HistoryCommissionRegisterReferrer;
                return new History
                {
                    MemberId = payload.MemberId,
                    AdminId = payload.AdminId,
                    TransactionAmount = payload.TransactionAmount,
                    TransactionTypeId = payload.TransactionTypeId,
                    CreateDate = DateTime.Now,
                    StatusId = 1,
                    InitAmount = payload.InitAmount
                };
            }
            else if (req is HistoryPaidWithdrawal)
            {
                var payload = req as HistoryPaidWithdrawal;
                return new History
                {
                    MemberId = payload.MemberId,
                    AdminId = payload.AdminId,
                    TransactionAmount = payload.TransactionAmount,
                    TransactionTypeId = payload.TransactionTypeId,
                    CreateDate = DateTime.Now,
                    StatusId = 1,
                    InitAmount = payload.InitAmount
                };
            }

            return null;
        }
    }
}
