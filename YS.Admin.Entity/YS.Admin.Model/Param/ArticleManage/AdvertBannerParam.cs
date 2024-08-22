using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-20 23:03
    /// 描 述：实体查询类
    /// </summary>
    public class AdvertBannerListParam
    {
        [JsonConverter(typeof(StringJsonConverter))]
        public long? Aid { get; set; }
        public int Ttile { get; set; }
    }
}
