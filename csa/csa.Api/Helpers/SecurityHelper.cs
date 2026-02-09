using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

using csa.Library;
using csa.Model.DataObject;

namespace csa.Api.Helpers
{
    public class SecurityHelper
    {
        public static bool VerifyRequest<T>(T request, GetRequest parameters, out HttpResponseMessage response, bool isSign = true)
        {
            //get utc timestamp
            DateTime curr = DateTime.UtcNow;

            //assign out variable
            response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            BaseReqDTO baseReq = null;
            Type typ = typeof(T);

            string secretKey = string.Empty, reqParam = string.Empty;

            if (request == null)
            { return false; }

            if ((typ != typeof(BaseReqDTO)) && (typ.BaseType != typeof(BaseReqDTO)) && (typ.BaseType != typeof(BaseGVReqDTO)))
            { return false; }

            baseReq = request as BaseReqDTO;
            reqParam = parameters.BuildParams();

            if (isSign)
            {
                if (string.IsNullOrEmpty(baseReq.SId) || string.IsNullOrEmpty(baseReq.Sign) || baseReq.Timestamp == 0)
                { response = new HttpResponseMessage(HttpStatusCode.BadRequest); }

                secretKey = $"{baseReq.SId.Replace("-", "")}{ConfigurationManager.AppSettings["ServerSecret"]}";
            }
            else
            {
                if (string.IsNullOrEmpty(baseReq.Sign) || baseReq.Timestamp == 0)
                { response = new HttpResponseMessage(HttpStatusCode.BadRequest); }

                secretKey = $"{ConfigurationManager.AppSettings["ServerSecret"]}";
            }

            string sign = Utils.Utilities.GenSignature(secretKey, reqParam);

            //check signature
            if (!sign.Equals(baseReq.Sign))
            {
                response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return false;
            }

            //acceptance expiration timeframe 5~10 seconds

            long acptRngFrom = curr.AddSeconds(-60).GetUnixTimeStamp();
            long acptRngTo = curr.AddSeconds(60).GetUnixTimeStamp();

            //check timestamp
            if ((baseReq.Timestamp < acptRngFrom) || (baseReq.Timestamp > acptRngTo))
            {
                response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
                return false;
            }

            return true;
        }
    }
}