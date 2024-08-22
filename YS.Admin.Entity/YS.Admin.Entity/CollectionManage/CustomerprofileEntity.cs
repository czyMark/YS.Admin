using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 17:04
    /// 描 述：客户档案实体类
    /// </summary>
    [Table("collection_customerprofile")]
    public class CustomerprofileEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 所在的表
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? PrintDataId { get; set; }
        /// <summary>
        /// '纸币条目数'
        /// </summary>
        /// <returns></returns>
        public string BanknoteCount { get; set; }
        /// <summary>
        /// '硬币条目数
        /// </summary>
        /// <returns></returns>
        public string CoinCount { get; set; }
        /// <summary>
        /// '客户名称'
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// '客户电话'
        /// </summary>
        /// <returns></returns>
        public string CustomerTel { get; set; }
        /// <summary>
        /// '邮票条目数'
        /// </summary>
        /// <returns></returns>
        public string StampCount { get; set; }


        /// <summary>
        /// '开始编码'
        /// </summary>
        /// <returns></returns>
        public string StartCode { get; set; }


        /// <summary>
        /// '结束编码'
        /// </summary>
        /// <returns></returns>
        public string EndCode { get; set; }
    }
}
