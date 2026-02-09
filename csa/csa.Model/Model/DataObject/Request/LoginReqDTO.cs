using System;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class LoginReqDTO : BaseReqDTO
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    //================================================================================================

    public class FirebaseTokenReqDTO : BaseReqDTO
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
