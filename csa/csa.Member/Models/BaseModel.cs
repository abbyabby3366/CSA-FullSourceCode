using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace csa.Member.Models
{
    public class BaseModel
    {
        [JsonProperty("usid")]
        public string USId { get; set; }
    }
}