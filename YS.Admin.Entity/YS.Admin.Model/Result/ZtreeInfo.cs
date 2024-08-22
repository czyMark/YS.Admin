using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Model.Result
{
    public class ZtreeInfo
    {
        [JsonConverter(typeof(StringJsonConverter))]
        public long? id { get; set; }

        [JsonConverter(typeof(StringJsonConverter))]
        public long? pId { get; set; }

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 中文释义
        /// </summary>
        public string comment { get; set; }
    }
}
