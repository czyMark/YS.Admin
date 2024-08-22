using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 07:16
    /// 描 述：用户读取系统公告实体类
    /// </summary>
    [Table("sys_user_messages")]
    public class UserMessagesEntity : BaseEntity
    {
        /// <summary>
        /// 消息id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? MessagesId { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        /// <returns></returns>
        public int? ReadStatus { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? UserId { get; set; }


        /// <summary>
        /// 已读时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? ReadTime { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
