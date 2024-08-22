using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
	/// <summary>
	/// 创 建：admin
	/// 日 期：2024-08-05 13:59
	/// 描 述：栏目对应模版实体类
	/// </summary>
	[Table("site_articles_template")]
	public class ArticlesTemplateEntity : BaseExtensionEntity
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string TemplateName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string TemplateAddr { get; set; }
		/// <summary>
		/// 0-无
		///1-单网页数据
		///2-多条数据一次显示
		///3-多条数据分页显示
		///4-层级数据一次显示
		/// </summary>
		/// <returns></returns>
		public int TemplateDataStatus { get; set; }
	}
}
