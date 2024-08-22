using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 19:39
    /// 描 述：短链接访问日志实体类
    /// </summary>
    [Table("short_link_log")]
    public class LinkLogEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 浏览器类型
        /// </summary>
        /// <returns></returns>
        public int? BrowserType { get; set; }
        /// <summary>
        /// 访问的ip地址
        /// </summary>
        /// <returns></returns>
        public string Ip { get; set; }
        /// <summary>
        /// 链接id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? LinkId { get; set; }
        /// <summary>
        /// OS
        /// </summary>
        /// <returns></returns>
        public int? OsType { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
