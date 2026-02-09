using csa.Admin.Models;
using csa.DataLogic;
using csa.DataLogic.Library;
using csa.Library;
using csa.Member.Helpers;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace csa.Admin.Controllers
{
    public class ApplicationController : Controller
    {
        [HttpPost]
        public ActionResult GetApplicationGV(string search, string order, int start, int length,int applicationStatusId,int? adminId)
        {
            try
            {
                int pageIndex = (start / length);
                var dtReqOrder = JsonConvert.DeserializeObject<List<GridViewDataOrderModel>>(order);
                var orderMap = new GridViewOrderMapping();
                orderMap.Add(1, "CustomerName", "CONCAT(IFNULL(creator.FirstName,''),' ',IFNULL(creator.LastName,''))");
                orderMap.Add(3, "ContactNo", "creator.PhoneNumber");
                orderMap.Add(4, "CompanyName", "creator.CompanyName");
                orderMap.Add(5, "Salary", "creator.Salary");
                orderMap.Add(6, "Status", "app.ApplicationStatusId");
                orderMap.Add(7, "CreateDate", "app.CreateDate", isDefault: true, defaultSortDirection: SQLSelect.OrderByEnum.DESC);
                var orderResult = orderMap.Find(dtReqOrder.Count > 0 ? dtReqOrder[0] : null);
                return Json(ApplicationBiz.GetApplicationGVByAdmin(search, pageIndex, length, orderResult.SortOrder, orderResult.SortDirection, applicationStatusId, adminId).ObjVal);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CanNextApplicationStatus(RequestNextApplicationStatus req)
        {
            try
            {
                return Json(ApplicationBiz.CanNextApplicationStatus(req.ApplicationId));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message,(int)ErrorCode.CUSTOM_ERROR));
            }
        }

        [HttpPost]
        public async Task<ActionResult> NextApplicationStatus(RequestNextApplicationStatus req)
        {
            try
            {
                return Json(await ApplicationBiz.GoToApplicationStatus(new RequestApplicaationGotoStatus(req.ApplicationId,req.AdminId,newStatus: null)));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> GoToApplicationStatus(RequestGoToApplicationStatus req)
        {
            try
            {
                return Json(await ApplicationBiz.GoToApplicationStatus(new RequestApplicaationGotoStatus(req.ApplicationId, req.AdminId, newStatus: req.StatusId)));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }            
        }

        [HttpPost]
        public async Task<ActionResult> PreChecking(RequestApplicationPreCheckingByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationPreCheckingData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments,data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if(resValidator.Error) throw new ArgumentException(resValidator.Message);


                Helpers.UploadHelper.Upload(req.RamciFile, FileHelper.FileDir.ApplicationPreCheckingDir, out var ramciFile);
                Helpers.UploadHelper.Upload(req.CcrisFile, FileHelper.FileDir.ApplicationPreCheckingDir, out var ccrisFile);
                Helpers.UploadHelper.Upload(req.PayslipFile, FileHelper.FileDir.ApplicationPreCheckingDir, out var payslipFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if(req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if(newFile != null) docs.Add(newFile);
                    }
                }
                
                return Json(await ApplicationBiz.SavePreCheckingAsync(req, ramciFile, ccrisFile, payslipFile, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> ProposalPreparation(RequestApplicationPreparationByAdmin req)
        {
            try
            {
                Helpers.UploadHelper.Upload(req.ProposalFile, FileHelper.FileDir.ApplicationPreparationDir, out var proposalFile);
                return Json(await ApplicationBiz.SavePreparationAsync(req, proposalFile));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> ProposalPresentation(RequestApplicationProposalPresentationByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationProposalPresentationData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveProposalAsync(data, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> PreSigning(RequestApplicationPreSigningByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationPreSigningData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SavePresignAsync(data, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> PendingZoomAtteptance(RequestApplicationPendingAcceptanceByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationPendingAcceptanceData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                Helpers.UploadHelper.Upload(req.PaySlipFile, FileHelper.FileDir.ApplicationZoomAcceptanceDir, out var payslipFile);
                Helpers.UploadHelper.Upload(req.RamciFile, FileHelper.FileDir.ApplicationZoomAcceptanceDir, out var ramciFile);
                Helpers.UploadHelper.Upload(req.CtosFile, FileHelper.FileDir.ApplicationZoomAcceptanceDir, out var ctosFile);
                Helpers.UploadHelper.Upload(req.RedemptionLetterFile, FileHelper.FileDir.ApplicationZoomAcceptanceDir, out var redeemptionLetterFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveZoomAcceptanceAsync(data,payslipFile,ramciFile,ctosFile,redeemptionLetterFile, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Settlement(RequestApplicationSettlementByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationSettlementData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                Helpers.UploadHelper.Upload(req.SettlementFile, FileHelper.FileDir.ApplicationSettlementDir, out var settlementFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveSettlementAsync(data, settlementFile, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> CcrisCleaning(RequestApplicationCcrisByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationCcrisData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                Helpers.UploadHelper.Upload(req.ReleaseLetterFile, FileHelper.FileDir.ApplicationCcrisDir, out var releaseLetterFile);
                Helpers.UploadHelper.Upload(req.CcrisReportFile, FileHelper.FileDir.ApplicationCcrisDir, out var ccrisReportFile);
                Helpers.UploadHelper.Upload(req.HrmisFile, FileHelper.FileDir.ApplicationCcrisDir, out var hrmisFile);
                Helpers.UploadHelper.Upload(req.AnmFile, FileHelper.FileDir.ApplicationCcrisDir, out var anmFile);
                Helpers.UploadHelper.Upload(req.LpsaFile, FileHelper.FileDir.ApplicationCcrisDir, out var lpsaFile);
                Helpers.UploadHelper.Upload(req.AngkasaFile, FileHelper.FileDir.ApplicationCcrisDir, out var angkasaFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveCcrisAsync(data, releaseLetterFile,ccrisReportFile, hrmisFile,anmFile,lpsaFile,angkasaFile, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> QueueForLoan(RequestApplicationQueueForLoanByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationQueueForLoanData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                Helpers.UploadHelper.Upload(req.IdentityFile, FileHelper.FileDir.ApplicationQueueDir, out var identityFile);
                Helpers.UploadHelper.Upload(req.PaySlipFile, FileHelper.FileDir.ApplicationQueueDir, out var paySlipFile);
                Helpers.UploadHelper.Upload(req.EcFile, FileHelper.FileDir.ApplicationQueueDir, out var ecFile);
                Helpers.UploadHelper.Upload(req.HrmisFile, FileHelper.FileDir.ApplicationQueueDir, out var hrmisFile);
                Helpers.UploadHelper.Upload(req.BankStatementFile, FileHelper.FileDir.ApplicationQueueDir, out var bankStatementFile);
                Helpers.UploadHelper.Upload(req.LppsaFile, FileHelper.FileDir.ApplicationQueueDir, out var lppsaFile);
                Helpers.UploadHelper.Upload(req.LicenseFile, FileHelper.FileDir.ApplicationQueueDir, out var licenseFile);
                Helpers.UploadHelper.Upload(req.RedemptionLetterFile, FileHelper.FileDir.ApplicationQueueDir, out var redemptionLetterFile);
                Helpers.UploadHelper.Upload(req.CreditCardFile, FileHelper.FileDir.ApplicationQueueDir, out var creditCardFile);
                Helpers.UploadHelper.Upload(req.RamciFile, FileHelper.FileDir.ApplicationQueueDir, out var ramciFile);
                Helpers.UploadHelper.Upload(req.SignatureFile, FileHelper.FileDir.ApplicationQueueDir, out var signaturFile);
                Helpers.UploadHelper.Upload(req.BiroAngkasaFile, FileHelper.FileDir.ApplicationQueueDir, out var biroAngkasaFile);
                Helpers.UploadHelper.Upload(req.Kew320File, FileHelper.FileDir.ApplicationQueueDir, out var kew320File);
                Helpers.UploadHelper.Upload(req.StaffCardFile, FileHelper.FileDir.ApplicationQueueDir, out var staffCardFile);
                Helpers.UploadHelper.Upload(req.PostDatedChequeFile, FileHelper.FileDir.ApplicationQueueDir, out var postDatedChequeFile);
                Helpers.UploadHelper.Upload(req.CompanyConfirmationFile, FileHelper.FileDir.ApplicationQueueDir, out var CompanyConfirmationFile);
                Helpers.UploadHelper.Upload(req.EpfFile, FileHelper.FileDir.ApplicationQueueDir, out var epfFile);
                Helpers.UploadHelper.Upload(req.EaformFile, FileHelper.FileDir.ApplicationQueueDir, out var eaformFile);
                Helpers.UploadHelper.Upload(req.BillUtilitiesFile, FileHelper.FileDir.ApplicationQueueDir, out var billUtilitiesFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                List<CsaModel.File> payslips = new List<CsaModel.File>();
                if (req.Payslips != null)
                {
                    foreach (var item in req.Payslips)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDir, out var newFile);
                        if (newFile != null) payslips.Add(newFile);
                    }
                }

                List<CsaModel.File> bankStatements = new List<CsaModel.File>();
                if (req.BankStatements != null)
                {
                    foreach (var item in req.BankStatements)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDir, out var newFile);
                        if (newFile != null) bankStatements.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveQueueAsync(data, identityFile,paySlipFile,ecFile,hrmisFile, bankStatementFile, lppsaFile,licenseFile,redemptionLetterFile,creditCardFile,ramciFile,signaturFile,biroAngkasaFile,kew320File,
                    staffCardFile,postDatedChequeFile,CompanyConfirmationFile,epfFile,eaformFile,billUtilitiesFile, docs,payslips,bankStatements));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Reloan(RequestApplicationReloanByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationReloanData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                Helpers.UploadHelper.Upload(req.OfferLetterFile, FileHelper.FileDir.ApplicationReloanDir, out var offerLetterFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveReloanAsync(data, offerLetterFile, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Collection(RequestApplicationCollectionByAdmin req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<RequestApplicationCollectionData>(req.Json);
                var validator = new ApplicationAdditionalDocumentCustomValidator(req.FileAdditionalDocuments, data.RemarkAdditionalDocuments, data.AdditionalDocumentToModify.Select(x => x.Remark).ToList());
                var resValidator = validator.Validate();
                if (resValidator.Error) throw new ArgumentException(resValidator.Message);

                Helpers.UploadHelper.Upload(req.DeclarationFile, FileHelper.FileDir.ApplicationCollectionDir, out var declarationFile);
                Helpers.UploadHelper.Upload(req.SettlementFile, FileHelper.FileDir.ApplicationCollectionDir, out var settlementFile);
                Helpers.UploadHelper.Upload(req.ServiceFeeFile, FileHelper.FileDir.ApplicationCollectionDir, out var serviceFeeFile);
                Helpers.UploadHelper.Upload(req.RezekiFile, FileHelper.FileDir.ApplicationCollectionDir, out var rezekiFeeFile);
                Helpers.UploadHelper.Upload(req.RezekiAgreementFile, FileHelper.FileDir.ApplicationCollectionDir, out var rezekiAgreementFile);

                List<CsaModel.File> docs = new List<CsaModel.File>();
                if (req.FileAdditionalDocuments != null)
                {
                    foreach (var item in req.FileAdditionalDocuments)
                    {
                        Helpers.UploadHelper.Upload(item, FileHelper.FileDir.ApplicationDocumentDir, out var newFile);
                        if (newFile != null) docs.Add(newFile);
                    }
                }

                return Json(await ApplicationBiz.SaveCollectionAsync(data, declarationFile,settlementFile,serviceFeeFile,rezekiFeeFile,rezekiAgreementFile, docs));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        [ActionName("assign")]
        public async Task<ActionResult> AssignAsync(RequestApplicationAssignByAdmin req)
        {
            try
            {
                var res = await ApplicationBiz.SaveAssignAsync(req);
                if(!res.Error && res.ObjVal == 0)
                {
                    await ApplicationBiz.GoToApplicationStatus(new RequestApplicaationGotoStatus(req.ApplicationId, req.AdminId, 1));
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Reject(RequestApplicationReject req)
        {
            try
            {
                return Json(await ApplicationBiz.RejectAsync(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> CancelReject(RequestApplicationCancelReject req)
        {
            try
            {
                return Json(await ApplicationBiz.CancelRejectAsync(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveApplicationInfo(RequestApplicationInfo req)
        {
            try
            {
                return Json(await ApplicationBiz.SaveApplicationInfo(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }
        [HttpPost]
        public async Task<ActionResult> ConvertHero(RequestApplicationHero req)
        {
            try
            {
                return Json(await ApplicationBiz.ConvertHero(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> ApplicationCreate(RequestApplicationCreate req)
        {
            try
            {
                return Json(await ApplicationBiz.ApplicationCreateByAdmin(req));
            }
            catch (Exception ex)
            {
                return Json(RespArgs<object>.CreateError(ex.Message));
            }
        }


        [HttpGet]
        public ActionResult GetMemberVts(string q)
        {
            var members = MemberBiz.GetAllActiveWithSearch(q);

            return Json(members.OrderBy(x => x.FullName).Select(x => new ValueText<long>(x.MemberId, $"{x.FullName} ({x.PhoneNumber})")).ToList(),JsonRequestBehavior.AllowGet);
        }
    }
}