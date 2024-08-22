using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:43
    /// 描 述：网站设置实体类
    /// </summary>
    [Table("blog_config")]
    public class BlogConfigEntity : BaseExtensionEntity
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
        /// <summary>
        /// 是否开启留言
        /// </summary>
        /// <returns></returns>
        public string FeedbackStatus { get; set; }
        /// <summary>
        /// 上传文件类型
        /// </summary>
        /// <returns></returns>
        public string FileType { get; set; }
        /// <summary>
        /// 首页标题
        /// </summary>
        /// <returns></returns>
        public string HomeTitle { get; set; }
        /// <summary>
        /// 每日最大评论数量
        /// </summary>
        /// <returns></returns>
        public string MaxCommentCount { get; set; }
        /// <summary>
        /// 每日最大留言数量
        /// </summary>
        /// <returns></returns>
        public string MaxFeedbackCount { get; set; }
        /// <summary>
        /// 上传文件最大限制
        /// </summary>
        /// <returns></returns>
        public string MaxFileSize { get; set; }
        /// <summary>
        /// 本地地址
        /// </summary>
        /// <returns></returns>
        public string WebAddr { get; set; }
        /// <summary>
        /// 网站版权信息
        /// </summary>
        /// <returns></returns>
        public string WebCopyRight { get; set; }
        /// <summary>
        /// 网站网站域名
        /// </summary>
        /// <returns></returns>
        public string WebDomain { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        /// <returns></returns>
        public string WebEmail { get; set; }
        /// <summary>
        /// META关键词
        /// </summary>
        /// <returns></returns>
        public string WebMETAKey { get; set; }
        /// <summary>
        /// META描述
        /// </summary>
        /// <returns></returns>
        public string WebMETAValue { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        /// <returns></returns>
        public string WebName { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        /// <returns></returns>
        public string WebQQ { get; set; }
        /// <summary>
        /// 网站标题
        /// </summary>
        /// <returns></returns>
        public string WebTitle { get; set; }



        /// <summary>
        /// 愿景
        /// </summary>
        /// <returns></returns>
        public string Slogan { get; set; }


        /// <summary>
        /// 目标
        /// </summary>
        /// <returns></returns>
        public string Target { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
