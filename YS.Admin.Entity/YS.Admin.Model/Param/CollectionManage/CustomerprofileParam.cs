using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 17:04
    /// 描 述：客户档案实体查询类
    /// </summary>
    public class CustomerprofileListParam
    {
        public string CustomerName { get; set; }
        public long? PrintDataId { get; set; }
    }
}
