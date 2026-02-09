using CsaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public static class FileBiz
    {
        public static File Get(string fileId)
        {
            using(CsaEntities db = new CsaEntities())
            {
                return db.Files.FirstOrDefault(x => x.FileId == fileId);
            }
        }

        public static List<File> Get(params string[] fileIds)
        {
            using (CsaEntities db = new CsaEntities())
            {
                return db.Files.Where(x => fileIds.Contains(x.FileId)).ToList();
            }
        }
    }
}
