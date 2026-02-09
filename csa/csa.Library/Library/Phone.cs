using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace csa.Library
{
    public class Phone
    {
        private string _phone { get; set; }
        public Phone(string phone)
        {
            _phone = Regex.Replace(phone, @"\D", "");
        }

        public Phone RemovePrefix(string prefix)
        {
            if (_phone != null)
            {
                if (_phone.StartsWith(prefix))
                {
                    _phone = _phone.Substring(prefix.Length);
                }
            }
            return this;
        }

        public Phone AddPrefix(string prefix)
        {
            if (_phone != null)
            {
                if (!_phone.StartsWith(prefix))
                {
                    _phone = prefix + _phone;
                }
            }
            return this;
        }

        public string GetResult()
        {
            return _phone;
        }
    }
}
