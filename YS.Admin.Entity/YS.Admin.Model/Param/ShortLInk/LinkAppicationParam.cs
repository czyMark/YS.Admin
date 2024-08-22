using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:32
    /// 描 述：短链接应用管理实体查询类
    /// </summary>
    public class LinkAppicationListParam
    {
        /// <summary>
        /// 应用码
        /// </summary>
        /// <returns></returns>
        public string AppCode { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        /// <returns></returns>
        public string AppName { get; set; }
    }
}
