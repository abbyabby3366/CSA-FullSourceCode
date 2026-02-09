using System;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class BaseGVReqDTO : BaseReqDTO
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("sortExpression")]
        public string SortExpression { get; set; }

        [JsonProperty("sortDirection")]
        public int SortDirection { get; set; }
    }
}
