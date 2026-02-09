using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace csa.Library
{
    public class Bulk360Response
    {
        public Bulk360Response()
        {
        }

        public int Code { get; set; }
        public string Desc { get; set; }
        public string To { get; set; }
        public string Ref { get; set; }
        public string Currency { get; set; }
        public string Balance { get; set; }
        public string RequestUrl { get; set; }
    }
    public class Bulk360Message {
        public Bulk360Message()
        {
        }

        public Bulk360Message(string from, string to, string message)
        {
            To = to;
            Message = message;
            From = from;
        }

        public string To { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }

    public class Bulk360Otp : Bulk360Message
    {
        public Bulk360Otp(string from,string to,string otpCode)
        {
            this.To = to;
            this.From = from;
            this.Message = $"{otpCode} adalah no OTP untuk pengesahan pendaftaran Ahli YABAM. Kod ini akan tamat tempoh dalam masa 15 minit."; ;
        }
    }
    public class Bulk360
    {
        public Bulk360Message _message;

        public Bulk360(Bulk360Message message)
        {
            _message = message;
        }
        
        public async Task<Bulk360Response> Send()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["BULK360_URL"];
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["BULK360_APIKEY"];
            var apiSecret = System.Configuration.ConfigurationManager.AppSettings["BULK360_APISECRET"];
            var isAllow = System.Configuration.ConfigurationManager.AppSettings["BULK360_ISALLOWSEND"];

            if (url == null || apiKey == null || apiSecret == null || isAllow == null) throw new ArgumentNullException("BULK360 configuration not found");

            if (isAllow == "0") throw new ArgumentNullException("BULK360 disabled feature");

            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{url}?user={apiKey}&pass={apiSecret}&from={_message.From}&to={_message.To}&text={System.Web.HttpUtility.UrlEncode(_message.Message)}";
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                string responseBody = await response.Content.ReadAsStringAsync();

                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<Bulk360Response>(responseBody);
                res.RequestUrl = requestUrl;
                return res;
            }
        }
    }
}
