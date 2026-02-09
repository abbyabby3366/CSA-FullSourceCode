using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class MiscReqDTO
    {

    }

    //================================================================================================

    public class IdReqDTO : BaseReqDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    //================================================================================================

    public class FilterTextReqDTO : BaseReqDTO
    {
        [JsonProperty("filterText")]
        public string FilterText { get; set; }
    }

    //================================================================================================

    public class StateReqDTO : BaseReqDTO
    {
        [JsonProperty("countryId")]
        public string CountryId { get; set; }
    }

    //================================================================================================

    public class EmailMsgReqDTO : BaseReqDTO
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    //================================================================================================

    public class FirebaseMsgReqDTO : BaseReqDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}