using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-14 21:10
    /// 描 述：ip地址访问黑名单实体类
    /// </summary>
    [Table("sys_ip_block")]
    public class IpBlockEntity : BaseExtensionEntity
    {
        /// <summary>
        /// ip地址
        /// </summary>
        /// <returns></returns>
        public string IpAddr { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        /// <returns></returns>
        public string Method { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
