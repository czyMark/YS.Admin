using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-14 21:10
    /// 描 述：ip地址访问黑名单实体查询类
    /// </summary>
    public class IpBlockListParam
    {
        /// <summary>
        /// ip地址
        /// </summary>
        /// <returns></returns>
        public string IpAddr { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
    }
}
