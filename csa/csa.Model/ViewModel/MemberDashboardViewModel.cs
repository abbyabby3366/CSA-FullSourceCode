using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class MemberDashboardViewModel
    {
        public MemberDashboardViewModel(decimal myWalletNow, decimal myWalletLast, int myReferralNow, int myReferralLast, GridViewModel<DashboardReferralGVByMember> lastReferrals, GridViewModel<HistoryGVByMember> lastHistories, GridViewModel<DashboardAnnouncementByMember> listAnnouncement, decimal savingsNow)
        {
            MyWalletNow = myWalletNow;
            MyWalletLast = myWalletLast;
            MyReferralNow = myReferralNow;
            MyReferralLast = myReferralLast;
            LastReferrals = lastReferrals;
            LastHistories = lastHistories;
            ListAnnouncement = listAnnouncement;
            SavingsNow = savingsNow;
        }

        public decimal MyWalletNow { get; set; }
        public decimal MyWalletLast { get; set; }
        public int MyReferralNow { get; set; }
        public int MyReferralLast { get; set; }
        public GridViewModel<DashboardReferralGVByMember> LastReferrals { get; set; }
        public GridViewModel<HistoryGVByMember> LastHistories { get; set; }
        public GridViewModel<DashboardAnnouncementByMember> ListAnnouncement { get; set; }
        public decimal SavingsNow { get; set; }
    }
}
