using csa.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class HistoryGVByMember
    {
        public int HistoryId { get; set; }
        public string MemberCode { get; set; }
        public int MemberId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription((HistoryType)Enum.ToObject(typeof(HistoryType), TransactionTypeId));
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        public decimal? TransactionAmount { get; set; }
    }

    public abstract class BaseHistory
    {
        protected BaseHistory(decimal transactionAmount, int transactionTypeId, decimal initAmount)
        {
            TransactionAmount = transactionAmount;
            TransactionTypeId = transactionTypeId;
            InitAmount = initAmount;
        }

        public decimal TransactionAmount { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal InitAmount { get; set; }
    }

    public class HistoryWalletMemberChangesByAdmin : BaseHistory
    {
        public HistoryWalletMemberChangesByAdmin(decimal transactionAmount,decimal initAmount, long memberId, int adminId) : base(transactionAmount, (int)HistoryType.ADMIN_CHANGES, initAmount)
        {
            MemberId = memberId;
            AdminId = adminId;
        }

        public long MemberId { get; set; }
        public int AdminId { get; set; }        
    }

    public class HistoryCommissionRegisterNewMember : BaseHistory
    {
        public HistoryCommissionRegisterNewMember(decimal transactionAmount, decimal initAmount, long memberId, int adminId) : base(transactionAmount, (int)HistoryType.COMMISSION_SURVEY, initAmount)
        {
            MemberId = memberId;
            AdminId = adminId;
        }

        public long MemberId { get; set; }
        public int AdminId { get; set; }
    }

    public class HistoryCommissionRegisterReferrer : BaseHistory
    {
        public HistoryCommissionRegisterReferrer(decimal transactionAmount, decimal initAmount, long memberId, int adminId) : base(transactionAmount, (int)HistoryType.COMMISSION_REFERRAL, initAmount)
        {
            MemberId = memberId;
            AdminId = adminId;
        }

        public long MemberId { get; set; }
        public int AdminId { get; set; }
    }

    public class HistoryPaidWithdrawal : BaseHistory
    {
        public HistoryPaidWithdrawal(decimal transactionAmount, decimal initAmount, long memberId, int adminId) : base(transactionAmount, (int)HistoryType.WITHDRAWAL, initAmount)
        {
            MemberId = memberId;
            AdminId = adminId;
        }

        public long MemberId { get; set; }
        public int AdminId { get; set; }
    }
}
