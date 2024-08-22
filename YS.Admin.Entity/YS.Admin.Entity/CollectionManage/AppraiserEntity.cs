using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:28
    /// 描 述：鉴定师实体类
    /// </summary>
    [Table("collection_appraiser")]
    public class AppraiserEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 鉴定师名称
        /// </summary>
        /// <returns></returns>
        public string AppraiserName { get; set; }
        /// <summary>
        /// 鉴定师备注
        /// </summary>
        /// <returns></returns>
        public string AppraiserRemark { get; set; }
    }
}
