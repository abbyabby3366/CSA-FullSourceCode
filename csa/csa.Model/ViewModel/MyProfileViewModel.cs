using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class MyProfileViewModel
    {
        public MyProfileViewModel(MemberProfileDetails member, MyProfileBankDetails bank, decimal myWallet, int myReferral, string referralLink)
        {
            Member = member;
            Bank = bank;
            MyWallet = myWallet;
            MyReferral = myReferral;
            ReferralLink = referralLink;
        }

        public MemberProfileDetails Member { get; set; }
        public MyProfileBankDetails Bank { get; set; }
        public decimal MyWallet { get; set; }
        public int MyReferral { get; set; }
        public string ReferralLink { get; set; }
    }
}
