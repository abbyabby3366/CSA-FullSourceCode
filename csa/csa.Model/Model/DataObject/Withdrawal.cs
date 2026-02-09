using csa.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class WithdrawalGVByMember
    {
        public int WithdrawalId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal? Amount { get; set; }
        public int? StatusId { get; set; }
    }

    public class WithdrawalRequestGVByAdmin
    {
        public int WithdrawalId { get; set; }
        public long MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime CreateDate { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal? Amount { get; set; }
        public int? StatusId { get; set; }
        public bool AllowPaid { get => StatusId == (int)WithdrawStatus.PROCESSING; }
        public bool AllowCancel { get => StatusId == (int)WithdrawStatus.PROCESSING; }
    }

    public class WithdrawalHistoryGVByAdmin
    {
        public int WithdrawalId { get; set; }
        public long MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberCode { get; set; }
        public DateTime? ResponseDate { get; set; }
        public decimal? Amount { get; set; }
        public int? StatusId { get; set; }
        public string TransactionType => HistoryType.WITHDRAWAL.GetDescription();
    }

    public class RequestApproveWithdrawal
    {
        public RequestApproveWithdrawal()
        {

        }
        public RequestApproveWithdrawal(int withdrawalId, int adminId, string notes)
        {
            WithdrawalId = withdrawalId;
            AdminId = adminId;
            Notes = notes;
        }

        public int WithdrawalId { get; set; }
        public int AdminId { get; set; }
        public string Notes { get; set; }
    }

    public class RequestRejectWithdrawal : RequestApproveWithdrawal
    {
        public RequestRejectWithdrawal()
        {

        }
        public RequestRejectWithdrawal(int withdrawalId, int adminId, string notes) : base(withdrawalId, adminId, notes)
        {
        }
    }
}
