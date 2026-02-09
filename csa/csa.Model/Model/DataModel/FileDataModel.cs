using System;

using csa.Library;

namespace csa.Model
{
    public class FileDataModel
    {
        public Guid Id { get; set; }

        public string Extension { get; set; }
    }

    //----------------------------------------------

    public class FileDataInfoModel
    {
        public Guid Id { get; set; }

        public double Size { get; set; }

        public string Filename { get; set; }

        public string Extension { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

    //================================================================================================'

}
