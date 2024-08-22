using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-13 22:29
    /// 描 述：自动执行任务实体类
    /// </summary>
    [Table("sys_quartzoptions")]
    public class QuartzoptionsEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        /// <returns></returns>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        /// <returns></returns>
        public string GroupName { get; set; }
        /// <summary>
        /// 任务状态(0禁用 1启用)
        /// </summary>
        /// <returns></returns>
        public int? TaskStatus { get; set; }
        /// <summary>
        /// Corn表达式
        /// </summary>
        /// <returns></returns>
        public string CronExpression { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        /// <returns></returns>
        public string Method { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        /// <returns></returns>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 权限key
        /// </summary>
        /// <returns></returns>
        public string AuthKey { get; set; }
        /// <summary>
        /// 权限值
        /// </summary>
        /// <returns></returns>
        public string AuthValue { get; set; }
        /// <summary>
        /// post参数
        /// </summary>
        /// <returns></returns>
        public string PostData { get; set; }
        /// <summary>
        /// 运行开始时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 运行结束时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 下次执行时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? NextStartTime { get; set; }
        /// <summary>
        /// 最后执行执行
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? LastRunTime { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// 超时时间(秒)
        /// </summary>
        /// <returns></returns>
        public int? TimeOut { get; set; }
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
