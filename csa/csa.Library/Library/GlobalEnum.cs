using System;
using System.ComponentModel;

namespace csa.Library
{
    public class GlobalEnum
    {

    }

    public enum ErrorCode
    {
        OK = 1,

        FAILED = -101,
        DATA_EXISTED = -102,
        DATA_NOT_FOUND = -103,
        INPUT_DATA_MISMATCH = -104,
        INVALID_INPUT = -105,
        INVALID_INPUT_FORMAT = -106,
        REQUIRED_PARAMETER_NOT_SUPPLIED = -107,
        INVALID_REQUEST = -108,
        INVALID_DATA = -109,

        /* hashing key */
        INVALID_HASHING = -201,
        INVALID_TIMESTAMP = -202,

        /* credential */
        INVALID_ACCESS = -301,
        INACTIVE_ACCOUNT = -302,
        INVALID_CREDENTIAL = -303,
        INVALID_CAPTCHA = -351,

        /* server */
        CONNECTION_FAILED = -801,
        NOT_RESPONDING = -802,
        SESSION_TIMEOUT = -811,
        SERVER_MAINTENANACE = -851,

        UNKNOWN_ERROR = -999,

        /* custom error */
        CUSTOM_ERROR = -1000
    }

    public enum Language
    {
        EN = 1,
        CN = 2,
        MS = 3
    }

    //bitwise
    public enum AccountType
    {
        SYSTEM = 1,
        ADMIN = 2,
        MEMBER = 4,
        AGENT = 8
    }

    //bitwise
    public enum RoleType
    {
        SYSTEM = 1,
        NORMAL = 2
    }

    //string value in `role`.`Code`
    public enum RoleCategory
    {
        SUPERADMIN = 1,
        ADMIN = 2,
        MEMBER = 3,
        AGENT = 4
    }

    public enum GlobalStatus
    {
        ACTIVE = 1,
        INACTIVE = 2,
        REMOVED = 9
    }

    //either 'Yes/No', 'True/False' etc. whatever 2 choices value
    public enum GlobalBoolean
    {
        YES = 1,
        NO = 0
    }

    //public enum UserStatus
    //{
    //    ACTIVE = 1,
    //    INACTIVE = 2,
    //    NON_ACTIVATED = 5,
    //    REMOVED = 9
    //}

    public enum AddressType
    {
        USER_ADDRESS = 1,
        BILLING_ADDRESS = 2,
        DELIVERY_ADDRESS = 3,

        COMPANY_ADDRESS = 11
    }

    //bitwise
    public enum UsrGrpInd
    {
        FIRST_LOGIN = 1
    }

    public enum Gender
    {
        NONE = 0,

        MALE = 1,
        FEMALE = 2
    }

    public enum MYICType
    {
        NEW_IC = 1,
        OLD_IC_OR_OTHER = 2
    }

    public enum MemberType
    {
        MEMBER = 1,
        AGENT,
        HERO
    }

    public enum MemberStatus
    {
        DRAFT,
        ACTIVE = 1,
        INACTIVE = 2,
        UNVERIFIED,
        REMOVED,
        REJECTED,
    }

    public enum AgentType
    {
        [Description("TYPE A")]
        TYPE_A = 1,
        [Description("TYPE B")]
        TYPE_B = 2,
        [Description("TYPE C")]
        TYPE_C = 3,
        [Description("TYPE D")]
        TYPE_D = 4
    }

    public enum AgentStatus
    {
        [Description("FINANCIAL SURVEY")]
        FINANCIAL_SURVEY = 1,
        [Description("ATTENDED A GROUP WEBSINAR")]
        ATTENDED_A_GROUP_WEBSINAR = 2,
        [Description("1ON1ZOOMWITHPFC")]
        PFC_1_ON_1_ZOOM = 3,
        [Description("MISSION PARTNER")]
        MISSION_PARTNER = 4,
        [Description("RNR APP STARTED OR COMPLETED")]
        RNR_APP_STARTED_OR_COMPLETED = 5
    }

    public enum WithdrawStatus
    {
        PENDING = 1,
        PROCESSING = 2,
        PAID = 3,
        CANCEL
    }

    //for approved and reviewed
    public enum ConfirmationStatus
    {
        INPROGRESS = 1,
        APPROVED,
        REJECTED
    }

    public enum ProposalStatus
    {
        INPROCESS = 1,
        PRESENTED,
        PROPOSAL_ACCEPTED,
        PROPOSAL_REJECTED
    }


    public enum ApplicationType
    {
        REGULAR = 1
    }

