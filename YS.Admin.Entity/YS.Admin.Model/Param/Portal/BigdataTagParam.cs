﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:24
    /// 描 述：大数据统计标志实体查询类
    /// </summary>
    public class BigdataTagListParam
    {
        public string TagName { get; set; }
        public string DataTag { get; set; }
        public string CategoryId { get; set; }
    }
}
