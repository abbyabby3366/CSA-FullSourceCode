using csa.Model.DataObject;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class ApplicationFileBiz
    {
        public static List<ApplicationFileDocument> ListFileByApplicationIdAndGroup(long applicationId, string group)
        {
            using (CsaEntities db = new CsaEntities())
            {
                var list = db.ApplicationFiles.Include("admin").Include("member").Where(x => x.ApplicationId == applicationId && x.GROUP == group).ToList();
                var files = FileBiz.Get(list.Select(x => x.FileId).ToArray());
                List<ApplicationFileDocument> retVal = new List<ApplicationFileDocument>();
                foreach (var item in list)
                {
                    var file = files.Find(y => y.FileId == item.FileId);
                    retVal.Add(new ApplicationFileDocument(item.ApplicationFileId, item.FileId, file?.Extension, file?.Filename,BuildFile(file,item.Admin?.Name,item.Member?.FullName,item.CreateDate)));
                }

                return retVal;
            }
        }

        public static FileUploadedBy<ValueText<string>> BuildFile(File file, string adminName, string memberName, DateTime? date)
        {
            if (file == null) return null;
            return new FileUploadedBy<ValueText<string>>(new ValueText<string>(file?.FileId + file?.Extension, file?.Filename), adminName != null ? adminName : memberName, date);
        }
    }
}
