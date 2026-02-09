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
using System.Web;
using System.Web.Mvc;

namespace csa.Admin.Controllers
{
    public class TmplateemailController : Controller
    {
        [HttpPost]
        public ActionResult GetEmailTemplateGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "Name", "et.Name", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.ASC);
                orderMap.Add(3, "CreateDate", "et.CreateDate");
                orderMap.Add(4, "Status", "et.StatusId");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(EmailTemplateBiz.GetGV(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Create(RequestNewEmailTemplate req)
        {
            try
            {
                var validator = new NewEmailTemplateValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                return Json(EmailTemplateBiz.Create(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<int>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Update(RequestUpdateEmailTemplate req)
        {
            try
            {
                var validator = new UpdateEmailTemplateValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                return Json(EmailTemplateBiz.Update(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<int>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                return Json(EmailTemplateBiz.Delete(id));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
        }
    }
}