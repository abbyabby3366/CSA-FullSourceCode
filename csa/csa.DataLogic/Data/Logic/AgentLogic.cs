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
    public class AgentLogic
    {
        /// <summary>
        /// Get `referral-agent` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;AgentGVByMemberModel&gt;</returns>
        public static RespArgs<GridViewModel<AgentGVByMemberModel>> GetAgentGVByMember(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<agent, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<AgentGVByMemberModel>> retVal = new RespArgs<GridViewModel<AgentGVByMemberModel>> { Error = true };

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

            IEnumerable<AgentGVByMemberModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `agent`
                    //IQueryable<withdraw> agents = db.agents
                    //    .Include("MemberData")
                    //    .Include("MemberData.UserData")
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<agent> withdraws = new List<agent>
                    {
                        //new agent { Id = new Guid("61361c71-9244-4ba1-af17-04896cbb2d8e"), SequenceId = 1, Type = (int)AgentType.TYPE_A, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.FINANCIAL_SURVEY, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 1, UserName = "agent001", Code = "EUR394PTE", Email = "agent001@mail.com", FirstName = "agent001", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1001", Gender = (int)Gender.MALE, PhoneNo = "60121001001", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                        //new agent { Id = new Guid("abd5ac8e-d49d-4152-a6ac-f2cb3dde409e"), SequenceId = 2, Type = (int)AgentType.TYPE_B, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.ATTENDED_A_GROUP_WEBSINAR, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 2, UserName = "agent002", Code = "FID024GJD", Email = "agent002@mail.com", FirstName = "agent002", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1002", Gender = (int)Gender.MALE, PhoneNo = "60121001002", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                        //new agent { Id = new Guid("0d2a80a1-0057-48a4-9b88-19198c9f6bac"), SequenceId = 3, Type = (int)AgentType.TYPE_C, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.PFC_1_ON_1_ZOOM, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 3, UserName = "agent003", Code = "IFN952SPS", Email = "agent003@mail.com", FirstName = "agent003", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1003", Gender = (int)Gender.MALE, PhoneNo = "60121001003", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                        //new agent { Id = new Guid("5dc602aa-b79a-4029-9ba2-7de4c5b5f3d1"), SequenceId = 4, Type = (int)AgentType.TYPE_D, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.RNR_APP_STARTED_OR_COMPLETED, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 4, UserName = "agent004", Code = "QP2053BBC", Email = "agent004@mail.com", FirstName = "agent004", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1004", Gender = (int)Gender.MALE, PhoneNo = "60121001004", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                        //new agent { Id = new Guid("61361c71-9244-4ba1-af17-04896cbb2d8e"), SequenceId = 5, Type = (int)AgentType.TYPE_A, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.MISSION_PARTNER, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 5, UserName = "agent005", Code = "SJQ003NBW", Email = "agent005@mail.com", FirstName = "agent005", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1005", Gender = (int)Gender.MALE, PhoneNo = "60121001005", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                        //new agent { Id = new Guid("71597650-ddbc-4634-9c53-fd661b9bf8e7"), SequenceId = 6, Type = (int)AgentType.TYPE_B, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.RNR_APP_STARTED_OR_COMPLETED, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 6, UserName = "agent006", Code = "PLF139CBZ", Email = "agent006@mail.com", FirstName = "agent006", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1006", Gender = (int)Gender.MALE, PhoneNo = "60121001006", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } },
                        //new agent { Id = new Guid("dd55bd66-02c4-4e8e-aa7e-183f8c5c7688"), SequenceId = 7, Type = (int)AgentType.TYPE_A, UserId = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), AgentAccNo = "940128-10-5945", Remark = "LOREN IPSUM", WalletAmount = 4000, LastWalletTransId = new Guid("1bb0fec2-d90a-4835-acdc-99f000927b75"), AgentGrpInd = 0, Status = (int)AgentStatus.RNR_APP_STARTED_OR_COMPLETED, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UserData = new user { Id = new Guid("72492dc7-5d16-4ddc-9e01-052745569f5b"), SequenceId = 7, UserName = "agent007", Code = "YQM947GDF", Email = "agent007@mail.com", FirstName = "agent007", LastName = "lastname", DateOfBirth = new DateTime(1990, 01, 01), ICNumber = "900101-01-1007", Gender = (int)Gender.MALE, PhoneNo = "60121001007", TimeZone = "Singapore Standard Time", Status = (int)UserStatus.ACTIVE, RoleId = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), AccountType = (int)AccountType.MEMBER, UsrGrpInd = 0, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), RoleData = new role { Id = new Guid("0835974e-ddbc-4cf9-b890-ef7b073437ce"), SequenceId = 1, Type = (int)RoleType.SYSTEM, Code = "AGENT", Ind = 3, Name = "Agent", Status = (int)GlobalStatus.ACTIVE, CreatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4") } } }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { withdraws = withdraws.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "Type":
                            withdraws = withdraws.AddOrdering(o => o.Type, (SortDirection)SortDirection);
                            break;

                        case "FullName":
                            withdraws = withdraws.AddOrdering(o => o.UserData.FirstName, (SortDirection)SortDirection).ThenBy(o => o.UserData.LastName);
                            break;

                        case "AgentAccNo":
                            withdraws = withdraws.AddOrdering(o => o.AgentAccNo, (SortDirection)SortDirection);
                            break;

                        case "Remark":
                            withdraws = withdraws.AddOrdering(o => o.Remark, (SortDirection)SortDirection);
                            break;

                        case "Amount":
                            withdraws = withdraws.AddOrdering(o => o.WalletAmount, (SortDirection)SortDirection);
                            break;

                        case "Status":
                            withdraws = withdraws.AddOrdering(o => o.Status, (SortDirection)SortDirection);
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
                    list = withdraws.Select(s => new AgentGVByMemberModel
                        {
                            DT_RowId = s.Id.ToString(),
                            Type = s.Type,
                            AgentAccNo = s.AgentAccNo,
                            FirstName = (s.UserData == null) ? "" : s.UserData.FirstName,
                            LastName = (s.UserData == null) ? "" : s.UserData.LastName,
                            Remark = s.Remark,
                            WalletAmount = s.WalletAmount,
                            Status = s.Status,
                            CreatedDate = s.CreatedDate,
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<AgentGVByMemberModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<AgentGVByMemberModel>
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
                retVal.ObjVal = new GridViewModel<AgentGVByMemberModel>
                {
                    data = new List<AgentGVByMemberModel>(),
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
