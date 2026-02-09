using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using csa.Model;

namespace csa.Model.DataObject
{
    public class MiscRespDTO
    {
    }

    public class DropDownListRespDTO<T> : BaseRespDTO
    {
        [JsonProperty("DropDownList")]
        public IEnumerable<DropDownListModel<T>> DropDownList { get; set; }

        public DropDownListRespDTO(bool error, int errorCode, string messages, IEnumerable<DropDownListModel<T>> listData)
            : base(error, errorCode, messages)
        {
            DropDownList = listData;
        }
    }
}
