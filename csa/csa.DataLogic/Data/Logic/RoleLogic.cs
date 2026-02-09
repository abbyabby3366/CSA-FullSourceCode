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
    public class RoleLogic
    {
        /// <summary>
        /// Get `all-admin-role` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;AdminRoleGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<AdminRoleGVByAdminModel>> GetRoleGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<role, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<AdminRoleGVByAdminModel>> retVal = new RespArgs<GridViewModel<AdminRoleGVByAdminModel>> { Error = true };

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

            IEnumerable<AdminRoleGVByAdminModel> list = null;
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

                IQueryable<role> roles = new List<role>
                {
                    new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Ind = 1, Code = "SUPERADMIN", Name = "Super Administrator", Description = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") },
                    new role { Id = new Guid("1986746d-b443-4eed-a676-0b4951c56dec"), SequenceId = 2, Type = (int)RoleType.SYSTEM, Ind = 2, Code = "ADMIN", Name = "Administrator", Description = "Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") },
                    new role { Id = new Guid("276d6e72-26b3-4139-831d-20cb3c3af995"), SequenceId = 3, Type = (int)RoleType.SYSTEM, Ind = 3, Code = "CUSTOMER", Name = "Customer", Description = "Customer", Status = (int)GlobalStatus.ACTIVE, CreatedDate = new DateTime(2024, 01, 23), CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = new DateTime(2024, 01, 23), UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") },
                }
                .AsQueryable();

                if (Predicate != null)
                { roles = roles.Where(Predicate); }

                switch (SortExpression)
                {
                    default:
                        roles = roles.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                        break;
                }

                totalItems = roles.Count();
                roles = roles.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                list = list = roles.Select(s => new AdminRoleGVByAdminModel
                    {
                        DT_RowId = $"{s.Id}",
                        RoleName = s.Name,
                        Status = s.Status
                    })
                    .ToList();

                //assign return object
                retVal = new RespArgs<GridViewModel<AdminRoleGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<AdminRoleGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<AdminRoleGVByAdminModel>
                {
                    data = new List<AdminRoleGVByAdminModel>(),
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

        /// <summary>
        /// Get `all-admin-role-users` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;AdminRoleUsersGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<AdminRoleUsersGVByAdminModel>> GetAllAdminRoleUsersGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<user, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<AdminRoleUsersGVByAdminModel>> retVal = new RespArgs<GridViewModel<AdminRoleUsersGVByAdminModel>> { Error = true };

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

            IEnumerable<AdminRoleUsersGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `user`
                    //IQueryable<user> withdraws = db.users
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<user> users = new List<user>
                    {
                       //new user { Id = new Guid("1029c7ae-c4e1-4116-9923-503673278aad"), SequenceId = 1, UserName = "admin001", Code = "CTYD853", Email = "admin001@mail.com", FirstName = "admin001", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001001", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = ((int)AccountType.SYSTEM | (int)AccountType.ADMIN), UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "SUPERADMIN", Ind = 1, Name = "Super Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                       //new user { Id = new Guid("4ad2ebab-a5af-4f03-b8ab-fe14f36a87a4"), SequenceId = 2, UserName = "admin002", Code = "F935JWL", Email = "admin002@mail.com", FirstName = "admin002", LastName = "lastname", DateOfBirth = new DateTime(1990, 11, 22), ICNumber = "900101-01-1002", Gender = (int)Gender.FEMALE, PhoneNo = "60121001002", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("1986746d-b443-4eed-a676-0b4951c56dec"), AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("1986746d-b443-4eed-a676-0b4951c56dec"), SequenceId = 2, Type = (int)RoleType.SYSTEM, Code = "ADMIN", Ind = 2, Name = "Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } },
                       //new user { Id = new Guid("aff6912c-3e14-4632-8bab-2cc0f9e0c287"), SequenceId = 3, UserName = "admin003", Code = "O925DDE", Email = "admin003@mail.com", FirstName = "admin003", LastName = "lastname", DateOfBirth = new DateTime(1990, 09, 17), ICNumber = "900101-02-1003", Gender = (int)Gender.FEMALE, PhoneNo = "60121001003", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("1986746d-b443-4eed-a676-0b4951c56dec"), AccountType = (int)AccountType.ADMIN, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("1986746d-b443-4eed-a676-0b4951c56dec"), SequenceId = 2, Type = (int)RoleType.SYSTEM, Code = "ADMIN", Ind = 2, Name = "Administrator", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { users = users.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "FullName":
                            users = users.AddOrdering(o => o.FirstName, (SortDirection)SortDirection).ThenBy(o => o.LastName);
                            break;

                        case "ICNumber":
                            users = users.AddOrdering(o => o.ICNumber, (SortDirection)SortDirection);
                            break;

                        case "PhoneNo":
                            users = users.AddOrdering(o => o.PhoneNo, (SortDirection)SortDirection);
                            break;

                        case "AccountType":
                            users = users.AddOrdering(o => o.AccountType, (SortDirection)SortDirection);
                            break;

                        case "RoleName":
                            users = users.AddOrdering(o => o.RoleData.Name, (SortDirection)SortDirection);
                            break;

                        default:
                            users = users.AddOrdering(o => o.FirstName, (SortDirection)SortDirection).ThenBy(o => o.LastName);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = users.Count();

                    //paging
                    users = users.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = users.Select(s => new AdminRoleUsersGVByAdminModel
                        {
                            DT_RowId = s.Id.ToString(),
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            ICNumber = s.ICNumber,
                            PhoneNo = s.PhoneNo,
                            AccountType = s.AccountType,
                            RoleId = s.RoleId,
                            RoleName = (s.RoleData == null) ? "" : s.RoleData.Name
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<AdminRoleUsersGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<AdminRoleUsersGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<AdminRoleUsersGVByAdminModel>
                {
                    data = new List<AdminRoleUsersGVByAdminModel>(),
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
