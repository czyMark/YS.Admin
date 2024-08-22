using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：xiaoyu
    /// 日 期：2024-05-13 22:33
    /// 描 述：系统编号设置实体类
    /// </summary>
    [Table("sys_number_set")]
    public class NumberSetEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 1 兑换礼品编号 2，订单号
        /// </summary>
        /// <returns></returns>
        public int? Type { get; set; }
        /// <summary>
        /// 编号基数
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? Number { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
