using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

using csa.Data;
using csa.Data.Cache;
using csa.Data.EntityModel;
using csa.Data.Library;
using csa.Library;
using csa.Model;

namespace csa.Data.Logic
{
    public class MemberLogic
    {
        /// <summary>
        /// Get `new-member-approval` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;NewMemberApprovalGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<NewMemberApprovalGVByAdminModel>> GetWithdrawGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<member, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<NewMemberApprovalGVByAdminModel>> retVal = new RespArgs<GridViewModel<NewMemberApprovalGVByAdminModel>> { Error = true };

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

            //int[] allowSts = { (int)UserStatus.NON_ACTIVATED };

            IEnumerable<NewMemberApprovalGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `member`
                    //IQueryable<member> withdraws = db.members
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<member> members = new List<member>
                    {
                       //new member { Id = new Guid("ae093134-bb79-4b54-8159-340dbabccf97"), SequenceId = 1, Type = (int)MemberType.MEMBER, UserId = new Guid("e66343c0-7447-456a-83c8-cfd9ea731736"), CompanyName = "Company 001", Occupation = "Occupation 001", JoinDate = new DateTime(2024-01-01), ReferrerId = new Guid("486241c7-50a7-4b13-9ebf-0b2d49e09edc"), MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("e66343c0-7447-456a-83c8-cfd9ea731736"), SequenceId = 1, UserName = "member001", Code = "U918RNF", Email = "member001@mail.com", FirstName = "member001", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001001", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }, ReferrerData = new user { Id = new Guid("486241c7-50a7-4b13-9ebf-0b2d49e09edc"), SequenceId = 101, UserName = "referrer001", Code = "E953IBV", Email = "referrer001@mail.com", FirstName = "referrer001", LastName = "lastname", DateOfBirth = new DateTime(1990, 02, 12), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001101", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), AccountType = (int)AccountType.AGENT, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), SequenceId = 4, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 4, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                       //new member { Id = new Guid("7768fbc7-b076-4552-82d1-14121fd5fdb9"), SequenceId = 2, Type = (int)MemberType.MEMBER, UserId = new Guid("7fd5805d-f99c-4f44-960d-89ef4b9ab68e"), CompanyName = "Company 002", Occupation = "Occupation 002", JoinDate = new DateTime(2024-02-12), ReferrerId = new Guid("c1b47ef2-e14a-4152-bf77-2720c6beeec9"), MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("7fd5805d-f99c-4f44-960d-89ef4b9ab68e"), SequenceId = 2, UserName = "member002", Code = "B682CHP", Email = "member002@mail.com", FirstName = "member002", LastName = "lastname", DateOfBirth = new DateTime(1990, 11, 22), ICNumber = "900101-01-1002", Gender = (int)Gender.FEMALE, PhoneNo = "60121001002", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }, ReferrerData = new user { Id = new Guid("c1b47ef2-e14a-4152-bf77-2720c6beeec9"), SequenceId = 102, UserName = "referrer002", Code = "B954JWQ", Email = "referrer002@mail.com", FirstName = "referrer002", LastName = "lastname", DateOfBirth = new DateTime(1990, 09, 25), ICNumber = "900101-01-1002", Gender = (int)Gender.FEMALE, PhoneNo = "60121001102", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), AccountType = (int)AccountType.AGENT, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), SequenceId = 4, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 4, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                       //new member { Id = new Guid("f00c09cc-4abc-4aac-b629-24e78ee08b82"), SequenceId = 3, Type = (int)MemberType.MEMBER, UserId = new Guid("fa2a712c-e1ee-4366-b852-81ce7399a103"), CompanyName = "Company 003", Occupation = "Occupation 003", JoinDate = new DateTime(2024-03-23), ReferrerId = new Guid("d293aa8f-4229-449b-8528-b3e70d063f74"), MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("fa2a712c-e1ee-4366-b852-81ce7399a103"), SequenceId = 3, UserName = "member003", Code = "R915KWC", Email = "member003@mail.com", FirstName = "member003", LastName = "lastname", DateOfBirth = new DateTime(1990, 09, 17), ICNumber = "900101-02-1003", Gender = (int)Gender.FEMALE, PhoneNo = "60121001003", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }, ReferrerData = new user { Id = new Guid("d293aa8f-4229-449b-8528-b3e70d063f74"), SequenceId = 103, UserName = "referrer003", Code = "D452MCX", Email = "referrer003@mail.com", FirstName = "referrer003", LastName = "lastname", DateOfBirth = new DateTime(1990, 12, 31), ICNumber = "900101-01-1003", Gender = (int)Gender.FEMALE, PhoneNo = "60121001103", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), AccountType = (int)AccountType.AGENT, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), SequenceId = 4, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 4, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { members = members.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "FullName":
                            members = members.AddOrdering(o => o.UserData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.UserData.LastName);
                            break;

                        case "ICNumber":
                            members = members.AddOrdering(o => o.UserData.ICNumber, (SortDirection)SortDirection);
                            break;

                        case "Gender":
                            members = members.AddOrdering(o => o.UserData.Gender, (SortDirection)SortDirection);
                            break;

                        case "ReferrerName":
                            members = members.AddOrdering(o => o.ReferrerData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.ReferrerData.LastName);
                            break;

                        case "PhoneNo":
                            members = members.AddOrdering(o => o.UserData.PhoneNo, (SortDirection)SortDirection);
                            break;

                        case "CompanyName":
                            members = members.AddOrdering(o => o.CompanyName, (SortDirection)SortDirection);
                            break;

                        case "Occupation":
                            members = members.AddOrdering(o => o.Occupation, (SortDirection)SortDirection);
                            break;

                        case "CreatedDate":
                            members = members.AddOrdering(o => o.UserData.CreatedDate, (SortDirection)SortDirection);
                            break;

                        default:
                            members = members.AddOrdering(o => o.UserData.CreatedDate, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = members.Count();

                    //paging
                    members = members.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = members.Select(s => new NewMemberApprovalGVByAdminModel
                        {
                            DT_RowId = s.Id.ToString(),
                            FirstName = (s.UserData == null) ? "" : s.UserData.FirstName,
                            LastName = (s.UserData == null) ? "" : s.UserData.LastName,
                            ICNumber = (s.UserData == null) ? "" : s.UserData.ICNumber,
                            Gender = (s.UserData == null) ? (int)Gender.MALE : s.UserData.Gender,
                            PhoneNo = (s.UserData == null) ? "" : s.UserData.PhoneNo,
                            CompanyName = s.CompanyName,
                            Occupation = s.Occupation,
                            ReferrerFirstName = (s.ReferrerData == null) ? "" : s.ReferrerData.FirstName,
                            ReferrerLastName = (s.ReferrerData == null) ? "" : s.ReferrerData.LastName,
                            //Status = (s.UserData == null) ? (int)UserStatus.NON_ACTIVATED : s.UserData.Status,
                            CreatedDate = (s.UserData == null) ? new DateTime(1900, 1, 1) : s.UserData.CreatedDate,
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<NewMemberApprovalGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<NewMemberApprovalGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<NewMemberApprovalGVByAdminModel>
                {
                    data = new List<NewMemberApprovalGVByAdminModel>(),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    PageIndex = PageIdx,
                    PageSize = PageSize,
                    SortExpression = SortExpression,
                    SortDirection = SortDirection
                };
            }

            return retVal;
        }

        /// <summary>
        /// Get `all-members` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;MemberGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<MemberGVByAdminModel>> GetAllMemberGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<member, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<MemberGVByAdminModel>> retVal = new RespArgs<GridViewModel<MemberGVByAdminModel>> { Error = true };

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

            //int[] allowSts = { (int)UserStatus.NON_ACTIVATED };

            IEnumerable<MemberGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `member`
                    //IQueryable<member> withdraws = db.members
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<member> members = new List<member>
                    {
                       //new member { Id = new Guid("ae093134-bb79-4b54-8159-340dbabccf97"), SequenceId = 1, Type = (int)MemberType.MEMBER, UserId = new Guid("e66343c0-7447-456a-83c8-cfd9ea731736"), CompanyName = "Company 001", Occupation = "Occupation 001", JoinDate = new DateTime(2024-01-01), ReferrerId = new Guid("486241c7-50a7-4b13-9ebf-0b2d49e09edc"), MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("e66343c0-7447-456a-83c8-cfd9ea731736"), SequenceId = 1, UserName = "member001", Code = "U918RNF", Email = "member001@mail.com", FirstName = "member001", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001001", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }, ReferrerData = new user { Id = new Guid("486241c7-50a7-4b13-9ebf-0b2d49e09edc"), SequenceId = 101, UserName = "referrer001", Code = "E953IBV", Email = "referrer001@mail.com", FirstName = "referrer001", LastName = "lastname", DateOfBirth = new DateTime(1990, 02, 12), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001101", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), AccountType = (int)AccountType.AGENT, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), SequenceId = 4, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 4, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                       //new member { Id = new Guid("7768fbc7-b076-4552-82d1-14121fd5fdb9"), SequenceId = 2, Type = (int)MemberType.MEMBER, UserId = new Guid("7fd5805d-f99c-4f44-960d-89ef4b9ab68e"), CompanyName = "Company 002", Occupation = "Occupation 002", JoinDate = new DateTime(2024-02-12), ReferrerId = new Guid("c1b47ef2-e14a-4152-bf77-2720c6beeec9"), MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("7fd5805d-f99c-4f44-960d-89ef4b9ab68e"), SequenceId = 2, UserName = "member002", Code = "B682CHP", Email = "member002@mail.com", FirstName = "member002", LastName = "lastname", DateOfBirth = new DateTime(1990, 11, 22), ICNumber = "900101-01-1002", Gender = (int)Gender.FEMALE, PhoneNo = "60121001002", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }, ReferrerData = new user { Id = new Guid("c1b47ef2-e14a-4152-bf77-2720c6beeec9"), SequenceId = 102, UserName = "referrer002", Code = "B954JWQ", Email = "referrer002@mail.com", FirstName = "referrer002", LastName = "lastname", DateOfBirth = new DateTime(1990, 09, 25), ICNumber = "900101-01-1002", Gender = (int)Gender.FEMALE, PhoneNo = "60121001102", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), AccountType = (int)AccountType.AGENT, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), SequenceId = 4, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 4, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                       //new member { Id = new Guid("f00c09cc-4abc-4aac-b629-24e78ee08b82"), SequenceId = 3, Type = (int)MemberType.MEMBER, UserId = new Guid("fa2a712c-e1ee-4366-b852-81ce7399a103"), CompanyName = "Company 003", Occupation = "Occupation 003", JoinDate = new DateTime(2024-03-23), ReferrerId = new Guid("d293aa8f-4229-449b-8528-b3e70d063f74"), MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("fa2a712c-e1ee-4366-b852-81ce7399a103"), SequenceId = 3, UserName = "member003", Code = "R915KWC", Email = "member003@mail.com", FirstName = "member003", LastName = "lastname", DateOfBirth = new DateTime(1990, 09, 17), ICNumber = "900101-02-1003", Gender = (int)Gender.FEMALE, PhoneNo = "60121001003", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }, ReferrerData = new user { Id = new Guid("d293aa8f-4229-449b-8528-b3e70d063f74"), SequenceId = 103, UserName = "referrer003", Code = "D452MCX", Email = "referrer003@mail.com", FirstName = "referrer003", LastName = "lastname", DateOfBirth = new DateTime(1990, 12, 31), ICNumber = "900101-01-1003", Gender = (int)Gender.FEMALE, PhoneNo = "60121001103", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), AccountType = (int)AccountType.AGENT, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("35a0caf8-aee4-4121-ac37-badca61f1f74"), SequenceId = 4, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 4, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { members = members.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "FullName":
                            members = members.AddOrdering(o => o.UserData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.UserData.LastName);
                            break;

                        case "ICNumber":
                            members = members.AddOrdering(o => o.UserData.ICNumber, (SortDirection)SortDirection);
                            break;

                        case "Gender":
                            members = members.AddOrdering(o => o.UserData.Gender, (SortDirection)SortDirection);
                            break;

                        case "ReferrerName":
                            members = members.AddOrdering(o => o.ReferrerData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.ReferrerData.LastName);
                            break;

                        case "PhoneNo":
                            members = members.AddOrdering(o => o.UserData.PhoneNo, (SortDirection)SortDirection);
                            break;

                        case "CompanyName":
                            members = members.AddOrdering(o => o.CompanyName, (SortDirection)SortDirection);
                            break;

                        case "Occupation":
                            members = members.AddOrdering(o => o.Occupation, (SortDirection)SortDirection);
                            break;

                        case "CreatedDate":
                            members = members.AddOrdering(o => o.UserData.CreatedDate, (SortDirection)SortDirection);
                            break;

                        default:
                            members = members.AddOrdering(o => o.UserData.CreatedDate, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = members.Count();

                    //paging
                    members = members.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = members.Select(s => new MemberGVByAdminModel
                    {
                        DT_RowId = s.Id.ToString(),
                        FirstName = (s.UserData == null) ? "" : s.UserData.FirstName,
                        LastName = (s.UserData == null) ? "" : s.UserData.LastName,
                        ICNumber = (s.UserData == null) ? "" : s.UserData.ICNumber,
                        Gender = (s.UserData == null) ? (int)Gender.MALE : s.UserData.Gender,
                        PhoneNo = (s.UserData == null) ? "" : s.UserData.PhoneNo,
                        CompanyName = s.CompanyName,
                        Occupation = s.Occupation,
                        ReferrerFirstName = (s.ReferrerData == null) ? "" : s.ReferrerData.FirstName,
                        ReferrerLastName = (s.ReferrerData == null) ? "" : s.ReferrerData.LastName,
                        //Status = (s.UserData == null) ? (int)UserStatus.NON_ACTIVATED : s.UserData.Status,
                        CreatedDate = (s.UserData == null) ? new DateTime(1900, 1, 1) : s.UserData.CreatedDate,
                    })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<MemberGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<MemberGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<MemberGVByAdminModel>
                {
                    data = new List<MemberGVByAdminModel>(),
                    recordsTotal = 0,
                    recordsFiltered = 0,
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