    public enum ApplicationStatus
    {
        [Description("PRE-CHECKING")]
        PRE_CHECKING = 1,
        [Description("PROPOSAL PREPARATION")]
        PREPARATION = 2,
        [Description("PROPOSAL PRESENTATION")]
        PRESENTATION = 3,
        [Description("PRE-SIGNING")]
        PRESIGN = 4,
        [Description("PENDING ZOOM & ACCEPTANCE")]
        PENDINGZOOMACCEPTANCE = 5,
        [Description("SETTLEMENT")]
        SETTLEMENT = 6,
        [Description("CCRIS")]
        CCRIS = 7,
        [Description("QUEUE FOR LOAN")]
        QUEUEFORLOAN = 8,
        [Description("RELOAN")]
        RELOAN = 9,
        [Description("COLLECTION")]
        COLLECTION = 10,
        [Description("WIRA")]
        WIRA = 11,
        [Description("REJECTION")]
        REJECTION = 20
    }

    public enum CustomerStatus
    {
        Eligible = 1,
        Burst,
        [Description("Drop Case")]
        Drop_Case,
        MIA
    }

    //old
    //public enum SalaryRange
    //{
    //    [Description("MYR 0 - MYR 4000")]
    //    MYR0ToMYR4000 = 1,
    //    [Description("MYR 4000 - MYR 6000")]
    //    MYR4000ToMYR6000,
    //    [Description("MYR 6000 - MYR 8000")]
    //    MYR6000ToMYR8000,
    //    [Description("MYR 10,000 - MYR 20,000")]
    //    MYR100000ToMYR20000
    //}

    public enum SalaryRange
    {
        [Description("Kurang RM2,500")]
        Kurang2500 = 1,
        [Description("RM2,501 - RM3,170")]
        RM2501To3170,
        [Description("RM3,171 - RM3,970")]
        RM3171To3970,
        [Description("RM3,971 - RM4,850")]
        RM3971To4850,
        [Description("RM4,851 - RM5,880")]
        RM4851To5880,
        [Description("RM5,881 - RM7,100")]
        RM5881To7100,
        [Description("RM7,101 - RM8,700")]
        RM7101To8700,
        [Description("RM8,701 - RM10,970")]
        RM8701To10970,
        [Description("RM10,971 - RM15,040")]
        RM10971To1504,
        [Description("RM15,041 dan lebih")]
        RM15041ToLebih,
    }

    public enum OrderStatus
    {
        NONE = 0
    }

    public enum PaymentType
    {
        MANUAL_PAYMENT = 1,
        PAYMENT_GATEWAY = 5
    }

    public enum PaymentStatus
    {
        PENDING_PAYMENT = 1,
        CANCELLED = 2,
        COMPLETED = 5,
        REJECTED = 7
    }

    public enum PGType
    {
        BETTER_PAY = 1,
        IPAY88 = 2,
        BILL_PLZ = 3,
        TOYYIB = 4
    }

    public enum MetaTagType
    {
        AUDIENCE = 1
    }

    public enum EmailLogType
    {
        NONE = 0,

        MEMB_NEW_CREATION = 101,
        MEMB_CHG_PWD = 102,
        MEMB_4GET_PWD = 103,

        ADMIN_NEW_MEMB_CREATION = 151
    }

    public enum SchedulerLogType
    {
        UPD_ORD_AUTO_COMPLETION = 101
    }

    public enum EmailLogStatus
    {
        NEW = 1,
        SENT = 2,
        PENDING = 3,
        CANCELLED = 5,
        FAILED = 7
    }

    public enum GroupType
    {
        WALLET_TYPE = 1,

        REGISTER_TRANS = 2,
        ORDER_TRANS = 3,
        WALLET_TRANS = 4,
        OTHERS_TRANS = 9,
    }

    public enum WalletType
    {
        ECASH = 1
    }

    public enum MemberRegisterTransType
    {
        MEMB_REG_BY_ADMIN = 201,
        MEMB_REG_SELF = 202,
        MEMB_REF_REG = 203
    }

    public enum OrderTransType
    {
        MEMB_ORD_SUBMITTED = 302,
        MEMB_ORD_VERIFIED = 303,

        MEMB_ORD_CANCELLED = 307,

        MEMB_ORD_SHIPPED = 310,
    }

    public enum WalletTransType
    {
        MEMB_WLT_PG_TOPUP = 401,
        MEMB_WLT_MANUAL_TOPUP = 402,

        MEMB_WLT_WITHDRAW = 405,
    }

    public enum ChangeLogType
    {
        MEMB_ORD_DRAFT = 200301,
        MEMB_ORD_PAID = 200304,

        MEMB_ORD_CANCELLED = 200307,
        MEMB_ORD_SHIPPED = 200315,
        MEMB_ORD_DELIVERED = 200320,
        MEMB_ORD_COMPLETED = 200350
    }

    public enum ConfigSettingType
    {
        MAINTENANCE_WEBSITE_CONFIG = 1,
        MAINTENANCE_MOBILE_CONFIG = 2,

        ADMINISTRATOR_EMAIL = 10,

