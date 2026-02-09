using csa.Admin.Models;
using csa.DataLogic;
using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace csa.Admin.Controllers
{
    public class AdminController : Controller
    {
        [HttpPost]
        public ActionResult ChangePassword(RequestAdminChangePassword req)
        {
            try
            {
                var validator = new AdminChangePasswordValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(AdminBiz.ChangePassword(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        [ActionName("changeRole")]
        public async Task<ActionResult> ChangeRoleAsync(RequestAdminChangeRole req)
        {
            try
            {
                return Json(await AdminBiz.ChangeRole(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<ActionResult> CreateAsync(RequestNewAdmin req)
        {
            try
            {
                var validator = new NewAdminValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(await AdminBiz.Create(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<ActionResult> UpdateAsync(RequestUpdateAdmin req)
        {
            try
            {
                var validator = new UpdateAdminValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(await AdminBiz.Update(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        [ActionName("changePasswordByAdmin")]
        public ActionResult ChangePasswordByAdmin(RequestAdminChangePasswordByAdmin req)
        {
            try
            {
                var validator = new AdminChangePasswordByAdminValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(AdminBiz.ChangePasswordByAdmin(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
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
                orderMap.Add(0, "Name", "a.Name");
                orderMap.Add(1, "Email", "a.Email");
                orderMap.Add(2, "IsSuperAdmin", "a.IsSuperAdmin");
                orderMap.Add(3, "StatusId", "a.StatusId");
                orderMap.Add(4, "CreateDate", "a.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(AdminBiz.GetGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetAdminSelectItems(string q, int? exceptId)
        {
            var admins = AdminBiz.GetAllWithSearch(q, exceptId);

            return Json(admins.OrderBy(x => x.Name).Select(x => new hudanLibrary.Data.SelectItem<long>(x.AdminId, $"{x.Name} ({x.Email})")).ToList());
        }
    }
}