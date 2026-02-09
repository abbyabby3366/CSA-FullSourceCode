using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class RequestNewRole
    {
        public string Name { get; set; }
        public int[] AccessIds { get; set; }
    }

    public class RequestUpdateRole
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int[] AccessIds { get; set; }
    }
}
