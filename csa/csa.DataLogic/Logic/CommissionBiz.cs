using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class CommissionBiz
    {
        public static decimal CommissionRegisterNewMember
        {
            get
            {
                var setting = SettingBiz.Get("CommissionRegisterNewMember");
                if (setting == null) throw new Exception("CommissionRegisterNewMember not found");

                decimal commission = 0;
                decimal.TryParse(setting.StrValue, out commission);

                return commission;
            }
        }

        public static decimal CommissionRegisterUpline
        {
            get
            {
                var setting = SettingBiz.Get("CommissionRegisterUpline");
                if (setting == null) throw new Exception("CommissionRegisterUpline not found");

                decimal commission = 0;
                decimal.TryParse(setting.StrValue, out commission);

                return commission;
            }
        }
    }
}
