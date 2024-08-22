using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:47
    /// 描 述：样式属性实体查询类
    /// </summary>
    public class PrintstylePropListParam
    {
        /// <summary>
        /// 样式模版的id 
        /// </summary>
        public long StyleId { get; set; }
        /// <summary>
        /// 属性列 
        /// </summary>
        public string StylePropElement { get; set; }
    }
}
