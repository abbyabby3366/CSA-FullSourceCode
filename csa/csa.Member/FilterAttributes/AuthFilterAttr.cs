using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

using csa.Data.Cache;
using csa.Model;

namespace csa.Member.FilterAttributes
{
    public class AuthFilterAttr : ActionFilterAttribute
    {
        public AuthFilterAttr()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.Session["AdminLoginSession"] == null)
            { filterContext.Result = new HttpUnauthorizedResult(); }

            //try
            //{
            //    LoginAdminModel session = (LoginAdminModel)ctx.Session["AdminLoginSession"];
            //    AdminSessionCacheModel cached = SessionCache.GetSessionCacheByKey<AdminSessionCacheModel>(session.ASId);

            //    if (cached == null)
            //    {
            //        filterContext.Result = new HttpUnauthorizedResult();
            //        //filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            //    }
            //}
            //catch { }

            base.OnActionExecuting(filterContext);
        }
    }
}