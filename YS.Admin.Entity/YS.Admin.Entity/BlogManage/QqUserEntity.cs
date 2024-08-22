using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using NPOI.SS.Formula.Functions;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:09
    /// 描 述：第三方登录用户实体类
    /// </summary>
    [Table("blog_user")]
    public class QqUserEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// 性别（0：女，1：男）
        /// </summary>
        /// <returns></returns>
        public int Gender { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        /// <returns></returns>
        public string HeadShot { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 指纹登录
        /// </summary>
        public string Crypto { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
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
