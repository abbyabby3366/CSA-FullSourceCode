using csa.Library;
using csa.Model;
using csa.Model.DataObject;
using csa.Model.Validator;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace csa.DataLogic
{
    public static class ApplicationBiz
    {        
        public static RespArgs<GridViewModel<ApplicationGVByMember>> GetApplicationGVByMember(long memberId, string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("application app");
                sqlSelect.AddSelect("app.ApplicationId,app.Name as CustomerName,app.ContactNo,app.SalaryRangeId,app.StatusId");
                sqlSelect.AddWhere($"app.CreatorMemberId = {memberId}");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(app.Name LIKE '%{search}%' OR app.ContactNo LIKE '%{search}%')");

                var list = db.ExecuteStoreQuery<ApplicationGVByMember>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ApplicationGVByMember>>.CreateSuccess(new GridViewModel<ApplicationGVByMember>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static RespArgs<GridViewModel<ApplicationGVByAdmin>> GetApplicationGVByAdmin(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection,int applicationStatusId,int? adminId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                search = search.ToLiteral();
                var sqlSelect = new SQLSelect("application app");
                sqlSelect.AddSelect("app.RejectedDate,app.ApplicationStatusId,app.CustomerStatusId,creator.CompanyTypeId,creator.FileNumber");
                sqlSelect.AddSelect("app.ReferrerMemberId,app.AMAdminId,PFCAdminId,RMAdminId,UMAdminId,PAAdminId");//for check assign exist or not
                sqlSelect.AddSelect("app.ApplicationId,CONCAT(IFNULL(creator.FirstName,''),' ',IFNULL(creator.LastName,'')) as CustomerName,creator.MemberId as CustomerId,creator.CompanyName,creator.PhoneNumber as ContactNo,creator.Salary,app.ApplicationStatusId,app.CreateDate");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);
                sqlSelect.AddJoin("member creator", "app.MemberId", "creator.MemberId");

                if(applicationStatusId != -1)
                {
                    sqlSelect.AddWhere($"app.ApplicationStatusId={applicationStatusId}");
                }

                if(adminId > 0) sqlSelect.AddWhere($"(app.AMAdminId={adminId} OR app.PFCAdminId={adminId} OR app.UMAdminId={adminId} OR app.PAAdminId={adminId} OR app.PreparedAdminId={adminId} OR app.AnalyzedAdminId={adminId})");

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(creator.PhoneNumber LIKE '%{search}%' OR CONCAT(IFNULL(creator.FirstName,''),' ',IFNULL(creator.LastName,'')) LIKE '%{search}%')");

                var list = db.ExecuteStoreQuery<ApplicationGVByAdmin>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                return RespArgs<GridViewModel<ApplicationGVByAdmin>>.CreateSuccess(new GridViewModel<ApplicationGVByAdmin>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }

        public static Application Get(long applicationId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Applications.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application1 GetApp1(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application1s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application2 GetApp2(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application2s.FirstOrDefault(x => x.ApplicationID == applicationId);
            }
        }

        public static Application3 GetApp3(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application3s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application4 GetApp4(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application4s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application5 GetApp5(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application5s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application6 GetApp6(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application6s.FirstOrDefault(x => x.ApplicationID == applicationId);
            }
        }

        public static Application7 GetApp7(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application7s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application8 GetApp8(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application8s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application9 GetApp9(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application9s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static Application10 GetApp10(long applicationId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Application10s.FirstOrDefault(x => x.ApplicationId == applicationId);
            }
        }

        public static ApplicationRemark GetAppRemark(long applicationId,int applicationStatusId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.ApplicationRemarks.FirstOrDefault(x => x.ApplicationId == applicationId && x.ApplicationStatusId == applicationStatusId);
            }
        }

        public static Application GetLastByMemberId(long memberId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Applications.Where(x => x.MemberId == memberId).OrderByDescending(x => x.CreateDate).FirstOrDefault();
            }
        }

        public static ApplicationDetailsInfo ConvertToInfo(Application application,Member member, State state, Member referral)
        {
            string verifiedBy = "";
            var app2 = ApplicationBiz.GetApp2(application.ApplicationId);
            if(app2 != null)
            {
                if (app2.ReviewAdminId.HasValue) verifiedBy = AdminBiz.Get(app2.ReviewAdminId.Value)?.Name;
            }

            string pfcName = "";
            if (application.PFCAdminId.HasValue)
            {
                pfcName = AdminBiz.Get(application.PFCAdminId.Value)?.Name;
            }
            else if (application.RMAdminId.HasValue)
            {
                pfcName = AdminBiz.Get(application.RMAdminId.Value)?.Name;
            }
            return new ApplicationDetailsInfo(member.FullName, member.ICNumber, member.Salary, null, member.CompanyName, state?.Name, member.RetirementAge, pfcName, application.SourceId, referral?.FullName, referral?.FileNumber, application.PFCAdminId.HasValue ? AdminBiz.Get(application.PFCAdminId.Value)?.Name : "", verifiedBy, application.CreateDate,application.CreditRemark);
        }

        public static ApplicationDetailsInfoProcess ConvertToInfoProcess(Application application, Member member, State state, Member referral)
        {
            string verifiedBy = "";
            var app2 = ApplicationBiz.GetApp2(application.ApplicationId);
            if (app2 != null)
            {
                if (app2.ReviewAdminId.HasValue) verifiedBy = AdminBiz.Get(app2.ReviewAdminId.Value)?.Name;
            }

            string pfcName = "";
            if (application.PFCAdminId.HasValue)
            {
                pfcName = AdminBiz.Get(application.PFCAdminId.Value)?.Name;
            }
            else if(application.RMAdminId.HasValue)
            {
                pfcName = AdminBiz.Get(application.RMAdminId.Value)?.Name;
            }
            return new ApplicationDetailsInfoProcess(member.FullName, member.ICNumber, member.Salary, null, member.CompanyName, state?.Name, member.RetirementAge, pfcName, application.SourceId, referral?.FullName, referral?.FileNumber, application.PFCAdminId.HasValue ? AdminBiz.Get(application.PFCAdminId.Value)?.Name : "", verifiedBy, application.CreateDate, application.CreditRemark,application.CustomerStatusId,application.BurstReasonId,application.ApplicationStatusId,
                application.RejectedDate, application.RejectedAdminId.HasValue ? AdminBiz.Get(application.RejectedAdminId.Value)?.Name : "",application.RejectedReason, application.ApplicationStatusLastChangeAdminId.HasValue ? new OptionSelectedBy<int>(application.ApplicationStatusLastChangeAdminId.Value,AdminBiz.Get(application.ApplicationStatusLastChangeAdminId.Value)?.Name,application.ApplicationStatusLastChangeDate) : null,application.CreditStatusId,application.ScoreClass);
        }

        public static async Task<RespArgs<bool>> SavePreCheckingAsync(RequestApplicationPreCheckingByAdmin req, File ramciFile, File ccrisFile, File payslipFile, List<File> docs)
        {
            using (var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestApplicationPreCheckingData>(req.Json);
                    var currentApplication = db.Application1s.FirstOrDefault(x => x.ApplicationId == data.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == data.ApplicationId);

                    if (data.FileDelete != null)
                    {
                        if (data.FileDelete.Any(x => x == nameof(currentApplication.RAMCIReportFileId))) currentApplication.RAMCIReportFileId = null;
                        if (data.FileDelete.Any(x => x == nameof(currentApplication.CCRISDocumentFileId))) currentApplication.CCRISDocumentFileId = null;
                        if (data.FileDelete.Any(x => x == nameof(currentApplication.PayslipFileId))) currentApplication.PayslipFileId = null;
                    }

                    var dt = DateTime.Now;
                    currentApplication.LegalSuitsCheck = data.LegalSuit ? 1 : 0;
                    currentApplication.BankruptcyCheck = data.Bankruptcy ? 1 : 0;
                    currentApplication.SpecialAttentionCheck = data.SpecialAttentionAccount ? 1 : 0;
                    currentApplication.BadPaymentRecordCheck = data.BadPaymentRecordCheck ? 1 : 0;
                    mainApplication.CustomerStatusId = data.Eligibility ?? (int)CustomerStatus.Eligible;
                    if (currentApplication.EligibilityId != data.Eligibility)
                    {
                        currentApplication.EligibilityId = data.Eligibility;
                        currentApplication.EligibilityAdminId = data.AdminId;
                        currentApplication.EligibilityLastUpdate = dt;
                    }

                    if (ramciFile != null)
                    {
                        db.Files.AddObject(ramciFile);
                        await db.SaveChangesAsync();

                        currentApplication.RAMCIReportFileId = ramciFile.FileId;
                        currentApplication.RAMCIReportAdminId = data.AdminId;
                        currentApplication.RAMCIReportLastUpdate = dt;
                    }

                    if (ccrisFile != null)
                    {
                        db.Files.AddObject(ccrisFile);
                        await db.SaveChangesAsync();

                        currentApplication.CCRISDocumentFileId = ccrisFile.FileId;
                        currentApplication.CCRISDocumentAdminId = data.AdminId;
                        currentApplication.CCRISDocumentLastUpdate = dt;
                    }

                    if (payslipFile != null)
                    {
                        db.Files.AddObject(payslipFile);
                        await db.SaveChangesAsync();

                        currentApplication.PayslipFileId = payslipFile.FileId;
                        currentApplication.PayslipAdminId = data.AdminId;
                        currentApplication.PayslipLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.PRE_CHECKING,
                            FileId = doc.FileId,
                            Remark = data.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = data.AdminId
                        });
                    }

                    foreach (var item in data.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    if (data.DocumentDelete != null)
                    {
                        foreach (var d in data.DocumentDelete)
                        {
                            var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                            db.ApplicationDocuments.DeleteObject(doDelete);
                        }
                    }

                    await SaveApplicationRemark(db, data.ApplicationRemark, mainApplication.ApplicationId,data.AdminId, dt,(int)ApplicationStatus.PRE_CHECKING);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, data.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }            
        }

        private static async Task SaveApplicationRemark(CsaEntities db, string remark, long applicationId,int adminId, DateTime dt,int applicationStatusId)
        {
            var findApplicationRemark = db.ApplicationRemarks.FirstOrDefault(x => x.ApplicationId == applicationId && x.ApplicationStatusId == applicationStatusId);
            if (findApplicationRemark != null)
            {
                findApplicationRemark.Remark = remark;
                findApplicationRemark.UpdatedAdminId = adminId;
                findApplicationRemark.UpdatedDate = dt;
            }
            else
            {
                db.ApplicationRemarks.AddObject(new ApplicationRemark { ApplicationId = applicationId, Remark = remark, CreatedDate = dt,AdminId = adminId,ApplicationStatusId = applicationStatusId });
            }
            await db.SaveChangesAsync();
        }

        static async Task CreateApplicationHistory(CsaEntities db, long applicationId,int? adminId,long? memberId)
        {
            db.ContextOptions.LazyLoadingEnabled = false;
            var appObj = db.Applications.FirstOrDefault(x => x.ApplicationId == applicationId);
            if (appObj == null) throw new NullReferenceException("application_not_found");
            appObj.Application1s.Load();
            appObj.Application2s.Load();
            appObj.Application3s.Load();
            appObj.Application4s.Load();
            appObj.Application5s.Load();
            appObj.Application6s.Load();
            appObj.Application7s.Load();
            appObj.Application8s.Load();
            appObj.Application9s.Load();
            appObj.Application10s.Load();
            appObj.ApplicationFiles.Load();
            appObj.ApplicationDocuments.Load();
            appObj.Settlements.Load();
            appObj.Caseupdates.Load();

            string appJson = Newtonsoft.Json.JsonConvert.SerializeObject(appObj);
            db.ApplicationHistories.AddObject(new ApplicationHistory
            {
                ApplicationId = appObj.ApplicationId,
                CreateDate = DateTime.Now,
                AdminId = adminId,
                MemberId = memberId,
                Content = appJson
            });
            await db.SaveChangesAsync();
        }

        public static async Task<RespArgs<bool>> SavePreparationAsync(RequestApplicationPreparationByAdmin req, File proposalFile)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestApplicationPreparationData>(req.Json);
                    var currentApplication = db.Application2s.FirstOrDefault(x => x.ApplicationID == data.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == data.ApplicationId);

                    if (data.FileDelete.Any(x => x == nameof(currentApplication.ProposalFileId)))
                    {
                        currentApplication.ProposalFileId = null;
                    }

                    var dt = DateTime.Now;
                    if (proposalFile != null)
                    {
                        db.Files.AddObject(proposalFile);
                        await db.SaveChangesAsync();

                        currentApplication.ProposalFileId = proposalFile.FileId;
                        currentApplication.ProposalAdminId = data.AdminId;
                        currentApplication.ProposalLastUpdate = dt;
                    }
                    currentApplication.SalaryGross = data.SalaryGross;
                    currentApplication.SalaryDeduction = data.SalaryDeduction;
                    currentApplication.NetIncome = data.NetIncome;
                    currentApplication.PriorDSRB1 = data.B1;
                    currentApplication.PriorDSRB2 = data.B2;
                    currentApplication.PriorDSRB3 = data.B3;
                    currentApplication.PriorDSRB4 = data.B4;
                    currentApplication.PriorDSRBAverage = data.BAverage;
                    currentApplication.CommitmentOutstanding = data.CommitmentOutstanding;
                    currentApplication.CommitmentInstallment = data.CommitmentInstallment;
                    currentApplication.OthersNetBalance = data.OtherNetBalance;
                    currentApplication.OthersBPA = data.OtherBPA;
                    currentApplication.OthersComparisonDSR = data.OtherComparisonDSR;
                    currentApplication.OthersComparisonDSRPctCommitment = data.OtherComparisonDSRPctCommitment.HasValue ? data.OtherComparisonDSRPctCommitment.Value / 100 : (decimal?)null;
                    currentApplication.OthersPctRefresh = data.OtherPctRefresh.HasValue ? data.OtherPctRefresh.Value / 100 : (decimal?)null;
                    currentApplication.OthersProposedRefresh = data.OtherProposedRefresh;
                    currentApplication.OthersCompositionDSR = data.OtherCompositionDSR;
                    currentApplication.OthersCompositionDSRPctCommitment = data.OtherCompositionDSRPctCommitment.HasValue ? data.OtherCompositionDSRPctCommitment.Value / 100 : (decimal?)null;
                    currentApplication.RefreshTotal = data.RefreshTotal;
                    currentApplication.RefreshRemainCommitment = data.RefreshRemainCommitment;
                    currentApplication.ReloanTotal = data.ReloanTotal;
                    currentApplication.ReloanMonthly = data.ReloanMonthly;
                    currentApplication.ReloanBersih = data.ReloanBersih;
                    currentApplication.ReloanBelanja = data.ReloanBelanja;
                    currentApplication.ReloanDeposit = data.ReloanDeposit;
                    currentApplication.ReloanDanaBantuan = data.ReloanDanaBantuan;
                    currentApplication.ReloanServiceFee = data.ReloanServiceFee;
                    currentApplication.ReloanServiceFeePct = data.ReloanServiceFeePct.HasValue ? data.ReloanServiceFeePct.Value / 100 : (decimal?)null;
                    currentApplication.ReloanIncomeAfterRNR = data.ReloanIncomeAfterRNR;
                    currentApplication.ReloanDifference = data.ReloanDifference;
                    currentApplication.ModelBackgroundScreeningId = data.ModelBackgroundScreeningId;
                    currentApplication.ModelCompositionDSRId = data.ModelCompositionDSRId;
                    currentApplication.ModelCommitmentId = data.ModelCommitmentId;
                    currentApplication.ModelSettlementId = data.ModelSettlementId;
                    currentApplication.ModelServiceFeeId = data.ModelServiceFeeId;
                    currentApplication.ModelNetIncomeAfterRNRId = data.ModelNetIncomeAfterRNRId;
                    currentApplication.ModelStatusId = data.ModelStatusId;
                    currentApplication.ModelStatusProposalId = data.ModelStatusProposalId;
                    currentApplication.ModelCheckId = data.ModelCheckId;
                    currentApplication.ReviewAdminId = data.ReviewAdminId.ToInt().IfZeroToNull();
                    currentApplication.ReviewStatusId = data.ReviewStatusId;
                    currentApplication.ReviewDate = data.ReviewDate;
                    currentApplication.ReviewComment = data.ReviewComment;
                    currentApplication.ApproveAdminId = data.ApproveAdminId.ToInt().IfZeroToNull();
                    currentApplication.ApproveStatusId = data.ApproveStatusId;
                    currentApplication.ApproveDate = data.ApproveDate;
                    currentApplication.ApproveComment = data.ApproveComment;
                    currentApplication.VerifiedAdminId = data.VerifiedAdminId.ToInt().IfZeroToNull();
                    currentApplication.VerifiedStatusId = data.VerifiedStatusId;
                    currentApplication.VerifiedDate = data.VerifiedDate;
                    currentApplication.VerifiedComment = data.VerifiedComment;

                    if(currentApplication.ApproveStatusId == (int)ApproveStatus.APPROVED_SINGLE)
                    {
                        mainApplication.CreditStatusId = (int)CreditStatus.SINGLE;
                    }
                    else if (currentApplication.ApproveStatusId == (int)ApproveStatus.APPROVED_RNR)
                    {
                        mainApplication.CreditStatusId = (int)CreditStatus.RANCANG_REZEKI_RNR;
                    }
                    if (currentApplication.ApproveStatusId == (int)ApproveStatus.REJECTED)
                    {
                        if(currentApplication.ApproveAdminId.HasValue)
                        {
                            var admin = db.Admins.FirstOrDefault(x => x.AdminId == currentApplication.ApproveAdminId.Value);
                            var role = db.Roles.FirstOrDefault(x => x.RoleId == admin.RoleId);
                            if(role != null)
                            {
                                if(role.AccessList.ToInt() == (int)AdminRoleType.CreditTeam)
                                {
                                    mainApplication.CreditStatusId = (int)CreditStatus.BURST;
                                }
                                else if (role.AccessList.ToInt() == (int)AdminRoleType.SalesDirector)
                                {
                                    mainApplication.CreditStatusId = (int)CreditStatus.DECLINED_BY_AM;
                                }
                            }
                        }                                               
                    }

                    //credit remark update
                    mainApplication.CreditRemark = data.CreditRemark;

                    await SaveApplicationRemark(db, data.ApplicationRemark, mainApplication.ApplicationId, data.AdminId, dt, (int)ApplicationStatus.PREPARATION);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, data.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveProposalAsync(RequestApplicationProposalPresentationData req,List<File> docs)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application3s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    var dt = DateTime.Now;
                    if (currentApplication.ProposalStatusId != req.ProposalStatusId)
                    {
                        currentApplication.ProposalStatusId = req.ProposalStatusId;
                        currentApplication.ProposalStatusAdminId = req.AdminId;
                        currentApplication.ProposalStatusLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.PRESENTATION,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.PRESENTATION);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SavePresignAsync(RequestApplicationPreSigningData req, List<File> docs)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application4s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    var dt = DateTime.Now;
                    if (currentApplication.ProposalSendId != req.ProposalSendId)
                    {
                        currentApplication.ProposalSendId = req.ProposalSendId;
                        currentApplication.ProposalSendAdminId = req.AdminId;
                        currentApplication.ProposalSendLastUpdate = dt;
                    }

                    if (currentApplication.SuratAkuanId != req.SuratAkuanId)
                    {
                        currentApplication.SuratAkuanId = req.SuratAkuanId;
                        currentApplication.SuratAkuanAdminId = req.AdminId;
                        currentApplication.SuratAkuanLastUpdate = dt;
                    }

                    if (currentApplication.ComprehensiveFormId != req.ComprehensiveFormId)
                    {
                        currentApplication.ComprehensiveFormId = req.ComprehensiveFormId;
                        currentApplication.ComprehensiveFormAdminId = req.AdminId;
                        currentApplication.ComprehensiveFormLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId.Value,
                            ApplicationStatusId = (int)ApplicationStatus.PRESIGN,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.PRESIGN);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }
        public static async Task<RespArgs<bool>> SaveZoomAcceptanceAsync(RequestApplicationPendingAcceptanceData req, File payslipFile,File ramciFile,File ctosFile,File redemptionLetterFile,List<File> docs)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application5s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.PayslipFileId)))
                    {
                        currentApplication.PayslipFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.RAMCIFileId)))
                    {
                        currentApplication.RAMCIFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.CTOSFileId)))
                    {
                        currentApplication.CTOSFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.RedemptionLetterFileId)))
                    {
                        currentApplication.RedemptionLetterFileId = null;
                    }

                    var dt = DateTime.Now;
                    if (payslipFile != null)
                    {
                        db.Files.AddObject(payslipFile);
                        await db.SaveChangesAsync();

                        currentApplication.PayslipFileId = payslipFile.FileId;
                        currentApplication.PayslipAdminId = req.AdminId;
                        currentApplication.PayslipLastUpdate = dt;
                    }

                    if (ramciFile != null)
                    {
                        db.Files.AddObject(ramciFile);
                        await db.SaveChangesAsync();

                        currentApplication.RAMCIFileId = ramciFile.FileId;
                        currentApplication.RAMCIAdminId = req.AdminId;
                        currentApplication.RAMCILastUpdate = dt;
                    }

                    if (ctosFile != null)
                    {
                        db.Files.AddObject(ctosFile);
                        await db.SaveChangesAsync();

                        currentApplication.CTOSFileId = ctosFile.FileId;
                        currentApplication.CTOSAdminId = req.AdminId;
                        currentApplication.CTOSLastUpdate = dt;
                    }

                    if (redemptionLetterFile != null)
                    {
                        db.Files.AddObject(redemptionLetterFile);
                        await db.SaveChangesAsync();

                        currentApplication.RedemptionLetterFileId = redemptionLetterFile.FileId;
                        currentApplication.RedemptionLetterAdminId = req.AdminId;
                        currentApplication.RedemptionLetterLastUpdate = dt;
                    }

                    currentApplication.ApplicantAddress = req.ApplicantAddress;
                    currentApplication.BankruptcyStatus = req.BankruptcyStatus;
                    currentApplication.LegalCase = req.LegalCase;
                    currentApplication.HealthCreditScore = req.HealthCreditScore;
                    currentApplication.Commitements = req.Commitments;

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.PENDINGZOOMACCEPTANCE,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.PENDINGZOOMACCEPTANCE);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveSettlementAsync(RequestApplicationSettlementData req, File settlementFile, List<File> docs)
        {
            using (var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application6s.FirstOrDefault(x => x.ApplicationID == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.PaymentReceiptFileId)))
                    {
                        currentApplication.PaymentReceiptFileId = null;
                    }

                    var dt = DateTime.Now;
                    if (settlementFile != null)
                    {
                        db.Files.AddObject(settlementFile);
                        await db.SaveChangesAsync();

                        currentApplication.PaymentReceiptFileId = settlementFile.FileId;
                        currentApplication.PaymentReceiptAdminId = req.AdminId;
                        currentApplication.PaymentReceiptLastUpdate = dt;
                    }

                    foreach (var item in req.SettlementDetails)
                    {
                        if (item.SettlementId.Value < 0)
                        {
                            db.Settlements.AddObject(new Settlement
                            {
                                ApplicationID = (int)currentApplication.ApplicationID,
                                Amount = item.Amount,
                                TotalPct = item.TotalPct.HasValue ? item.TotalPct.Value / 100 : (decimal?)null,
                                TotalPctAmount = item.TotalPctAmount,
                                PaymentDate = item.PaymentDate,
                                BankId = item.BankId.ToInt().IfZeroToNull(),
                                BankAccountNumber = item.BankAccountNumber,
                                DueDate = item.DueDate,
                                FacilitiesOther = item.FacilitiesOther,
                                FacilitiesId = item.FacilitiesId,
                                AmountFacilities = item.AmountFacilities,
                                FlexyCampaignId = item.FlexyCampaignId,
                                FlexyCampaignOther = item.FlexyCampaignOther,
                                TotalCampaign = item.TotalCampaign,
                                RedemptionLetterDate = item.RedemptionLetterDate,
                                RedemptionAmount = item.RedemptionAmount,
                                LoanReleaseDate = item.LoanReleaseDate,
                                SettlementStatusId = item.SettlementStatusId,
                                Remark = item.Remark,
                                CreateAdminId = req.AdminId
                            });
                        }
                        else
                        {
                            var findSettlement = db.Settlements.FirstOrDefault(x => x.SettlementId == item.SettlementId);
                            if (findSettlement != null)
                            {
                                findSettlement.Amount = item.Amount;
                                findSettlement.TotalPct = item.TotalPct.HasValue ? item.TotalPct.Value / 100 : (decimal?)null;
                                findSettlement.TotalPctAmount = item.TotalPctAmount;
                                findSettlement.PaymentDate = item.PaymentDate;
                                findSettlement.BankId = item.BankId.ToInt().IfZeroToNull();
                                findSettlement.BankAccountNumber = item.BankAccountNumber;
                                findSettlement.DueDate = item.DueDate;
                                findSettlement.FacilitiesOther = item.FacilitiesOther;
                                findSettlement.FacilitiesId = item.FacilitiesId;
                                findSettlement.AmountFacilities = item.AmountFacilities;
                                findSettlement.FlexyCampaignId = item.FlexyCampaignId;
                                findSettlement.FlexyCampaignOther = item.FlexyCampaignOther;
                                findSettlement.TotalCampaign = item.TotalCampaign;
                                findSettlement.RedemptionLetterDate = item.RedemptionLetterDate;
                                findSettlement.RedemptionAmount = item.RedemptionAmount;
                                findSettlement.LoanReleaseDate = item.LoanReleaseDate;
                                findSettlement.SettlementStatusId = item.SettlementStatusId;
                                findSettlement.Remark = item.Remark;
                                findSettlement.CreateAdminId = req.AdminId;
                            }
                        }
                    }

                    foreach (var item in req.CaseUpdates)
                    {
                        if (item.CaseUpdateId < 0)
                        {
                            db.Caseupdates.AddObject(new Caseupdate
                            {
                                ApplicationId = currentApplication.ApplicationID,
                                BankId = item.BankId.ToInt().IfZeroToNull(),
                                LoanAmount = item.LoanAmount,
                                SubmitDate = item.SubmitDate,
                                Banker = item.Banker,
                                CompleteStatusId = item.CompleteStatusId,
                                Consolidate = item.Consolidate,
                                CashNet = item.CashNet,
                                Instalment = item.Installment,
                                ApprovedDate = item.ApprovedDate,
                                SignDate = item.SignDate,
                                DisbursementDate = item.DisbursementDate,
                                UpdateDate = item.UpdateDate,
                                LoanAccountNumber = item.LoanAccountNumber,
                                StDueDate = item.FirstDueDate,
                                Remarkds = item.Remarkds,
                                AdminId = req.AdminId
                            });
                        }
                        else
                        {
                            var find = db.Caseupdates.FirstOrDefault(x => x.CaseUpdateId == item.CaseUpdateId);
                            if (find != null)
                            {
                                find.BankId = item.BankId.ToInt().IfZeroToNull();
                                find.LoanAmount = item.LoanAmount;
                                find.SubmitDate = item.SubmitDate;
                                find.Banker = item.Banker;
                                find.CompleteStatusId = item.CompleteStatusId;
                                find.Consolidate = item.Consolidate;
                                find.CashNet = item.CashNet;
                                find.Instalment = item.Installment;
                                find.ApprovedDate = item.ApprovedDate;
                                find.SignDate = item.SignDate;
                                find.DisbursementDate = item.DisbursementDate;
                                find.UpdateDate = item.UpdateDate;
                                find.LoanAccountNumber = item.LoanAccountNumber;
                                find.StDueDate = item.FirstDueDate;
                                find.Remarkds = item.Remarkds;
                                find.AdminId = req.AdminId;
                            }
                        }
                    }

                    foreach (var item in req.CaseUpdateDelete)
                    {
                        var doDelete = db.Caseupdates.FirstOrDefault(x => x.CaseUpdateId == item);
                        db.Caseupdates.DeleteObject(doDelete);
                    }

                    foreach (var item in req.SettlementDelete)
                    {
                        var doDelete = db.Settlements.FirstOrDefault(x => x.SettlementId == item);
                        db.Settlements.DeleteObject(doDelete);
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationID,
                            ApplicationStatusId = (int)ApplicationStatus.SETTLEMENT,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.SETTLEMENT);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    await MemberBiz.RecalculateFileNumber(db, mainApplication.MemberId);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveCcrisAsync(RequestApplicationCcrisData req, File releaseLetterFile, File ccrisReportFile, File hrmisFile, File anmFile, File lpsaFile, File angkasaFile, List<File> docs)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application7s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.ReleaseLetterFileId))) currentApplication.ReleaseLetterFileId = null;
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.CCRISReportFileId))) currentApplication.CCRISReportFileId = null;
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.HRMISFileId))) currentApplication.HRMISFileId = null;
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.ANMFileId))) currentApplication.ANMFileId = null;
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.LPSAFileId))) currentApplication.LPSAFileId = null;
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.AngkasaFileId))) currentApplication.AngkasaFileId = null;

                    currentApplication.BankruptcyStatus = req.BankruptcyStatus;
                    currentApplication.LegalCase = req.LegalCase;
                    currentApplication.HealthCreditScore = req.HealthCreditScore;
                    currentApplication.Commitments = req.Commitments;

                    var dt = DateTime.Now;
                    if (releaseLetterFile != null)
                    {
                        db.Files.AddObject(releaseLetterFile);
                        await db.SaveChangesAsync();

                        currentApplication.ReleaseLetterFileId = releaseLetterFile.FileId;
                        currentApplication.ReleaseLetterAdminId = req.AdminId;
                        currentApplication.ReleaseLetterLastUpdate = dt;
                    }

                    if (ccrisReportFile != null)
                    {
                        db.Files.AddObject(ccrisReportFile);
                        await db.SaveChangesAsync();

                        currentApplication.CCRISReportFileId = ccrisReportFile.FileId;
                        currentApplication.CCRISReportAdminId = req.AdminId;
                        currentApplication.CCRISReportLastUpdate = dt;
                    }

                    if (hrmisFile != null)
                    {
                        db.Files.AddObject(hrmisFile);
                        await db.SaveChangesAsync();

                        currentApplication.HRMISFileId = hrmisFile.FileId;
                        currentApplication.HRMISAdminId = req.AdminId;
                        currentApplication.HRMISLastUpdate = dt;
                    }

                    if (anmFile != null)
                    {
                        db.Files.AddObject(anmFile);
                        await db.SaveChangesAsync();

                        currentApplication.ANMFileId = anmFile.FileId;
                        currentApplication.ANMAdminId = req.AdminId;
                        currentApplication.ANMLastUpdate = dt;
                    }

                    if (lpsaFile != null)
                    {
                        db.Files.AddObject(lpsaFile);
                        await db.SaveChangesAsync();

                        currentApplication.LPSAFileId = lpsaFile.FileId;
                        currentApplication.LPSAAdminId = req.AdminId;
                        currentApplication.LPSALastUpdate = dt;
                    }

                    if (angkasaFile != null)
                    {
                        db.Files.AddObject(angkasaFile);
                        await db.SaveChangesAsync();

                        currentApplication.AngkasaFileId = angkasaFile.FileId;
                        currentApplication.AngkasaAdminId = req.AdminId;
                        currentApplication.AngkasaLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.CCRIS,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.CCRIS);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveQueueAsync(RequestApplicationQueueForLoanData req, File identityFile, File paySlipFile, File ecFile, File hrmisFile, File bankStatementFile, File lppsaFile, File licenseFile, File redemptionLetterFile, File creditCardFile, File ramciFile, File signaturFile, File biroAngkasaFile, File kew320File,
                    File staffCardFile, File postDatedChequeFile, File CompanyConfirmationFile, File epfFile, File eaformFile,File billUtilitiesFile, List<File> docs, List<File> payslips, List<File> bankStatements)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application8s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.IdentityCardFileId)))
                    {
                        currentApplication.IdentityCardFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.PayslipFileId)))
                    {
                        currentApplication.PayslipFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.ECFileId)))
                    {
                        currentApplication.ECFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.HRMISFileId)))
                    {
                        currentApplication.HRMISFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.BankStatementFileId)))
                    {
                        currentApplication.BankStatementFileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.LPPSAFileId)))
                    {
                        currentApplication.LPPSAFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.LicenseFileId)))
                    {
                        currentApplication.LicenseFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.RedemptionLetterFileId)))
                    {
                        currentApplication.RedemptionLetterFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.CCStatementFileId)))
                    {
                        currentApplication.CCStatementFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.RAMCIFileId)))
                    {
                        currentApplication.RAMCIFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.SignatureFileId)))
                    {
                        currentApplication.SignatureFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.BIROFileId)))
                    {
                        currentApplication.BIROFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.KEW320FileId)))
                    {
                        currentApplication.KEW320FileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.StaffCardFileId)))
                    {
                        currentApplication.StaffCardFileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.PostDatedChequeFileId)))
                    {
                        currentApplication.PostDatedChequeFileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.CompanyConfirmationFileId)))
                    {
                        currentApplication.CompanyConfirmationFileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.EPFFileId)))
                    {
                        currentApplication.EPFFileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.EAFormFileId)))
                    {
                        currentApplication.EAFormFileId = null;
                    }
                    if (req.FileDelete.Any(x => x == nameof(currentApplication.BillUtilitiesFileId)))
                    {
                        currentApplication.BillUtilitiesFileId = null;
                    }

                    currentApplication.WorkerTypeId = req.WorkerTypeId;

                    var dt = DateTime.Now;
                    if (identityFile != null)
                    {
                        db.Files.AddObject(identityFile);
                        await db.SaveChangesAsync();

                        currentApplication.IdentityCardFileId = identityFile.FileId;
                        currentApplication.IdentityCardAdminId = req.AdminId;
                        currentApplication.IdentityCardLastUpdate = dt;
                    }

                    if (paySlipFile != null)
                    {
                        db.Files.AddObject(paySlipFile);
                        await db.SaveChangesAsync();

                        currentApplication.PayslipFileId = paySlipFile.FileId;
                        currentApplication.PayslipAdminId = req.AdminId;
                        currentApplication.PayslipLastUpdate = dt;
                    }

                    if (ecFile != null)
                    {
                        db.Files.AddObject(ecFile);
                        await db.SaveChangesAsync();

                        currentApplication.ECFileId = ecFile.FileId;
                        currentApplication.ECAdminId = req.AdminId;
                        currentApplication.ECLastUpdate = dt;
                    }

                    if (hrmisFile != null)
                    {
                        db.Files.AddObject(hrmisFile);
                        await db.SaveChangesAsync();

                        currentApplication.HRMISFileId = hrmisFile.FileId;
                        currentApplication.HRMISAdminId = req.AdminId;
                        currentApplication.HRMISLastUpdate = dt;
                    }

                    if (bankStatementFile != null)
                    {
                        db.Files.AddObject(bankStatementFile);
                        await db.SaveChangesAsync();

                        currentApplication.BankStatementFileId = bankStatementFile.FileId;
                        currentApplication.BankStatementAdminId = req.AdminId;
                        currentApplication.BankStatementLastUpdate = dt;
                    }

                    if (lppsaFile != null)
                    {
                        db.Files.AddObject(lppsaFile);
                        await db.SaveChangesAsync();

                        currentApplication.LPPSAFileId = lppsaFile.FileId;
                        currentApplication.LPPSAAdminId = req.AdminId;
                        currentApplication.LPPSALastUpdate = dt;
                    }

                    if (licenseFile != null)
                    {
                        db.Files.AddObject(licenseFile);
                        await db.SaveChangesAsync();

                        currentApplication.LicenseFileId = licenseFile.FileId;
                        currentApplication.LicenseAdminId = req.AdminId;
                        currentApplication.LicenseLastUpdate = dt;
                    }

                    if (redemptionLetterFile != null)
                    {
                        db.Files.AddObject(redemptionLetterFile);
                        await db.SaveChangesAsync();

                        currentApplication.RedemptionLetterFileId = redemptionLetterFile.FileId;
                        currentApplication.RedemptionLetterAdminId = req.AdminId;
                        currentApplication.RedemptionLetterLastUpdate = dt;
                    }

                    if (creditCardFile != null)
                    {
                        db.Files.AddObject(creditCardFile);
                        await db.SaveChangesAsync();

                        currentApplication.CCStatementFileId = creditCardFile.FileId;
                        currentApplication.CCStatementAdminId = req.AdminId;
                        currentApplication.CCStatementLastUpdate = dt;
                    }

                    if (ramciFile != null)
                    {
                        db.Files.AddObject(ramciFile);
                        await db.SaveChangesAsync();

                        currentApplication.RAMCIFileId = ramciFile.FileId;
                        currentApplication.RAMCIAdminId = req.AdminId;
                        currentApplication.RAMCILastUpdate = dt;
                    }

                    if (signaturFile != null)
                    {
                        db.Files.AddObject(signaturFile);
                        await db.SaveChangesAsync();

                        currentApplication.SignatureFileId = signaturFile.FileId;
                        currentApplication.SignatureAdminId = req.AdminId;
                        currentApplication.SignatureLastUpdate = dt;
                    }

                    if (biroAngkasaFile != null)
                    {
                        db.Files.AddObject(biroAngkasaFile);
                        await db.SaveChangesAsync();

                        currentApplication.BIROFileId = biroAngkasaFile.FileId;
                        currentApplication.BIROAdminId = req.AdminId;
                        currentApplication.BIROLastUpdate = dt;
                    }

                    if (kew320File != null)
                    {
                        db.Files.AddObject(kew320File);
                        await db.SaveChangesAsync();

                        currentApplication.KEW320FileId = kew320File.FileId;
                        currentApplication.KEW320AdminId = req.AdminId;
                        currentApplication.KEW320LastUpdate = dt;
                    }

                    if (staffCardFile != null)
                    {
                        db.Files.AddObject(staffCardFile);
                        await db.SaveChangesAsync();

                        currentApplication.StaffCardFileId = staffCardFile.FileId;
                        currentApplication.StaffCardAdminId = req.AdminId;
                        currentApplication.StaffCardLastUpdate = dt;
                    }

                    if (postDatedChequeFile != null)
                    {
                        db.Files.AddObject(postDatedChequeFile);
                        await db.SaveChangesAsync();

                        currentApplication.PostDatedChequeFileId = postDatedChequeFile.FileId;
                        currentApplication.PostDatedChequeAdminId = req.AdminId;
                        currentApplication.PostDatedChequeLastUpdate = dt;
                    }

                    if (CompanyConfirmationFile != null)
                    {
                        db.Files.AddObject(CompanyConfirmationFile);
                        await db.SaveChangesAsync();

                        currentApplication.CompanyConfirmationFileId = CompanyConfirmationFile.FileId;
                        currentApplication.CompanyConfirmationAdminId = req.AdminId;
                        currentApplication.CompanyConfirmationLastUpdate = dt;
                    }

                    if (epfFile != null)
                    {
                        db.Files.AddObject(epfFile);
                        await db.SaveChangesAsync();

                        currentApplication.EPFFileId = epfFile.FileId;
                        currentApplication.EPFAdminId = req.AdminId;
                        currentApplication.EPFLastUpdate = dt;
                    }

                    if (eaformFile != null)
                    {
                        db.Files.AddObject(eaformFile);
                        await db.SaveChangesAsync();

                        currentApplication.EAFormFileId = eaformFile.FileId;
                        currentApplication.EAFormAdminId = req.AdminId;
                        currentApplication.EAFormLastUpdate = dt;
                    }

                    if (billUtilitiesFile != null)
                    {
                        db.Files.AddObject(billUtilitiesFile);
                        await db.SaveChangesAsync();

                        currentApplication.BillUtilitiesFileId = billUtilitiesFile.FileId;
                        currentApplication.BillUtilitiesAdminId = req.AdminId;
                        currentApplication.BillUtilitiesLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.QUEUEFORLOAN,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    for (int i = 0; i < payslips.Count; i++)
                    {
                        var doc = payslips[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationFiles.AddObject(new ApplicationFile
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            FileId = doc.FileId,
                            CreateDate = dt,
                            CreateAdminId = req.AdminId,
                            GROUP = "payslip"
                        });
                    }

                    for (int i = 0; i < bankStatements.Count; i++)
                    {
                        var doc = bankStatements[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationFiles.AddObject(new ApplicationFile
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            FileId = doc.FileId,
                            CreateDate = dt,
                            CreateAdminId = req.AdminId,
                            GROUP = "bank_statement"
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    foreach (var d in req.AppFileDelete)
                    {
                        var doDelete = db.ApplicationFiles.FirstOrDefault(x => x.ApplicationFileId == d);
                        db.ApplicationFiles.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.QUEUEFORLOAN);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveReloanAsync(RequestApplicationReloanData req, File offerLetterFile, List<File> docs)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application9s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.OfferLetterFileId)))
                    {
                        currentApplication.OfferLetterFileId = null;
                    }

                    currentApplication.ReloadStatusId = req.ReloanStatusId;
                    currentApplication.ApprovedDate = req.ApprovedDate;
                    currentApplication.SigningDate = req.SigningDate;
                    currentApplication.ApprovedAmount = req.ApprovedAmount;

                    var dt = DateTime.Now;
                    if (offerLetterFile != null)
                    {
                        db.Files.AddObject(offerLetterFile);
                        await db.SaveChangesAsync();

                        currentApplication.OfferLetterFileId = offerLetterFile.FileId;
                        currentApplication.OfferLetterAdminId = req.AdminId;
                        currentApplication.OfferLetterLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.RELOAN,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.RELOAN);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveCollectionAsync(RequestApplicationCollectionData req, File declarationFile, File settlementFile, File serviceFeeFile, File rezekiFeeFile, File rezekiAgreementFile ,List<File> docs)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Application10s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    var mainApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.DeclarationFormFileId)))
                    {
                        currentApplication.DeclarationFormFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.SettlementReceiptFileId)))
                    {
                        currentApplication.SettlementReceiptFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.ServiceFeeReceiptFileId)))
                    {
                        currentApplication.ServiceFeeReceiptFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.RezekiReceiptFileId)))
                    {
                        currentApplication.RezekiReceiptFileId = null;
                    }

                    if (req.FileDelete.Any(x => x == nameof(currentApplication.RezekiAgreementFileId)))
                    {
                        currentApplication.RezekiAgreementFileId = null;
                    }

                    currentApplication.ServiceFee = req.ServiceFee;
                    currentApplication.DepositAmount = req.DepositAmount;
                    currentApplication.DepositDate = req.DepositDate;

                    currentApplication.SettlementDate = req.SettlementDate;
                    currentApplication.SettlementAmount = req.SettlementAmount;
                    currentApplication.SettlementCPct = req.SettlementCPct.HasValue ? req.SettlementCPct.Value / 100 : (decimal?)null;
                    currentApplication.CollectionAmountPct = req.CollectionAmountPct.HasValue ? req.CollectionAmountPct.Value / 100 : (decimal?)null; 
                    currentApplication.SettlementDuration = req.SettlementDuration;
                    currentApplication.TotalReloan = req.TotalReloan;
                    currentApplication.TotalLoanRepayment = req.TotalLoanRepayment;
                    currentApplication.DBBBankAccount = req.DBBBankAccount;
                    currentApplication.DBBTenure = req.DBBTenure;
                    currentApplication.DBBAgreementDate = req.DBBAgreementDate;
                    currentApplication.MonthlyFund = req.MonthlyFund;
                    currentApplication.DBBAmount = req.DBBAmount;
                    currentApplication.ReceiptNo = req.ReceiptNo;
                    currentApplication.TaxNumber = req.TaxNumber;
                    currentApplication.StatusId = req.StatusId;
                    currentApplication.InstallmentDate = req.InstallmentDate;

                    var dt = DateTime.Now;
                    if (declarationFile != null)
                    {
                        db.Files.AddObject(declarationFile);
                        await db.SaveChangesAsync();

                        currentApplication.DeclarationFormFileId = declarationFile.FileId;
                        currentApplication.DeclarationFormAdminId = req.AdminId;
                        currentApplication.DeclarationFormLastUpdate = dt;
                    }

                    if (settlementFile != null)
                    {
                        db.Files.AddObject(settlementFile);
                        await db.SaveChangesAsync();

                        currentApplication.SettlementReceiptFileId = settlementFile.FileId;
                        currentApplication.SettlementReceiptAdminId = req.AdminId;
                        currentApplication.SettlementReceiptLastUpdate = dt;
                    }

                    if (serviceFeeFile != null)
                    {
                        db.Files.AddObject(serviceFeeFile);
                        await db.SaveChangesAsync();

                        currentApplication.ServiceFeeReceiptFileId = serviceFeeFile.FileId;
                        currentApplication.ServiceFeeReceiptAdminId = req.AdminId;
                        currentApplication.ServiceFeeReceiptLastUpdate = dt;
                    }

                    if (rezekiFeeFile != null)
                    {
                        db.Files.AddObject(rezekiFeeFile);
                        await db.SaveChangesAsync();

                        currentApplication.RezekiReceiptFileId = rezekiFeeFile.FileId;
                        currentApplication.RezekiReceiptAdminId = req.AdminId;
                        currentApplication.RezekiReceiptLastUpdate = dt;
                    }

                    if (rezekiAgreementFile != null)
                    {
                        db.Files.AddObject(rezekiAgreementFile);
                        await db.SaveChangesAsync();

                        currentApplication.RezekiAgreementFileId = rezekiAgreementFile.FileId;
                        currentApplication.RezekiAgreementAdminId = req.AdminId;
                        currentApplication.RezekiAgreementLastUpdate = dt;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        var doc = docs[i];
                        db.Files.AddObject(doc);
                        await db.SaveChangesAsync();

                        db.ApplicationDocuments.AddObject(new ApplicationDocument
                        {
                            ApplicationId = currentApplication.ApplicationId,
                            ApplicationStatusId = (int)ApplicationStatus.COLLECTION,
                            FileId = doc.FileId,
                            Remark = req.RemarkAdditionalDocuments[i],
                            CreateDate = dt,
                            CreateAdminId = req.AdminId
                        });
                    }

                    foreach (var item in req.AdditionalDocumentToModify)
                    {
                        var currentDoc = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == item.ApplicationDocumentId);
                        if (currentDoc != null)
                        {
                            currentDoc.Remark = item.Remark;
                        }
                    }

                    foreach (var d in req.DocumentDelete)
                    {
                        var doDelete = db.ApplicationDocuments.FirstOrDefault(x => x.ApplicationDocumentId == d);
                        db.ApplicationDocuments.DeleteObject(doDelete);
                    }

                    await SaveApplicationRemark(db, req.ApplicationRemark, mainApplication.ApplicationId, req.AdminId, dt, (int)ApplicationStatus.COLLECTION);
                    await db.SaveChangesAsync();
                    await CreateApplicationHistory(db, mainApplication.ApplicationId, req.AdminId, null);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<int>> SaveAssignAsync(RequestApplicationAssignByAdmin req)
        {
            using(var tscope = new TransactionScope())
            {
                using (CsaEntities db = new CsaEntities())
                {
                    var currentApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    List<int> adminReceiveEmailNotification = new List<int>();

                    if (currentApplication.AMAdminId != req.AmAdminId.ToInt().IfZeroToNull() && req.AmAdminId.ToInt().IfZeroToNull().HasValue) adminReceiveEmailNotification.Add(req.AmAdminId.Value);
                    if (currentApplication.RMAdminId != req.RmAdminId.ToInt().IfZeroToNull() && req.RmAdminId.ToInt().IfZeroToNull().HasValue) adminReceiveEmailNotification.Add(req.RmAdminId.Value);
                    if (currentApplication.UMAdminId != req.UmAdminId.ToInt().IfZeroToNull() && req.UmAdminId.ToInt().IfZeroToNull().HasValue) adminReceiveEmailNotification.Add(req.UmAdminId.Value);
                    if (currentApplication.PAAdminId != req.PaAdminId.ToInt().IfZeroToNull() && req.PaAdminId.ToInt().IfZeroToNull().HasValue) adminReceiveEmailNotification.Add(req.PaAdminId.Value);
                    if (currentApplication.PreparedAdminId != req.PreparedAdminId.ToInt().IfZeroToNull() && req.PreparedAdminId.ToInt().IfZeroToNull().HasValue) adminReceiveEmailNotification.Add(req.PreparedAdminId.Value);
                    if (currentApplication.AnalyzedAdminId != req.AnalyzedAdminId.ToInt().IfZeroToNull() && req.AnalyzedAdminId.ToInt().IfZeroToNull().HasValue) adminReceiveEmailNotification.Add(req.AnalyzedAdminId.Value);

                    currentApplication.PFCAdminId = req.PfcAdminId.ToInt().IfZeroToNull();
                    currentApplication.ReferrerMemberId = req.MemberId.ToInt().IfZeroToNull();
                    currentApplication.AMAdminId = req.AmAdminId.ToInt().IfZeroToNull();
                    currentApplication.RMAdminId = req.RmAdminId.ToInt().IfZeroToNull();
                    currentApplication.UMAdminId = req.UmAdminId.ToInt().IfZeroToNull();
                    currentApplication.PAAdminId = req.PaAdminId.ToInt().IfZeroToNull();
                    currentApplication.PreparedAdminId = req.PreparedAdminId.ToInt().IfZeroToNull();
                    currentApplication.AnalyzedAdminId = req.AnalyzedAdminId.ToInt().IfZeroToNull();

                    string pfcName = "";
                    if(currentApplication.PFCAdminId.HasValue)
                    {                        
                        pfcName = db.Admins.FirstOrDefault(x => x.AdminId == currentApplication.PFCAdminId.Value)?.Name;
                    }

                    await db.SaveChangesAsync();                    

                    foreach (var item in adminReceiveEmailNotification)
                    {
                        var admin = db.Admins.FirstOrDefault(x => x.AdminId == item);
                        EmailBiz.NewClientAssigned(db, new EmailNewAssignedData(admin.Email, currentApplication.Member_MemberId.FullName, currentApplication.Member_MemberId.CreateDate, currentApplication.Member_MemberId.FileNumber, pfcName,admin.Name));
                    }

                    tscope.Complete();
                    return RespArgs<int>.CreateSuccess(currentApplication.ApplicationStatusId);
                }
            }
        }

        public static async Task<RespArgs<bool>> RejectAsync(RequestApplicationReject req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using(var tscope = new TransactionScope())
                {
                    var currentApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    currentApplication.RejectedDate = DateTime.Now;
                    currentApplication.RejectedApplicationStatusId = currentApplication.ApplicationStatusId;
                    currentApplication.RejectedAdminId = req.AdminId;
                    currentApplication.RejectedReason = req.RejectReason;

                    await db.SaveChangesAsync();
                    await MemberBiz.RecalculateFileNumber(db, currentApplication.MemberId);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> CancelRejectAsync(RequestApplicationCancelReject req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using(var tscope = new TransactionScope())
                {
                    var currentApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    currentApplication.RejectedDate = null;
                    currentApplication.RejectedApplicationStatusId = null;

                    await db.SaveChangesAsync();
                    await MemberBiz.RecalculateFileNumber(db, currentApplication.MemberId);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> SaveApplicationInfo(RequestApplicationInfo req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using (var tscope = new TransactionScope())
                {
                    var currentApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
                    if (currentApplication == null) throw new ArgumentException("application_not_found");

                    currentApplication.SourceId = req.SourceId;
                    currentApplication.CreditStatusId = req.CreditStatusId;
                    currentApplication.ScoreClass = req.ScoreClass;
                    currentApplication.CustomerStatusId = req.CustomerStatusId;
                    if (currentApplication.CustomerStatusId != (int)CustomerStatus.Burst)
                    {
                        currentApplication.BurstReasonId = null;
                    }
                    else
                    {
                        currentApplication.BurstReasonId = req.BurstReasonId;
                    }
                    currentApplication.CreditRemark = req.CreditRemark;
                    if(currentApplication.CustomerStatusId == (int)CustomerStatus.Drop_Case)
                    {
                        currentApplication.CreditStatusId = (int)CreditStatus.DECLINED_BY_AM;
                    }

                    await db.SaveChangesAsync();
                    var res = await GoToApplicationStatus(db, new RequestApplicaationGotoStatus(currentApplication.ApplicationId, req.AdminId, req.ApplicationStatusId, changeManually: true));
                    await MemberBiz.RecalculateFileNumber(db, currentApplication.MemberId);
                    tscope.Complete();
                    return res;
                }
            }
        }

        public static async Task<RespArgs<bool>> ConvertHero(RequestApplicationHero req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                await DoConvertHero(req, db);
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        private static async Task DoConvertHero(RequestApplicationHero req, CsaEntities db)
        {
            var currentApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
            if (currentApplication == null) throw new ArgumentException("application_not_found");

            var validator = new ApplicationConvertHeroCustomValidator(currentApplication.ReferrerMemberId, currentApplication.AMAdminId, currentApplication.PFCAdminId, currentApplication.RMAdminId, currentApplication.UMAdminId, currentApplication.PAAdminId);
            var resValidator = validator.Validate();
            if (resValidator.Error) throw new ArgumentException(resValidator.Message);

            var currentMember = db.Members.FirstOrDefault(x => x.MemberId == currentApplication.MemberId);
            currentMember.MemberTypeId = (int)MemberType.HERO;
            await db.SaveChangesAsync();
            await MemberBiz.RecalculateFileNumber(db, currentMember.MemberId);
        }

        public static async Task<RespArgs<bool>> ConvertHero(CsaEntities db, RequestApplicationHero req)
        {
            await DoConvertHero(req, db);
            return RespArgs<bool>.CreateSuccess(true);
        }

        public static async Task<RespArgs<bool>> ApplicationCreateByAdmin(RequestApplicationCreate req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using(var tscope = new TransactionScope())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    int? sourceId = null;
                    if (member.ProgramEventId == (int)MemberProgramEvent.YABAM) sourceId = (int)ApplicationSourceType.YABAM;

                    var app = new Application { MemberId = req.MemberId, CreateDate = DateTime.Now, CustomerStatusId = 1, ApplicationStatusId = 0,SourceId = sourceId };
                    db.Applications.AddObject(app);
                    await db.SaveChangesAsync();
                    db.Application1s.AddObject(new Application1 { ApplicationId = app.ApplicationId });
                    db.Application2s.AddObject(new Application2 { ApplicationID = app.ApplicationId });
                    db.Application3s.AddObject(new Application3 { ApplicationId = app.ApplicationId });
                    db.Application4s.AddObject(new Application4 { ApplicationId = app.ApplicationId });
                    db.Application5s.AddObject(new Application5 { ApplicationId = app.ApplicationId });
                    db.Application6s.AddObject(new Application6 { ApplicationID = app.ApplicationId });
                    db.Application7s.AddObject(new Application7 { ApplicationId = app.ApplicationId });
                    db.Application8s.AddObject(new Application8 { ApplicationId = app.ApplicationId });
                    db.Application9s.AddObject(new Application9 { ApplicationId = app.ApplicationId });
                    db.Application10s.AddObject(new Application10 { ApplicationId = app.ApplicationId });
                    await db.SaveChangesAsync();
                    await MemberBiz.RecalculateFileNumber(db, member.MemberId);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> ApplicationCreateByMember(RequestApplicationCreate req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                using(var tscope = new TransactionScope())
                {
                    var member = db.Members.FirstOrDefault(x => x.MemberId == req.MemberId);
                    if (member == null) throw new ArgumentException("Member not found");

                    int? sourceId = null;
                    if (member.ProgramEventId == (int)MemberProgramEvent.YABAM) sourceId = (int)ApplicationSourceType.YABAM;

                    var app = new Application { MemberId = member.MemberId, CreateDate = DateTime.Now, CustomerStatusId = 1, ApplicationStatusId = 0,SourceId = sourceId };
                    db.Applications.AddObject(app);
                    await db.SaveChangesAsync();
                    db.Application1s.AddObject(new Application1 { ApplicationId = app.ApplicationId });
                    db.Application2s.AddObject(new Application2 { ApplicationID = app.ApplicationId });
                    db.Application3s.AddObject(new Application3 { ApplicationId = app.ApplicationId });
                    db.Application4s.AddObject(new Application4 { ApplicationId = app.ApplicationId });
                    db.Application5s.AddObject(new Application5 { ApplicationId = app.ApplicationId });
                    db.Application6s.AddObject(new Application6 { ApplicationID = app.ApplicationId });
                    db.Application7s.AddObject(new Application7 { ApplicationId = app.ApplicationId });
                    db.Application8s.AddObject(new Application8 { ApplicationId = app.ApplicationId });
                    db.Application9s.AddObject(new Application9 { ApplicationId = app.ApplicationId });
                    db.Application10s.AddObject(new Application10 { ApplicationId = app.ApplicationId });
                    await db.SaveChangesAsync();
                    await MemberBiz.RecalculateFileNumber(db, member.MemberId);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static RespArgs<List<string>> CanNextApplicationStatus(long applicationId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var findApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == applicationId);
                if (findApplication == null) throw new ArgumentException("Application not found");

                if (findApplication.CustomerStatusId == (int)CustomerStatus.Burst) throw new Exception("Burst Mode dont allow to move stage");

                List<string> errorMessages = new List<string>();

                switch (findApplication.ApplicationStatusId)
                {
                    case (int)ApplicationStatus.PRE_CHECKING:
                        var app1 = db.Application1s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app1.RAMCIReportFileId.IsEmpty()) errorMessages.Add("RAMCI Report file must exist");
                        if (app1.CCRISDocumentFileId.IsEmpty()) errorMessages.Add("CCRIS Document must exist");
                        if (app1.EligibilityId != 1) errorMessages.Add("Eligiblity must be set to Yes");
                        if (app1.LegalSuitsCheck == 1 || app1.BankruptcyCheck == 1 || app1.SpecialAttentionCheck == 1 || app1.BadPaymentRecordCheck == 1) errorMessages.Add("None of these must be checked: Legal Suits, Bankruptcy, Special Attention, & Bad Payment Record");
                        break;
                    case (int)ApplicationStatus.PREPARATION:
                        var app2 = db.Application2s.FirstOrDefault(x => x.ApplicationID == applicationId);
                        if (app2.ProposalFileId.IsEmpty()) errorMessages.Add("Application Proposal file must exist");
                        break;
                    case (int)ApplicationStatus.PRESENTATION:
                        var app3 = db.Application3s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app3.ProposalStatusId != (int)ProposalStatus.PROPOSAL_ACCEPTED) errorMessages.Add("Proposal status must be Accepted");
                        break;
                    case (int)ApplicationStatus.PRESIGN:
                        var app4 = db.Application4s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app4.ProposalSendId != 1 || app4.SuratAkuanId != 1 || app4.ComprehensiveFormId != 1) errorMessages.Add("Passed all requirements to proceed to the next status");
                        break;
                    case (int)ApplicationStatus.PENDINGZOOMACCEPTANCE:
                        var app5 = db.Application5s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app5.PayslipFileId.IsEmpty()) errorMessages.Add("Payslip file must exist");
                        if (app5.RAMCIFileId.IsEmpty()) errorMessages.Add("RAMCI file must exist");
                        if (app5.CTOSFileId.IsEmpty()) errorMessages.Add("CTOS file must exist");
                        if (app5.RedemptionLetterFileId.IsEmpty()) errorMessages.Add("Redemption Letter file must exist");
                        break;
                    case (int)ApplicationStatus.SETTLEMENT:
                        if (!db.Settlements.Any(x => x.ApplicationID == applicationId)) errorMessages.Add("Must have at least 1 Settlement");
                        if (!db.Caseupdates.Any(x => x.ApplicationId == applicationId)) errorMessages.Add("Must have at least 1 Case Update");
                        break;
                    case (int)ApplicationStatus.CCRIS:
                        var app7 = db.Application7s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app7.ReleaseLetterFileId.IsEmpty()) errorMessages.Add("Release Letter file must be uploaded");
                        if (app7.CCRISReportFileId.IsEmpty()) errorMessages.Add("CCRIS Report file must be uploaded");
                        break;
                    case (int)ApplicationStatus.QUEUEFORLOAN:
                        var app8 = db.Application8s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app8.IdentityCardFileId.IsEmpty()) errorMessages.Add("IC file must be uploaded");
                        if (app8.PayslipFileId.IsEmpty()) errorMessages.Add("Payslip file must be uploaded");
                        if (app8.BankStatementFileId.IsEmpty()) errorMessages.Add("Bank Statement file must be uploaded");
                        if (app8.SignatureFileId.IsEmpty()) errorMessages.Add("Signature file must be uploaded");
                        break;
                    case (int)ApplicationStatus.RELOAN:
                        var app9 = db.Application9s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app9.OfferLetterFileId.IsEmpty()) errorMessages.Add("Offer Letter must be uploaded");
                        if (app9.ReloadStatusId != 2) errorMessages.Add("Reloan status must be Approved");
                        break;
                    case (int)ApplicationStatus.COLLECTION:
                        var app10 = db.Application10s.FirstOrDefault(x => x.ApplicationId == applicationId);
                        if (app10.DeclarationFormFileId.IsEmpty()) errorMessages.Add("Declaration Form file must be uploaded");
                        if (app10.SettlementReceiptFileId.IsEmpty()) errorMessages.Add("Settlement Receipt file must be uploaded");
                        if (app10.ServiceFeeReceiptFileId.IsEmpty()) errorMessages.Add("Service Fee Receipt file must be uploaded");
                        if (app10.RezekiReceiptFileId.IsEmpty()) errorMessages.Add("Rezeki Receipt file must be uploaded");
                        if (app10.RezekiAgreementFileId.IsEmpty()) errorMessages.Add("Rezeki Agreement file must be uploaded");
                        break;
                    case (int)ApplicationStatus.WIRA:
                        break;
                    case (int)ApplicationStatus.REJECTION:
                        break;
                    default:
                        break;
                }

                if(errorMessages.Count > 0)
                {
                    return RespArgs<List<string>>.CreateError(string.Join("", errorMessages.Select(x => $"<p>{x}</p>")));
                }

                return RespArgs<List<string>>.CreateSuccess(errorMessages);
            }
        }

        public static async Task<RespArgs<bool>> GoToApplicationStatus(RequestApplicaationGotoStatus req)
        {
            using(CsaEntities db = new CsaEntities())
            {
                using(var tscope = new TransactionScope())
                {
                    await DoGoToApplicationStatus(db, req);
                    tscope.Complete();
                    return RespArgs<bool>.CreateSuccess(true);
                }
            }
        }

        public static async Task<RespArgs<bool>> GoToApplicationStatus(CsaEntities db,RequestApplicaationGotoStatus req)
        {
            await DoGoToApplicationStatus(db, req);
            return RespArgs<bool>.CreateSuccess(true);
        }

        private static async Task DoGoToApplicationStatus(CsaEntities db, RequestApplicaationGotoStatus req)
        {
            var findApplication = db.Applications.FirstOrDefault(x => x.ApplicationId == req.ApplicationId);
            if (findApplication == null) throw new ArgumentException("application_not_found");            

            if (req.NewStatus.HasValue && req.ChangeManually)
            {
                //if (findApplication.ApplicationStatusId < req.NewStatus) throw new ArgumentException("cant_change_status");                   
            }
            else
            {
                req.NewStatus = findApplication.ApplicationStatusId + 1;
            }

            bool convertToHeroAutomatically = findApplication.ApplicationStatusId <= (int)ApplicationStatus.SETTLEMENT && req.NewStatus > (int)ApplicationStatus.SETTLEMENT;
            if (convertToHeroAutomatically) await ConvertHero(db, new RequestApplicationHero(findApplication.ApplicationId, req.AdminId));

            bool isChangedApplication = findApplication.ApplicationStatusId != req.NewStatus.Value;
            findApplication.ApplicationStatusId = req.NewStatus.Value;

            if (req.ChangeManually && isChangedApplication)
            {
                findApplication.ApplicationStatusLastChangeAdminId = req.AdminId;
                findApplication.ApplicationStatusLastChangeDate = DateTime.Now;
            }
            else
            {
                findApplication.ApplicationStatusLastChangeAdminId = null;
                findApplication.ApplicationStatusLastChangeDate = null;
            }

            switch (req.NewStatus.Value)
            {
                case (int)ApplicationStatus.PRE_CHECKING:
                    if (db.Application1s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application1s.AddObject(new Application1 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.PREPARATION:
                    if (db.Application2s.FirstOrDefault(x => x.ApplicationID == req.ApplicationId) == null)
                    {
                        db.Application2s.AddObject(new Application2 { ApplicationID = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.PRESENTATION:
                    if (db.Application3s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application3s.AddObject(new Application3 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.PRESIGN:
                    if (db.Application4s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application4s.AddObject(new Application4 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.PENDINGZOOMACCEPTANCE:
                    if (db.Application5s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application5s.AddObject(new Application5 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.SETTLEMENT:
                    if (db.Application6s.FirstOrDefault(x => x.ApplicationID == req.ApplicationId) == null)
                    {
                        db.Application6s.AddObject(new Application6 { ApplicationID = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.CCRIS:
                    if (db.Application7s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application7s.AddObject(new Application7 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.QUEUEFORLOAN:
                    if (db.Application8s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application8s.AddObject(new Application8 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.RELOAN:
                    if (db.Application9s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application9s.AddObject(new Application9 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.COLLECTION:
                    if (db.Application10s.FirstOrDefault(x => x.ApplicationId == req.ApplicationId) == null)
                    {
                        db.Application10s.AddObject(new Application10 { ApplicationId = req.ApplicationId });
                    }
                    break;
                case (int)ApplicationStatus.WIRA:
                    break;
                case (int)ApplicationStatus.REJECTION:
                    break;
                default:
                    break;
            }
            await db.SaveChangesAsync();
            await MemberBiz.RecalculateFileNumber(db, findApplication.MemberId);
        }
    }
}
