using System;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class BaseRespDTO
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public BaseRespDTO(bool Error, int Code, string Message)
        {
            this.Error = Error;
            this.Code = Code;
            this.Message = Message ?? string.Empty;
        }
    }
}
