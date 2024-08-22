using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 00:20
    /// 描 述：系统公告实体类
    /// </summary>
    [Table("sys_messages")]
    public class MessagesEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 消息正文
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        /// <returns></returns>
        public string MessagesTag { get; set; }
        /// <summary>
        /// 消息摘要 obj.Data.MassageTag
        /// </summary>
        /// <returns></returns>
        public string Summary { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        /// <returns></returns>
        public string ThumbImage { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// 查看次数
        /// </summary>
        /// <returns></returns>
        public int? ViewTimes { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;



        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string UserId { get; set; } = string.Empty;
        [NotMapped]
        public string ReadStatus { get; set; } = string.Empty;
    }
}
