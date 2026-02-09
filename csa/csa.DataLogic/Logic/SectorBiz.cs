using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class SectorBiz
    {
        public static List<Sector> Gets()
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Sectors.ToList();
            }
        }
    }
}
