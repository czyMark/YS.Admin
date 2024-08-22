using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-05 14:00
    /// 描 述：栏目文字数据实体类
    /// </summary>
    [Table("site_articles_descriptiondata")]
    public class ArticlesDescriptiondataEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 列表项1
        /// </summary>
        /// <returns></returns>
        public string C1 { get; set; }
        /// <summary>
        /// 列表项2
        /// </summary>
        /// <returns></returns>
        public string C2 { get; set; }
        /// <summary>
        /// 列表项3
        /// </summary>
        /// <returns></returns>
        public string C3 { get; set; }
        /// <summary>
        /// 列表项4
        /// </summary>
        /// <returns></returns>
        public string C4 { get; set; }
        /// <summary>
        /// 列表项5
        /// </summary>
        /// <returns></returns>
        public string C5 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public int SortId { get; set; }


        /// <summary>
        /// 栏目内容id
        /// </summary>
        /// <returns></returns>

        [JsonConverter(typeof(StringJsonConverter))]
        public long? ArticlesId { get; set; }
    }
}
