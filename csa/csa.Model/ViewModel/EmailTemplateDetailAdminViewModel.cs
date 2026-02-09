using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class EmailTemplateDetailAdminViewModel
    {
        public EmailTemplateDetailAdminViewModel(EmailTemplateDetails emailTemplate)
        {
            EmailTemplate = emailTemplate;
        }

        public EmailTemplateDetails EmailTemplate { get; set; }
    }
}
