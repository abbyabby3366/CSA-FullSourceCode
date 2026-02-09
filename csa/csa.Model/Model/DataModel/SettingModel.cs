using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model
{
    public class SettingModel
    {

    }

    //================================================================================================//================================================================================================

    public class GeneralSettingByAdminModel
    {
        public bool MaintWebMode { get; set; }

        public string MaintWebMsg { get; set; }

        public string AdminEmail { get; set; }
    }

    //------------------------------------------------------------------------------------------------

    public class AddEditGeneralSettingBaseModel
    {
        public bool MaintWebMode { get; set; }

        public string MaintWebMsg { get; set; }

        public string AdminEmail { get; set; }
    }

    public class EditGeneralSettingModel : AddEditGeneralSettingBaseModel
    {

    }

    //------------------------------------------------------------------------------------------------

    public class GeneralSettingByMemCacheModel
    {
        public bool MaintWebMode { get; set; }

        public string MaintWebMsg { get; set; }
    }
}
