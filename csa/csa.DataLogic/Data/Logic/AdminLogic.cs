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
    public class AdminLogic : BaseLogic
    {
        /// <summary>
        /// Get `administrator` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;AdminGVModel&gt;</returns>
        public static RespArgs<GridViewModel<AdminGVModel>> GetAdminGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<user, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<AdminGVModel>> retVal = new RespArgs<GridViewModel<AdminGVModel>> { Error = true };

            if (ASid == null)
            {
                retVal.Error = true;
                retVal.Code = (int)ErrorCode.SESSION_TIMEOUT;
                retVal.Message = "Session timeout";

                return retVal;
            }

            AdminSessionCacheModel session = SessionCache.GetSessionCacheByKey<AdminSessionCacheModel>(ASid.Value);

            if (session == null)
            {
                retVal.Error = true;
                retVal.Code = (int)ErrorCode.SESSION_TIMEOUT;
                retVal.Message = "Session timeout";

                return retVal;
            }

            //validation

            if (PageIdx < 1) { PageIdx = 1; }
            if (PageSize < 10) { PageSize = 10; }
            if (PageSize > 500) { PageSize = 500; }

            int[] allowSts = { (int)GlobalStatus.ACTIVE, (int)GlobalStatus.INACTIVE };

            IEnumerable<AdminGVModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                //using (DataContext db = new DataContext())
                //{
                //    //get `member`
                //    IQueryable<member> customers = db.customers
                //        .Include("CustomerData")
                //        .Include("CustomerData.UserData")
                //        .Include("SOData")
                //        .Where(w => allowSts.Contains(w.Status) && w.Id == MemberId);

                //    if (Predicate != null)
                //    { customers = customers.Where(Predicate); }

                //    switch (SortExpression)
                //    {
                //        case "Customer":
                //            customers = customers.AddOrdering(o => o.CustomerData.UserData.FirstName, SortDirection);
                //            break;

                //        case "Rating":
                //            customers = customers.AddOrdering(o => o.Rating, SortDirection);
                //            break;

                //        default:
                //            customers = customers.AddOrdering(o => o.CreatedDate, SortDirection);
                //            break;
                //    }

                //    //get `TotalItems`
                //    totalItems = customers.Count();

                //    //paging
                //    customers = customers.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                //    //toList
                //    list = customers.Select(s => new CustomerGVModel
                //        {
                //            RatingId = s.Id,
                //            Rating = s.Rating,
                //            Member = (s.MemberData == null || s.MemberData.UserData == null) ? null : new IdValueModel
                //            { Id = s.MemberData.Id, Value = s.MemberData.UserData.FirstName },
                //            SellerOrder = (s.SOData == null) ? null : new IdValueModel
                //            { Id = s.SOData.Id, Value = s.SOData.SequenceId.ToString() },
                //            Comment = (s.Comment.Length < 20) ? s.Comment : s.Comment.Substring(0, 18) + "...",
                //            Status = s.Status,
                //            CreatedDate = s.CreatedDate
                //        })
                //        .ToList();
                //}

                IQueryable<user> users = new List<user>
                {
                    //new user { Id = new Guid("04f3b42e-db0c-42e9-8663-c72d1bf216ce"), SequenceId = 1, UserName = "admin001", Email = "admin001@mail.com", FirstName = "admin001", LastName = "lastname", ICNumber = "900101141001", PassportNo = "A1001001", DateOfBirth = new DateTime(1990, 01, 12), Gender = (int)Gender.MALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001001", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("1326d4a1-2b42-40bb-939e-4d4eb2f91970"), SequenceId = 2, UserName = "admin002", Email = "admin002@mail.com", FirstName = "admin002", LastName = "lastname", ICNumber = "900101141002", PassportNo = "A1001002", DateOfBirth = new DateTime(1992, 02, 09), Gender = (int)Gender.MALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001002", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("2a4d14b3-6379-4f8d-b17e-2a37a61ba598"), SequenceId = 3, UserName = "admin003", Email = "admin003@mail.com", FirstName = "admin003", LastName = "lastname", ICNumber = "900101141003", PassportNo = "A1001003", DateOfBirth = new DateTime(1998, 04, 04), Gender = (int)Gender.MALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001003", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("35dd66cf-005c-44fc-8f52-f9b145f25597"), SequenceId = 4, UserName = "admin004", Email = "admin004@mail.com", FirstName = "admin004", LastName = "lastname", ICNumber = "900101141004", PassportNo = "A1001004", DateOfBirth = new DateTime(2000, 05, 14), Gender = (int)Gender.MALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001004", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("40dc133d-2f49-4427-9323-30a21fcc0410"), SequenceId = 5, UserName = "admin005", Email = "admin005@mail.com", FirstName = "admin005", LastName = "lastname", ICNumber = "900101141005", PassportNo = "A1001005", DateOfBirth = new DateTime(2000, 08, 27), Gender = (int)Gender.MALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001005", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("5304a0d0-30c6-490c-9a15-6903321b6c41"), SequenceId = 6, UserName = "admin006", Email = "admin006@mail.com", FirstName = "admin006", LastName = "lastname", ICNumber = "900101141006", PassportNo = "A1001006", DateOfBirth = new DateTime(2000, 08, 31), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001006", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("6f448ba2-528f-4139-9a8c-2d90a955e6cd"), SequenceId = 7, UserName = "admin007", Email = "admin007@mail.com", FirstName = "admin007", LastName = "lastname", ICNumber = "900101141007", PassportNo = "A1001007", DateOfBirth = new DateTime(2001, 10, 17), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001007", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("710acbaf-f7bf-415d-a98d-c504276344f3"), SequenceId = 8, UserName = "admin008", Email = "admin008@mail.com", FirstName = "admin008", LastName = "lastname", ICNumber = "900101141008", PassportNo = "A1001008", DateOfBirth = new DateTime(2001, 12, 28), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001008", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("824beab6-9f1b-4074-b2ed-37078cdffec9"), SequenceId = 9, UserName = "admin009", Email = "admin009@mail.com", FirstName = "admin009", LastName = "lastname", ICNumber = "900101141009", PassportNo = "A1001009", DateOfBirth = new DateTime(2002, 07, 03), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001009", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("973201b3-a48c-40f0-97a0-47bb685ebc23"), SequenceId = 10, UserName = "admin010", Email = "admin010@mail.com", FirstName = "admin010", LastName = "lastname", ICNumber = "900101141010", PassportNo = "A1001010", DateOfBirth = new DateTime(2002, 07, 03), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001010", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("a144ce6a-b600-4eb2-ace2-9958687753fc"), SequenceId = 11, UserName = "admin011", Email = "admin011@mail.com", FirstName = "admin011", LastName = "lastname", ICNumber = "900101141011", PassportNo = "A1001011", DateOfBirth = new DateTime(2002, 07, 03), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001011", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                    //new user { Id = new Guid("b7058136-bd51-4700-94fd-7ecf519fb754"), SequenceId = 12, UserName = "admin012", Email = "admin012@mail.com", FirstName = "admin012", LastName = "lastname", ICNumber = "900101141012", PassportNo = "A1001012", DateOfBirth = new DateTime(2002, 07, 03), Gender = (int)Gender.FEMALE, Nationality = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"), PhoneNo = "60121001012", TimeZone = "Singapore Standard Time", AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, ImageId = null, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), Status = (int)UserStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }
                }
                .AsQueryable();

                if (Predicate != null)
                { users = users.Where(Predicate); }

                switch (SortExpression)
                {
                    default:
                        users = users.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                        break;
                }

                totalItems = users.Count();
                users = users.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                list = list = users.Select(s => new AdminGVModel
                    {
                        DT_RowId = $"{s.Id}",
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Email = s.Email,
                        Role = (s.RoleData == null) ? string.Empty : s.RoleData.Name,
                        Status = s.Status,
                        CreatedDate = s.CreatedDate
                    })
                    .ToList();

                //assign return object
                retVal = new RespArgs<GridViewModel<AdminGVModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<AdminGVModel>
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
                retVal.ObjVal = new GridViewModel<AdminGVModel>
                {
                    data = new List<AdminGVModel>(),
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
