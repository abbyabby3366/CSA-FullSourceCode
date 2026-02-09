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
using csa.Library;

namespace csa.Admin.Controllers
{
    public class CampaignController : Controller
    {
        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetCampaignGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
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
                Expression<Func<campaign, bool>> predicate = (w => w.Name.Contains(SearchText) || w.Subject.Contains(SearchText));

                //get `new-member-approval`
                RespArgs<GridViewModel<CampaignGVByAdminModel>> retObj = CampaignLogic.GetCampaignGVByAdmin(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("CampaignController.GetCampaignGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<CampaignGVByAdminModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetAudienceTagGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
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
                Expression<Func<meta_tag, bool>> predicate = (w => w.Name.Contains(SearchText));

                //get `new-member-approval`
                RespArgs<GridViewModel<AudienceTagGVByAdminModel>> retObj = CampaignLogic.GetAudienceTagGVByAdmin(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("CampaignController.GetAudienceTagGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<AudienceTagGVByAdminModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public ActionResult Create(RequestNewCampaign req)
        {
            try
            {
                var validator = new NewCampaignValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                return Json(CampaignBiz.Create(req));
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
        public ActionResult Update(RequestUpdateCampaign req)
        {
            try
            {
                var validator = new UpdateCampaignValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                return Json(CampaignBiz.Update(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
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
                return Json(CampaignBiz.Delete(id));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public ActionResult GetGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(2, "Name", "cam.Name", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.ASC);
                orderMap.Add(3, "Subject", "cam.Subject");
                orderMap.Add(4, "Status", "cam.StatusId");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(CampaignBiz.GetGV(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}