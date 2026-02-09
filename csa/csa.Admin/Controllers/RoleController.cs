using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

using csa.Admin.FilterAttributes;
using csa.Admin.Models;
using csa.Data.EntityModel;
using csa.Data.Logic;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using csa.DataLogic;
using System.Net;
using System.Threading.Tasks;
using csa.Library;

namespace csa.Admin.Controllers
{
    public class RoleController : Controller
    {
        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetAllAdminRoleGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
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
                Expression<Func<user, bool>> predicate = (w =>
                    w.FirstName.Contains(SearchText) || w.LastName.Contains(SearchText) || w.ICNumber.Contains(SearchText) || w.PhoneNo.Contains(SearchText)
                );

                //get `new-member-approval`
                RespArgs<GridViewModel<AdminRoleUsersGVByAdminModel>> retObj = RoleLogic.GetAllAdminRoleUsersGVByAdmin(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("RoleController.GetAllAdminRoleGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<AdminRoleUsersGVByAdminModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public ActionResult GetAdminsGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "CreateDate", "a.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                orderMap.Add(5, "Role", "r.Name");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(AdminBiz.GetRequestGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<ActionResult> CreateAsync(RequestNewRole req)
        {
            try
            {
                var validator = new NewRoleValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(await RoleBiz.Create(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<int>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<ActionResult> UpdateAsync(RequestUpdateRole req)
        {
            try
            {
                var validator = new UpdateRoleValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(await RoleBiz.Update(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
        }
    }
}