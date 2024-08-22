using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using TagLib.Riff;
using System.Collections.Generic;

namespace YS.Admin.Entity.SiteManage
{
	/// <summary>
	/// 创 建：admin
	/// 日 期：2022-01-02 18:06
	/// 描 述：实体类
	/// </summary>
	[Table("site_article_category")]
	public class ArticleCategoryEntity : BaseExtensionEntity
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
		[JsonConverter(typeof(StringJsonConverter))]
		public long? ParentId { get; set; }

        [JsonConverter(typeof(StringJsonConverter))]
        public long? TemplateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? ModelId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CallIndex { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ClassList { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int? ClassLayer { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int? SortId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string LinkUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ImgUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ImgUrl2 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ImgUrl3 { get; set; }
		/// <summary>
		/// Banner图片对应的地址
		/// </summary>
		/// <returns></returns>
		public string BannerLinkUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string Remarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string Contents { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int? IsLock { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int? Status { get; set; }
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
		public string SeoDescription { get; set; }

        [NotMapped]
        public List<ArticlesEntity> Articles { get; set; } 
    }
}
