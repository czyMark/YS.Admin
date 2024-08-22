using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:07
    /// 描 述：博客留言实体查询类
    /// </summary>
    public class FeedbackListParam
    {
        public long? ParentId { get; set; }
        public long? ArticleId { get; set; }

    }
}
