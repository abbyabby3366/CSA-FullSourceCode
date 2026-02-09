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
    public class WalletLogic
    {
        /// <summary>
        /// Get `withdraw-transaction` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;WithdrawGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<WithdrawGVByAdminModel>> GetWithdrawGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<withdraw, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<WithdrawGVByAdminModel>> retVal = new RespArgs<GridViewModel<WithdrawGVByAdminModel>> { Error = true };

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

            //int[] allowSts = { (int)WithdrawStatus.PENDING, (int)WithdrawStatus.APPROVED, (int)WithdrawStatus.REJECTED };

            IEnumerable<WithdrawGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `withdraw`
                    //IQueryable<withdraw> withdraws = db.withdraws
                    //    .Include("MemberData")
                    //    .Include("MemberData.UserData")
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<withdraw> withdraws = new List<withdraw>
                    {
                        //new withdraw { Id = Guid.NewGuid(), MemberId = new Guid("61361c71-9244-4ba1-af17-04896cbb2d8e"), BankName = "MAYBANK", BankAccHolder = "member001", BankAccNo = "1-11-1111-1001", TransRefNo = "TA1001001", Amount = 200.00, Status = (int)WithdrawStatus.PENDING, CreatedDate = DateTime.UtcNow, CreatedBy = Guid.NewGuid(), MemberData = new member { Id = new Guid("61361c71-9244-4ba1-af17-04896cbb2d8e"), SequenceId = 1, Type = (int)MemberType.MEMBER, UserId = new Guid("e66343c0-7447-456a-83c8-cfd9ea731736"), ReferrerId = Guid.NewGuid(), JoinDate = DateTime.UtcNow, MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("e66343c0-7447-456a-83c8-cfd9ea731736"), SequenceId = 1, UserName = "member001", Code = "U918RNF", Email = "member001@mail.com", FirstName = "member001", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001001", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } } },
                        //new withdraw { Id = Guid.NewGuid(), MemberId = new Guid("4133afdc-2ee7-4ba1-ba6c-deb3fa3f6f6f"), BankName = "CIMB", BankAccHolder = "member002", BankAccNo = "1-11-1111-1002", TransRefNo = "TA1001002", Amount = 370.00, Status = (int)WithdrawStatus.APPROVED, CreatedDate = DateTime.UtcNow, CreatedBy = Guid.NewGuid(), MemberData = new member { Id = new Guid("4133afdc-2ee7-4ba1-ba6c-deb3fa3f6f6f"), SequenceId = 2, Type = (int)MemberType.MEMBER, UserId = new Guid("7fd5805d-f99c-4f44-960d-89ef4b9ab68e"), ReferrerId = Guid.NewGuid(), JoinDate = DateTime.UtcNow, MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("7fd5805d-f99c-4f44-960d-89ef4b9ab68e"), SequenceId = 2, UserName = "member002", Code = "B682CHP", Email = "member002@mail.com", FirstName = "member002", LastName = "lastname", DateOfBirth = new DateTime(1990, 11, 22), ICNumber = "900101-01-1002", Gender = (int)Gender.FEMALE, PhoneNo = "60121001002", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } } },
                        //new withdraw { Id = Guid.NewGuid(), MemberId = new Guid("33e0a06d-760a-48f2-8c5d-9457a039ec00"), BankName = "RHB", BankAccHolder = "member003", BankAccNo = "1-11-1111-1003", TransRefNo = "TA1001003", Amount = 160.00, Status = (int)WithdrawStatus.REJECTED, CreatedDate = DateTime.UtcNow, CreatedBy = Guid.NewGuid(), MemberData = new member { Id = new Guid("4133afdc-2ee7-4ba1-ba6c-deb3fa3f6f6f"), SequenceId = 3, Type = (int)MemberType.MEMBER, UserId = new Guid("33e0a06d-760a-48f2-8c5d-9457a039ec00"), ReferrerId = Guid.NewGuid(), JoinDate = DateTime.UtcNow, MembGrpInd = 0, Status = (int)MemberStatus.ACTIVE, UserData = new user { Id = new Guid("fa2a712c-e1ee-4366-b852-81ce7399a103"), SequenceId = 3, UserName = "member003", Code = "R915KWC", Email = "member003@mail.com", FirstName = "member003", LastName = "lastname", DateOfBirth = new DateTime(1990, 09, 17), ICNumber = "900101-02-1003", Gender = (int)Gender.FEMALE, PhoneNo = "60121001003", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "MEMBER", Ind = 3, Name = "Member", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } } }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { withdraws = withdraws.Where(Predicate); }

                    switch (SortExpression)
                    {
                        //case "FullName":
                        //    withdraws = withdraws.AddOrdering(o => o.UserData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.UserData.LastName);
                        //    break;

                        //case "Email":
                        //    withdraws = withdraws.AddOrdering(o => o.UserData.Email, (SortDirection)SortDirection);
                        //    break;

                        //case "PhoneNo":
                        //    withdraws = withdraws.AddOrdering(o => o.UserData.PhoneNo, (SortDirection)SortDirection);
                        //    break;

                        //case "Status":
                        //    withdraws = withdraws.AddOrdering(o => o.Status, (SortDirection)SortDirection);
                        //    break;

                        case "BankAccNo":
                            withdraws = withdraws.AddOrdering(o => o.BankAccNo, (SortDirection)SortDirection);
                            break;

                        case "TransRefNo":
                            withdraws = withdraws.AddOrdering(o => o.TransRefNo, (SortDirection)SortDirection);
                            break;

                        case "Amount":
                            withdraws = withdraws.AddOrdering(o => o.Amount, (SortDirection)SortDirection);
                            break;

                        case "CreatedDate":
                            withdraws = withdraws.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                            break;

                        default:
                            withdraws = withdraws.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = withdraws.Count();

                    //paging
                    withdraws = withdraws.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = withdraws.Select(s => new WithdrawGVByAdminModel
                        {
                            DT_RowId = s.Id.ToString(),
                            MemberId = s.MemberId,
                            //Email = (s.UserData == null) ? string.Empty : s.UserData.Email,
                            //FirstName = (s.UserData == null) ? string.Empty : s.UserData.FirstName,
                            //LastName = (s.UserData == null) ? string.Empty : s.UserData.LastName,
                            //PhoneNo = (s.UserData == null) ? string.Empty : s.UserData.PhoneNo,
                            BankAccNo = s.BankAccNo,
                            Amount = s.Amount,
                            Status = s.Status,
                            CreatedDate = s.CreatedDate,
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<WithdrawGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<WithdrawGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<WithdrawGVByAdminModel>
                {
                    data = new List<WithdrawGVByAdminModel>(),
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
        /// Get `wallet-transaction` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;WalletTransGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<WalletTransGVByAdminModel>> GetWalletTransGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<WalletTransByAdminModel, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<WalletTransGVByAdminModel>> retVal = new RespArgs<GridViewModel<WalletTransGVByAdminModel>> { Error = true };

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

            //int[] allowSts = { (int)WithdrawStatus.PENDING, (int)WithdrawStatus.APPROVED, (int)WithdrawStatus.REJECTED };

            IEnumerable<WalletTransGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `wallet_trans`
                    //IQueryable<withdraw> walletTrans = db.withdraws
                    //    .Include("MemberData")
                    //    .Include("MemberData.UserData")
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<WalletTransByAdminModel> walletTrans = new List<WalletTransByAdminModel>
                    {
                        //new WalletTransByAdminModel { Id = Guid.NewGuid(), SequenceId = 1, TransTypeId = (int)WalletTransType.MEMB_WLT_WITHDRAW, Amount = 200.00, UserCode = "CTY836RDF", Status = (int)WithdrawStatus.APPROVED, CreatedDate = DateTime.UtcNow },
                        //new WalletTransByAdminModel { Id = Guid.NewGuid(), SequenceId = 2, TransTypeId = (int)WalletTransType.MEMB_WLT_WITHDRAW, Amount = 100.00, UserCode = "BBH285FQP", Status = (int)WithdrawStatus.REJECTED, CreatedDate = DateTime.UtcNow },
                        //new WalletTransByAdminModel { Id = Guid.NewGuid(), SequenceId = 3, TransTypeId = (int)WalletTransType.MEMB_WLT_WITHDRAW, Amount = 50.00, UserCode = "HSL793NCM", Status = (int)WithdrawStatus.APPROVED, CreatedDate = DateTime.UtcNow }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { walletTrans = walletTrans.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "SequenceId":
                            walletTrans = walletTrans.AddOrdering(o => o.SequenceId, (SortDirection)SortDirection);
                            break;

                        case "TransTypeId":
                            walletTrans = walletTrans.AddOrdering(o => o.TransTypeId, (SortDirection)SortDirection);
                            break;

                        case "UserCode":
                            walletTrans = walletTrans.AddOrdering(o => o.UserCode, (SortDirection)SortDirection);
                            break;

                        case "Amount":
                            walletTrans = walletTrans.AddOrdering(o => o.Amount, (SortDirection)SortDirection);
                            break;

                        case "Status":
                            walletTrans = walletTrans.AddOrdering(o => o.Status, (SortDirection)SortDirection);
                            break;

                        case "CreatedDate":
                            walletTrans = walletTrans.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                            break;

                        default:
                            walletTrans = walletTrans.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = walletTrans.Count();

                    //paging
                    walletTrans = walletTrans.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = walletTrans.Select(s => new WalletTransGVByAdminModel
                        {
                            DT_RowId = s.Id.ToString(),
                            TransNo = s.SequenceId,
                            TransTypeId = s.TransTypeId,
                            UserCode = s.UserCode,
                            Amount = s.Amount,
                            Status = s.Status,
                            CreatedDate = s.CreatedDate
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<WalletTransGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<WalletTransGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<WalletTransGVByAdminModel>
                {
                    data = new List<WalletTransGVByAdminModel>(),
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
