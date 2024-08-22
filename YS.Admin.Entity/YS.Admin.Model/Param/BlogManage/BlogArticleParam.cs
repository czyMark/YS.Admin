using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-17 08:02
    /// 描 述：博客文章实体查询类
    /// </summary>
    public class BlogArticleListParam
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        /// <returns></returns>
        public int? ClassId { get; set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        /// <returns></returns>
        public int? TypeId { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
    }
}
