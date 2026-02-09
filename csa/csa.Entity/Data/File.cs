using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaModel
{
    partial class File
    {
        public FileDisplay ToDisplay()
        {
            return new FileDisplay(this.Filename, this.FileId + this.Extension);
        }
    }
}
