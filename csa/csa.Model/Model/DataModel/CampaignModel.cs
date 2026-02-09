using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model
{
    public class CampaignModel
    {
        
    }

    //================================================================================================

    public class CampaignGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public string CampaignName { get; set; }

        public string Subject { get; set; }

        public int OrderSeq { get; set; }

        public int Status { get; set; }
    }

    //================================================================================================

    public class AudienceTagGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public string TagName { get; set; }

        public int OrderSeq { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
