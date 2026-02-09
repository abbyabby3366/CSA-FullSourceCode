using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class DashboardAdminViewModel
    {
        public DashboardAdminViewModel(GridViewModel<MemberGVByAdmin> memberApprovals, GridViewModel<WithdrawalRequestGVByAdmin> withdrawals, List<TopReferrals> listTopReferrals)
        {
            MemberApprovals = memberApprovals;
            Withdrawals = withdrawals;
            ListTopReferrals = listTopReferrals;
        }

        public GridViewModel<MemberGVByAdmin> MemberApprovals { get; set; }
        public GridViewModel<WithdrawalRequestGVByAdmin> Withdrawals { get; set; }
        public List<TopReferrals> ListTopReferrals { get; set; }
    }
}
