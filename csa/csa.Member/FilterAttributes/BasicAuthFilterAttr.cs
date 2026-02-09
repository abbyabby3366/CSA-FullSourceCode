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
    public class BasicAuthFilterAttr : ActionFilterAttribute
    {
        //BasicAuthentication :- { "Username": "quikBasicAuth", "Password": "P@55w0rd123" }
        public static string AuthorizationKey = @"cXVpa0Jhc2ljQXV0aDpQQDU1dzByZDEyMw==";

        public BasicAuthFilterAttr()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.Request.Headers["Authorization"] == null)
            {
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationString = string.Empty;

                string headerAuth = ctx.Request.Headers["Authorization"];
                List<string> chunks = headerAuth.Split(' ').ToList();

                chunks.ForEach(x => { if (x.Equals("basic", StringComparison.InvariantCultureIgnoreCase)) { authenticationString = chunks.Last(); return; } });

                // Validate
                if (authenticationString != AuthorizationKey)
                {
                    // returns unauthorized error  
                    filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}