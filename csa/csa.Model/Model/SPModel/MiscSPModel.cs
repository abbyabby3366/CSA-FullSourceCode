using System;

namespace csa.Model.SPModel
{
    public class MiscSPModel
    { }

    public class SP_default_Result : SP_Base_Result
    {

    }

    public class SP_add_user_Result : SP_Base_Result
    {
        public Guid UserId { get; set; }
    }
    
    public class SP_add_user_registration_Result : SP_Base_Result
    {
        public Guid UserId { get; set; }
    }

    public class SP_add_admin_Result : SP_Base_Result
    {
        public Guid UserId { get; set; }
    }
}