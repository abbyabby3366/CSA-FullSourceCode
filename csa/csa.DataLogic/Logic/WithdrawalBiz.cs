using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace csa.DataLogic
{
    public static class WithdrawalBiz
    {
        public static RespArgs<GridViewModel<WithdrawalGVByMember>> GetGVByMember(int pageIndex,int pageSize)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("withdrawal w");
                sqlSelect.AddSelect("w.WithdrawalId,w.BankAccountNumber,w.CreateDate,w.ResponseDate,w.StatusId,w.Amount");
                sqlSelect.SetOrderBY("w.WithdrawalId", SQLSelect.OrderByEnum.DESC);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                var list = db.ExecuteStoreQuery<WithdrawalGVByMember>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<WithdrawalGVByMember>>.CreateSuccess(new GridViewModel<WithdrawalGVByMember>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<WithdrawalRequestGVByAdmin>> GetRequestGVByAdmin(string search,int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("withdrawal w");
                sqlSelect.AddSelect("w.WithdrawalId,w.BankAccountNumber,w.CreateDate,w.StatusId,w.Amount,m.MemberId,CONCAT(IFNULL(m.FirstName,''),' ',IFNULL(m.LastName,'')) as MemberName");
                sqlSelect.AddJoin("member m", "w.MemberId", "m.MemberId");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);
                sqlSelect.AddWhere($"w.StatusId IN({(int)WithdrawStatus.PENDING},{(int)WithdrawStatus.PROCESSING})");

                if (!search.IsEmpty()) sqlSelect.AddWhere($"w.BankAccountNumber LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<WithdrawalRequestGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<WithdrawalRequestGVByAdmin>>.CreateSuccess(new GridViewModel<WithdrawalRequestGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<WithdrawalHistoryGVByAdmin>> GetHistoryGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("withdrawal w");
                sqlSelect.AddSelect("w.WithdrawalId,w.ResponseDate,w.StatusId,w.Amount,m.MemberId,m.MemberCode,CONCAT(IFNULL(m.FirstName,''),' ',IFNULL(m.LastName,'')) as MemberName");
                sqlSelect.AddJoin("member m", "w.MemberId", "m.MemberId");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);
                sqlSelect.AddWhere($"w.StatusId IN({(int)WithdrawStatus.PAID},{(int)WithdrawStatus.CANCEL})");

                if (!search.IsEmpty()) sqlSelect.AddWhere($"w.MemberId LIKE '%{search}%'");

                var list = db.ExecuteStoreQuery<WithdrawalHistoryGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<WithdrawalHistoryGVByAdmin>>.CreateSuccess(new GridViewModel<WithdrawalHistoryGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static async Task<RespArgs<bool>> PaidWithdrawal(RequestApproveWithdrawal req)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var withdrawal = db.Withdrawals.FirstOrDefault(x => x.WithdrawalId == req.WithdrawalId);
                    if (withdrawal == null) throw new ArgumentException("withdrawal not found");

                    var member = db.Members.FirstOrDefault(x => x.MemberId == withdrawal.MemberId);
                    if (member.WalletCash < withdrawal.Amount) throw new ArgumentException("Insufficient cash");

                    withdrawal.StatusId = (int)WithdrawStatus.PAID;
                    withdrawal.AdminId = req.AdminId;
                    withdrawal.ResponseDate = DateTime.Now;
                    withdrawal.AdminNote = req.Notes;

                    //minus wallet member
                    member.WalletCash = (member.WalletCash ?? 0) - withdrawal.Amount;
                    db.Histories.AddObject(HistoryBiz.Factory(new HistoryPaidWithdrawal((withdrawal.Amount ?? 0) * -1, member.WalletCash ?? 0, member.MemberId, req.AdminId)));

                    await db.SaveChangesAsync();
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task ProcessingWithdrawalByMemberIdWhenAdminApprove(CsaEntities db, RequestProcessingWithdrawal req)
        {
            var withdrawal = db.Withdrawals.FirstOrDefault(x => x.MemberId == req.MemberId);
            if (withdrawal == null) throw new ArgumentException("withdrawal not found");

            withdrawal.StatusId = (int)WithdrawStatus.PROCESSING;

            var withdrawalParent = db.Withdrawals.FirstOrDefault(x => x.ParentMemberId == req.MemberId);
            if (withdrawalParent != null)
            {
                withdrawalParent.StatusId = (int)WithdrawStatus.PROCESSING;
            }

            await db.SaveChangesAsync();
        }

        public static RespArgs<bool> CancelWithdrawal(RequestRejectWithdrawal req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var withdrawal = db.Withdrawals.FirstOrDefault(x => x.WithdrawalId == req.WithdrawalId);
                if (withdrawal == null) throw new ArgumentException("withdrawal_not_found");

                withdrawal.StatusId = (int)WithdrawStatus.CANCEL;
                withdrawal.AdminId = req.AdminId;
                withdrawal.ResponseDate = DateTime.Now;
                withdrawal.AdminNote = req.Notes;
                db.SaveChanges();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static Withdrawal Factory(RequestNewWithdrawal req)
        {
            return new Withdrawal
            {
                MemberId = req.MemberId,
                Amount = req.Amount,
                BankId = req.BankId,
                BankAccountName = req.BankAccountName,
                BankAccountNumber = req.BankAccountNumber,
                CreateDate = req.CreateDate,
                StatusId = (int)WithdrawStatus.PENDING,
                ParentMemberId = req.ParentMemberId
            };
        }
    }

    public class RequestNewWithdrawal
    {
        public RequestNewWithdrawal(long memberId, int? adminId, decimal amount, int? bankId, string bankAccountName, string bankAccountNumber, long? parentMemberId, DateTime createDate)
        {
            MemberId = memberId;
            AdminId = adminId;
            Amount = amount;
            BankId = bankId;
            BankAccountName = bankAccountName;
            BankAccountNumber = bankAccountNumber;
            ParentMemberId = parentMemberId;
            CreateDate = createDate;
        }

        public long MemberId { get; set; }
        public long? ParentMemberId { get; set; }
        public int? AdminId { get; set; }
        public decimal Amount { get; set; }
        public int? BankId { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class RequestProcessingWithdrawal
    {
        public RequestProcessingWithdrawal(long memberId)
        {
            MemberId = memberId;
        }

        public long MemberId { get; set; }
    }
}
