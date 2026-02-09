using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csa.Library
{
    public class GetRequest
    {
        private Dictionary<string, string> _params;

        public GetRequest(GetRequest request = null)
        {
            if (request != null)
            { _params = new Dictionary<string, string>(request._params); }
            else
            { _params = new Dictionary<string, string>(); }
        }

        public GetRequest AddParam(string property, string value)
        {
            if ((property != null) && (value != null))
            { _params.Add(Uri.EscapeDataString(property), Uri.EscapeDataString(value)); }

            return this;
        }

        public GetRequest AddParam(GetRequest request)
        {
            _params.Concat(request._params);
            return this;
        }

        public string BuildParams()
        {
            if (_params.Count == 0)
            { return string.Empty; }

            StringBuilder sb = new StringBuilder();

            foreach (var para in _params.OrderBy(i => i.Key, StringComparer.Ordinal))
            {
                sb.Append('&');
                sb.Append(para.Key).Append('=').Append(para.Value);
            }

            return sb.ToString().Substring(1);
        }
    }
}
