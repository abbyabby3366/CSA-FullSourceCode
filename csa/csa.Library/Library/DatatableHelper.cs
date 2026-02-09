using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace csa.Library
{
    public class DatatableHelper
    {
        public static SortDirection ParseSortDirection(string sort)
        {
            return sort == "asc" ? SortDirection.Ascending : SortDirection.Descending;
        }
    }
}
