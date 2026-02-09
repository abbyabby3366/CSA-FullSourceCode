using csa.Library;
using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class CountryBiz
    {
        public static List<CountryDisplay> GetAllDisplay()
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Countries.OrderBy(x => x.Name).AsEnumerable().Select(s => new CountryDisplay(s.CountryId, s.Name,s.Iso2,s.Currency,s.PhoneCode,s.IsActive)).ToList();
            }
        }

        public static List<CountryDisplay> GetActiveDisplay()
        {
            using (CsaEntities db = new CsaEntities())
            {
                int statusId = (int)GlobalStatus.ACTIVE;
                return db.Countries.Where(x => x.IsActive == statusId).OrderBy(x => x.Name).AsEnumerable().Select(s => new CountryDisplay(s.CountryId, s.Name, s.Iso2, s.Currency, s.PhoneCode, s.IsActive)).ToList();
            }
        }

        public static Country Get(int countryId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Countries.FirstOrDefault(x => x.CountryId == countryId);
            }
        }
    }
}
