using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace csa.Admin.Models
{
    public class MiscModel : BaseModel
    {

    }

    //================================================================================================

    public class StateReqDTO : BaseModel
    {
        [JsonProperty("countryId")]
        public string CountryId { get; set; }
    }
}