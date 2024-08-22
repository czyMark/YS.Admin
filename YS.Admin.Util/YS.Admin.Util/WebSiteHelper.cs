using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Util
{
	/// <summary>
	/// 
	/// </summary>
	public class WebSiteHelper
	{
		public static string GetUrl(string UrlPath)
		{
			string url = UrlPath;
			if (!string.IsNullOrEmpty(UrlPath))
			{
				if (!(UrlPath.ToLower().StartsWith("http://") || UrlPath.ToLower().StartsWith("https://")))
				{
					url = GlobalContext.SystemConfig.SiteWeb + UrlPath;
				}
			}
			return url;
		}
		/// <summary>
		/// 图集替换
		/// </summary>
		/// <param name="UrlPath">图集URL地址</param>
		/// <returns></returns>
		public static string GetUrls(string UrlPath)
		{
			string url = string.Empty;
			string[] urls = UrlPath.Split(';');
			foreach (var item in urls)
			{
				if (!string.IsNullOrEmpty(item))
				{
					if (!(item.ToLower().StartsWith("http://") || item.ToLower().StartsWith("https://")))
					{
						url += GlobalContext.SystemConfig.SiteWeb + item + ";";
					}
				}
			}
			if(!string.IsNullOrEmpty(url))
			{
				url = url.TrimEnd(';');
			}
			return url;
		}
		public static string RepContent(string Contents)
		{

			if (!string.IsNullOrEmpty(Contents))
			{
				Contents = Contents.Replace("src=\"/upload/", "src=\"" + GlobalContext.SystemConfig.SiteWeb + "/upload/");
			}
			return Contents;
		}
	}
}
