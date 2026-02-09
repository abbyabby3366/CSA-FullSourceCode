using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

using csa.Admin.FilterAttributes;
using csa.Admin.Models;
using csa.Data.EntityModel;
using csa.Data.Logic;
using csa.Model;
using csa.Model.DataObject;
using csa.DataLogic;
using System.Net;
using csa.Model.Validator;
using System.IO;
using csa.DataLogic.Library;
using csa.Member.Helpers;
using csa.Library;
using System.Text;
using System.Threading.Tasks;

namespace csa.Admin.Controllers
{
    public class MemberController : Controller
    {
        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetNewMemberApprovalGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
        {
            try
            {
                ////get session
                //LoginAdminModel session = SessionManager.AdminSession;

                //get post value
                var dtReqColumns = JsonConvert.DeserializeObject<List<GridViewDataColumnModel>>(Columns);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(Order);
                var dtReqSearch = JsonConvert.DeserializeObject<GridViewDataSearchModel>(Search);

                //get page
                int page = (Start / Length) + 1;

                //get 1st sort attributes
                int idx = dtReqOrder.FirstOrDefault() == null ? 0 : dtReqOrder.FirstOrDefault().column;

                //get sort-field-name
                string sortExpression = dtReqColumns.ElementAt(idx).data;

                //get sort-direction
                int sortDirection = (dtReqOrder.FirstOrDefault() == null) ? (int)SortDirection.Ascending : (dtReqOrder.FirstOrDefault().dir == "asc" ? (int)SortDirection.Ascending : (int)SortDirection.Descending);

                //get predicate
                Expression<Func<member, bool>> predicate = (w => 
                    w.UserData.FirstName.Contains(SearchText) || w.UserData.LastName.Contains(SearchText) || w.UserData.ICNumber.Contains(SearchText) || w.UserData.PhoneNo.Contains(SearchText) || 
                    w.CompanyName.Contains(SearchText) || w.Occupation.Contains(SearchText)
                );

                //get `new-member-approval`
                RespArgs<GridViewModel<NewMemberApprovalGVByAdminModel>> retObj = MemberLogic.GetWithdrawGVByAdmin(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("MemberController.GetNewMemberApprovalGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<NewMemberApprovalGVByAdminModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetAllMemberGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
        {
            try
            {
                ////get session
                //LoginAdminModel session = SessionManager.AdminSession;

                //get post value
                var dtReqColumns = JsonConvert.DeserializeObject<List<GridViewDataColumnModel>>(Columns);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(Order);
                var dtReqSearch = JsonConvert.DeserializeObject<GridViewDataSearchModel>(Search);

                //get page
                int page = (Start / Length) + 1;

                //get 1st sort attributes
                int idx = dtReqOrder.FirstOrDefault() == null ? 0 : dtReqOrder.FirstOrDefault().column;

                //get sort-field-name
                string sortExpression = dtReqColumns.ElementAt(idx).data;

                //get sort-direction
                int sortDirection = (dtReqOrder.FirstOrDefault() == null) ? (int)SortDirection.Ascending : (dtReqOrder.FirstOrDefault().dir == "asc" ? (int)SortDirection.Ascending : (int)SortDirection.Descending);

                //get predicate
                Expression<Func<member, bool>> predicate = (w =>
                    w.UserData.FirstName.Contains(SearchText) || w.UserData.LastName.Contains(SearchText) || w.UserData.ICNumber.Contains(SearchText) || w.UserData.PhoneNo.Contains(SearchText) ||
                    w.CompanyName.Contains(SearchText) || w.Occupation.Contains(SearchText)
                );

                //get `new-member-approval`
                RespArgs<GridViewModel<MemberGVByAdminModel>> retObj = MemberLogic.GetAllMemberGVByAdmin(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("MemberController.GetAllMemberApprovalGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<NewMemberApprovalGVByAdminModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(RequestNewMemberByAdmin req)
        {
            try
            {
                var validator = new NewMemberByAdminValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);

                CsaModel.File icFile = null;
                if (req.IcFile != null && req.IcFile.ContentLength > 0)
                {
                    var fileInfo = new FileInfo(req.IcFile.FileName);
                    icFile = new CsaModel.File
                    {
                        FileId = Guid.NewGuid().ToString(),
                        Filename = req.IcFile.FileName,
                        Extension = fileInfo.Extension,
                        Size = req.IcFile.ContentLength
                    };
                    var uploadDir = FileHelper.GetUploadPhysic(AppSettings.UploadPath, FileHelper.FileDir.IcFileDir);
                    var filePath = Path.Combine(uploadDir, icFile.FileId + fileInfo.Extension);

                    req.IcFile.SaveAs(filePath);
                }

                CsaModel.File profileFile = null;
                if (req.ProfileFile != null && req.ProfileFile.ContentLength > 0)
                {
                    var fileInfo = new FileInfo(req.ProfileFile.FileName);
                    profileFile = new CsaModel.File
                    {
                        FileId = Guid.NewGuid().ToString(),
                        Filename = req.ProfileFile.FileName,
                        Extension = fileInfo.Extension,
                        Size = req.ProfileFile.ContentLength
                    };
                    var uploadDir = FileHelper.GetUploadPhysic(AppSettings.UploadPath, FileHelper.FileDir.ProfileFileDir);
                    var filePath = Path.Combine(uploadDir, profileFile.FileId + fileInfo.Extension);
                    var thumbnailPath = Path.Combine(uploadDir, profileFile.FileId + Constant.THUMBNAIL + fileInfo.Extension);

                    req.ProfileFile.SaveAs(filePath);
                    ImageThumbnailGenerator.GenerateThumbnail(filePath, thumbnailPath, 250);
                }

                Helpers.UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.MemberDir, out var payslipFile);
                
                req.PhoneNumber = new csa.Library.Phone(req.PhoneNumber).RemovePrefix("0").RemovePrefix("60").GetResult();
                return Json(await MemberBiz.CreateByAdmin(req, icFile, profileFile, payslipFile));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<int>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

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
        public ActionResult ChangePassword(RequestChangePasswordByAdmin req)
        {
            try
            {
                var validator = new ChangePasswordByAdminValidator();
                var resValidator = validator.Validate(req);
                if (!resValidator.IsValid) throw new ArgumentException(resValidator.Errors[0].ErrorMessage);
                return Json(MemberBiz.ChangePasswordByAdmin(req));
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
        public async Task<ActionResult> Update(RequestUpdateMemberByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestUpdateMemberByAdminData>(req.Json);
                var validator = new UpdateMemberByAdminValidator();
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

                if (sb.ToString().IsNotEmpty())
                {
                    throw new ArgumentException(sb.ToString());
                }

                Helpers.UploadHelper.Upload(req.IcFile, FileHelper.FileDir.IcFileDir, out var icFile);
                Helpers.UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.MemberDir, out var payslipFile);
                Helpers.UploadHelper.Upload(req.OfferLetterFile, FileHelper.FileDir.MemberDir, out var offerLetterFile);

                return Json(await MemberBiz.UpdateByAdmin(data, icFile, payslipFile, offerLetterFile));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<int>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult ChangeMemberPicture(RequestChangeProfilePicture req)
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

                return Json(MemberBiz.ChangeProfilePicture(req, file));
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

        //[HttpPost]
        //public ActionResult GetMemberGV(string search, string order, int start, int length)
        //{
        //    try
        //    {
        //        int pageIndex = (start / length);
        //        var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
        //        var orderMap = new GridViewOrderMapping();
        //        orderMap.Add(0, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
        //        orderMap.Add(2, "MemberType", "m.MemberTypeId");
        //        orderMap.Add(3, "ICNumber", "m.ICNumber");
        //        orderMap.Add(4, "Gender", "m.GenderId");
        //        orderMap.Add(5, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
        //        orderMap.Add(6, "PhoneNumber", "m.PhoneNumber");
        //        orderMap.Add(7, "CompanyName", "m.CompanyName");
        //        orderMap.Add(8, "Occupation", "m.Occupation");
        //        orderMap.Add(9, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
        //        var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

        //        var res = MemberBiz.GetMemberGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
        //        foreach (var item in res.data)
        //        {
        //            item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
        //        }
        //        return Json(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost]
        public ActionResult GetClientGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FileNumber", "m.FileNumber");
                orderMap.Add(2, "MemberType", "m.MemberTypeId");
                orderMap.Add(3, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(4, "ICNumber", "m.ICNumber");
                orderMap.Add(5, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(6, "SalarayRange", "m.SalaryRangeId");
                orderMap.Add(7, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
                orderMap.Add(8, "EmployerName", "m.CompanyEmployerName");
                orderMap.Add(9, "State", "m.StateId");
                orderMap.Add(10, "Sector", "m.CompanySectorId");
                orderMap.Add(11, "Occupation", "m.Occupation");
                orderMap.Add(12, "BankAccountName", "m.BankAccountName");
                orderMap.Add(13, "Bank", "bank.Name");
                orderMap.Add(14, "BankAccountNumber", "m.BankAccountNumber");
                orderMap.Add(15, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

                var res = MemberBiz.GetMemberGVByAdmin(MemberGVType.Client,search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
                foreach (var item in res.data)
                {
                    item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetYabamClientGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FileNumber", "m.FileNumber");
                orderMap.Add(2, "MemberType", "m.MemberTypeId");
                orderMap.Add(3, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(4, "ICNumber", "m.ICNumber");
                orderMap.Add(5, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(6, "SalarayRange", "m.SalaryRangeId");
                orderMap.Add(7, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
                orderMap.Add(8, "EmployerName", "m.CompanyEmployerName");
                orderMap.Add(9, "State", "m.StateId");
                orderMap.Add(10, "Sector", "m.CompanySectorId");
                orderMap.Add(11, "Occupation", "m.Occupation");
                orderMap.Add(12, "BankAccountName", "m.BankAccountName");
                orderMap.Add(13, "Bank", "bank.Name");
                orderMap.Add(14, "BankAccountNumber", "m.BankAccountNumber");
                orderMap.Add(15, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

                var res = MemberBiz.GetMemberGVByAdmin(MemberGVType.Yabam, search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
                foreach (var item in res.data)
                {
                    item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetDropMiaGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FileNumber", "m.FileNumber");
                orderMap.Add(2, "MemberType", "m.MemberTypeId");
                orderMap.Add(3, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(4, "ICNumber", "m.ICNumber");
                orderMap.Add(5, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(6, "SalarayRange", "m.SalaryRangeId");
                orderMap.Add(7, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
                orderMap.Add(8, "EmployerName", "m.CompanyEmployerName");
                orderMap.Add(9, "State", "m.StateId");
                orderMap.Add(10, "Sector", "m.CompanySectorId");
                orderMap.Add(11, "Occupation", "m.Occupation");
                orderMap.Add(12, "BankAccountName", "m.BankAccountName");
                orderMap.Add(13, "Bank", "bank.Name");
                orderMap.Add(14, "BankAccountNumber", "m.BankAccountNumber");
                orderMap.Add(15, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

                var res = MemberBiz.GetMemberGVByAdmin(MemberGVType.Drop_Mia, search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
                foreach (var item in res.data)
                {
                    item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetAgentGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FileNumber", "m.FileNumber");
                orderMap.Add(2, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(3, "ICNumber", "m.ICNumber");
                orderMap.Add(4, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(5, "State", "m.StateId");
                orderMap.Add(6, "Sector", "m.CompanySectorId");
                orderMap.Add(7, "Position", "m.CompanyDepartmentId");
                orderMap.Add(9, "Payslip", "m.PayslipFileId");
                orderMap.Add(11, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

                var res = MemberBiz.GetAgentGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
                foreach (var item in res.data)
                {
                    item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetHeroGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FileNumber", "m.FileNumber");
                orderMap.Add(2, "MemberType", "m.MemberTypeId");
                orderMap.Add(3, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(4, "ICNumber", "m.ICNumber");
                orderMap.Add(5, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(6, "SalarayRange", "m.SalaryRangeId");
                orderMap.Add(7, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
                orderMap.Add(8, "EmployerName", "m.CompanyEmployerName");
                orderMap.Add(9, "State", "m.StateId");
                orderMap.Add(10, "Sector", "m.CompanySectorId");
                orderMap.Add(11, "Occupation", "m.Occupation");
                orderMap.Add(12, "BankAccountName", "m.BankAccountName");
                orderMap.Add(13, "Bank", "bank.Name");
                orderMap.Add(14, "BankAccountNumber", "m.BankAccountNumber");
                orderMap.Add(15, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

                var res = MemberBiz.GetMemberGVByAdmin(MemberGVType.Hero_Wira, search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
                foreach (var item in res.data)
                {
                    item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetMemberGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "FileNumber", "m.FileNumber");
                orderMap.Add(2, "MemberType", "m.MemberTypeId");
                orderMap.Add(3, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(4, "ICNumber", "m.ICNumber");
                orderMap.Add(5, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(6, "SalarayRange", "m.SalaryRangeId");
                orderMap.Add(7, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
                orderMap.Add(8, "EmployerName", "m.CompanyEmployerName");
                orderMap.Add(9, "State", "m.StateId");
                orderMap.Add(10, "Sector", "m.CompanySectorId");
                orderMap.Add(11, "Occupation", "m.Occupation");
                orderMap.Add(12, "BankAccountName", "m.BankAccountName");
                orderMap.Add(13, "Bank", "bank.Name");
                orderMap.Add(14, "BankAccountNumber", "m.BankAccountNumber");
                orderMap.Add(15, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);

                var res = MemberBiz.GetMemberGVByAdmin(MemberGVType.Member, search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal;
                foreach (var item in res.data)
                {
                    item.ReferralLink = csa.Member.Helpers.AppSettings.MemberAppPath + "/Survey.aspx?ref=" + csa.Library.SecurityLibrary.Encrypt(item.MemberId.ToString());
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetMemberPendingAprovalGV(string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(0, "FullName", "CONCAT(IFNULL(m.FirstName,''),IFNULL(m.LastName,''))");
                orderMap.Add(1, "ICNumber", "m.ICNumber");
                orderMap.Add(2, "Gender", "m.GenderId");
                orderMap.Add(3, "ReferrerName", "CONCAT(IFNULL(refm.FirstName,''),' ',IFNULL(refm.LastName,''))");
                orderMap.Add(4, "PhoneNumber", "m.PhoneNumber");
                orderMap.Add(5, "CompanyName", "m.CompanyName");
                orderMap.Add(6, "Occupation", "m.Occupation");
                orderMap.Add(7, "CreateDate", "m.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(MemberBiz.GetMemberNeedApprovalGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Approve(RequestApproveMember req)
        {
            try
            {
                return Json(await MemberBiz.ApproveMember(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ApproveHero(RequestApproveMember req)
        {
            try
            {
                return Json(await MemberBiz.ApproveMemberHero(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Reject(RequestRejectMember req)
        {
            try
            {
                return Json(await MemberBiz.RejectMember(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult NewUserDashboard(string type)
        {
            try
            {
                return Json(MemberBiz.NewUserDashboard(type));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetMemberVts(string q,int? exceptMemberId)
        {
            var members = MemberBiz.GetAllActiveWithSearch(q,exceptMemberId);

            return Json(members.OrderBy(x => x.FullName).Select(x => new ValueText<long>(x.MemberId, $"{x.FullName} ({x.PhoneNumber})")).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> WalletChangesByAdmin(RequestWalletChangesByAdmin req)
        {
            try
            {
                return Json(await MemberBiz.WalletChangesByAdmin(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> WalletSavingsChangesByAdmin(RequestWalletChangesByAdmin req)
        {
            try
            {
                return Json(await MemberBiz.WalletSavingsChangesByAdmin(req));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RecalculateAllMember()
        {
            try
            {
                await MemberBiz.RecalculateAllMember();
                return Json(RespArgs<bool>.CreateSuccess(true));
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ExportSurvey(DateTime? startDate,DateTime? endDate)
        {
            try
            {
                var listState = StateBiz.GetAllDisplay();
                var listSector = SectorBiz.Gets().Select(y => new DropdownItem(y.SectorId.ToString(), y.Name)).ToList();
                var listJob = JobPositionBiz.Gets().Select(z => new Dropdown3(z.JobPositionId.ToString(), z.Name, z.SectorId.ToString())).ToList();
                string fileName = "Yabam.xlsx";
                var resp = MemberBiz.ExportSurvey(new RequestExportSurvey(startDate,endDate),0,int.MaxValue);
                var fileBytes = new hudanLibrary.ExportExcel<ExportSurvey>(resp.ObjVal.data.ToList())
                    .AddColumn("FullName", x => x.FullName)
                    .AddColumn("IC Number", x => x.ICNumber)
                    .AddColumn("Bank Name", x => x.Bank)
                    .AddColumn("Bank Number", x => x.BankAccountNumber)
                    .AddColumn("IC Uploaded", x => x.ICFileId.IsNotEmpty() ? "Yes" : "No")
                    .AddColumn("Payslip Uploaded", x => x.PayslipFileId.IsNotEmpty() ? "Yes" : "No")
                    .AddColumn("1. Negeri manakah yang anda tinggal sekarang?", x => x.GetA1(listState))
                    .AddColumn("2. Apakah jawatan/ posisi anda di tempat kerja?", x => x.GetA2(listSector,listJob ))
                    .AddColumn("3. Apakah Taraf Pendidikan Anda?", x => x.GetA3())
                    .AddColumn("4. Apakah status perkahwinan anda?", x => x.GetA4())
                    .AddColumn("5. Berapakah jumlah tanggungan anda termasuk diri sendiri?", x => x.GetA5())
                    .AddColumn("6. Adakah anda mempunyai tanggungan ahli keluarga OKU atau sakit tenat?", x => x.GetA6())
                    .AddColumn("7. Adakah anda mempunyai rumah sendiri atau menyewa?", x => x.GetA7())
                    .AddColumn("8. Dimanakah lokasi kediaman rumah anda?", x => x.GetA8())
                    .AddColumn("9. Berapakah jumlah kerata yang anda miliki?", x => x.GetA9())
                    .AddColumn("10. Apakah hobi/ minat anda ketika waktu lapang?", x => x.GetA10())
                    .AddColumn("11. Berapakah jumlah pendapatan kasar sekeluarga anda?", x => x.GetB1())
                    .AddColumn("12. Berapa banyak hutang anda buat masa sekarang? (tidak termasuk hutang rumah)", x => x.GetB2())
                    .AddColumn("13. Berapakah simpanan anda sebulan?", x => x.GetB3())
                    .AddColumn("14. Apakah yang mendorong anda untuk menabung? (Sila pilih 3)", x => x.GetB4())
                    .AddColumn("15. Apakah cabaran paling sukar untuk menabung? (Silih pilih 3)", x => x.GetB5())
                    .AddColumn("16. Sepanjang tahun terkini, berapa kerap anda terpaksa meminjam wang atau menggunakan kredit untuk menampung perbelanjaan harian?", x => x.GetB6())
                    .AddColumn("17. Apakah ketakutan anda dalam menambah / menggunakan komitmen kewangan?", x => x.GetB7())
                    .AddColumn("18. Jika anda menerima RM100,000 hari ini, bagaimana anda akan menggunakannya secara utama? (Sila pilih 3)", x => x.GetB8())
                    .AddColumn("19. Apakah jenis perniagaan atau pelaburan di pandangan anda mampu jana pendapatan tinggi? (Silih pilih 3)", x => x.GetB9())
                    .AddColumn("20. Apa pendapat anda tentang aliran tunai positif / kewangan berada dalam keadaan baik dan stabil?", x => x.GetB10())
                    .AddColumn("21. Siapakah guru kewangan yang paling anda minati?", x => x.GetC1())
                    .AddColumn("22. Bilakah anda selesa untuk dihubungi bagi sesi perbualan lanjut?", x => x.GetC2()).GetByte();

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ExportFinance(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                string fileName = "Finance.xlsx";
                var resp = MemberBiz.ExportFinance(new RequestExportFinance(startDate, endDate), 0, int.MaxValue);
                var fileBytes = new hudanLibrary.ExportExcel<ExportFinance>(resp.ObjVal.data.ToList())
                    .AddColumn("FullName", x => x.FullName)
                    .AddColumn("IC Number", x => x.ICNumber)
                    .AddColumn("Bank Name", x => x.Bank)
                    .AddColumn("Bank Number", x => x.BankAccountNumber)
                    .AddColumn("Status", x => x.Status)
                    .AddColumn("Amount To Transfer", x => $"RM {hudanLibrary.ExtensionMethod.StringCurrency(x.Amount.GetValueOrDefault())}").GetByte();

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ExportDemographicMarketAnalysis(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                string fileName = "Demographic Market Analysis.xlsx";
                var resp = MemberBiz.ExportDemographicMarketAnalysis(new RequestExportDemographicMarketAnalysis(startDate, endDate), 0, int.MaxValue);
                var fileBytes = new hudanLibrary.ExportExcel<ExportDemographicMarketAnalysis>(resp.ObjVal.data.ToList())
                    .AddColumn("Source", x => x.ProgramEvent)
                    .AddColumn("Race", x => x.Race)
                    .AddColumn("Age", x => x.Age)
                    .AddColumn("Gender", x => x.Gender)
                    .AddColumn("Salary", x => x.Salary.HasValue ? hudanLibrary.ExtensionMethod.StringCurrency(x.Salary.Value) : "")
                    .AddColumn("Salary Range", x => x.SalaryRange)
                    .AddColumn("State", x => x.State)
                    .AddColumn("Sector", x => x.Sector).GetByte();

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ExportRM(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                string fileName = "RM.xlsx";
                var resp = MemberBiz.ExportRM(new RequestExportRM(startDate, endDate), 0, int.MaxValue);
                var list = resp.ObjVal.data.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].CalcTierRank(i);
                }
                var fileBytes = new hudanLibrary.ExportExcel<ExportRM>(list)
                    .AddColumn("Referral Name", x => x.FullName)
                    .AddColumn("Total Number of Referral", x => x.ReferralCount.ToString())
                    .AddColumn("Tier Rank", x => x.TierRank)
                    .GetByte();

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        [HttpGet]
        public ActionResult ExportOperation(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                string fileName = "Operation.xlsx";
                var resp = MemberBiz.ExportOperation(new RequestExportOperation(startDate, endDate), 0, int.MaxValue);
                var list = resp.ObjVal.data.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].No = i + 1;
                }
                var fileBytes = new hudanLibrary.ExportExcel<ExportOperation>(list)
                    .AddColumn("No", x => x.No)
                    .AddColumn("Months", x => "???")
                    .AddColumn("Consultant", x => "???")
                    .AddColumn("File No", x => x.FileNumber)
                    .AddColumn("Customer Name", x => x.FullName)
                    .AddColumn("Settlement Amount", x => hudanLibrary.ExtensionMethod.StringCurrency(x.Amount.GetValueOrDefault()))
                    .AddColumn("Car Campaign", x => hudanLibrary.ExtensionMethod.StringCurrency(x.AmountFacilities.GetValueOrDefault()))
                    .AddColumn("Total (A+B)", x => hudanLibrary.ExtensionMethod.StringCurrency(x.TotalAB))
                    .AddColumn("%", x => "???")
                    .AddColumn("Total", x => hudanLibrary.ExtensionMethod.StringCurrency(x.Total))
                    .AddColumn("Payout Settlement Invoice", x => "???")
                    .GetByte();

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ExportCredit()
        {
            try
            {
                string fileName = "Credit.xlsx";
                var resp = MemberBiz.ExportCredit();
                var data = resp.ObjVal;
                var exportExcel = new hudanLibrary.ExportExcel()
                    .Add(1, 1, "Pre-checking Stage")
                    .Add(1, 2, "Proposal Preparation Stage")
                    .Add(1, 3, "Proposal Presentation Stage")
                    .Add(1, 4, "Queue for Reloan Stage")
                    .Add(1, 5, "Reloan Submission Stage");

                exportExcel
                    .Add(2, 1, data.PreCheckingStage.ToString())
                    .Add(2, 2, data.ProposalPreparationStage.ToString())
                    .Add(2, 3, data.ProposalPresentationStage.ToString())
                    .Add(2, 4, data.QueueForReloanStage.ToString())
                    .Add(2, 5, data.ReloanSubmissionStage.ToString());

                exportExcel
                    .Add(4, 1, "Total Approves Cases")
                    .Add(4, 2, "Total Burst Cases")
                    .Add(4, 3, "Total Dropped Cases")
                    .Add(4, 4, "Total MIA Cases");

                exportExcel
                    .Add(5, 1, data.TotalApprovesCases.ToString())
                    .Add(5, 2, data.TotalBurstCases.ToString())
                    .Add(5, 3, data.TotalDroppedCases.ToString())
                    .Add(5, 4, data.TotalMIACases.ToString());

                exportExcel
                    .Add(7, 1, "Total Single Cases")
                    .Add(7, 2, "Total RNR Cases");

                exportExcel
                    .Add(8, 1, data.TotalSingleCases.ToString())
                    .Add(8, 2, data.TotalRNRCases.ToString());

                exportExcel
                    .AddTableFormat(1, 1, 2, 5)
                    .AddTableFormat(4, 1, 5, 4)
                    .AddTableFormat(7, 1, 8, 2);

                string contentType = MimeMapping.GetMimeMapping(fileName);

                var fileBytes = exportExcel.GetByte();
                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ExportNewMemberApproval(string search,string order)
        {
            try
            {
                var result = GetMemberPendingAprovalGV(search, order, 0, int.MaxValue) as JsonResult;
                var gv = result.Data as GridViewModel<MemberGVByAdmin>;

                string fileName = "New Member Approval.xlsx";
                var fileBytes = new hudanLibrary.ExportExcel<MemberGVByAdmin>(gv.data.ToList())
                    .AddColumn("FULL NAME", x => x.FullName)
                    .AddColumn("IC", x => x.ICNumber)
                    .AddColumn("GENDER", x => x.Gender)
                    .AddColumn("REFERRAL NAME", x => x.ReferrerName)
                    .AddColumn("CONTACT NO", x => x.PhoneNumber)
                    .AddColumn("COMPANY", x => x.CompanyName)
                    .AddColumn("OCCUPATION", x => x.Occupation)
                    .AddColumn("CREATE DATE", x => hudanLibrary.ExtensionMethod.DateTextOrDefault(x.CreateDate,"dd-MM-yyyy HH:mm"))
                    .GetByte();

                string contentType = MimeMapping.GetMimeMapping(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (ArgumentException ex)
            {
                return Json(RespArgs<bool>.CreateError(ex.Message));
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}