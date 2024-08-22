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
    [Table("collection_bn")]
    public class BNEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 年
        /// </summary>
        /// <returns></returns>
        public int Year { get; set; }
        /// <summary>
        /// 最新编号
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? Bn { get; set; }
    }
}
