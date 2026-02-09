using System;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class BaseReqDTO
    {
        [JsonProperty("sid")]
        public string SId { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
