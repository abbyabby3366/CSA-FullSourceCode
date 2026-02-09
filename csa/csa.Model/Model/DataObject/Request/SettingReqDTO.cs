using System;

using Newtonsoft.Json;

namespace csa.Model.DataObject
{
    public class SettingReqDTO : BaseReqDTO
    {
        
    }

    //================================================================================================

    public class MaintenanceModeReqDTO : BaseReqDTO
    {
        [JsonProperty("isMaintenanceMode")]
        public string IsMaintenanceMode { get; set; }
    }
}
