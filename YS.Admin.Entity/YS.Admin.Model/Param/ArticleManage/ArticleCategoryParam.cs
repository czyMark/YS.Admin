using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-02 18:06
    /// 描 述：实体查询类
    /// </summary>
    public class ArticleCategoryListParam
    {
        //public string Id { get; set; }
        public long ParentId { get; set; } = -1;

        public long IsLock { get; set; }
        public string ClassList { get; set; }
    }
}
