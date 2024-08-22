using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:31
    /// 描 述：短链接管理实体查询类
    /// </summary>
    public class LinkListParam
    {
        /// <summary>
        /// 原链接
        /// </summary>
        /// <returns></returns>
        public string OriginUrl { get; set; }
        /// <summary>
        /// 短链接
        /// </summary>
        /// <returns></returns>
        public string ShortUrl { get; set; }
    }
}