        EMAIL_NEW_MEMB_CREATION = 1101,
        EMAIL_MEMB_PWD_CHG = 1102,
        EMAIL_MEMB_4GET_PWD = 1103,
        EMAIL_MEMB_ACC_CONFIRMATION = 1104,
        EMAIL_MEMB_ORD_STS_CHANGED = 1111,
        EMAIL_MEMB_HELP_SUPP = 1121,
        EMAIL_MEMB_ACC_TERMINATION = 1122,

        EMAIL_ADMIN_NEW_MEMB_CREATION = 1501,
        EMAIL_ADMIN_MEMB_ORD_PLACED_NOTIFICATION = 1513,
        EMAIL_ADMIN_MEMB_ORD_PAID_NOTIFICATION = 1518,

        BETTER_PAY_MERCHANT_ID = 501,
        BETTER_PAY_API_KEY = 502,
        IPAY88_MERCHANT_CODE = 511,
        IPAY88_MERCHANT_KEY = 512
    }

    public enum HistoryType
    {
        [Description("TOP UP")]
        TOPUP = 1,
        [Description("TRANSFER")]
        TRANSFER,
        [Description("WITHDRAWAL")]
        WITHDRAWAL,
        [Description("ADMIN CHANGES")]
        ADMIN_CHANGES,
        [Description("COMMISSION SURVEY")]
        COMMISSION_SURVEY,
        [Description("COMMISSION REFERRAL")]
        COMMISSION_REFERRAL,
    }

    public enum TemplateType
    {
        HTML = 1,
        PlainText
    }

    public enum SurveyStatus
    {
        New = 1,
        Eligible,
        Not_Eligible,
        Zoom_Attended
    }

    public enum SurveyType
    {
        YABAM = 1
    }

    public enum MemberProgramEvent
    {
        YABAM = 1
    }

    public enum AdminTeamType
    {
        [Description("Credit Team")]
        CreditTeam = 1,
        [Description("Verified Officers")]
        VerifiedOfficers,
        [Description("Credit Dept")]
        CreditDept,
        [Description("Sales Director")]
        SalesDirector,
        RM,
        PA
    }

    public enum AdminRoleType
    {
        CreditTeam = 1,
        VerifiedOfficers,
        CreditDept,
        SalesDirector,
        RM,
        PA
    }

    public enum ApplicationSourceType
    {
        HerosIndividual = 1,
        HerosFormerPFC,
        HeroSeminar,
        Roadshow,
        ColdCallFresh,
        CompanyAds,
        AgentMemberIndividual,
        AgentMemberSeminar,
        YABAM,
    }

    public enum FacilitiesType {
        [Description("PELNFNCE - PERSONAL LOANS/FINANCING")]
        PERSONAL_LOANS_FINANCING = 1,
        [Description("OTLNFNCE - OTHER TERM LOANS/FINANCING")]
        OTHER_TERM_LOANS_FINANCING,
        [Description("CRDTCARD - CREDIT CARD")]
        CREDIT_CARD,
        [Description("PCPASCAR - PURCHASE OF PASSENGER CARS")]
        PURCHASE_OF_PASSENGER_CARS,
        [Description("HSLNFNCE - HOUSING LOANS/FINANCING")]
        HOUSING_LOANS_FINANCING,
        [Description("NHEDFNCE - NATIONAL HIGHER EDUCATION FINANCING")]
        NATIONAL_HIGHER_EDUCATION_FINANCING,
    }

    public enum FlexyCampaignType
    {
        [Description("RM 60K CAR")]
        RM_60K_CAR = 1,
        [Description("RM 40K CAR")]
        RM_40K_CAR,
        [Description("UMRAH SEPTEMBER 2025")]
        UMRAH_SEPTEMBER_2025,
        [Description("UMRAH NOVEMBER 2025")]
        UMRAH_NOVEMBER_2025,
        [Description("RM 40K UMRAH")]
        RM_40K_UMRAH,
        [Description("RM 40K MOTORCYCLE")]
        RM_40K_MOTORCYCLE,
        [Description("RM 135K CAR")]
        RM_135K_CAR,
    }

    public enum MemberGVType
    {
        Client,
        Hero_Wira,
        Member,
        Agent,
        Drop_Mia,
        Yabam
    }

    public enum ApproveStatus
    {
        IN_PROGRESS = 1,
        APPROVED_SINGLE,
        APPROVED_RNR,
        REJECTED
    }

    public enum CreditStatus
    {
        RANCANG_REZEKI_RNR = 1,
        BURST,
        PAYSLIP,
        REVIEW,
        PENDING,
        DECLINED_BY_AM,
        CUSTOMER_MISSING_IN_ACTION_MIA,
        REZEKI_DIRECT,
        REZEKI_SETTLEMENT,
        SINGLE,
    }

    public enum Race
    {
        Malay = 1,
        Chinese,
        Indian,
        Sabahan,
        Sarawakian,
        Other
    }
}
