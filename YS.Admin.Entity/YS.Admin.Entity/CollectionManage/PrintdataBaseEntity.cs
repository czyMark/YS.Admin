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
    [Table("collection_printdata_base")]
    public class PrintdataBaseEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        /// <returns></returns>
        public string TagTypeName { get; set; }
        /// <summary>
        /// 打印模版名称
        /// </summary>
        /// <returns></returns>
        public string PrintStyleName{ get; set; }
        /// <summary>
        /// 鉴定师名称
        /// </summary>
        /// <returns></returns>
        public string AppraiserName { get; set; }
        /// <summary>
        /// 藏品名称	
        /// </summary>
        /// <returns></returns>
        public string CollectionName { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        /// <returns></returns>
        public string CollectionValue { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        /// <returns></returns>
        public string CollectionYear { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 所属档案
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? CustomerProfileId { get; set; }
        /// <summary>
        /// 0-锁定\n1-编辑\n2-打印
        /// </summary>
        /// <returns></returns>
        public int? DataState { get; set; }
        /// <summary>
        /// 0无标志/1纸币数据/2邮票标志/3硬币数据标志
        /// </summary>
        /// <returns></returns>
        public int? DataTag { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 版别
        /// </summary>
        /// <returns></returns>
        public string Edition { get; set; }
        /// <summary>
        /// 版别/个性化
        /// </summary>
        /// <returns></returns>
        public string EditionPersonalization { get; set; }
        /// <summary>
        /// 估值保价
        /// </summary>
        /// <returns></returns>
        public string EstimatedValue { get; set; }
        /// <summary>
        /// HQP
        /// </summary>
        /// <returns></returns>
        public string HQP { get; set; }
        /// <summary>
        /// 鉴定编号
        /// </summary>
        /// <returns></returns>
        public string IDCode { get; set; }
        /// <summary>
        /// 发行单位
        /// </summary>
        /// <returns></returns>
        public string IssuingUnit { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        /// <returns></returns>
        public string Material { get; set; }
        /// <summary>
        /// 志号
        /// </summary>
        /// <returns></returns>
        public string NumberCode { get; set; }
        /// <summary>
        /// OS
        /// </summary>
        /// <returns></returns>
        public string OS { get; set; }
        /// <summary>
        /// 个性化
        /// </summary>
        /// <returns></returns>
        public string Personalization { get; set; }
        /// <summary>
        /// 印刷工艺	
        /// </summary>
        /// <returns></returns>
        public string PrintArt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? PrintStyleID { get; set; }
        /// <summary>
        /// 珍惜度
        /// </summary>
        /// <returns></returns>
        public string Rarity { get; set; }
        /// <summary>
        /// 评分	
        /// </summary>
        /// <returns></returns>
        public string Rating { get; set; }
        /// <summary>
        /// 冠字编号	
        /// </summary>
        /// <returns></returns>
        public string SerialCode { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        /// <returns></returns>
        public string Size { get; set; }
        /// <summary>
        /// 原胶
        /// </summary>
        /// <returns></returns>
        public string VirginRubber { get; set; }
        /// <summary>
        /// 三星标志
        /// </summary>
        /// <returns></returns>
        public string StarTag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? TagTypeID { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        /// <returns></returns>
        public string Weight { get; set; }
        /// <summary>
        /// 大数据标志
        /// </summary>
        /// <returns></returns>
        public string BigDataTag { get; set; }
        /// <summary>
        /// 藏品图片集合 多个图片使用;分割
        /// </summary>
        /// <returns></returns>
        public string CollectionImage { get; set; }

        [NotMapped]
        public long Index { get; set; }
        [NotMapped]
        public string MainTagName { get; set; }
        [NotMapped]
        public string TableName { get; set; }
        /// <summary>
        /// 评分对应英语说明
        /// </summary>
        [NotMapped]
        public string RatingEnglish { get; set; }
    }
}
