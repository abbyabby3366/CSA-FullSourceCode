using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class CampaignNewViewModel
    {
        public CampaignNewViewModel(List<DropdownItem> emailTemplates, List<DropdownItem> audienceGroups)
        {
            EmailTemplates = emailTemplates;
            AudienceGroups = audienceGroups;
        }

        public List<DropdownItem> EmailTemplates { get; set; }
        public List<DropdownItem> AudienceGroups { get; set; }
    }
}
