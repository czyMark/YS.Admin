using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-02 18:05
    /// 描 述：实体查询类
    /// </summary>
    public class ArticlesListParam
    {
        [JsonConverter(typeof(StringJsonConverter))]
        public long? CategoryId { get; set; }
        public int? Status { get; set; }
	}
}
