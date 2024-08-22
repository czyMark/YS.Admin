using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-24 11:29
    /// 描 述：评分信息实体查询类
    /// </summary>
    public class ScoreListParam
    {
        public string Score { set; get; }
        public int ScoreType { set; get; }
    }
}
