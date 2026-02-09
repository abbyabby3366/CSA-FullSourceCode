using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

using csa.Member.FilterAttributes;
using csa.Member.Models;
using csa.Data.EntityModel;
using csa.Data.Logic;
using csa.Model;

namespace csa.Member.Controllers
{
    public class AgentController : Controller
    {
        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetAgentGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
        {
            try
            {
                ////get session
                //LoginAdminModel session = SessionManager.AdminSession;

                //get post value
                var dtReqColumns = JsonConvert.DeserializeObject<List<GridViewDataColumnModel>>(Columns);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(Order);
                var dtReqSearch = JsonConvert.DeserializeObject<GridViewDataSearchModel>(Search);

                //get page
                int page = (Start / Length) + 1;

                //get 1st sort attributes
                int idx = dtReqOrder.FirstOrDefault() == null ? 0 : dtReqOrder.FirstOrDefault().column;

                //get sort-field-name
                string sortExpression = dtReqColumns.ElementAt(idx).data;

                //get sort-direction
                int sortDirection = (dtReqOrder.FirstOrDefault() == null) ? (int)SortDirection.Ascending : (dtReqOrder.FirstOrDefault().dir == "asc" ? (int)SortDirection.Ascending : (int)SortDirection.Descending);

                //get predicate
                Expression<Func<agent, bool>> predicate = (w =>
                    w.UserData.Code.Contains(SearchText) || w.UserData.FirstName.Contains(SearchText) || w.UserData.LastName.Contains(SearchText)
                );

                //get `new-member-approval`
                RespArgs<GridViewModel<AgentGVByMemberModel>> retObj = AgentLogic.GetAgentGVByMember(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("AgentController.GetAgentGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<AgentGVByMemberModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}