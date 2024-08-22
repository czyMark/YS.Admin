using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表实体类
    /// </summary>
    [Table("collection_printdata_temp_")]
    public class PrintdataTempEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        /// <returns></returns>
        public string IDCode { get; set; }
        /// <summary>
        /// 临时存放的数据对应的表名称id
        /// </summary>
        /// <returns></returns>
        public long MainTagID  { get; set; }
        /// <summary>
        /// 临时存放的数据对应的表名
        /// </summary>
        /// <returns></returns>
        public string MainTagName { get; set; }
        /// <summary>
        /// 临时存放的数据对应的数据表名称
        /// </summary>
        /// <returns></returns>
        public string DataTableName { get; set; }
    }
}
