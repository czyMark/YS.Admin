using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:48
    /// 描 述：分表数据实体类
    /// </summary>
    [Table("collection_pringdatamaintag")]
    public class PringdatamaintagEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 纸币条目数
        /// </summary>
        /// <returns></returns>
        public int? BanknoteCount { get; set; }
        /// <summary>
        /// 硬币条目数
        /// </summary>
        /// <returns></returns>
        public int? CoinCount { get; set; }
        /// <summary>
        /// 数据表名称
        /// </summary>
        /// <returns></returns>
        public string DataTableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string MainTagName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string MainTagRemark { get; set; }
        /// <summary>
        /// 邮票条目数
        /// </summary>
        /// <returns></returns>
        public int? StampCount { get; set; }
        /// <summary>
        /// 编码开始
        /// </summary>
        /// <returns></returns>
        public string StartCode { get; set; }
    }
}
