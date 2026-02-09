using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

using csa.Data;
using csa.Data.Cache;
using csa.Data.EntityModel;
using csa.Data.Library;
using csa.Library;
using csa.Model;

namespace csa.Data.Logic
{
    public class ApplicationLogic
    {
        /// <summary>
        /// Get `status-of-applicant` gridview listing by member
        /// </summary>
        /// <param name="USid">User sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;ApplicantStatusGVByMemberModel&gt;</returns>
        public static RespArgs<GridViewModel<ApplicantStatusGVByMemberModel>> GetApplicantStatusGVByMember(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<application, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<ApplicantStatusGVByMemberModel>> retVal = new RespArgs<GridViewModel<ApplicantStatusGVByMemberModel>> { Error = true };

            //if (ASid == null)
            //{
            //    retVal.Error = true;
            //    retVal.Code = (int)ErrorCode.SESSION_TIMEOUT;
            //    retVal.Message = "Session timeout";

            //    return retVal;
            //}

            //AdminSessionCacheModel session = SessionCache.GetSessionCacheByKey<AdminSessionCacheModel>(ASid.Value);

            //if (session == null)
            //{
            //    retVal.Error = true;
            //    retVal.Code = (int)ErrorCode.SESSION_TIMEOUT;
            //    retVal.Message = "Session timeout";

            //    return retVal;
            //}

            //validation

            if (PageIdx < 1) { PageIdx = 1; }
            if (PageSize < 10) { PageSize = 10; }
            if (PageSize > 500) { PageSize = 500; }

            int[] allowSts = { (int)GlobalStatus.ACTIVE, (int)GlobalStatus.INACTIVE };

            IEnumerable<ApplicantStatusGVByMemberModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `application`
                    //IQueryable<application> applications = db.applications
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<application> applications = new List<application>
                    {
                       //new application { Id = new Guid("2b1f9dbd-ddd6-415d-8ae5-70b1798b0987"), SequenceId = 1, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("9afcb499-785b-45cf-b3f7-992fc6a1da02"), Salary = 4000, AppGrpInd = 0, Status = (int)ApplicationStatus.PRE_CHECKING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("9afcb499-785b-45cf-b3f7-992fc6a1da02"), SequenceId = 1, Type = (int)MemberType.MEMBER, UserId = new Guid("4de124fb-a4a8-4dd1-942a-35a506d40c9e"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("4de124fb-a4a8-4dd1-942a-35a506d40c9e"), SequenceId = 1, Code = "FGE836GDE2", FirstName = "member001", LastName = "lastname", Email = "member001@mail.com", ICNumber = "900101-01-1001", PhoneNo = "60121001001", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("a7b230be-46c8-4a8c-89e0-af3877a738c5"), SequenceId = 2, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("ebeeb6b9-0af8-4d23-b193-0c38fd6bf590"), Salary = 5000, AppGrpInd = 0, Status = (int)ApplicationStatus.PROCESSING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("ebeeb6b9-0af8-4d23-b193-0c38fd6bf590"), SequenceId = 2, Type = (int)MemberType.REGULAR, UserId = new Guid("2103478c-79bf-43a1-ab68-88351e9f0e53"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("2103478c-79bf-43a1-ab68-88351e9f0e53"), SequenceId = 2, Code = "HEC378NDG5", FirstName = "member002", LastName = "lastname", Email = "member002@mail.com", ICNumber = "900101-01-1002", PhoneNo = "60121001002", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("26a315ee-e948-4eec-84cf-c1a56feeab30"), SequenceId = 3, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("78464a1c-9803-42e4-a442-d0fb27f6f1a2"), Salary = 3800, AppGrpInd = 0, Status = (int)ApplicationStatus.PROCESSING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("78464a1c-9803-42e4-a442-d0fb27f6f1a2"), SequenceId = 3, Type = (int)MemberType.REGULAR, UserId = new Guid("c90e12c5-27eb-4196-b3ab-83a43c831806"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("c90e12c5-27eb-4196-b3ab-83a43c831806"), SequenceId = 3, Code = "NSU820JMD7", FirstName = "member003", LastName = "lastname", Email = "member003@mail.com", ICNumber = "900101-01-1003", PhoneNo = "60121001003", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("9bbc3184-e7eb-49f2-8445-b1f7719aceed"), SequenceId = 4, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("e2e9d602-af3b-4f12-9beb-94346a54fd4c"), Salary = 5600, AppGrpInd = 0, Status = (int)ApplicationStatus.PRE_CHECKING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("e2e9d602-af3b-4f12-9beb-94346a54fd4c"), SequenceId = 4, Type = (int)MemberType.REGULAR, UserId = new Guid("673affc1-f707-458b-84bf-483f0fd49984"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("673affc1-f707-458b-84bf-483f0fd49984"), SequenceId = 4, Code = "NAU116GDY5", FirstName = "member004", LastName = "lastname", Email = "member004@mail.com", ICNumber = "900101-01-1004", PhoneNo = "60121001004", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("88c8c955-129c-4937-b68e-82a824d59434"), SequenceId = 5, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("a30b3ae9-0114-4b06-a066-7c05a937bb5b"), Salary = 2200, AppGrpInd = 0, Status = (int)ApplicationStatus.BURST, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("a30b3ae9-0114-4b06-a066-7c05a937bb5b"), SequenceId = 5, Type = (int)MemberType.REGULAR, UserId = new Guid("811f8116-d7f7-427b-b289-db4cfaf21310"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("811f8116-d7f7-427b-b289-db4cfaf21310"), SequenceId = 5, Code = "FGE788XXZ4", FirstName = "member005", LastName = "lastname", Email = "member005@mail.com", ICNumber = "900101-01-1005", PhoneNo = "60121001005", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("6c42b36a-6d4a-440f-9830-7d365a48bcb7"), SequenceId = 6, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("04f7f018-4f67-493f-97a7-6ff9696db7cf"), Salary = 1800, AppGrpInd = 0, Status = (int)ApplicationStatus.WIRA, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("04f7f018-4f67-493f-97a7-6ff9696db7cf"), SequenceId = 6, Type = (int)MemberType.REGULAR, UserId = new Guid("6982e580-79be-4b36-ab76-58999bbec130"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("6982e580-79be-4b36-ab76-58999bbec130"), SequenceId = 6, Code = "LPW931JUI8", FirstName = "member006", LastName = "lastname", Email = "member006@mail.com", ICNumber = "900101-01-1006", PhoneNo = "60121001006", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("b4254e74-e204-49db-9ea1-6a1d6386534b"), SequenceId = 7, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("f71e9700-83be-4c77-97fd-5bccf09c5645"), Salary = 2400, AppGrpInd = 0, Status = (int)ApplicationStatus.PROCESSING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("f71e9700-83be-4c77-97fd-5bccf09c5645"), SequenceId = 7, Type = (int)MemberType.REGULAR, UserId = new Guid("593d9dfe-4f4c-458e-836c-419d98989d82"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("593d9dfe-4f4c-458e-836c-419d98989d82"), SequenceId = 7, Code = "KRY042LOQ2", FirstName = "member007", LastName = "lastname", Email = "member007@mail.com", ICNumber = "900101-01-1007", PhoneNo = "60121001007", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("ab6bf140-1638-4f05-8d3c-09473a188b14"), SequenceId = 8, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("79e074aa-f753-4288-8ff6-a557d93db702"), Salary = 4600, AppGrpInd = 0, Status = (int)ApplicationStatus.BURST, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("79e074aa-f753-4288-8ff6-a557d93db702"), SequenceId = 8, Type = (int)MemberType.REGULAR, UserId = new Guid("4da65bf9-29ce-4143-8154-d54e783dc3ed"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("4da65bf9-29ce-4143-8154-d54e783dc3ed"), SequenceId = 8, Code = "UQB732MNV1", FirstName = "member008", LastName = "lastname", Email = "member008@mail.com", ICNumber = "900101-01-1008", PhoneNo = "60121001008", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("55028a64-7d51-42ba-8cd9-2aea5a6f785b"), SequenceId = 9, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("c9df6e52-8b6e-405f-8086-c9c25e574f73"), Salary = 6600, AppGrpInd = 0, Status = (int)ApplicationStatus.PROCESSING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("c9df6e52-8b6e-405f-8086-c9c25e574f73"), SequenceId = 9, Type = (int)MemberType.REGULAR, UserId = new Guid("a93dbc1c-2b84-4d4f-b508-937b62c2aa17"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("a93dbc1c-2b84-4d4f-b508-937b62c2aa17"), SequenceId = 9, Code = "ZKD956LD3", FirstName = "member009", LastName = "lastname", Email = "member009@mail.com", ICNumber = "900101-01-1009", PhoneNo = "60121001009", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("86fb3887-e9db-436e-b6ab-005b2c9366c3"), SequenceId = 10, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("d514027d-514a-4f18-9667-95dff91f066e"), Salary = 4600, AppGrpInd = 0, Status = (int)ApplicationStatus.WIRA, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("d514027d-514a-4f18-9667-95dff91f066e"), SequenceId = 10, Type = (int)MemberType.REGULAR, UserId = new Guid("ed445fea-1aae-4e4f-8393-7cfa6cf8824f"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("ed445fea-1aae-4e4f-8393-7cfa6cf8824f"), SequenceId = 10, Code = "LDI341SD4", FirstName = "member010", LastName = "lastname", Email = "member010@mail.com", ICNumber = "900101-01-1010", PhoneNo = "60121001010", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("01384108-823b-41d1-b687-eef9bcd27a23"), SequenceId = 11, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("f567e18c-7d03-4eb3-868a-f2e5283d563a"), Salary = 4800, AppGrpInd = 0, Status = (int)ApplicationStatus.PRE_CHECKING, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("f567e18c-7d03-4eb3-868a-f2e5283d563a"), SequenceId = 11, Type = (int)MemberType.REGULAR, UserId = new Guid("4056a74d-3bb8-4e35-bef2-8a3433c9b350"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("4056a74d-3bb8-4e35-bef2-8a3433c9b350"), SequenceId = 11, Code = "CHH396JJS2", FirstName = "member011", LastName = "lastname", Email = "member011@mail.com", ICNumber = "900101-01-1011", PhoneNo = "60121001011", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("fe40ff50-ddc0-48be-a666-70f60ba78d34"), SequenceId = 12, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("fa625aac-42bf-49d2-b3cb-b26fd0604c65"), Salary = 3600, AppGrpInd = 0, Status = (int)ApplicationStatus.BURST, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("fa625aac-42bf-49d2-b3cb-b26fd0604c65"), SequenceId = 12, Type = (int)MemberType.REGULAR, UserId = new Guid("016d488a-3c5c-4d9a-8912-7b54d8e77f44"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("016d488a-3c5c-4d9a-8912-7b54d8e77f44"), SequenceId = 12, Code = "RHS856UQI6", FirstName = "member012", LastName = "lastname", Email = "member012@mail.com", ICNumber = "900101-01-1012", PhoneNo = "60121001012", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } },
                       //new application { Id = new Guid("d18859f8-802c-4944-b098-054813de588f"), SequenceId = 13, Type = (int)ApplicationType.REGULAR, MemberId = new Guid("99bea395-a3f0-4a65-9369-fb981c277ce9"), Salary = 2900, AppGrpInd = 0, Status = (int)ApplicationStatus.WIRA, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), MemberData = new member { Id = new Guid("99bea395-a3f0-4a65-9369-fb981c277ce9"), SequenceId = 13, Type = (int)MemberType.REGULAR, UserId = new Guid("a76730f6-03c2-4ee9-9b9e-daada47792a4"), Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("a76730f6-03c2-4ee9-9b9e-daada47792a4"), SequenceId = 13, Code = "BMS456MVE9", FirstName = "member013", LastName = "lastname", Email = "member013@mail.com", ICNumber = "900101-01-1013", PhoneNo = "60121001013", AccountType = (int)AccountType.MEMBER, Status = (int)UserStatus.ACTIVE, RoleId = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), RoleData = new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Name = "Member" } } } }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { applications = applications.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "FullName":
                            applications = applications.AddOrdering(o => o.MemberData.UserData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.MemberData.UserData.LastName);
                            break;

                        case "UserCode":
                            applications = applications.AddOrdering(o => o.MemberData.UserData.Code, (SortDirection)SortDirection);
                            break;

                        case "PhoneNo":
                            applications = applications.AddOrdering(o => o.MemberData.UserData.PhoneNo, (SortDirection)SortDirection);
                            break;

                        case "Salary":
                            applications = applications.AddOrdering(o => o.Salary, (SortDirection)SortDirection);
                            break;

                        case "Status":
                            applications = applications.AddOrdering(o => o.Status, (SortDirection)SortDirection);
                            break;

                        default:
                            applications = applications.AddOrdering(o => o.MemberData.UserData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.MemberData.UserData.LastName);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = applications.Count();

                    //paging
                    applications = applications.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = applications.Select(s => new ApplicantStatusGVByMemberModel
                        {
                            DT_RowId = s.Id.ToString(),
                            MemberId = (s.MemberId == null) ? Guid.Empty : s.MemberData.Id,
                            UserCode = (s.MemberData == null || s.MemberData.UserData == null) ? "" : s.MemberData.UserData.Code,
                            FirstName = (s.MemberData == null || s.MemberData.UserData == null) ? "" : s.MemberData.UserData.FirstName,
                            LastName = (s.MemberData == null || s.MemberData.UserData == null) ? "" : s.MemberData.UserData.FirstName,
                            PhoneNo = (s.MemberData == null || s.MemberData.UserData == null) ? "" : s.MemberData.UserData.PhoneNo,
                            Salary = s.Salary,
                            Status = s.Status,
                            CreatedDate = s.CreatedDate
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<ApplicantStatusGVByMemberModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<ApplicantStatusGVByMemberModel>
                    {
                        data = list,
                        recordsTotal = totalItems,
                        recordsFiltered = totalItems,
                        PageIndex = PageIdx,
                        PageSize = PageSize,
                        SortExpression = SortExpression,
                        SortDirection = SortDirection
                    }
                };
            }
            catch (Exception ex)
            {
                retVal.Error = true;
                retVal.Code = (int)ErrorCode.UNKNOWN_ERROR;
                retVal.Message = ex.Message;
                retVal.ObjVal = new GridViewModel<ApplicantStatusGVByMemberModel>
                {
                    data = new List<ApplicantStatusGVByMemberModel>(),
                    recordsTotal = totalItems,
                    recordsFiltered = totalItems,
                    PageIndex = PageIdx,
                    PageSize = PageSize,
                    SortExpression = SortExpression,
                    SortDirection = SortDirection
                };
            }

            return retVal;
        }
    }
}
