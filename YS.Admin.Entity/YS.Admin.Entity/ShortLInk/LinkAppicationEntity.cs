using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:32
    /// 描 述：短链接应用管理实体类
    /// </summary>
    [Table("short_link_appication")]
    public class LinkAppicationEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 应用码
        /// </summary>
        /// <returns></returns>
        public string AppCode { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        /// <returns></returns>
        public string AppName { get; set; }
        /// <summary>
        /// 应用授权Key
        /// </summary>
        /// <returns></returns>
        public string AppSecret { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
