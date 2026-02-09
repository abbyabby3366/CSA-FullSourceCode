using csa.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace csa.Member.Controllers
{
    public class WithdrawalController : Controller
    {
        [HttpPost]
        public ActionResult GetMemberGV(int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                return Json(WithdrawalBiz.GetGVByMember(pageIndex, length).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}