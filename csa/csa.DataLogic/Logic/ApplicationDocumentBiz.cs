using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class ApplicationDocumentBiz
    {
        public static List<ApplicationAdditionalDocument> ListDocumentByApplicationIdAndStatus(long applicationId,int statusId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                var list = db.ApplicationDocuments.Include("admin").Where(x => x.ApplicationId == applicationId && x.ApplicationStatusId == statusId).ToList();
                var files = FileBiz.Get(list.Select(x => x.FileId).ToArray());
                List<ApplicationAdditionalDocument> retVal = new List<ApplicationAdditionalDocument>();
                foreach (var item in list)
                {
                    var file = files.Find(y => y.FileId == item.FileId);
                    retVal.Add(new ApplicationAdditionalDocument(item.FileId, file?.Extension, file?.Filename, item.Remark, item.Admin.Name,item.ApplicationDocumentId,item.CreateDate));
                }

                return retVal;
            }
        }

    }
}
