using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-11 23:06
    /// 描 述：自动执行任务日志实体类
    /// </summary>
    [Table("sys_quartzlog")]
    public class QuartzlogEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 耗时(秒)
        /// </summary>
        /// <returns></returns>
        public int? ElapsedTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 执行状态(0失败 1成功)
        /// </summary>
        /// <returns></returns>
        public int? LogStatus { get; set; }
        /// <summary>
        /// 返回内容
        /// </summary>
        /// <returns></returns>
        public string ResponseContent { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StratDate { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        /// <returns></returns>
        public string TaskName { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
