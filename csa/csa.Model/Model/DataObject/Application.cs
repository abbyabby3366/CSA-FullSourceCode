using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace csa.Model.DataObject
{
    public class RequestNewApplicationByMember
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string CompanyName { get; set; }
        public int? PositionId { get; set; }
        public int? SalaryRangeId { get; set; }
        public int CreatorMemberId { get; set; }
    }

    public class ApplicationGVByMember
    {
        public long ApplicationId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string ContactNo { get; set; }
        public int? SalaryRangeId { get; set; }
        public int? StatusId { get; set; }
        public string SalaryRange
        {
            get
            {
                try
                {
                    return Library.EnumHelper.GetDescription((Library.SalaryRange)SalaryRangeId);
                }
                catch (Exception)
                {
                    return SalaryRangeId.ToString();
                }
            }
        }
        public string Status
        {
            get
            {
                try
                {
                    return Library.EnumHelper.GetDescription((Library.ApplicationStatus)StatusId);
                }
                catch (Exception)
                {
                    return StatusId.ToString();
                }
            }
        }
    }

    public class ApplicationGVByAdmin
    {
        public long ApplicationId { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        //public int? SalaryRangeId { get; set; }
        public DateTime? RejectedDate { get; set; }
        public int? ApplicationStatusId { get; set; }
        //public string SalaryRange
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Library.EnumHelper.GetDescription((Library.SalaryRange)SalaryRangeId);
        //        }
        //        catch (Exception)
        //        {
        //            return SalaryRangeId.ToString();
        //        }
        //    }
        //}
        public decimal? Salary { get; set; }
        public string Status
        {
            get
            {
                try
                {
                    return Library.EnumHelper.GetDescription((Library.ApplicationStatus)ApplicationStatusId);
                }
                catch (Exception)
                {
                    return ApplicationStatusId.ToString();
                }
            }
        }
        public string CreatorMemberName { get; set; }
        public DateTime CreateDate { get; set; }
        public long? ReferrerMemberId { get; set; }
        public int? AMAdminId { get; set; }
        public int? PFCAdminId { get; set; }
        public int? RMAdminId { get; set; }
        public int? UMAdminId { get; set; }
        public int? PAAdminId { get; set; }
        public bool CompletedAssign => ReferrerMemberId.HasValue && AMAdminId.HasValue && PFCAdminId.HasValue && RMAdminId.HasValue && UMAdminId.HasValue && PAAdminId.HasValue;
        public string FileNumber { get; set; }
        public int CompanyTypeId { get; set; }
        public int? CustomerStatusId { get; set; }
        public int? MemberTypeId { get; set; }
        public int? CaseCount { get; set; }
    }

    public class RequestApplicationPrecheckingByMember
    {
        public long ApplicationId { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
        public HttpPostedFileBase SuratAkuanFile { get; set; }
    }

    public class RequestApplicationProcessingByMember
    {
        public long ApplicationId { get; set; }
        public HttpPostedFileBase IcFile { get; set; }
        public HttpPostedFileBase ReleaseLetterFile { get; set; }
        public HttpPostedFileBase CreditDisbursementFile { get; set; }
        public HttpPostedFileBase PaySlipFile { get; set; }
        public IdPasswordAccount Anm { get; set; }
        public IdPasswordAccount Lppsa { get; set; }
        public IdPasswordAccount Hrmis { get; set; }
        public IdPasswordAccount Angkasa { get; set; }
    }

    public class ApplicationDetailsMember
    {
        public ApplicationDetailsMember(long applicationId, int statusId)
        {
            ApplicationId = applicationId;
            StatusId = statusId;
        }

        public long ApplicationId { get; set; }
        public int StatusId { get; set; }
    }

    public class IdPasswordAccount
    {
        public string ID { get; set; }
        public string Password { get; set; }
    }

    public class ApplicationDetailsInfoProcess : ApplicationDetailsInfo
    {
        public ApplicationDetailsInfoProcess(string customerName, string iCNumber, decimal? grossSalary, int? salaryRange, string employer, string state, int? retirementAge, string pFC, int? sourceId, string referralName, string referralFileNumber, string preparedBy, string verifiedBy, DateTime? keyInDate, string remark, int customerStatusId, int? burstReasonId, int applicationStatusId, DateTime? rejectedDate, string rejectedAdmin, string rejectedReason, OptionSelectedBy<int> applicationStatusLastChangeAdminId, int? creditStatusId, string scoreClass) : base(customerName, iCNumber, grossSalary, salaryRange, employer, state, retirementAge, pFC, sourceId, referralName, referralFileNumber, preparedBy, verifiedBy, keyInDate, remark)
        {
            CustomerStatusId = customerStatusId;
            BurstReasonId = burstReasonId;
            ApplicationStatusId = applicationStatusId;
            RejectedDate = rejectedDate;
            RejectedAdmin = rejectedAdmin;
            RejectedReason = rejectedReason;
            ApplicationStatusLastChangeAdminId = applicationStatusLastChangeAdminId;
            CreditStatusId = creditStatusId;
            ScoreClass = scoreClass;
        }

        public int CustomerStatusId { get; set; }
        public int? BurstReasonId { get; set; }
        public int ApplicationStatusId { get; set; }     
        public DateTime? RejectedDate { get; set; }
        public string RejectedAdmin { get; set; }
        public string RejectedReason { get; set; }
        public int? CreditStatusId { get; set; }
        public string ScoreClass { get; set; }
        public OptionSelectedBy<int> ApplicationStatusLastChangeAdminId { get; set; }
    }

    public class ApplicationDetailsInfo
    {
        public ApplicationDetailsInfo(string customerName, string iCNumber, decimal? grossSalary, int? salaryRange, string employer, string state, int? retirementAge, string pFC, int? sourceId, string referralName, string referralFileNumber, string preparedBy, string verifiedBy, DateTime? keyInDate, string remark)
        {
            CustomerName = customerName;
            ICNumber = iCNumber;
            GrossSalary = grossSalary;
            SalaryRange = salaryRange;
            Employer = employer;
            State = state;
            RetirementAge = retirementAge;
            PFC = pFC;
            SourceId = sourceId;
            ReferralName = referralName;
            ReferralFileNumber = referralFileNumber;
            PreparedBy = preparedBy;
            VerifiedBy = verifiedBy;
            KeyInDate = keyInDate;
            Remark = remark;
        }

        public string CustomerName { get; set; }
        public string ICNumber { get; set; }
        public decimal? GrossSalary { get; set; }
        public int? SalaryRange { get; set; }
        public string Employer { get; set; }
        public string State { get; set; }
        public int? RetirementAge  { get; set; }
        public string PFC { get; set; }
        public int? SourceId { get; set; }
        public string ReferralName { get; set; }
        public string ReferralFileNumber { get; set; }
        public string PreparedBy { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? KeyInDate { get; set; }
        public string Remark { get; set; }
    }

    public class BaseApplicationRequestDataByAdmin
    {
        public string ApplicationRemark { get; set; }
    }

    public class RequestApplicationPreCheckingByAdmin
    {
        public HttpPostedFileBase RamciFile { get; set; }
        public HttpPostedFileBase CcrisFile { get; set; }
        public HttpPostedFileBase PayslipFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }        
        public string Json { get; set; }        
    }

    public class RequestNextApplicationStatus
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
    }

    public class RequestGoToApplicationStatus
    {
        public int AdminId { get; set; }
        public long ApplicationId { get; set; }
        public int StatusId { get; set; }
    }

    public class RequestApplicationPreCheckingData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int? Eligibility { get; set; }
        public bool LegalSuit { get; set; }
        public bool Bankruptcy { get; set; }
        public bool SpecialAttentionAccount { get; set; }
        public bool BadPaymentRecordCheck { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
    }

    public class RequestAdditionalDocumentEditByAdmin
    {
        public long ApplicationDocumentId { get; set; }
        public string Remark { get; set; }
    }

    public class FileToDelete<T>
    {
        public string FieldId { get; set; }
        public T Value { get; set; }
    }

    public class RequestApplicationPreparationByAdmin
    {
        public HttpPostedFileBase ProposalFile { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationPreparationData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public decimal? SalaryGross { get; set; }
        public decimal? SalaryDeduction { get; set; }
        public decimal? NetIncome { get; set; }
        public decimal? B1 { get; set; }
        public decimal? B2 { get; set; }
        public decimal? B3 { get; set; }
        public decimal? B4 { get; set; }
        public decimal? BAverage { get; set; }
        public decimal? CommitmentOutstanding { get; set; }
        public decimal? CommitmentInstallment { get; set; }
        public decimal? OtherNetBalance { get; set; }
        public decimal? OtherBPA { get; set; }
        public decimal? OtherComparisonDSR { get; set; }
        public decimal? OtherComparisonDSRPctCommitment { get; set; }
        public decimal? OtherPctRefresh { get; set; }
        public decimal? OtherProposedRefresh { get; set; }
        public decimal? OtherCompositionDSR { get; set; }
        public decimal? OtherCompositionDSRPctCommitment { get; set; }
        public decimal? RefreshTotal { get; set; }
        public decimal? RefreshRemainCommitment { get; set; }
        public decimal? ReloanTotal { get; set; }
        public decimal? ReloanMonthly { get; set; }
        public decimal? ReloanBersih { get; set; }
        public decimal? ReloanBelanja { get; set; }
        public decimal? ReloanDeposit { get; set; }
        public decimal? ReloanDanaBantuan { get; set; }
        public decimal? ReloanServiceFee { get; set; }
        public decimal? ReloanServiceFeePct { get; set; }
        public decimal? ReloanIncomeAfterRNR { get; set; }
        public decimal? ReloanDifference { get; set; }
        public int? ModelBackgroundScreeningId { get; set; }
        public int? ModelCompositionDSRId { get; set; }
        public int? ModelCommitmentId { get; set; }
        public int? ModelSettlementId { get; set; }
        public int? ModelServiceFeeId { get; set; }
        public int? ModelNetIncomeAfterRNRId { get; set; }
        public int? ModelStatusId { get; set; }
        public int? ModelStatusProposalId { get; set; }
        public int? ModelCheckId { get; set; }
        public int? ReviewAdminId { get; set; }
        public int? ReviewStatusId { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewComment { get; set; }
        public int? ApproveAdminId { get; set; }
        public int? ApproveStatusId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproveComment { get; set; }
        public List<string> FileDelete { get; set; }
        public int? VerifiedAdminId { get; set; }
        public int? VerifiedStatusId { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string VerifiedComment { get; set; }
        public string CreditRemark { get; set; }
    }

    public class RequestApplicationProposalPresentationByAdmin
    {
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationProposalPresentationData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int? ProposalStatusId { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
    }

    public class RequestApplicationPreSigningData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int? ProposalSendId { get; set; }
        public int? SuratAkuanId { get; set; }
        public int? ComprehensiveFormId { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
    }

    public class RequestApplicationPreSigningByAdmin
    {
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationPendingAcceptanceByAdmin
    {
        public HttpPostedFileBase PaySlipFile { get; set; }
        public HttpPostedFileBase RamciFile { get; set; }
        public HttpPostedFileBase CtosFile { get; set; }
        public HttpPostedFileBase RedemptionLetterFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationPendingAcceptanceData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public string ApplicantAddress { get; set; }
        public int? BankruptcyStatus { get; set; }
        public int? LegalCase { get; set; }
        public int? HealthCreditScore { get; set; }
        public decimal? Commitments { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
    }



    public class RequestApplicationSettlementByAdmin
    {
        public HttpPostedFileBase SettlementFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationSettlementData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public List<ApplicationSettlementDetails> SettlementDetails { get; set; }
        public List<ApplicationCaseUpdate> CaseUpdates { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
        public List<long> SettlementDelete { get; set; }
        public List<long> CaseUpdateDelete { get; set; }
    }

    public class RequestApplicationCcrisByAdmin
    {
        public HttpPostedFileBase ReleaseLetterFile { get; set; }
        public HttpPostedFileBase CcrisReportFile { get; set; }
        public HttpPostedFileBase HrmisFile { get; set; }
        public HttpPostedFileBase AnmFile { get; set; }
        public HttpPostedFileBase LpsaFile { get; set; }
        public HttpPostedFileBase AngkasaFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationCcrisData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
        public int? BankruptcyStatus { get; set; }
        public int? LegalCase { get; set; }
        public int? HealthCreditScore { get; set; }
        public string Commitments { get; set; }
    }

    public class RequestApplicationQueueForLoanByAdmin
    {
        public HttpPostedFileBase IdentityFile { get; set; }
        public HttpPostedFileBase PaySlipFile { get; set; }
        public HttpPostedFileBase EcFile { get; set; }
        public HttpPostedFileBase HrmisFile { get; set; }
        public HttpPostedFileBase BankStatementFile { get; set; }
        public HttpPostedFileBase LppsaFile { get; set; }
        public HttpPostedFileBase LicenseFile { get; set; }
        public HttpPostedFileBase RedemptionLetterFile { get; set; }
        public HttpPostedFileBase CreditCardFile { get; set; }
        public HttpPostedFileBase RamciFile { get; set; }
        public HttpPostedFileBase SignatureFile { get; set; }
        public HttpPostedFileBase BiroAngkasaFile { get; set; }
        public HttpPostedFileBase Kew320File { get; set; }
        public HttpPostedFileBase StaffCardFile { get; set; }
        public HttpPostedFileBase PostDatedChequeFile { get; set; }
        public HttpPostedFileBase CompanyConfirmationFile { get; set; }
        public HttpPostedFileBase EpfFile { get; set; }
        public HttpPostedFileBase EaformFile { get; set; }
        public HttpPostedFileBase BillUtilitiesFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public List<HttpPostedFileBase> Payslips { get; set; }
        public List<HttpPostedFileBase> BankStatements { get; set; }
        public string Json { get; set; }
    }

    public class RequestApplicationQueueForLoanData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int WorkerTypeId { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
        public List<long> AppFileDelete { get; set; }
    }

    public class RequestApplicationReloanByAdmin
    {
        public HttpPostedFileBase OfferLetterFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }       
    }
    public class RequestApplicationReloanData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int? ReloanStatusId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SigningDate { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
    }

    public class RequestApplicationCollectionByAdmin
    {
        public HttpPostedFileBase DeclarationFile { get; set; }
        public HttpPostedFileBase SettlementFile { get; set; }
        public HttpPostedFileBase ServiceFeeFile { get; set; }
        public HttpPostedFileBase RezekiFile { get; set; }
        public HttpPostedFileBase RezekiAgreementFile { get; set; }
        public List<HttpPostedFileBase> FileAdditionalDocuments { get; set; }
        public string Json { get; set; }       
    }

    public class RequestApplicationCollectionData : BaseApplicationRequestDataByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? DepositAmount { get; set; }
        public DateTime? DepositDate { get; set; }
        public List<string> RemarkAdditionalDocuments { get; set; }
        public List<RequestAdditionalDocumentEditByAdmin> AdditionalDocumentToModify { get; set; }
        public List<long> DocumentDelete { get; set; }
        public List<string> FileDelete { get; set; }
        public DateTime? SettlementDate { get; set; }
        public decimal? SettlementAmount { get; set; }
        public decimal? SettlementCPct { get; set; }
        public decimal? CollectionAmountPct { get; set; }
        public int? SettlementDuration { get; set; }
        public decimal? TotalReloan { get; set; }
        public decimal? TotalLoanRepayment { get; set; }
        public string DBBBankAccount { get; set; }
        public string DBBTenure { get; set; }
        public DateTime? DBBAgreementDate { get; set; }
        public decimal? MonthlyFund { get; set; }
        public decimal? DBBAmount { get; set; }
        public string ReceiptNo { get; set; }
        public string TaxNumber { get; set; }
        public int? StatusId { get; set; }
        public DateTime? InstallmentDate { get; set; }
    }

    public class ApplicationSettlementDetails
    {
        public ApplicationSettlementDetails()
        {
        }

        public ApplicationSettlementDetails(long? settlementId, decimal? amount, decimal? totalPct, decimal? totalPctAmount, DateTime? paymentDate, int? bankId, string bankAccountNumber, DateTime? dueDate, string facilitiesOther, decimal? amountFacilities, int? flexyCampaignId, decimal? totalCampaign, DateTime? redemptionLetterDate, decimal? redemptionAmount, DateTime? loanReleaseDate, int? settlementStatusId, string remark, string admin, int? facilitiesId, string flexyCampaignOther)
        {
            SettlementId = settlementId;
            Amount = amount;
            TotalPct = totalPct;
            TotalPctAmount = totalPctAmount;
            PaymentDate = paymentDate;
            BankId = bankId;
            BankAccountNumber = bankAccountNumber;
            DueDate = dueDate;
            FacilitiesOther = facilitiesOther;
            AmountFacilities = amountFacilities;
            FlexyCampaignId = flexyCampaignId;
            TotalCampaign = totalCampaign;
            RedemptionLetterDate = redemptionLetterDate;
            RedemptionAmount = redemptionAmount;
            LoanReleaseDate = loanReleaseDate;
            SettlementStatusId = settlementStatusId;
            Remark = remark;
            Admin = admin;
            FacilitiesId = facilitiesId;
            FlexyCampaignOther = flexyCampaignOther;
        }

        public long? SettlementId { get; set; } 
        public decimal? Amount { get; set; }
        public decimal? TotalPct { get; set; }
        public decimal? TotalPctAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? BankId { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public string FacilitiesOther { get; set; }
        public decimal? AmountFacilities { get; set; }
        public int? FlexyCampaignId { get; set; }
        public decimal? TotalCampaign { get; set; }
        public DateTime? RedemptionLetterDate { get; set; }
        public decimal? RedemptionAmount { get; set; }
        public DateTime? LoanReleaseDate { get; set; }
        public int? SettlementStatusId { get; set; }
        public string Remark { get; set; }
        public string Admin { get; set; }
        public int? FacilitiesId { get; set; }
        public string FlexyCampaignOther { get; set; }
    }

    public class ApplicationCaseUpdate
    {
        public ApplicationCaseUpdate()
        {

        }
        public ApplicationCaseUpdate(long? caseUpdateId, int? bankId, decimal? loanAmount, DateTime? submitDate, string banker, int? completeStatusId, string consolidate, decimal? cashNet, decimal? installment, DateTime? approvedDate, DateTime? signDate, DateTime? disbursementDate, DateTime? updateDate, string loanAccountNumber, DateTime? firstDueDate, string remarkds, string admin)
        {
            CaseUpdateId = caseUpdateId;
            BankId = bankId;
            LoanAmount = loanAmount;
            SubmitDate = submitDate;
            Banker = banker;
            CompleteStatusId = completeStatusId;
            Consolidate = consolidate;
            CashNet = cashNet;
            Installment = installment;
            ApprovedDate = approvedDate;
            SignDate = signDate;
            DisbursementDate = disbursementDate;
            UpdateDate = updateDate;
            LoanAccountNumber = loanAccountNumber;
            FirstDueDate = firstDueDate;
            Remarkds = remarkds;
            Admin = admin;
        }

        public long? CaseUpdateId { get; set; }
        public int? BankId { get; set; }
        public decimal? LoanAmount { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string Banker { get; set; }
        public int? CompleteStatusId { get; set; }
        public string Consolidate { get; set; }
        public decimal? CashNet { get; set; }
        public decimal? Installment { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? DisbursementDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string LoanAccountNumber { get; set; }
        public DateTime? FirstDueDate { get; set; }
        public string Remarkds { get; set; }
        public string Admin { get; set; }
    }

    public class RequestApplicationAdditionalDocument
    {
        public HttpPostedFileBase FileData { get; set; }
        public string Remark { get; set; }
    }

    public class RequestApplicaationGotoStatus
    {
        public RequestApplicaationGotoStatus(long applicationId, int adminId, int? newStatus, bool changeManually = false)
        {
            ApplicationId = applicationId;
            AdminId = adminId;
            NewStatus = newStatus;
            ChangeManually = changeManually;
        }

        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int? NewStatus { get; set; }
        public bool ChangeManually { get; set; }
    }

    public class ApplicationAdditionalDocument
    {
        public ApplicationAdditionalDocument(string fileId, string extension, string fileName, string remark, string uploaded, long applicationDocumentId, DateTime? createDate)
        {
            FileId = fileId;
            Extension = extension;
            FileName = fileName;
            Remark = remark;
            Uploaded = uploaded;
            ApplicationDocumentId = applicationDocumentId;
            CreateDate = createDate;
        }
        public long ApplicationDocumentId { get; set; }
        public string FileId { get; set; }
        public string Extension { get; set; }
        public string File => FileId + Extension;
        public string FileName { get; set; }
        public string Remark { get; set; }
        public string Uploaded { get; set; }
        public DateTime? CreateDate { get; set; }
    }

    public class ApplicationFileDocument
    {
        public ApplicationFileDocument(long applicationFileId, string fileId, string extension, string fileName, FileUploadedBy<ValueText<string>> uploadedBy)
        {
            ApplicationFileId = applicationFileId;
            FileId = fileId;
            Extension = extension;
            FileName = fileName;
            UploadedBy = uploadedBy;
        }

        public long ApplicationFileId { get; set; }
        public string FileId { get; set; }
        public string Extension { get; set; }
        public string File => FileId + Extension;
        public string FileName { get; set; }
        public FileUploadedBy<ValueText<string>> UploadedBy { get; set; }
    }

    public abstract class BaseUploadOrSelectByCreator<T>
    {
        protected BaseUploadOrSelectByCreator(T value, string name, DateTime? date)
        {
            Value = value;
            Name = name;
            Date = date;
        }

        public T Value { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Desc { get; set; }
    }

    public class FileUploadedBy<T> : BaseUploadOrSelectByCreator<T>
    {
        public FileUploadedBy(T value, string name, DateTime? date) : base(value, name, date)
        {
            Desc = $"*Uploaded by [<b>{Name}</b>] {Date?.ToString("dd MMMM yyyy HH:mm")}";
        }
    }

    public class OptionSelectedBy<T> : BaseUploadOrSelectByCreator<T>
    {
        public OptionSelectedBy(T value, string name, DateTime? date) : base(value, name, date)
        {
            Desc = $"*Selected by [<b>{Name}</b>] {Date?.ToString("dd MMMM yyyy HH:mm")}";
        }
    }

    public class RequestApplicationAssignByAdmin
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public long? MemberId { get; set; }
        public int? PfcAdminId { get; set; }
        public int? UmAdminId { get; set; }
        public int? AmAdminId { get; set; }
        public int? RmAdminId { get; set; }
        public int? PaAdminId { get; set; }
        public int? PreparedAdminId { get; set; }
        public int? AnalyzedAdminId { get; set; }
    }

    public class RequestApplicationReject
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public string RejectReason { get; set; }
    }

    public class RequestApplicationCancelReject
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
    }

    public class RequestApplicationInfo
    {
        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
        public int? SourceId { get; set; }
        public int? CreditStatusId { get; set; }
        public int CustomerStatusId { get; set; }
        public int? BurstReasonId { get; set; }
        public string CreditRemark { get; set; }
        public string ScoreClass { get; set; }
        public int? ApplicationStatusId { get; set; }
    }

    public class RequestApplicationHero
    {
        public RequestApplicationHero()
        {
        }

        public RequestApplicationHero(long applicationId, int adminId)
        {
            ApplicationId = applicationId;
            AdminId = adminId;
        }

        public long ApplicationId { get; set; }
        public int AdminId { get; set; }
    }

    public class RequestApplicationCreate
    {
        public long MemberId { get; set; }
    }
}
