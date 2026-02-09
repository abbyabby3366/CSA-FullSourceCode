using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

using csa.Member.FilterAttributes;
using csa.Member.Models;
using csa.Data.EntityModel;
using csa.Data.Logic;
using csa.Model;
using csa.Model.DataObject;
using System.Threading.Tasks;
using csa.Model.Validator;
using csa.DataLogic;
using System.Net;
using csa.Library;
using System.IO;
using csa.Member.Helpers;

namespace csa.Member.Controllers
{
    public class ApplicationController : Controller
    {
        [AllowAnonymous]
        //[AuthFilterAttr]
        [HttpPost]
        public JsonResult GetApplicantStatusGV(string SearchText, string Columns, int Start, int Length, string Order, string Search)
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
                Expression<Func<application, bool>> predicate = (w =>
                    w.MemberData.UserData.Code.Contains(SearchText) || w.MemberData.UserData.FirstName.Contains(SearchText) || w.MemberData.UserData.LastName.Contains(SearchText) || w.MemberData.UserData.PhoneNo.Contains(SearchText)
                );

                //get `new-member-approval`
                RespArgs<GridViewModel<ApplicantStatusGVByMemberModel>> retObj = ApplicationLogic.GetApplicantStatusGVByMember(Guid.Empty, page, Length, sortExpression, sortDirection, predicate);

                if (retObj == null)
                { throw new Exception("ApplicationController.GetApplicantStatusGV failed"); }

                return new JsonResult { Data = retObj.ObjVal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch //(Exception ex)
            {
                //error log

                return new JsonResult { Data = new GridViewModel<AdminRoleUsersGVByAdminModel>(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<ActionResult> CreateAsync(RequestApplicationCreate req)
        {
            try
            {
                return Json(await ApplicationBiz.ApplicationCreateByMember(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<LoginMember>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public ActionResult GetApplicationGV(int memberId,string search, string order, int start, int length)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(2, "Name", "app.Name");
                orderMap.Add(4, "ContactNo", "app.ContactNo");
                orderMap.Add(5, "SalaryRange", "app.SalaryRangeId");
                orderMap.Add(6, "Status", "app.StatusId");
                orderMap.Add(7, "CreateDate", "app.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(ApplicationBiz.GetApplicationGVByMember(memberId, search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public ActionResult DownloadSuratAkuanTemplate()
        {
            string fileName = "Template Surat Akuan.txt";
            // Define the path to your file
            string filePath = GetTemplateFile(fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound("File not found");
            }

            // Read the file into a byte array
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Get the file extension and set the content type accordingly
            string contentType = MimeMapping.GetMimeMapping(fileName);

            // Return the file for download
            return File(fileBytes, contentType, fileName);
        }

        private static string GetTemplateFile(string fileName)
        {
            return Path.Combine(AppSettings.TemplatePath, fileName);
        }

        public ActionResult DownloadApplicationAgreementDocument()
        {
            string fileName = "Application Agreement Document.docx";
            // Define the path to your file
            string filePath = GetTemplateFile(fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound("File not found");
            }

            // Read the file into a byte array
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Get the file extension and set the content type accordingly
            string contentType = MimeMapping.GetMimeMapping(fileName);

            // Return the file for download
            return File(fileBytes, contentType, fileName);
        }

        [HttpPost]
        public ActionResult PreCheckingByMember(RequestApplicationPrecheckingByMember req)
        {
            try
            {
                throw new Exception("service unavailable");
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public ActionResult ProcessingByMember(RequestApplicationProcessingByMember req)
        {
            try
            {
                throw new Exception("service unavailable");
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }
    }
}