using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class DropdownItem
    {
        public DropdownItem(string key, string text)
        {
            Key = key;
            Text = text;
        }

        public string Key { get; set; }
        public string Text { get; set; }
    }
}
