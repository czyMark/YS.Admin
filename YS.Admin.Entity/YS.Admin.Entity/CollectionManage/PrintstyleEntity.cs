using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:47
    /// 描 述：打印样式实体类
    /// </summary>
    [Table("collection_printstyle")]
    public class PrintstyleEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 模版名称
        /// </summary>
        /// <returns></returns>
        public string PrintStyleName { get; set; }
        /// <summary>
        /// 模版备注
        /// </summary>
        /// <returns></returns>
        public string PrintStyleRemark { get; set; }
    }
}
