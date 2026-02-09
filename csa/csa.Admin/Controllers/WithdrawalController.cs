using csa.Admin.Models;
using csa.DataLogic;
using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace csa.Admin.Controllers
{
    public class WithdrawalController : Controller
    {
        [HttpPost]
        public ActionResult GetWithdrawalRequestGV(string search,string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(0, "CreateDate", "w.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                orderMap.Add(1, "MemberName", "CONCAT(IFNULL(m.FirstName,''),' ',IFNULL(m.LastName,''))");
                orderMap.Add(2, "BankAccountNumber", "w.BankAccountNumber");
                orderMap.Add(3, "Amount", "w.Amount");
                orderMap.Add(4, "StatusId", "w.StatusId");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(WithdrawalBiz.GetRequestGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetWithdrawalHistoryGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(0, "WithdrawalId", "w.WithdrawalId");
                orderMap.Add(1, "MemberName", "CONCAT(IFNULL(m.FirstName,''),' ',IFNULL(m.LastName,''))");
                orderMap.Add(2, "ResponseDate", "w.ResponseDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                orderMap.Add(4, "Amount", "w.Amount");
                orderMap.Add(5, "StatusId", "w.StatusId");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(WithdrawalBiz.GetHistoryGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Paid(RequestApproveWithdrawal req)
        {
            try
            {
                return Json(WithdrawalBiz.PaidWithdrawal(req));
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
        public ActionResult Cancel(RequestRejectWithdrawal req)
        {
            try
            {
                return Json(WithdrawalBiz.CancelWithdrawal(req));
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
    }
}