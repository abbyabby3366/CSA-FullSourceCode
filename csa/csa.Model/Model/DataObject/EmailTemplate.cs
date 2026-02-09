using csa.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class RequestNewEmailTemplate
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int TemplateTypeId { get; set; }
        public string Content { get; set; }
    }

    public class RequestUpdateEmailTemplate
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int TemplateTypeId { get; set; }
        public string Content { get; set; }
    }

    public class EmailTemplateGV
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public int TemplateTypeId { get; set; }
        public string TemplateType => ((TemplateType)Enum.ToObject(typeof(TemplateType), TemplateTypeId)).GetDescription();
        public DateTime? CreateDate { get; set; }
        public int StatusId { get; set; }
        public int SequenceId { get; set; }
    }

    public class EmailTemplateDetails
    {
        public EmailTemplateDetails(int emailTemplateId, string name, int? statusId, int templateTypeId, string content)
        {
            EmailTemplateId = emailTemplateId;
            Name = name;
            StatusId = statusId;
            TemplateTypeId = templateTypeId;
            Content = content;
        }

        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public int TemplateTypeId { get; set; }
        public string Content { get; set; }
    }
}
