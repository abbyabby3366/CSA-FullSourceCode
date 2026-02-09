using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public abstract class BaseApplicationDetailsViewModel
    {
        public BaseApplicationDetailsViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, string applicationRemark)
        {
            Info = info;
            ApplicationId = applicationId;
            ApplicationStatusId = applicationStatusId;
            ApplicationRemark = applicationRemark;
        }

        public ApplicationDetailsInfoProcess Info { get; set; }
        public long ApplicationId { get; set; }
        public int ApplicationStatusId { get; set; }
        public string ApplicationRemark { get; set; }
        public List<hudanLibrary.Data.SelectItem<int>> CustomerStatusType
        {
            get
            {
                return hudanLibrary.EnumHelper.EnumSource(typeof(Library.CustomerStatus));
            }
        }

    }

    public class ApplicationDetailsPreCheckingViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsPreCheckingViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> ramciFile, FileUploadedBy<ValueText<string>> ccrisFile, OptionSelectedBy<int?> eligibility, bool legalSuit, bool bankruptcy, bool specialAttentionAccount, bool badPaymentRecordCheck, List<ApplicationAdditionalDocument> additionalDocuments, string applicationRemark, FileUploadedBy<ValueText<string>> payslipFile) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            RamciFile = ramciFile;
            CcrisFile = ccrisFile;
            Eligibility = eligibility;
            LegalSuit = legalSuit;
            Bankruptcy = bankruptcy;
            SpecialAttentionAccount = specialAttentionAccount;
            AdditionalDocuments = additionalDocuments;
            BadPaymentRecordCheck = badPaymentRecordCheck;
            PayslipFile = payslipFile;
        }
        public FileUploadedBy<ValueText<string>> RamciFile { get; set; }
        public FileUploadedBy<ValueText<string>> CcrisFile { get; set; }
        public OptionSelectedBy<int?> Eligibility { get; set; }
        public bool LegalSuit { get; set; }
        public bool Bankruptcy { get; set; }
        public bool SpecialAttentionAccount { get; set; }
        public bool BadPaymentRecordCheck { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
        public FileUploadedBy<ValueText<string>> PayslipFile { get; set; }
    }

    public class ApplicationDetailsPreparationViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsPreparationViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> proposalFile, decimal? salaryGross, decimal? salaryDeduction, decimal? netIncome, decimal? b1, decimal? b2, decimal? b3, decimal? b4, decimal? bAverage, decimal? commitmentOutstanding, decimal? commitmentInstallment, decimal? otherNetBalance, decimal? otherBPA, decimal? otherComparisonDSR, decimal? otherComparisonDSRPctCommitment, decimal? otherPctRefresh, decimal? otherProposedRefresh, decimal? otherCompositionDSR, decimal? otherCompositionDSRPctCommitment, decimal? refreshTotal, decimal? refreshRemainCommitment, decimal? reloanTotal, decimal? reloanMonthly, decimal? reloanBersih, decimal? reloanBelanja, decimal? reloanDeposit, decimal? reloanDanaBantuan, decimal? reloanServiceFee, decimal? reloanServiceFeePct, decimal? reloanIncomeAfterRNR, decimal? reloanDifference, int? modelBackgroundScreeningId, int? modelCompositionDSRId, int? modelCommitmentId, int? modelSettlementId, int? modelServiceFeeId, int? modelNetIncomeAfterRNRId, int? modelStatusId, int? modelStatusProposalId, int? modelCheckId, int? reviewAdminId, int? reviewStatusId, DateTime? reviewDate, string reviewComment, int? approveAdminId, DateTime? approveDate, string approveComment, int? approveStatusId, string applicationRemark, int? verifiedAdminId, int? verifiedStatusId, DateTime? verifiedDate, string verifiedComment, List<ValueText<int>> adminVerified, List<ValueText<int>> adminReviewed, List<ValueText<int>> adminApproved) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            ProposalFile = proposalFile;
            SalaryGross = salaryGross;
            SalaryDeduction = salaryDeduction;
            NetIncome = netIncome;
            B1 = b1;
            B2 = b2;
            B3 = b3;
            B4 = b4;
            BAverage = bAverage;
            CommitmentOutstanding = commitmentOutstanding;
            CommitmentInstallment = commitmentInstallment;
            OtherNetBalance = otherNetBalance;
            OtherBPA = otherBPA;
            OtherComparisonDSR = otherComparisonDSR;
            OtherComparisonDSRPctCommitment = otherComparisonDSRPctCommitment;
            OtherPctRefresh = otherPctRefresh;
            OtherProposedRefresh = otherProposedRefresh;
            OtherCompositionDSR = otherCompositionDSR;
            OtherCompositionDSRPctCommitment = otherCompositionDSRPctCommitment;
            RefreshTotal = refreshTotal;
            RefreshRemainCommitment = refreshRemainCommitment;
            ReloanTotal = reloanTotal;
            ReloanMonthly = reloanMonthly;
            ReloanBersih = reloanBersih;
            ReloanBelanja = reloanBelanja;
            ReloanDeposit = reloanDeposit;
            ReloanDanaBantuan = reloanDanaBantuan;
            ReloanServiceFee = reloanServiceFee;
            ReloanServiceFeePct = reloanServiceFeePct;
            ReloanIncomeAfterRNR = reloanIncomeAfterRNR;
            ReloanDifference = reloanDifference;
            ModelBackgroundScreeningId = modelBackgroundScreeningId;
            ModelCompositionDSRId = modelCompositionDSRId;
            ModelCommitmentId = modelCommitmentId;
            ModelSettlementId = modelSettlementId;
            ModelServiceFeeId = modelServiceFeeId;
            ModelNetIncomeAfterRNRId = modelNetIncomeAfterRNRId;
            ModelStatusId = modelStatusId;
            ModelStatusProposalId = modelStatusProposalId;
            ModelCheckId = modelCheckId;
            ReviewAdminId = reviewAdminId;
            ReviewStatusId = reviewStatusId;
            ReviewDate = reviewDate;
            ReviewComment = reviewComment;
            ApproveAdminId = approveAdminId;
            ApproveDate = approveDate;
            ApproveComment = approveComment;
            ApproveStatusId = approveStatusId;
            VerifiedAdminId = verifiedAdminId;
            VerifiedStatusId = verifiedStatusId;
            VerifiedDate = verifiedDate;
            VerifiedComment = verifiedComment;
            AdminVerified = adminVerified;
            AdminReviewed = adminReviewed;
            AdminApproved = adminApproved;
        }
        public FileUploadedBy<ValueText<string>> ProposalFile { get; set; }
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
        public decimal? OtherPctRefresh{ get; set; }
        public decimal? OtherProposedRefresh{ get; set; }
        public decimal? OtherCompositionDSR{ get; set; }
        public decimal? OtherCompositionDSRPctCommitment{ get; set; }
        public decimal? RefreshTotal{ get; set; }
        public decimal? RefreshRemainCommitment{ get; set; }
        public decimal? ReloanTotal{ get; set; }
        public decimal? ReloanMonthly{ get; set; }
        public decimal? ReloanBersih{ get; set; }
        public decimal? ReloanBelanja{ get; set; }
        public decimal? ReloanDeposit{ get; set; }
        public decimal? ReloanDanaBantuan{ get; set; }
        public decimal? ReloanServiceFee{ get; set; }
        public decimal? ReloanServiceFeePct{ get; set; }
        public decimal? ReloanIncomeAfterRNR{ get; set; }
        public decimal? ReloanDifference{ get; set; }
        public int? ModelBackgroundScreeningId{ get; set; }
        public int? ModelCompositionDSRId{ get; set; }
        public int? ModelCommitmentId{ get; set; }
        public int? ModelSettlementId{ get; set; }
        public int? ModelServiceFeeId{ get; set; }
        public int? ModelNetIncomeAfterRNRId{ get; set; }
        public int? ModelStatusId{ get; set; }
        public int? ModelStatusProposalId{ get; set; }
        public int? ModelCheckId{ get; set; }
        public int? ReviewAdminId{ get; set; }
        public int? ReviewStatusId{ get; set; }
        public DateTime? ReviewDate{ get; set; }
        public string ReviewComment{ get; set; }
        public int? ApproveAdminId { get; set; }
        public int? ApproveStatusId{ get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproveComment { get; set; }
        public int? VerifiedAdminId { get; set; }
        public int? VerifiedStatusId { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string VerifiedComment { get; set; }
        public List<ValueText<int>> AdminVerified { get; set; }
        public List<ValueText<int>> AdminReviewed { get; set; }
        public List<ValueText<int>> AdminApproved { get; set; }
    }

    public class ApplicationDetailsProposalViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsProposalViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, OptionSelectedBy<int> proposalStatusId, List<ApplicationAdditionalDocument> additionalDocuments, string applicationRemark) : base(applicationId, applicationStatusId, info,applicationRemark)
        {
            ProposalStatusId = proposalStatusId;
            AdditionalDocuments = additionalDocuments;
        }

        public OptionSelectedBy<int> ProposalStatusId { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
    }
    
    public class ApplicationDetailsPresignViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsPresignViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, OptionSelectedBy<int> proposalSendId, OptionSelectedBy<int> suratAkuanId, OptionSelectedBy<int> comprehensiveFormId, List<ApplicationAdditionalDocument> additionalDocuments, string applicationRemark) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            ProposalSendId = proposalSendId;
            SuratAkuanId = suratAkuanId;
            ComprehensiveFormId = comprehensiveFormId;
            AdditionalDocuments = additionalDocuments;
        }

        public OptionSelectedBy<int> ProposalSendId { get; set; }
        public OptionSelectedBy<int> SuratAkuanId { get; set; }
        public OptionSelectedBy<int> ComprehensiveFormId { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
    }

    public class ApplicationDetailsZoomAcceptanceViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsZoomAcceptanceViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> payslipFile, FileUploadedBy<ValueText<string>> ramciFile, FileUploadedBy<ValueText<string>> ctosFile, FileUploadedBy<ValueText<string>> redemptionLetterFile, string applicantAddress, int? bankruptcyStatus, int? legalCase, int? healthCreditScore, decimal? commitments, List<ApplicationAdditionalDocument> additionalDocuments, string applicationRemark) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            PayslipFile = payslipFile;
            RamciFile = ramciFile;
            CtosFile = ctosFile;
            RedemptionLetterFile = redemptionLetterFile;
            ApplicantAddress = applicantAddress;
            BankruptcyStatus = bankruptcyStatus;
            LegalCase = legalCase;
            HealthCreditScore = healthCreditScore;
            Commitments = commitments;
            AdditionalDocuments = additionalDocuments;
        }

        public FileUploadedBy<ValueText<string>> PayslipFile { get; set; }
        public FileUploadedBy<ValueText<string>> RamciFile { get; set; }
        public FileUploadedBy<ValueText<string>> CtosFile { get; set; }
        public FileUploadedBy<ValueText<string>> RedemptionLetterFile { get; set; }
        public string ApplicantAddress { get; set; }
        public int? BankruptcyStatus { get; set; }
        public int? LegalCase { get; set; }
        public int? HealthCreditScore { get; set; }
        public decimal? Commitments { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
    }

    public class ApplicationDetailsSettlementViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsSettlementViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> settlementFile, List<ApplicationSettlementDetails> settlementDetails, List<ApplicationCaseUpdate> caseUpdates, List<ApplicationAdditionalDocument> additionalDocuments, List<DropdownItem> vtBanks, string applicationRemark, List<hudanLibrary.Data.SelectItem<int>> facilities, List<hudanLibrary.Data.SelectItem<int>> flexyCampaign) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            SettlementFile = settlementFile;
            SettlementDetails = settlementDetails;
            CaseUpdates = caseUpdates;
            AdditionalDocuments = additionalDocuments;
            VtBanks = vtBanks;
            Facilities = facilities;
            FlexyCampaign = flexyCampaign;
        }

        public FileUploadedBy<ValueText<string>> SettlementFile { get; set; }
        public List<ApplicationSettlementDetails> SettlementDetails { get; set; }
        public List<ApplicationCaseUpdate> CaseUpdates { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
        public List<DropdownItem> VtBanks { get; set; }
        public List<hudanLibrary.Data.SelectItem<int>> Facilities { get; set; }
        public List<hudanLibrary.Data.SelectItem<int>> FlexyCampaign { get; set; }
    }

    public class ApplicationDetailsCcrisViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsCcrisViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> releaseLetterFile, FileUploadedBy<ValueText<string>> ccrisReportFile, List<ApplicationAdditionalDocument> additionalDocuments, string applicationRemark, FileUploadedBy<ValueText<string>> hrmisFile, FileUploadedBy<ValueText<string>> anmFile, FileUploadedBy<ValueText<string>> lpsaFile, FileUploadedBy<ValueText<string>> angkasaFile, int? bankruptcyStatus, int? legalCase, int? healthCreditScore, string commitments, List<ApplicationCaseUpdate> caseUpdates) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            ReleaseLetterFile = releaseLetterFile;
            CcrisReportFile = ccrisReportFile;
            AdditionalDocuments = additionalDocuments;
            HrmisFile = hrmisFile;
            AnmFile = anmFile;
            LpsaFile = lpsaFile;
            AngkasaFile = angkasaFile;
            BankruptcyStatus = bankruptcyStatus;
            LegalCase = legalCase;
            HealthCreditScore = healthCreditScore;
            Commitments = commitments;
            CaseUpdates = caseUpdates;
        }

        public FileUploadedBy<ValueText<string>> ReleaseLetterFile { get; set; }
        public FileUploadedBy<ValueText<string>> CcrisReportFile { get; set; }
        public FileUploadedBy<ValueText<string>> HrmisFile { get; set; }
        public FileUploadedBy<ValueText<string>> AnmFile { get; set; }
        public FileUploadedBy<ValueText<string>> LpsaFile { get; set; }
        public FileUploadedBy<ValueText<string>> AngkasaFile { get; set; }
        public int? BankruptcyStatus { get; set; }
        public int? LegalCase { get; set; }
        public int? HealthCreditScore { get; set; }
        public string Commitments { get; set; }
        public List<ApplicationCaseUpdate> CaseUpdates { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
    }

    public class ApplicationDetailsQueueForLoanViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsQueueForLoanViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> identityCardFile, FileUploadedBy<ValueText<string>> payslipFile, FileUploadedBy<ValueText<string>> ecFileFile, FileUploadedBy<ValueText<string>> hrmisFile, FileUploadedBy<ValueText<string>> bankStatementFile, FileUploadedBy<ValueText<string>> lppsaFile, FileUploadedBy<ValueText<string>> licenseFile, FileUploadedBy<ValueText<string>> redemptionLetterFile, FileUploadedBy<ValueText<string>> ccStatementFile, FileUploadedBy<ValueText<string>> ramciFile, FileUploadedBy<ValueText<string>> signatureFile, FileUploadedBy<ValueText<string>> biroFile, FileUploadedBy<ValueText<string>> kew320File, FileUploadedBy<ValueText<string>> staffCardFile, FileUploadedBy<ValueText<string>> postDatedChequeFile, FileUploadedBy<ValueText<string>> companyConfirmationFile, FileUploadedBy<ValueText<string>> epfFile, FileUploadedBy<ValueText<string>> eaformFile, FileUploadedBy<ValueText<string>> billUtilitiesFile, List<ApplicationAdditionalDocument> additionalDocuments, int? workerTypeId, List<ApplicationFileDocument> payslips, List<ApplicationFileDocument> bankStatement, string applicationRemark) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            IdentityCardFile = identityCardFile;
            PayslipFile = payslipFile;
            EcFileFile = ecFileFile;
            HrmisFile = hrmisFile;
            BankStatementFile = bankStatementFile;
            LppsaFile = lppsaFile;
            LicenseFile = licenseFile;
            RedemptionLetterFile = redemptionLetterFile;
            CcStatementFile = ccStatementFile;
            RamciFile = ramciFile;
            SignatureFile = signatureFile;
            BiroFile = biroFile;
            Kew320File = kew320File;
            StaffCardFile = staffCardFile;
            PostDatedChequeFile = postDatedChequeFile;
            CompanyConfirmationFile = companyConfirmationFile;
            EpfFile = epfFile;
            EaformFile = eaformFile;
            BillUtilitiesFile = billUtilitiesFile;
            AdditionalDocuments = additionalDocuments;
            WorkerTypeId = workerTypeId;
            Payslips = payslips;
            BankStatement = bankStatement;
        }

        public FileUploadedBy<ValueText<string>> IdentityCardFile { get; set; }
        public FileUploadedBy<ValueText<string>> PayslipFile { get; set; }
        public FileUploadedBy<ValueText<string>> EcFileFile { get; set; }
        public FileUploadedBy<ValueText<string>> HrmisFile { get; set; }
        public FileUploadedBy<ValueText<string>> BankStatementFile { get; set; }
        public FileUploadedBy<ValueText<string>> LppsaFile { get; set; }
        public FileUploadedBy<ValueText<string>> LicenseFile { get; set; }
        public FileUploadedBy<ValueText<string>> RedemptionLetterFile { get; set; }
        public FileUploadedBy<ValueText<string>> CcStatementFile { get; set; }
        public FileUploadedBy<ValueText<string>> RamciFile { get; set; }
        public FileUploadedBy<ValueText<string>> SignatureFile { get; set; }
        public FileUploadedBy<ValueText<string>> BiroFile { get; set; }
        public FileUploadedBy<ValueText<string>> Kew320File { get; set; }
        public FileUploadedBy<ValueText<string>> StaffCardFile { get; set; }
        public FileUploadedBy<ValueText<string>> PostDatedChequeFile { get; set; }
        public FileUploadedBy<ValueText<string>> CompanyConfirmationFile { get; set; }
        public FileUploadedBy<ValueText<string>> EpfFile { get; set; }
        public FileUploadedBy<ValueText<string>> EaformFile { get; set; }
        public FileUploadedBy<ValueText<string>> BillUtilitiesFile { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
        public List<ApplicationFileDocument> Payslips { get; set; }
        public List<ApplicationFileDocument> BankStatement { get; set; }
        public int? WorkerTypeId { get; set; }
    }

    public class ApplicationDetailsReloanViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsReloanViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> offerLetterFile, int? reloanStatusId, DateTime? approvedDate, DateTime? signingDate, List<ApplicationAdditionalDocument> additionalDocuments, decimal? approvedAmount, string applicationRemark) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            OfferLetterFile = offerLetterFile;
            ReloanStatusId = reloanStatusId;
            ApprovedDate = approvedDate;
            SigningDate = signingDate;
            AdditionalDocuments = additionalDocuments;
            ApprovedAmount = approvedAmount;
        }

        public FileUploadedBy<ValueText<string>> OfferLetterFile { get; set; }
        public int? ReloanStatusId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SigningDate { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
        public decimal? ApprovedAmount { get; set; }
    }

    public class ApplicationDetailsCollectionViewModel : BaseApplicationDetailsViewModel
    {
        public ApplicationDetailsCollectionViewModel(long applicationId, int applicationStatusId, ApplicationDetailsInfoProcess info, FileUploadedBy<ValueText<string>> declarationFile, FileUploadedBy<ValueText<string>> settlementFile, FileUploadedBy<ValueText<string>> serviceFeeFile, FileUploadedBy<ValueText<string>> rezekiFile, FileUploadedBy<ValueText<string>> rezekiAgreementFile, decimal? serviceFee, decimal? depositAmount, DateTime? depositDate, List<ApplicationAdditionalDocument> additionalDocuments, string applicationRemark, DateTime? settlementDate, decimal? settlementAmount, decimal? settlementCPct, decimal? collectionAmountPct, int? settlementDuration, decimal? totalReloan, decimal? totalLoanRepayment, string dBBBankAccount, string dBBTenure, DateTime? dBBAgreementDate, decimal? monthlyFund, decimal? dBBAmount, string receiptNo, string taxNumber, int? statusId, DateTime? installmentDate) : base(applicationId, applicationStatusId, info, applicationRemark)
        {
            DeclarationFile = declarationFile;
            SettlementFile = settlementFile;
            ServiceFeeFile = serviceFeeFile;
            RezekiFile = rezekiFile;
            RezekiAgreementFile = rezekiAgreementFile;
            ServiceFee = serviceFee;
            DepositAmount = depositAmount;
            DepositDate = depositDate;
            AdditionalDocuments = additionalDocuments;
            SettlementDate = settlementDate;
            SettlementAmount = settlementAmount;
            SettlementCPct = settlementCPct;
            CollectionAmountPct = collectionAmountPct;
            SettlementDuration = settlementDuration;
            TotalReloan = totalReloan;
            TotalLoanRepayment = totalLoanRepayment;
            DBBBankAccount = dBBBankAccount;
            DBBTenure = dBBTenure;
            DBBAgreementDate = dBBAgreementDate;
            MonthlyFund = monthlyFund;
            DBBAmount = dBBAmount;
            ReceiptNo = receiptNo;
            TaxNumber = taxNumber;
            StatusId = statusId;
            InstallmentDate = installmentDate;
        }

        public FileUploadedBy<ValueText<string>> DeclarationFile { get; set; }
        public FileUploadedBy<ValueText<string>> SettlementFile { get; set; }
        public FileUploadedBy<ValueText<string>> ServiceFeeFile { get; set; }
        public FileUploadedBy<ValueText<string>> RezekiFile { get; set; }
        public FileUploadedBy<ValueText<string>> RezekiAgreementFile { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? DepositAmount { get; set; }
        public DateTime? DepositDate { get; set; }
        public List<ApplicationAdditionalDocument> AdditionalDocuments { get; set; }
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
}
