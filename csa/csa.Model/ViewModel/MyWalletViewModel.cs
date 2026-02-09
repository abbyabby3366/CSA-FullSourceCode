using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class MyWalletViewModel
    {
        public MyWalletViewModel(decimal myWallet)
        {
            MyWallet = myWallet;
        }

        public decimal MyWallet { get; set; }
    }
}
