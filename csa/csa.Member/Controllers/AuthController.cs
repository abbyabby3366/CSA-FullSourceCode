using csa.DataLogic;
using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace csa.Member.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public ActionResult Login(RequestLoginMember req)
        {
            try
            {
                req.PhoneNumber = new csa.Library.Phone(req.PhoneNumber).RemovePrefix("0").RemovePrefix("60").GetResult();
                return Json(MemberBiz.Login(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));                
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(RequestRegisterMember req)
        {
            try
            {
                var validator = new RegisterMemberValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                req.PhoneNumber = new csa.Library.Phone(req.PhoneNumber).RemovePrefix("0").RemovePrefix("60").GetResult();
                var res = await MemberBiz.RegisterFromMember(req);
                if(!res.Error)
                {
                    string otpCode = hudanLibrary.Security.GenerateNumeric(6);
                    var bulk360 = new Bulk360(new Bulk360Otp("YABAM", new csa.Library.Phone(res.ObjVal).AddPrefix("60").GetResult(), otpCode));
                    try
                    {
                        var resSend = await bulk360.Send();
                        new hudanLibrary.Logger("bulk360").Log(Newtonsoft.Json.JsonConvert.SerializeObject(resSend));
                    }
                    catch (Exception ex)
                    {
                        new hudanLibrary.Logger("bulk360").Log(ex);
                    }
                }
                return Json(res);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult ForgotPassword(RequestForgotPasswordMember req)
        {
            try
            {
                return Json(MemberBiz.ForgotPassword(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}