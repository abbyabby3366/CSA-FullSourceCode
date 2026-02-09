using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class AudienceGroupBiz
    {
        public static List<DropdownItem> GetAllDropdown()
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Audiencegroups.OrderBy(x => x.Name).AsEnumerable().Select(s => new DropdownItem(s.AudienceGroupId.ToString(), s.Name)).ToList();
            }
        }
    }
}
