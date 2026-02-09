using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class TagGV
    {
        public int TagId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Label { get; set; }
        public int? StatusID { get; set; }
        public int SequenceId { get; set; }
    }

    public class RequestNewTag
    {
        public string Label { get; set; }
    }

    public class RequestUpdateTag
    {
        public int TagId { get; set; }
        public string Label { get; set; }
        public int StatusId { get; set; }
    }

    public class RequestDeleteTag
    {
        public int TagId { get; set; }
        public int AdminId { get; set; }
    }

    public class TagDetails
    {
        public TagDetails(int tagId, string label, int? statusId)
        {
            TagId = tagId;
            Label = label;
            StatusId = statusId;
        }

        public int TagId { get; set; }
        public string Label { get; set; }
        public int? StatusId { get; set; }
    }
}
