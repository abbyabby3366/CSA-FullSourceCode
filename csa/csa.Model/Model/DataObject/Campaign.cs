using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class RequestNewCampaign
    {
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int EmailTemplateId { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? AudienceGroupId { get; set; }
        public int[] TagIds { get; set; }
    }

    public class RequestUpdateCampaign
    {
        public int CampaignId { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int EmailTemplateId { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? AudienceGroupId { get; set; }
        public int[] TagIds { get; set; }
    }

    public class CampaignDetails
    {
        public CampaignDetails(int campaignId, int? statusId, string name, string subject, int emailTemplateId, DateTime? scheduledDate, int? audienceGroupId, int[] tagIds)
        {
            CampaignId = campaignId;
            StatusId = statusId;
            Name = name;
            Subject = subject;
            EmailTemplateId = emailTemplateId;
            ScheduledDate = scheduledDate;
            AudienceGroupId = audienceGroupId;
            TagIds = tagIds;
        }

        public int CampaignId { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int EmailTemplateId { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? AudienceGroupId { get; set; }
        public int[] TagIds { get; set; }
    }

    public class CampaignGV
    {
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int? StatusId { get; set; }
        public int SequenceId { get; set; }
    }
}
