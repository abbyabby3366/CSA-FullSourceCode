using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class AudienceTagDetailAdminViewModel
    {
        public AudienceTagDetailAdminViewModel(TagDetails tag)
        {
            Tag = tag;
        }

        public TagDetails Tag { get; set; }
    }
}
