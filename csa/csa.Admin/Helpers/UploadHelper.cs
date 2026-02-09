using csa.DataLogic.Library;
using csa.Member.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace csa.Admin.Helpers
{
    public static class UploadHelper
    {
        public static void Upload(HttpPostedFileBase postedFile,FileHelper.FileDir dir,out CsaModel.File file)
        {
            file = null;
            if (postedFile != null && postedFile.ContentLength > 0)
            {                
                var fileInfo = new FileInfo(postedFile.FileName);
                file = new CsaModel.File
                {
                    FileId = Guid.NewGuid().ToString(),
                    Filename = postedFile.FileName,
                    Extension = fileInfo.Extension,
                    Size = postedFile.ContentLength,
                    CreateDate = DateTime.Now
                };
                var filePath = Path.Combine(FileHelper.GetUploadPhysic(AppSettings.UploadPath, dir), file.FileId + file.Extension);
                postedFile.SaveAs(filePath);
            }
        }
    }
}