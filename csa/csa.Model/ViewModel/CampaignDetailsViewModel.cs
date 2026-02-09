using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class CampaignDetailsViewModel
    {
        public CampaignDetailsViewModel(CampaignDetails campaign, List<DropdownItem> emailTemplates, List<DropdownItem> audienceGroups)
        {
            Campaign = campaign;
            EmailTemplates = emailTemplates;
            AudienceGroups = audienceGroups;
        }

        public CampaignDetails Campaign { get; set; }
        public List<DropdownItem> EmailTemplates { get; set; }
        public List<DropdownItem> AudienceGroups { get; set; }
    }
}
