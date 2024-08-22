using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:05
    /// 描 述：博客文章评论实体查询类
    /// </summary>
    public class CommentListParam
    {

        public bool ParentId { get; set; }
        public long? ArticleId { get; set; }
    }
}
