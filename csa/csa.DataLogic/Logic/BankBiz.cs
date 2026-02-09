using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class BankBiz
    {
        public enum Status
        {
            Active = 1
        }
        public static List<DropdownItem> GetAllDropdown()
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Banks.OrderBy(x=>x.Index).AsEnumerable().Select(s=> new DropdownItem(s.BankId.ToString(),s.Name)).ToList();
            }
        }

        public static List<DropdownItem> GetActiveDropdown()
        {
            using (CsaEntities db = new CsaEntities())
            {
                int statusId = (int)Status.Active;
                return db.Banks.Where(x=>x.StatusId == statusId).OrderBy(x => x.Index).AsEnumerable().Select(s => new DropdownItem(s.BankId.ToString(), s.Name)).ToList();
            }
        }

        public static Bank Get(int bankId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Banks.FirstOrDefault(x => x.BankId == bankId);
            }
        }
    }
}
