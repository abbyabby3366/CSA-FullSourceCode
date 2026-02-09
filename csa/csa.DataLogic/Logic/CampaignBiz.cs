using csa.Model;
using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csa.Library;

namespace csa.DataLogic
{
    public static class CampaignBiz
    {
        public static RespArgs<int> Create(RequestNewCampaign req)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var emailTemplate = EmailTemplateBiz.Get(req.EmailTemplateId);
                if (emailTemplate == null) throw new ArgumentException("email_template_not_found");

                var newObj = new Campaign
                {
                    Name = req.Name,
                    Subject = req.Subject,
                    EmailTemplateId = req.EmailTemplateId,
                    CreateDate = DateTime.Now,
                    ScheduledDate = req.ScheduledDate,
                    AudienceGroupId = req.AudienceGroupId.ToInt().IfZeroToNull(),
                    StatusId = req.StatusId
                };

                db.Campaigns.AddObject(newObj);
                db.SaveChangesAsync();

                return RespArgs<int>.CreateSuccess(newObj.CampaignId);
            }
        }

        public static RespArgs<bool> Update(RequestUpdateCampaign req)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var campaign = db.Campaigns.FirstOrDefault(x => x.CampaignId == req.CampaignId);
                if (campaign == null) throw new ArgumentException("campaign_not_found");

                var emailTemplate = EmailTemplateBiz.Get(req.EmailTemplateId);
                if (emailTemplate == null) throw new ArgumentException("email_template_not_found");

                campaign.Name = req.Name;
                campaign.Subject = req.Subject;
                campaign.EmailTemplateId = req.EmailTemplateId;
                campaign.ScheduledDate = req.ScheduledDate;
                campaign.AudienceGroupId = req.AudienceGroupId;
                campaign.StatusId = req.StatusId;

                db.SaveChangesAsync();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static RespArgs<bool> Delete(int id)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var campaign = db.Campaigns.FirstOrDefault(x => x.CampaignId == id);
                if (campaign == null) throw new ArgumentException("campaign_not_found");

                db.Campaigns.DeleteObject(campaign);
                db.SaveChanges();
                return RespArgs<bool>.CreateSuccess(true);
            }
        }

        public static Campaign Get(int campaignId)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Campaigns.FirstOrDefault(x => x.CampaignId == campaignId);
            }
        }

        public static RespArgs<GridViewModel<CampaignGV>> GetGV(string search, int pageIndex, int pageSize, string sortOrder, SQLSelect.OrderByEnum sortDirection)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var sqlSelect = new SQLSelect("campaign cam");
                sqlSelect.AddSelect("cam.CampaignId,cam.Name,cam.Subject,cam.StatusId");
                sqlSelect.SetOrderBY(sortOrder, sortDirection);
                sqlSelect.SetLimit((pageIndex * pageSize), pageSize);

                if (!search.IsEmpty()) sqlSelect.AddWhere($"(cam.Name LIKE '%{search}%' OR cam.Subject LIKE '%{search}%')");

                var list = db.ExecuteStoreQuery<CampaignGV>(sqlSelect.Result).ToList();
                var count = db.ExecuteStoreQuery<int>(sqlSelect.SQLCount()).FirstOrDefault();

                //add sequence
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].SequenceId = (pageIndex * pageSize) + i;
                }

                return RespArgs<GridViewModel<CampaignGV>>.CreateSuccess(new GridViewModel<CampaignGV>(list, count, count, pageIndex + 1, pageSize, null, 1));
            }
        }
    }
}
