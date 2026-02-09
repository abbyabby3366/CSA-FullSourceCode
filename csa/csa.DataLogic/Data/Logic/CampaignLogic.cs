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
    public class CampaignLogic
    {
        /// <summary>
        /// Get `campaign` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;CampaignGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<CampaignGVByAdminModel>> GetCampaignGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<campaign, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<CampaignGVByAdminModel>> retVal = new RespArgs<GridViewModel<CampaignGVByAdminModel>> { Error = true };

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

            IEnumerable<CampaignGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `campaign`
                    //IQueryable<campaign> withdraws = db.campaigns
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<campaign> users = new List<campaign>
                    {
                       new campaign { Id = new Guid("967b8bab-2a20-4c43-a5e2-eac57c42b410"), Name = "CAMPAIGN NAME 1", Subject = "SUBJECT NAME 1", OrderSeq = 1, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("ace9816e-03a8-4bbc-9de1-927ec6f46c34"), Name = "CAMPAIGN NAME 2", Subject = "SUBJECT NAME 2", OrderSeq = 2, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("c0006927-d825-4ead-a8ae-1209c4a4e35c"), Name = "CAMPAIGN NAME 3", Subject = "SUBJECT NAME 3", OrderSeq = 3, Status = (int)GlobalStatus.INACTIVE },
                       new campaign { Id = new Guid("1c2cb619-4e7a-4cab-a101-578a3021ac6a"), Name = "CAMPAIGN NAME 4", Subject = "SUBJECT NAME 4", OrderSeq = 4, Status = (int)GlobalStatus.INACTIVE },
                       new campaign { Id = new Guid("2bd685f9-b1f2-4146-bf14-da799373515f"), Name = "CAMPAIGN NAME 5", Subject = "SUBJECT NAME 5", OrderSeq = 5, Status = (int)GlobalStatus.INACTIVE },
                       new campaign { Id = new Guid("4ce78efd-6dd8-48c3-8b6e-1e05ec0106b2"), Name = "CAMPAIGN NAME 6", Subject = "SUBJECT NAME 6", OrderSeq = 6, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("5ba67192-5fbb-4398-a48c-07572eb39e83"), Name = "CAMPAIGN NAME 7", Subject = "SUBJECT NAME 7", OrderSeq = 7, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("6a198d98-f0e7-4493-9ad2-b2868e65ea53"), Name = "CAMPAIGN NAME 8", Subject = "SUBJECT NAME 8", OrderSeq = 8, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("87623205-a8b2-4182-a8e4-9e4b0446ed7d"), Name = "CAMPAIGN NAME 9", Subject = "SUBJECT NAME 9", OrderSeq = 9, Status = (int)GlobalStatus.INACTIVE },
                       new campaign { Id = new Guid("788a9987-7a53-43a8-b8f5-052b13884143"), Name = "CAMPAIGN NAME 10", Subject = "SUBJECT NAME 10", OrderSeq = 10, Status = (int)GlobalStatus.INACTIVE },
                       new campaign { Id = new Guid("94d47aa2-3723-4faa-bec8-b226560008dd"), Name = "CAMPAIGN NAME 11", Subject = "SUBJECT NAME 11", OrderSeq = 11, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("0bbb3e94-6885-4b11-afea-0e5862bbcd40"), Name = "CAMPAIGN NAME 12", Subject = "SUBJECT NAME 12", OrderSeq = 12, Status = (int)GlobalStatus.ACTIVE },
                       new campaign { Id = new Guid("bf4db9e0-8b4a-417b-bdba-e48e03bcdd4c"), Name = "CAMPAIGN NAME 13", Subject = "SUBJECT NAME 13", OrderSeq = 13, Status = (int)GlobalStatus.INACTIVE }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { users = users.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "CampaignName":
                            users = users.AddOrdering(o => o.Name, (SortDirection)SortDirection);
                            break;

                        case "Subject":
                            users = users.AddOrdering(o => o.Subject, (SortDirection)SortDirection);
                            break;

                        case "OrderSeq":
                            users = users.AddOrdering(o => o.OrderSeq, (SortDirection)SortDirection);
                            break;

                        case "Status":
                            users = users.AddOrdering(o => o.Status, (SortDirection)SortDirection);
                            break;

                        default:
                            users = users.AddOrdering(o => o.Name, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = users.Count();

                    //paging
                    users = users.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = users.Select(s => new CampaignGVByAdminModel
                        {
                            DT_RowId = s.Id.ToString(),
                            CampaignName = s.Name,
                            Subject = s.Subject,
                            OrderSeq = s.OrderSeq,
                            Status = s.Status
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<CampaignGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<CampaignGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<CampaignGVByAdminModel>
                {
                    data = new List<CampaignGVByAdminModel>(),
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
        /// Get `audience-tag` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;AudienceTagGVByAdminModel&gt;</returns>
        public static RespArgs<GridViewModel<AudienceTagGVByAdminModel>> GetAudienceTagGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<meta_tag, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<AudienceTagGVByAdminModel>> retVal = new RespArgs<GridViewModel<AudienceTagGVByAdminModel>> { Error = true };

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

            IEnumerable<AudienceTagGVByAdminModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `campaign`
                    //IQueryable<campaign> withdraws = db.campaigns
                    //    .Where(w => allowSts.Contains(w.Status));

                    IQueryable<meta_tag> users = new List<meta_tag>
                    {
                       new meta_tag { Id = new Guid("6c54d17b-4c6a-485a-9df4-5cd7072a3fb4"), Type = (int)MetaTagType.AUDIENCE, Name = "BRONZE", Remark = "BRONZE", TagGrpInd = 0, OrderSeq = 1, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("dc89e27d-9aa6-4e7a-bd6d-fdc90f25e4ef"), Type = (int)MetaTagType.AUDIENCE, Name = "SILVER", Remark = "SILVER", TagGrpInd = 0, OrderSeq = 2, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("55eea09e-cc95-438b-b8ce-31c71c76b777"), Type = (int)MetaTagType.AUDIENCE, Name = "GOLD", Remark = "GOLD", TagGrpInd = 0, OrderSeq = 3, Status = (int)GlobalStatus.INACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("fc6858a3-0bfc-46e3-a040-19ca82039521"), Type = (int)MetaTagType.AUDIENCE, Name = "PLATINUM", Remark = "PLATINUM", TagGrpInd = 0, OrderSeq = 4, Status = (int)GlobalStatus.INACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("860cb988-a546-45fa-8134-17c6001fb522"), Type = (int)MetaTagType.AUDIENCE, Name = "DIAMOND", Remark = "DIAMOND", TagGrpInd = 0, OrderSeq = 5, Status = (int)GlobalStatus.INACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("d65cb5c4-d7e6-4d21-a5b9-23cc72a76470"), Type = (int)MetaTagType.AUDIENCE, Name = "OPAL", Remark = "OPAL", TagGrpInd = 0, OrderSeq = 6, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("23198826-5c79-4ade-be11-a9150fd30b9d"), Type = (int)MetaTagType.AUDIENCE, Name = "EMARALD", Remark = "EMARALD", TagGrpInd = 0, OrderSeq = 7, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("01cafdb4-ab8d-4012-ba5e-09d7c689af86"), Type = (int)MetaTagType.AUDIENCE, Name = "SAPPHIRE", Remark = "SAPPHIRE", TagGrpInd = 0, OrderSeq = 8, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("485588c0-6a27-4b89-a85a-5c41c70ed2be"), Type = (int)MetaTagType.AUDIENCE, Name = "RUBY", Remark = "RUBY", TagGrpInd = 0, OrderSeq = 9, Status = (int)GlobalStatus.INACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("43df0f37-f7fc-476e-aa9c-595bd0a4a67c"), Type = (int)MetaTagType.AUDIENCE, Name = "PEARL", Remark = "PEARL", TagGrpInd = 0, OrderSeq = 10, Status = (int)GlobalStatus.INACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("5295ed91-8602-47f3-a610-d6fd3bf05356"), Type = (int)MetaTagType.AUDIENCE, Name = "AMBER", Remark = "AMBER", TagGrpInd = 0, OrderSeq = 11, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("a1c27dbc-0e7c-4910-bcb9-91153940edf2"), Type = (int)MetaTagType.AUDIENCE, Name = "AMETHYST", Remark = "AMETHYST", TagGrpInd = 0, OrderSeq = 12, Status = (int)GlobalStatus.ACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") },
                       new meta_tag { Id = new Guid("a03c9f85-5033-4d17-ac44-67786fe820ba"), Type = (int)MetaTagType.AUDIENCE, Name = "TOPAZ", Remark = "TOPAZ", TagGrpInd = 0, OrderSeq = 13, Status = (int)GlobalStatus.INACTIVE, CreatedDate = DateTime.UtcNow, CreatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58"), UpdatedDate = DateTime.UtcNow, UpdatedBy = new Guid("cd035519-ae28-4aa8-8832-1a358b0e2f58") }
                    }
                    .AsQueryable();

                    if (Predicate != null)
                    { users = users.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "TagName":
                            users = users.AddOrdering(o => o.Name, (SortDirection)SortDirection);
                            break;

                        case "OrderSeq":
                            users = users.AddOrdering(o => o.OrderSeq, (SortDirection)SortDirection);
                            break;

                        case "Status":
                            users = users.AddOrdering(o => o.Status, (SortDirection)SortDirection);
                            break;

                        case "CreatedDate":
                            users = users.AddOrdering(o => o.CreatedDate, (SortDirection)SortDirection);
                            break;

                        case "UpdatedDate":
                            users = users.AddOrdering(o => o.UpdatedDate, (SortDirection)SortDirection);
                            break;

                        default:
                            users = users.AddOrdering(o => o.Name, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = users.Count();

                    //paging
                    users = users.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //toList
                    list = users.Select(s => new AudienceTagGVByAdminModel
                        {
                            DT_RowId = s.Id.ToString(),
                            TagName = s.Name,
                            OrderSeq = s.OrderSeq,
                            Status = s.Status,
                            CreatedDate = s.CreatedDate,
                            UpdatedDate = s.UpdatedDate
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<AudienceTagGVByAdminModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<AudienceTagGVByAdminModel>
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
                retVal.ObjVal = new GridViewModel<AudienceTagGVByAdminModel>
                {
                    data = new List<AudienceTagGVByAdminModel>(),
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
