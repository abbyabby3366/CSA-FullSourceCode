using csa.DataLogic;
using csa.Member.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace csa.Member.Controllers
{
    public class HistoryController : Controller
    {
        [HttpPost]
        public ActionResult GetMemberGV(int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                return Json(HistoryBiz.GetGVByMember(SessionManager.CurrentLoginMember.MemberId, pageIndex, length).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}