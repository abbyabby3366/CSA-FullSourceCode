using csa.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class FileDisplay
    {
        public FileDisplay(string fileName, string guidName)
        {
            FileName = fileName;
            GuidName = guidName;
            var fileInfo = new FileInfo(guidName);
            GuidThumbnailName = guidName.Replace(fileInfo.Extension,"") + Constant.THUMBNAIL + fileInfo.Extension;
        }

        public string FileName { get; set; }
        private string GuidName { get; set; }
        private string GuidThumbnailName { get; set; }
        private string UploadPath { get; set; }
        public string FilePath => UploadPath + GuidName;
        public string FileThumbnailPath => UploadPath + GuidThumbnailName;
        public void SetUploadPath(string path) => UploadPath = path;
    }
}
