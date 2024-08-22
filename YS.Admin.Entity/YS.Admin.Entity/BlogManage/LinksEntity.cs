using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:08
    /// 描 述：友情链接实体类
    /// </summary>
    [Table("blog_links")]
    public class LinksEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string Describe { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public string Icon { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        /// <returns></returns>
        public string Url { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
