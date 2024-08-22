using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using System.Collections.Generic;

namespace YS.Admin.Entity.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:23
    /// 描 述：大数据栏目实体类
    /// </summary>
    [Table("bigdata_category")]
    public class BigdataCategoryEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 大数据名称
        /// </summary>
        /// <returns></returns>
        public string CategoryName { get; set; }
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
        [NotMapped]
        public List<BigdataTagEntity> TagList { get; set; }
    }
}
