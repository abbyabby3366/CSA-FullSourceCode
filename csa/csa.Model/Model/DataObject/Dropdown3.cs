using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class Dropdown3
    {
        public Dropdown3(string value, string text, string refValue)
        {
            Value = value;
            Text = text;
            RefValue = refValue;
        }

        public string Value { get; set; }
        public string Text { get; set; }
        public string RefValue { get; set; }
    }
}
