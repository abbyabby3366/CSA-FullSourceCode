using csa.DataLogic;
using csa.DataLogic.Library;
using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace csa.Member.Controllers
{
    public class SurveyCtrlController : Controller
    {
        //[HttpPost]
        //public async Task<ActionResult> CompleteSurvey1Async(RequestNewSurveyByMember req)
        //{
        //    try
        //    {

        //        var data = JsonConvert.DeserializeObject<RequestNewSurveyDataByMember>(req.Json);
        //        data.Login.IsLogged = Helpers.SessionManager.CurrentLoginMember != null;
        //        var validator = new NewSurveyByMemberValidator();
        //        var resValidator = validator.Validate(data);
        //        if (!resValidator.IsValid)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            foreach (var item in resValidator.Errors)
        //            {
        //                sb.AppendLine($"<p>{item.ErrorMessage}</p>");
        //            }

        //            if(req.IcFile == null) sb.AppendLine($"<p>IC required</p>");
        //            if(req.PayslipFile == null) sb.AppendLine($"<p>Payslip required</p>");

        //            throw new ArgumentException(sb.ToString());
        //        }

        //        Helpers.UploadHelper.Upload(req.IcFile, FileHelper.FileDir.IcFileDir, out var icFile);
        //        Helpers.UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.MemberDir, out var payslipFile);
        //        Helpers.UploadHelper.Upload(req.OfferLetterFile, FileHelper.FileDir.MemberDir, out var offerLetterFile);

        //        var res = await MemberBiz.SurveyAsync(data, icFile, payslipFile, offerLetterFile);
        //        if(!res.Error)
        //        {
        //            //remove session survey
        //            Helpers.SessionManager.SetAccountMemberSurvey(null);
        //        }
        //        return Json(res);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return Json(RespArgs<long>.CreateError(ex.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}


        [HttpPost]
        public async Task<ActionResult> CompleteSurvey1Async(RequestNewSurvey1ByMember req)
        {
            try
            {

                var data = JsonConvert.DeserializeObject<RequestNewSurvey1DataByMember>(req.Json);
                data.IsLogged = Helpers.SessionManager.CurrentLoginMember != null;
                var validator = new NewSurvey1ByMemberValidator();
                var resValidator = validator.Validate(data);
                StringBuilder sb = new StringBuilder();
                if (!resValidator.IsValid)
                {
                    foreach (var item in resValidator.Errors)
                    {
                        sb.AppendLine($"<p>{item.ErrorMessage}</p>");
                    }                    

                    throw new ArgumentException(sb.ToString());
                }

                //request upload optional
                //if (req.IcFile == null) sb.AppendLine($"<p>IC diperlukan</p>");
                //if (req.PayslipFile == null) sb.AppendLine($"<p>Slip gaji diperlukan</p>");

                string[] allowedExtension = { ".png", ".jpg", ".jpeg", ".pdf" };
                if (req.IcFile != null)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(req.IcFile.FileName);
                    if (!allowedExtension.Contains(fi.Extension.ToLower())) sb.AppendLine($"<p>Not allowed extension</p>");
                }

                if (req.PayslipFile != null)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(req.PayslipFile.FileName);
                    if (!allowedExtension.Contains(fi.Extension.ToLower())) sb.AppendLine($"<p>Not allowed extension</p>");
                }

                if (sb.ToString().IsNotEmpty())
                {
                    throw new ArgumentException(sb.ToString());
                }

                Helpers.UploadHelper.Upload(req.IcFile, FileHelper.FileDir.IcFileDir, out var icFile);
                Helpers.UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.MemberDir, out var payslipFile);

                string linkLogin = $"{Request.Url.Scheme}://{Request.Url.Authority}/SignIn.aspx";

                var res = await MemberBiz.SurveyAsync(data, icFile, payslipFile,linkLogin);
                if (!res.Error)
                {
                    //remove session survey
                    Helpers.SessionManager.SetAccountMemberSurvey(null);

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
                return Json(RespArgs<string>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> RegisterMemberBySurvey(RequestNewMemberSurvey req)
        {
            try
            {
                var validator = new NewSurveyAccountByMemberValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in resValidator.Errors)
                    {
                        sb.AppendLine($"<p>{item.ErrorMessage}</p>");
                    }

                    throw new ArgumentException(sb.ToString());
                }

                req.PhoneNumber = new csa.Library.Phone(req.PhoneNumber).RemovePrefix("0").RemovePrefix("60").GetResult();

                var findMemberNewSurvey = MemberBiz.Get(req.PhoneNumber);
                if(findMemberNewSurvey != null)
                {
                    if(findMemberNewSurvey.StatusId != (int)MemberStatus.DRAFT)
                    {
                        throw new ArgumentException("Account already exist");
                    }

                    await MemberBiz.UpdateMemberBySurvey(req);
                    var sessionData = new ResponseNewMemberSurvey(req.FullName, req.PhoneNumber, findMemberNewSurvey.MemberId);
                    Helpers.SessionManager.SetAccountMemberSurvey(sessionData);
                    return Json(RespArgs<ResponseNewMemberSurvey>.CreateSuccess(sessionData));
                }
                else
                {                    
                    var resMember = await MemberBiz.RegisterMemberBySurvey(req);
                    var sessionData = new ResponseNewMemberSurvey(resMember.ObjVal.FullName, resMember.ObjVal.PhoneNumber,resMember.ObjVal.MemberId);
                    Helpers.SessionManager.SetAccountMemberSurvey(sessionData);
                    return Json(RespArgs<ResponseNewMemberSurvey>.CreateSuccess(sessionData));
                }
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<ResponseNewMemberSurvey>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}