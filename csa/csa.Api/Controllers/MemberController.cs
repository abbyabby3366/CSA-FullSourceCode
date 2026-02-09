using csa.Api.FilterAttributes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using csa.Api.Helpers;
using csa.Library;
using csa.DataLogic;
using csa.Model.DataObject;
using System.Threading.Tasks;

namespace csa.Api.Controllers
{
    [RoutePrefix("Banner")]
    public class MemberController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpPost]
        [BasicAuthFilterAttr]
        [Route("login")]
        public IHttpActionResult Login(HttpRequestMessage request, [FromBody] RequestLoginMember reqPostObj)
        {
            try
            {
                if (reqPostObj == null)
                { throw new ArgumentNullException(); }

                var reqParam = new GetRequest();
                reqParam.AddParam("timestamp", $"{reqPostObj.Timestamp}");
                reqParam.AddParam("phoneNumber", $"{reqPostObj.PhoneNumber}");
                reqParam.AddParam("password", $"{reqPostObj.Password}");

                HttpResponseMessage response;

                if (!SecurityHelper.VerifyRequest(reqPostObj, reqParam, out response, isSign: false))
                { return ResponseMessage(response); }

                return Json(MemberBiz.Login(reqPostObj));
            }
            catch(ArgumentNullException)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [BasicAuthFilterAttr]
        [Route("register")]
        public async Task<IHttpActionResult> Register(HttpRequestMessage request, [FromBody] RequestRegisterMember reqPostObj)
        {
            try
            {
                if (reqPostObj == null)
                { throw new ArgumentNullException(); }

                var reqParam = new GetRequest();
                reqParam.AddParam("timestamp", $"{reqPostObj.Timestamp}");
                reqParam.AddParam("phoneNumber", $"{reqPostObj.PhoneNumber}");
                reqParam.AddParam("password", $"{reqPostObj.Password}");

                HttpResponseMessage response;

                if (!SecurityHelper.VerifyRequest(reqPostObj, reqParam, out response, isSign: false))
                { return ResponseMessage(response); }

                return Json(await MemberBiz.RegisterFromMember(reqPostObj));
            }
            catch (ArgumentNullException)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [BasicAuthFilterAttr]
        [Route("forgotPassword")]
        public IHttpActionResult ForgotPassword(HttpRequestMessage request, [FromBody] RequestForgotPasswordMember reqPostObj)
        {
            try
            {
                if (reqPostObj == null)
                { throw new ArgumentNullException(); }

                var reqParam = new GetRequest();
                reqParam.AddParam("timestamp", $"{reqPostObj.Timestamp}");
                reqParam.AddParam("phoneNumber", $"{reqPostObj.PhoneNumber}");

                HttpResponseMessage response;

                if (!SecurityHelper.VerifyRequest(reqPostObj, reqParam, out response, isSign: false))
                { return ResponseMessage(response); }

                return Json(MemberBiz.ForgotPassword(reqPostObj));
            }
            catch (ArgumentNullException)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
