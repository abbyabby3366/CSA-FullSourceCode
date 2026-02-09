using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class SettingBiz
    {
        public static Setting Get(string code)
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Settings.FirstOrDefault(x => x.Code == code);
            }
        }
    }
}
