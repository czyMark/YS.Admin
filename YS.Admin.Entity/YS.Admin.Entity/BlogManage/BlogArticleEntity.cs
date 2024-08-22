using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-17 08:02
    /// 描 述：博客文章实体类
    /// </summary>
    [Table("blog_article")]
    public class BlogArticleEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        /// <returns></returns>
        public int? ClassId { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        /// <returns></returns>
        public int? CommentNum { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        /// <returns></returns>
        public long? UserId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Author { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        /// <returns></returns>
        public int? Ding { get; set; }
        /// <summary>
        /// 文章封面
        /// </summary>
        /// <returns></returns>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        /// <returns></returns>
        public int? ReadNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        /// <returns></returns>
        public int? TypeId { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        /// <returns></returns>
        public string ZhaiYao { get; set; }
        [NotMapped]
        public string ClassName { get; set; }
        [NotMapped]
        public string TypeName { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }



    [Table("blog_article_access")]
    public class BlogArticleAccessEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 文章id
        /// </summary>
        /// <returns></returns>
        public long? ArticleId { get; set; }
        /// <summary>
        /// 阅读标记    
        /// </summary>
        /// <returns></returns>
        public string? Tag { get; set; } 
    }
}
