using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class StateBiz
    {
        public static List<StateDisplay> GetAllDisplay()
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.States.OrderBy(x => x.Name).AsEnumerable().Select(s => new StateDisplay(s.StateId,s.Code, s.Name,s.CountryId)).ToList();
            }
        }

        public static State Get(int stateId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.States.FirstOrDefault(x => x.StateId == stateId);
            }
        }
    }
}
