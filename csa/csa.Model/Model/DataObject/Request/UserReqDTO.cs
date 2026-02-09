using System;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class UserReqDTO : BaseReqDTO
    {

    }

    public class UserRegistrationReqDTO : BaseReqDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("phoneNo")]
        public string PhoneNo { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class ResetPasswordReqDTO : BaseReqDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class ChangePasswordReqDTO : BaseReqDTO
    {
        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
