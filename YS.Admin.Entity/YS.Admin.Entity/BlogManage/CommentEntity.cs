using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using System.ComponentModel;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:05
    /// 描 述：博客文章评论实体类
    /// </summary>
    [Table("blog_comment")]
    public class CommentEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        /// <returns></returns>
        public long? SendId { get; set; }
        /// <summary>
        /// 目标人员ID
        /// </summary>
        /// <returns></returns>
        public long? AcceptId { get; set; }
        /// <summary>
        /// 文章ID
        /// </summary>
        /// <returns></returns>
        public long? ArticleId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; } 
        /// <summary>
        /// 父ID
        /// </summary>
        /// <returns></returns>
        public long? ParentId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public bool? Status { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
