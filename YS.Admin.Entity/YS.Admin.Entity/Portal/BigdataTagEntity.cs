using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:24
    /// 描 述：大数据统计标志实体类
    /// </summary>
    [Table("bigdata_tag")]
    public class BigdataTagEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 大数据标志ID
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? CategoryId { get; set; }
        /// <summary>
        /// 大数据统计项1
        /// </summary>
        /// <returns></returns>
        public string TagName1 { get; set; }
        /// <summary>
        /// 大数据统计项2
        /// </summary>
        /// <returns></returns>
        public string TagName2 { get; set; }
        /// <summary>
        /// 0无标志/1纸币数据/2邮票标志/3硬币数据标志
        /// </summary>
        /// <returns></returns>
        public string DataTag { get; set; }
        public int? SortId { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;

    }
}
