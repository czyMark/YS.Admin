using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-03-03 15:00
    /// 描 述：留言实体类
    /// </summary>
    [Table("site_feedback")]
    public class FeedbackEntity : BaseZyEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? groupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string userTel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string userQq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string userEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string replyContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? replyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? isLock { get; set; }
    }
}
