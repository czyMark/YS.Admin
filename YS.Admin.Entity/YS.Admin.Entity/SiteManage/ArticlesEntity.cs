using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using System.Collections.Generic;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-02 18:05
    /// 描 述：实体类
    /// </summary>
    [Table("site_articles")]
    public class ArticlesEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SubTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
       
        public long? CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Author { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
		/// <summary>
		///  送评地址
		/// </summary>
		/// <returns></returns>
		public string Courier { get; set; }
        /// <summary>
        /// 快递方式
        /// </summary>
        /// <returns></returns>
        public string Express { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Regions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SeoTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SeoKeywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SeoDescripiton { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ZhaiYao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Contents { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Explanatory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? IsTop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? IsHot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? IsRed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? SortId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? Click { get; set; }

        [NotMapped]
        public List<ArticlesDescriptiondataEntity> ArticlesDescriptiondata { get; set; }


	}
}
