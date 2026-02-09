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
    public class TagController : Controller
    {
        [HttpPost]
        public ActionResult GetTagGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(2, "Label", "t.Label", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.ASC);
                orderMap.Add(3, "CreateDate", "t.CreateDate");
                orderMap.Add(4, "Status", "t.StatusId");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(TagBiz.GetGV(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateTag(RequestNewTag req)
        {
            try
            {
                var validator = new NewTagValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                return Json(TagBiz.CreateTag(req));
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
        public ActionResult UpdateTag(RequestUpdateTag req)
        {
            try
            {
                var validator = new UpdateTagValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                return Json(TagBiz.UpdateTag(req));
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
        public ActionResult DeleteTag(RequestDeleteTag req)
        {
            try
            {
                return Json(TagBiz.DeleteTag(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
        }
    }
}