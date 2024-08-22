using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
	/// <summary>
	/// 创 建：admin
	/// 日 期：2022-02-01 03:31
	/// 描 述：实体类
	/// </summary>
	[Table("site_config")]
	public class SiteConfigEntity : BaseEntity
	{
		/// <summary>
		/// 网站名称
		/// </summary>
		/// <returns></returns>
		public string WebName { get; set; }
		/// <summary>
		/// 网站名称
		/// </summary>
		/// <returns></returns>
		public string WebUrl { get; set; }
		/// <summary>
		/// 网站LOGO
		/// </summary>
		/// <returns></returns>
		public string WebLogo { get; set; }
		/// <summary>
		/// 微信二维码
		/// </summary>
		/// <returns></returns>
		public string WebWxQCode { get; set; }
		/// <summary>
		/// 公司名称
		/// </summary>
		/// <returns></returns>
		public string WebCompany { get; set; }
		/// <summary>
		/// 联系人
		/// </summary>
		/// <returns></returns>
		public string WebContact { get; set; }
		/// <summary>
		/// 通讯地址
		/// </summary>
		/// <returns></returns>
		public string WebAddress { get; set; }
		/// <summary>
		/// 联系电话
		/// </summary>
		/// <returns></returns>
		public string WebTel { get; set; }
		/// <summary>
		/// 咨询电话
		/// </summary>
		/// <returns></returns>
		public string WebPhone { get; set; }
		/// <summary>
		/// 邮编
		/// </summary>
		/// <returns></returns>
		public string WebPost { get; set; }
		/// <summary>
		/// 传真号码
		/// </summary>
		/// <returns></returns>
		public string WebFax { get; set; }
		/// <summary>
		/// 管理员邮箱
		/// </summary>
		/// <returns></returns>
		public string WebEmail { get; set; }
		/// <summary>
		/// 网站备案号
		/// </summary>
		/// <returns></returns>
		public string WebCord { get; set; }
		/// <summary>
		/// 京公网安备
		/// </summary>
		/// <returns></returns>
		public string WebPolice { get; set; }
		/// <summary>
		/// 首页标题(SEO)
		/// </summary>
		/// <returns></returns>
		public string WebTitle { get; set; }
		/// <summary>
		/// 页面关健词(SEO)
		/// </summary>
		/// <returns></returns>
		public string WebKeyword { get; set; }
		/// <summary>
		/// 页面描述(SEO)
		/// </summary>
		/// <returns></returns>
		public string WebDescription { get; set; }
		/// <summary>
		/// 网站版权信息
		/// </summary>
		/// <returns></returns>
		public string WebCopyRight { get; set; }

		/// <summary>
		/// 公开课单价
		/// </summary>
		public int ClassPrices { get; set; }
		/// <summary>
		/// 公开课价格单价描述
		/// </summary>
		public string ClassPricesDes { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ApkVersion { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ApkFilePath { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ImgUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ImgUrl2 { get; set; }
		/// <summary>
		/// 版权所有
		/// </summary>
		public string AllRight { get; set; }
		/// <summary>
		/// 地址
		/// </summary>
		public string Addr { get; set; }
		/// <summary>
		/// 工作时间
		/// </summary>
		public string WorkTime { get; set; }
		/// <summary>
		/// 服务电话
		/// </summary>
		public string ServiceTel { get; set; }



	}
}
