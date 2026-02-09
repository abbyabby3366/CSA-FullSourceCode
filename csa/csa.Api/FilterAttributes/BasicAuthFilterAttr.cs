using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace csa.Api.FilterAttributes
{
    public class BasicAuthFilterAttr : AuthorizationFilterAttribute
    {
        //BasicAuthentication :- { "Username": "quikBasicAuth", "Password": "P@55w0rd123" }
        public static string AuthorizationKey = @"cXVpa0Jhc2ljQXV0aDpQQDU1dzByZDEyMw==";

        public override void OnAuthorization(HttpActionContext ActionContext)
        {
            if (ActionContext.Request.Headers.Authorization == null)
            {
                ActionContext.Response = ActionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationString = string.Empty;

                if (ActionContext.Request.Headers.Authorization.Scheme.Equals("basic", StringComparison.InvariantCultureIgnoreCase))
                { authenticationString = ActionContext.Request.Headers.Authorization.Parameter; }
                else
                { authenticationString = ActionContext.Request.Headers.Authorization.Scheme; }

                // Validate
                if (authenticationString != AuthorizationKey)
                {
                    // returns unauthorized error  
                    ActionContext.Response = ActionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }

            base.OnAuthorization(ActionContext);
        }
    }
}