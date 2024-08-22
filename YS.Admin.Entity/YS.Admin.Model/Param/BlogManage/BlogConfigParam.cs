using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:43
    /// 描 述：网站设置实体查询类
    /// </summary>
    public class BlogConfigListParam
    {
        /// <summary>
        /// 缓存时间
        /// </summary>
        /// <returns></returns>
        public string CacheTime { get; set; }
        /// <summary>
        /// 是否开启评论
        /// </summary>
        /// <returns></returns>
        public string CommentStatus { get; set; }
    }
}
