using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:31
    /// 描 述：短链接管理实体类
    /// </summary>
    [Table("short_link")]
    public class LinkEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 访问次数
        /// </summary>
        /// <returns></returns>
        public int? AccessCount { get; set; }
        /// <summary>
        /// 原链接
        /// </summary>
        /// <returns></returns>
        public string OriginUrl { get; set; }
        /// <summary>
        /// 短链接
        /// </summary>
        /// <returns></returns>
        public string ShortUrl { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
