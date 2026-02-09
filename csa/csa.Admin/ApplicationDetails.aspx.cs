using csa.DataLogic;
using csa.Library;
using csa.Model.DataObject;
using csa.Model.ViewModel;
using CsaModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class ApplicationDetails : BaseAdminPage
    {
        protected long CurrentApplicationId
        {
            get
            {
                return (long)ViewState["CurrentApplicationId"];
            }
            set
            {
                ViewState["CurrentApplicationId"] = value;
            }
        }
        protected int CurrentApplicationStatusId
        {
            get
            {
                return (int)ViewState["CurrentApplicationStatusId"];
            }
            set
            {
                ViewState["CurrentApplicationStatusId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idText = Request.Params["id"];
                if (long.TryParse(idText, out long id))
                {
                    SetEdit(id);
                }
                else
                {
                    Response.Redirect("Applications.aspx");
                }
            }
        }

        private void SetEdit(long applicationId)
        {
            var findApplication = ApplicationBiz.Get(applicationId);
            if (findApplication == null) Response.Redirect("Applications.aspx");

            var member = MemberBiz.Get(findApplication.MemberId);
            var referralMember = findApplication.ReferrerMemberId.HasValue ? MemberBiz.Get(findApplication.ReferrerMemberId.Value) : null;
            var state = member.StateId.HasValue ? StateBiz.Get(member.StateId.Value) : null;

            CurrentApplicationId = applicationId;
            CurrentApplicationStatusId = findApplication.ApplicationStatusId;
            //BaseApplicationDetailsViewModel vm = null;
            Dictionary<int, BaseApplicationDetailsViewModel> vmDic = new Dictionary<int, BaseApplicationDetailsViewModel>();
            vmDic.Add((int)ApplicationStatus.PRE_CHECKING, LoadPreChecking(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.PREPARATION, LoadPreparation(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.PRESENTATION, LoadProposal(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.PRESIGN, LoadPresign(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.PENDINGZOOMACCEPTANCE, LoadZoomAcceptance(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.SETTLEMENT, LoadSettlement(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.CCRIS, LoadCcris(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.QUEUEFORLOAN, LoadQueueForLoan(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.RELOAN, LoadReloan(findApplication, member, state, referralMember));
            vmDic.Add((int)ApplicationStatus.COLLECTION, LoadCollection(findApplication, member, state, referralMember));


            //switch (findApplication.ApplicationStatusId)
            //{
            //    case (int)ApplicationStatus.PRE_CHECKING:
            //        vm = LoadPreChecking(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.PREPARATION:
            //        vm = LoadPreparation(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.PRESENTATION:
            //        vm = LoadProposal(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.PRESIGN:
            //        vm = LoadPresign(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.PENDINGZOOMACCEPTANCE:
            //        vm = LoadZoomAcceptance(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.SETTLEMENT:
            //        vm = LoadSettlement(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.CCRIS:
            //        vm = LoadCcris(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.QUEUEFORLOAN:
            //        vm = LoadQueueForLoan(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.RELOAN:
            //        vm = LoadReloan(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.COLLECTION:
            //        vm = LoadCollection(findApplication, member, state, referralMember);
            //        break;
            //    case (int)ApplicationStatus.WIRA:
            //        break;
            //    case (int)ApplicationStatus.REJECTION:
            //        break;
            //    default:
            //        break;
            //}

            hfModelView.Value = JsonConvert.SerializeObject(vmDic);
        }

        ApplicationDetailsPreCheckingViewModel LoadPreChecking(Application findApplication,CsaModel.Member member,CsaModel.State state, CsaModel.Member referralMember)
        {
            var app1 = ApplicationBiz.GetApp1(findApplication.ApplicationId);
            var files = FileBiz.Get(app1.RAMCIReportFileId, app1.CCRISDocumentFileId,app1.PayslipFileId);
            var fileRamci = files.Find(x => x.FileId == app1.RAMCIReportFileId);
            var fileCcris = files.Find(x => x.FileId == app1.CCRISDocumentFileId);
            var filePayslip = files.Find(x => x.FileId == app1.PayslipFileId);
            return new ApplicationDetailsPreCheckingViewModel(findApplication.ApplicationId,findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app1.RAMCIReportFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileRamci?.FileId + fileRamci?.Extension, fileRamci?.Filename), AdminBiz.Get(app1.RAMCIReportAdminId.Value)?.Name, app1.RAMCIReportLastUpdate) : null,
                app1.CCRISDocumentFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileCcris?.FileId + fileCcris?.Extension, fileCcris?.Filename), AdminBiz.Get(app1.CCRISDocumentAdminId.Value)?.Name, app1.CCRISDocumentLastUpdate) : null,
                app1.EligibilityId.HasValue ? new OptionSelectedBy<int?>(app1.EligibilityId, AdminBiz.Get(app1.EligibilityAdminId.Value)?.Name, app1.EligibilityLastUpdate.Value) : null,
                app1.LegalSuitsCheck == 1,
                app1.BankruptcyCheck == 1,
                app1.SpecialAttentionCheck == 1,
                app1.BadPaymentRecordCheck == 1,
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.PRE_CHECKING),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.PRE_CHECKING)?.Remark,
                BuildFile(filePayslip, app1.PayslipAdminId, null, app1.PayslipLastUpdate)
                );
        }

        ApplicationDetailsPreparationViewModel LoadPreparation(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app2 = ApplicationBiz.GetApp2(findApplication.ApplicationId);
            var files = FileBiz.Get(new string[] { app2.ProposalFileId });
            var fileProposal = files.Find(x => x.FileId == app2.ProposalFileId);
            var admins = AdminBiz.GetAllAdminActive();
            var roles = RoleBiz.Gets();
            int roleVerified = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.VerifiedOfficers).RoleId;
            int roleReviewed = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.CreditDept).RoleId;
            int roleApproved = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.SalesDirector).RoleId;

            var verifiedAdmins = admins.Where(x => x.RoleId == roleVerified).Select(x=> new ValueText<int>(x.AdminId,x.Name)).ToList();
            var reviewedAdmins = admins.Where(x => x.RoleId == roleReviewed).Select(x => new ValueText<int>(x.AdminId, x.Name)).ToList();
            var approvedAdmins = admins.Where(x => x.RoleId == roleApproved).Select(x => new ValueText<int>(x.AdminId, x.Name)).ToList();

            verifiedAdmins.Insert(0, new ValueText<int>(0, "Select an option"));
            reviewedAdmins.Insert(0, new ValueText<int>(0, "Select an option"));
            approvedAdmins.Insert(0, new ValueText<int>(0, "Select an option"));
            return new ApplicationDetailsPreparationViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app2.ProposalFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileProposal?.FileId + fileProposal?.Extension, fileProposal?.Filename), AdminBiz.Get(app2.ProposalAdminId.Value)?.Name, app2.ProposalLastUpdate) : null,
                app2.SalaryGross,
                app2.SalaryDeduction,
                app2.NetIncome,
                app2.PriorDSRB1,
                app2.PriorDSRB2,
                app2.PriorDSRB3,
                app2.PriorDSRB4,
                app2.PriorDSRBAverage,
                app2.CommitmentOutstanding,
                app2.CommitmentInstallment,
                app2.OthersNetBalance,
                app2.OthersBPA,
                app2.OthersComparisonDSR,
                app2.OthersComparisonDSRPctCommitment.HasValue ? app2.OthersComparisonDSRPctCommitment.Value * 100 : (decimal?)null,
                app2.OthersPctRefresh.HasValue ? app2.OthersPctRefresh.Value * 100 : (decimal?)null,
                app2.OthersProposedRefresh,
                app2.OthersCompositionDSR,
                app2.OthersCompositionDSRPctCommitment.HasValue ? app2.OthersCompositionDSRPctCommitment.Value * 100 : (decimal?)null,
                app2.RefreshTotal,
                app2.RefreshRemainCommitment,
                app2.ReloanTotal,
                app2.ReloanMonthly,
                app2.ReloanBersih,
                app2.ReloanBelanja,
                app2.ReloanDeposit,
                app2.ReloanDanaBantuan,
                app2.ReloanServiceFee,
                app2.ReloanServiceFeePct.HasValue ? app2.ReloanServiceFeePct.Value * 100 : (decimal?)null,
                app2.ReloanIncomeAfterRNR,
                app2.ReloanDifference,
                app2.ModelBackgroundScreeningId,
                app2.ModelCompositionDSRId,
                app2.ModelCommitmentId,
                app2.ModelSettlementId,
                app2.ModelServiceFeeId,
                app2.ModelNetIncomeAfterRNRId,
                app2.ModelStatusId,
                app2.ModelStatusProposalId,
                app2.ModelCheckId,
                app2.ReviewAdminId,
                app2.ReviewStatusId,
                app2.ReviewDate,
                app2.ReviewComment,
                app2.ApproveAdminId,
                app2.ApproveDate,
                app2.ApproveComment,
                app2.ApproveStatusId,
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.PREPARATION)?.Remark,
                app2.VerifiedAdminId,
                app2.VerifiedStatusId,
                app2.VerifiedDate,
                app2.VerifiedComment,
                verifiedAdmins,
                reviewedAdmins,
                approvedAdmins
                );
        }

        ApplicationDetailsProposalViewModel LoadProposal(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app3 = ApplicationBiz.GetApp3(findApplication.ApplicationId);
            return new ApplicationDetailsProposalViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app3.ProposalStatusId.HasValue ? new OptionSelectedBy<int>(app3.ProposalStatusId.Value, AdminBiz.Get(app3.ProposalStatusAdminId.Value)?.Name, app3.ProposalStatusLastUpdate.Value) : null,
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.PRESENTATION),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.PRESENTATION)?.Remark
                );
        }

        ApplicationDetailsPresignViewModel LoadPresign(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp4(findApplication.ApplicationId);
            return new ApplicationDetailsPresignViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app.ProposalSendId.HasValue ? new OptionSelectedBy<int>(app.ProposalSendId.Value, AdminBiz.Get(app.ProposalSendAdminId.Value)?.Name, app.ProposalSendLastUpdate.Value) : null,
                app.SuratAkuanId.HasValue ? new OptionSelectedBy<int>(app.SuratAkuanId.Value, AdminBiz.Get(app.SuratAkuanAdminId.Value)?.Name, app.SuratAkuanLastUpdate.Value) : null,
                app.ComprehensiveFormId.HasValue ? new OptionSelectedBy<int>(app.ComprehensiveFormId.Value, AdminBiz.Get(app.ComprehensiveFormAdminId.Value)?.Name, app.ComprehensiveFormLastUpdate.Value) : null,
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.PRESIGN),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.PRESIGN)?.Remark
                );
        }

        ApplicationDetailsZoomAcceptanceViewModel LoadZoomAcceptance(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp5(findApplication.ApplicationId);
            var files = FileBiz.Get(app.PayslipFileId, app.RAMCIFileId,app.CTOSFileId,app.RedemptionLetterFileId);
            var filePayslip = files.Find(x => x.FileId == app.PayslipFileId);
            var fileRamci = files.Find(x => x.FileId == app.RAMCIFileId);
            var fileCtos = files.Find(x => x.FileId == app.CTOSFileId);
            var fileRedemption = files.Find(x => x.FileId == app.RedemptionLetterFileId);
            
            return new ApplicationDetailsZoomAcceptanceViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app.PayslipFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(filePayslip?.FileId + filePayslip?.Extension, filePayslip?.Filename), AdminBiz.Get(app.PayslipAdminId.Value)?.Name, app.PayslipLastUpdate) : null,
                app.RAMCIFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileRamci?.FileId + fileRamci?.Extension, fileRamci?.Filename), AdminBiz.Get(app.RAMCIAdminId.Value)?.Name, app.RAMCILastUpdate) : null,
                app.CTOSFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileCtos?.FileId + fileCtos?.Extension, fileCtos?.Filename), AdminBiz.Get(app.CTOSAdminId.Value)?.Name, app.CTOSLastUpdate) : null,
                app.RedemptionLetterFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileRedemption?.FileId + fileRedemption?.Extension, fileRedemption?.Filename), AdminBiz.Get(app.RedemptionLetterAdminId.Value)?.Name, app.RedemptionLetterLastUpdate) : null,
                member.StreetAddress1,
                app.BankruptcyStatus,
                app.LegalCase,
                app.HealthCreditScore,
                app.Commitements,
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.PENDINGZOOMACCEPTANCE),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.PENDINGZOOMACCEPTANCE)?.Remark
                );
        }

        ApplicationDetailsSettlementViewModel LoadSettlement(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp6(findApplication.ApplicationId);
            var files = FileBiz.Get(new string[] { app.PaymentReceiptFileId });
            var fileSettlement = files.Find(x => x.FileId == app.PaymentReceiptFileId);
            return new ApplicationDetailsSettlementViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app.PaymentReceiptFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileSettlement?.FileId + fileSettlement?.Extension, fileSettlement?.Filename), AdminBiz.Get(app.PaymentReceiptAdminId.Value)?.Name, app.PaymentReceiptLastUpdate) : null,
                SettlementBiz.ListByApplicationId(findApplication.ApplicationId),
                CaseUpdateBiz.ListByApplicationId(findApplication.ApplicationId),
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.SETTLEMENT),
                BankBiz.GetActiveDropdown(),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.SETTLEMENT)?.Remark,
                hudanLibrary.EnumHelper.EnumSource(typeof(FacilitiesType), insertText: "Select an option", lastText: "Others"),
                hudanLibrary.EnumHelper.EnumSource(typeof(FlexyCampaignType), insertText: "Select an option", lastText: "Others")
                );
        }

        ApplicationDetailsCcrisViewModel LoadCcris(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp7(findApplication.ApplicationId);
            var files = FileBiz.Get(app.ReleaseLetterFileId,app.CCRISReportFileId,app.HRMISFileId,app.ANMFileId,app.LPSAFileId,app.AngkasaFileId);
            var fileReleaseLetter = files.Find(x => x.FileId == app.ReleaseLetterFileId);
            var fileCcris = files.Find(x => x.FileId == app.CCRISReportFileId);
            var fileHrmis = files.Find(x => x.FileId == app.HRMISFileId);
            var fileAnm = files.Find(x => x.FileId == app.ANMFileId);
            var fileLpsa = files.Find(x => x.FileId == app.LPSAFileId);
            var fileAngkasa = files.Find(x => x.FileId == app.AngkasaFileId);
            return new ApplicationDetailsCcrisViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                app.ReleaseLetterFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileReleaseLetter?.FileId + fileReleaseLetter?.Extension, fileReleaseLetter?.Filename), AdminBiz.Get(app.ReleaseLetterAdminId.Value)?.Name, app.ReleaseLetterLastUpdate) : null,
                app.CCRISReportFileId.IsNotEmpty() ? new FileUploadedBy<ValueText<string>>(new ValueText<string>(fileCcris?.FileId + fileCcris?.Extension, fileCcris?.Filename), AdminBiz.Get(app.CCRISReportAdminId.Value)?.Name, app.CCRISReportLastUpdate) : null,                
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.CCRIS),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.CCRIS)?.Remark,
                BuildFile(fileHrmis,app.HRMISAdminId,app.HRMISLastUpdate),
                BuildFile(fileAnm, app.ANMAdminId,app.ANMLastUpdate),
                BuildFile(fileLpsa, app.LPSAAdminId,app.LPSALastUpdate),
                BuildFile(fileAngkasa, app.AngkasaAdminId,app.AngkasaLastUpdate),
                app.BankruptcyStatus,
                app.LegalCase,
                app.HealthCreditScore,
                app.Commitments,
                CaseUpdateBiz.ListByApplicationId(findApplication.ApplicationId)
                );
        }

        ApplicationDetailsQueueForLoanViewModel LoadQueueForLoan(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp8(findApplication.ApplicationId);
            var files = FileBiz.Get(app.IdentityCardFileId, app.PayslipFileId,
                app.ECFileId, app.HRMISFileId, app.BankStatementFileId, app.LPPSAFileId, app.LicenseFileId,
                app.RedemptionLetterFileId, app.CCStatementFileId, app.RAMCIFileId, app.SignatureFileId, app.BIROFileId,
                app.KEW320FileId, app.StaffCardFileId, app.PostDatedChequeFileId, app.CompanyConfirmationFileId, app.EPFFileId, app.EAFormFileId, app.BillUtilitiesFileId);
            var fileIdentity = files.Find(x => x.FileId == app.IdentityCardFileId);
            var filePayslip = files.Find(x => x.FileId == app.PayslipFileId);
            var fileEc = files.Find(x => x.FileId == app.ECFileId);
            var fileHrmis = files.Find(x => x.FileId == app.HRMISFileId);
            var fileBankSettlement = files.Find(x => x.FileId == app.BankStatementFileId);
            var fileLppsa = files.Find(x => x.FileId == app.LPPSAFileId);
            var fileLicense= files.Find(x => x.FileId == app.LicenseFileId);
            var fileRedemptionLetter= files.Find(x => x.FileId == app.RedemptionLetterFileId);
            var fileCc= files.Find(x => x.FileId == app.CCStatementFileId);
            var fileRamci= files.Find(x => x.FileId == app.RAMCIFileId);
            var fileSignature= files.Find(x => x.FileId == app.SignatureFileId);
            var fileBiro= files.Find(x => x.FileId == app.BIROFileId);
            var fileKew320= files.Find(x => x.FileId == app.KEW320FileId);
            var fileStaffCard= files.Find(x => x.FileId == app.StaffCardFileId);
            var fileSPostDatedCheque= files.Find(x => x.FileId == app.PostDatedChequeFileId);
            var fileCompanyConfirmation= files.Find(x => x.FileId == app.CompanyConfirmationFileId);
            var fileEpf= files.Find(x => x.FileId == app.EPFFileId);
            var fileEaform= files.Find(x => x.FileId == app.EAFormFileId);
            var fileBillUtilities= files.Find(x => x.FileId == app.BillUtilitiesFileId);
            return new ApplicationDetailsQueueForLoanViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                BuildFile(fileIdentity, app.IdentityCardAdminId, app.IdentityCardMemberId, app.IdentityCardLastUpdate),
                BuildFile(filePayslip, app.PayslipAdminId, app.IdentityCardMemberId, app.PayslipLastUpdate),
                BuildFile(fileEc, app.ECAdminId, app.ECMemberId, app.ECLastUpdate),
                BuildFile(fileHrmis, app.HRMISAdminId, app.HRMISMemberId, app.HRMISLastUpdate),
                BuildFile(fileBankSettlement, app.BankStatementAdminId, app.BankStatementMemberId, app.BankStatementLastUpdate),
                BuildFile(fileLppsa, app.LPPSAAdminId, app.LPPSAAdminId, app.LPPSALastUpdate),
                BuildFile(fileLicense, app.LicenseAdminId, app.LicenseMemberId, app.LicenseLastUpdate),
                BuildFile(fileRedemptionLetter, app.RedemptionLetterAdminId, app.RedemptionLetterMemberId, app.RedemptionLetterLastUpdate),
                BuildFile(fileCc, app.CCStatementAdminId, app.CCStatementMemberId, app.CCStatementLastUpdate),
                BuildFile(fileRamci, app.RAMCIAdminId, app.RAMCIMemberId, app.RAMCILastUpdate),
                BuildFile(fileSignature, app.SignatureAdminId, app.SignatureMemberId, app.SignatureLastUpdate),
                BuildFile(fileBiro, app.BIROAdminId, app.BIROMemberId, app.BIROLastUpdate),
                BuildFile(fileKew320, app.KEW320AdminId, app.KEW320MemberId, app.KEW320LastUpdate),
                BuildFile(fileStaffCard, app.StaffCardAdminId, app.StaffCardMemberId, app.StaffCardLastUpdate),
                BuildFile(fileSPostDatedCheque, app.PostDatedChequeAdminId, app.PostDatedChequeMemberId, app.PostDatedChequeLastUpdate),
                BuildFile(fileCompanyConfirmation, app.CompanyConfirmationAdminId, app.CompanyConfirmationMemberId, app.CompanyConfirmationLastUpdate),
                BuildFile(fileEpf, app.EPFAdminId, app.EPFMemberId, app.EPFLastUpdate),
                BuildFile(fileEaform, app.EAFormAdminId, app.EAFormMemberId, app.EAFormLastUpdate),
                BuildFile(fileBillUtilities, app.BillUtilitiesAdminId, app.BillUtilitiesMemberId, app.BillUtilitiesLastUpdate),
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.QUEUEFORLOAN),
                app.WorkerTypeId,
                ApplicationFileBiz.ListFileByApplicationIdAndGroup(findApplication.ApplicationId, group: "payslip"),
                ApplicationFileBiz.ListFileByApplicationIdAndGroup(findApplication.ApplicationId, group: "bank_statement"),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.QUEUEFORLOAN)?.Remark
                );
        }

        FileUploadedBy<ValueText<string>> BuildFile(File file,int? adminId,long? memberId,DateTime? date)
        {
            if (file == null) return null;
            return new FileUploadedBy<ValueText<string>>(new ValueText<string>(file?.FileId + file?.Extension, file?.Filename), adminId.HasValue ? AdminBiz.Get(adminId.Value)?.Name : MemberBiz.Get(memberId.Value)?.FullName, date);
        }

        FileUploadedBy<ValueText<string>> BuildFile(File file, int? adminId, DateTime? date)
        {
            if (file == null) return null;
            return new FileUploadedBy<ValueText<string>>(new ValueText<string>(file?.FileId + file?.Extension, file?.Filename), AdminBiz.Get(adminId.Value)?.Name, date);
        }

        ApplicationDetailsReloanViewModel LoadReloan(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp9(findApplication.ApplicationId);
            var files = FileBiz.Get(new string[] { app.OfferLetterFileId });
            var fileOfferLetter = files.Find(x => x.FileId == app.OfferLetterFileId);
            return new ApplicationDetailsReloanViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                BuildFile(fileOfferLetter, app.OfferLetterAdminId,app.OfferLetterLastUpdate),
                app.ReloadStatusId,
                app.ApprovedDate,
                app.SigningDate,
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.RELOAN),
                app.ApprovedAmount,
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.RELOAN)?.Remark
                );
        }

        ApplicationDetailsCollectionViewModel LoadCollection(Application findApplication, CsaModel.Member member, CsaModel.State state, CsaModel.Member referralMember)
        {
            var app = ApplicationBiz.GetApp10(findApplication.ApplicationId);
            var files = FileBiz.Get(app.DeclarationFormFileId,app.SettlementReceiptFileId,app.ServiceFeeReceiptFileId,app.RezekiReceiptFileId,app.RezekiAgreementFileId);
            var fileDeclarationLetter = files.Find(x => x.FileId == app.DeclarationFormFileId);
            var fileSettlement = files.Find(x => x.FileId == app.SettlementReceiptFileId);
            var fileService = files.Find(x => x.FileId == app.ServiceFeeReceiptFileId);
            var fileRezekiReceipt = files.Find(x => x.FileId == app.RezekiReceiptFileId);
            var fileRezekiAgreement = files.Find(x => x.FileId == app.RezekiAgreementFileId);
            return new ApplicationDetailsCollectionViewModel(findApplication.ApplicationId, findApplication.ApplicationStatusId, ApplicationBiz.ConvertToInfoProcess(findApplication, member, state, referralMember),
                BuildFile(fileDeclarationLetter, app.DeclarationFormAdminId,app.DeclarationFormLastUpdate),
                BuildFile(fileSettlement, app.SettlementReceiptAdminId,app.SettlementReceiptLastUpdate),
                BuildFile(fileService, app.ServiceFeeReceiptAdminId,app.ServiceFeeReceiptLastUpdate),
                BuildFile(fileRezekiReceipt, app.RezekiReceiptAdminId,app.RezekiReceiptLastUpdate),
                BuildFile(fileRezekiAgreement, app.RezekiAgreementAdminId,app.RezekiAgreementLastUpdate),
                app.ServiceFee,
                app.DepositAmount,
                app.DepositDate,
                ApplicationDocumentBiz.ListDocumentByApplicationIdAndStatus(findApplication.ApplicationId, (int)ApplicationStatus.COLLECTION),
                ApplicationBiz.GetAppRemark(findApplication.ApplicationId, (int)ApplicationStatus.COLLECTION)?.Remark,
                app.SettlementDate,
                app.SettlementAmount,
                app.SettlementCPct.HasValue ? app.SettlementCPct.Value * 100 : (decimal?)null,
                app.CollectionAmountPct.HasValue ? app.CollectionAmountPct.Value * 100 : (decimal?)null,
                app.SettlementDuration,
                app.TotalReloan,
                app.TotalLoanRepayment,
                app.DBBBankAccount,
                app.DBBTenure,
                app.DBBAgreementDate,
                app.MonthlyFund,
                app.DBBAmount,
                app.ReceiptNo,
                app.TaxNumber,
                app.StatusId,
                app.InstallmentDate
                );
        }
    }
}