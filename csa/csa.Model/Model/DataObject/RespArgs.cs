using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace csa.Model
{
    public class RespArgs<T>
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("objval", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T ObjVal { get; set; }

        public static RespArgs<T> CreateError(string message)
        {
            return new RespArgs<T> { Error = true, Message = message };
        }

        public static RespArgs<T> CreateError(string message,int code)
        {
            return new RespArgs<T> { Error = true, Message = message, Code = code };
        }

        public static RespArgs<T> CreateSuccess(T data)
        {
            return new RespArgs<T> { Error = false, ObjVal = data, Message = "successfully" };
        }
    }
}
