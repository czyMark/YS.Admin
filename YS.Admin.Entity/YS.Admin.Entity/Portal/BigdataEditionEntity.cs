using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-08 15:13
    /// 描 述：版别大数据实体类
    /// </summary>
    [Table("bigdata_edition")]
    public class BigdataEditionEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 大数据标识
        /// </summary>
        /// <returns></returns>
        public string BigDataTag { get; set; }
        /// <summary>
        /// 藏品名称
        /// </summary>
        /// <returns></returns>
        public string CollectionName { get; set; }
        /// <summary>
        /// 统计项
        /// </summary>
        /// <returns></returns>
        public string TotalTag { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        /// <returns></returns>
        public string Result { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
