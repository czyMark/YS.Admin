using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-08 15:14
    /// 描 述：评分大数据实体类
    /// </summary>
    [Table("bigdata_rating")]
    public class BigdataRatingEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 大数据标识
        /// </summary>
        /// <returns></returns>
        public string BigDataTag { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        /// <returns></returns>
        public string Rating { get; set; }
        /// <summary>
        /// 总量
        /// </summary>
        /// <returns></returns>
        public string Total { get; set; }
        /// <summary>
        /// 当月新增量
        /// </summary>
        /// <returns></returns>
        public string NewDataCount { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
