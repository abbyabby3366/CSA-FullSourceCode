using csa.DataLogic;
using csa.Model;
using csa.Model.DataObject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace csa.Admin.Controllers
{
    public class AuthController : Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        public ActionResult Login(RequestLoginAdmin req)
        {
            try
            {
                return Json(AdminBiz.Login(req));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Json(RespArgs<LoginAdmin>.CreateError(ex.Message));
            }
        }
    }
}