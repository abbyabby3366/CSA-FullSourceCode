using csa.DataLogic;
using csa.DataLogic.Library;
using csa.Library;
using csa.Member.Helpers;
using csa.Member.Models;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace csa.Member.Controllers
{
    public class MemberController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> ChangeBankDetails(RequestChangeBankDetails req)
        {
            try
            {
                return Json(await MemberBiz.ChangeBankDetails(req));
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
        public ActionResult ChangePassword(RequestChangePassword req)
        {
            try
            {
                var validator = new ChangePasswordValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(MemberBiz.ChangePassword(req));
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
        public ActionResult ChangePersonalDetail(RequestChangePersonalDetails req)
        {
            try
            {
                var validator = new ChangePersonalDetailsValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                StringBuilder sb = new StringBuilder();
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

                CsaModel.File file = null;
                if (req.IcFile != null && req.IcFile.ContentLength > 0)
                {
                    var fileInfo = new FileInfo(req.IcFile.FileName);
                    file = new CsaModel.File
                    {
                        FileId = Guid.NewGuid().ToString(),
                        Filename = req.IcFile.FileName,
                        Extension = fileInfo.Extension,
                        Size = req.IcFile.ContentLength
                    };
                    var uploadDir = FileHelper.GetUploadPhysic(AppSettings.UploadPath,FileHelper.FileDir.IcFileDir);
                    var filePath = Path.Combine(uploadDir, file.FileId + fileInfo.Extension);

                    req.IcFile.SaveAs(filePath);
                }

                UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.MemberDir, out var payslipFile);

                return Json(MemberBiz.ChangePersonalDetails(req, file, payslipFile));
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
        public ActionResult ChangeProfilePicture(RequestChangeProfilePicture req)
        {
            try
            {
                CsaModel.File file = null;
                if (req.ImageFile != null && req.ImageFile.ContentLength > 0)
                {
                    if (!req.ImageFile.ContentType.StartsWith("image")) throw new ArgumentException("uploaded_file_is_not_an_image");
                    var fileInfo = new FileInfo(req.ImageFile.FileName);
                    file = new CsaModel.File
                    {
                        FileId = Guid.NewGuid().ToString(),
                        Filename = req.ImageFile.FileName,
                        Extension = fileInfo.Extension,
                        Size = req.ImageFile.ContentLength
                    };
                    var uploadDir = FileHelper.GetUploadPhysic(AppSettings.UploadPath, FileHelper.FileDir.ProfileFileDir);
                    var filePath = Path.Combine(uploadDir, file.FileId + fileInfo.Extension);
                    var thumbnailPath = Path.Combine(uploadDir, file.FileId + Constant.THUMBNAIL + fileInfo.Extension);

                    req.ImageFile.SaveAs(filePath);
                    ImageThumbnailGenerator.GenerateThumbnail(filePath, thumbnailPath, 250);
                }
                var result = MemberBiz.ChangeProfilePicture(req, file);
                if(!result.Error)
                {
                    //change profileId in session
                    var member = MemberBiz.Get(req.MemberId);
                    SessionManager.SetLoginMember(member.Convert());
                }
                return Json(result);
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
        public ActionResult GetReferralGVByMember(string search,string order,int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(2, "MemberCode", "m.MemberId");
                orderMap.Add(3, "CreateDate", "m.CreateDate",isDefault: true,defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                orderMap.Add(4, "ReferralType", "m.ReferralTypeId");
                orderMap.Add(6, "StatusId", "m.StatusId");
                orderMap.Add(7, "ReferralAmount", "m.ReferralAmount");
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(MemberBiz.GetReferralGVByMember(SessionManager.CurrentLoginMember.MemberId, search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateAgent(RequestAgentByMember req)
        {
            try
            {
                var validator = new AgentByMemberValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                CsaModel.File file = null;
                if (req.PayslipFile != null && req.PayslipFile.ContentLength > 0)
                {
                    var fileInfo = new FileInfo(req.PayslipFile.FileName);
                    file = new CsaModel.File
                    {
                        FileId = Guid.NewGuid().ToString(),
                        Filename = req.PayslipFile.FileName,
                        Extension = fileInfo.Extension,
                        Size = req.PayslipFile.ContentLength
                    };
                    var uploadDir = FileHelper.GetUploadPhysic(AppSettings.UploadPath, FileHelper.FileDir.IcFileDir);
                    var filePath = Path.Combine(uploadDir, file.FileId + fileInfo.Extension);

                    req.PayslipFile.SaveAs(filePath);
                }

                return Json(MemberBiz.CreateAgentByMember(req, file));
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
        public async Task<ActionResult> SaveMemberBeforeApplicationCreate(RequestSaveMemberBeforeApplicationCreate req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestSaveMemberBeforeApplicationCreateData>(req.Json);
                var validator = new UpdateMemberBeforeApplicationCreateValidator();
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

                if (req.OfferLetterFile != null)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(req.OfferLetterFile.FileName);
                    if (!allowedExtension.Contains(fi.Extension.ToLower())) sb.AppendLine($"<p>Not allowed extension</p>");
                }

                if(sb.ToString().IsNotEmpty())
                {
                    throw new ArgumentException(sb.ToString());
                }

                Helpers.UploadHelper.Upload(req.IcFile, FileHelper.FileDir.IcFileDir, out var icFile);
                Helpers.UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.MemberDir, out var payslipFile);
                Helpers.UploadHelper.Upload(req.OfferLetterFile, FileHelper.FileDir.MemberDir, out var offerLetterFile);

                return Json(await MemberBiz.UpdateMemberApplicationCreatae(data, icFile,payslipFile,offerLetterFile));
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