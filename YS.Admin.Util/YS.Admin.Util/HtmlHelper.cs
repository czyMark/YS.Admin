using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YS.Admin.Util
{
    public class HtmlHelper
    {
        /// <summary>
        /// Get part Content from HTML by apply prefix part and subfix part
        /// </summary>
        /// <param name="html">souce html</param>
        /// <param name="prefix">prefix</param>
        /// <param name="subfix">subfix</param>
        /// <returns>part content</returns>
        public static string Resove(string html, string prefix, string subfix)
        {
            int inl = html.IndexOf(prefix);
            if (inl == -1)
            {
                return null;
            }
            inl += prefix.Length;
            int inl2 = html.IndexOf(subfix, inl);
            string s = html.Substring(inl, inl2 - inl);
            return s;
        }
        public static string ResoveReverse(string html, string subfix, string prefix)
        {
            int inl = html.IndexOf(subfix);
            if (inl == -1)
            {
                return null;
            }
            string subString = html.Substring(0, inl);
            int inl2 = subString.LastIndexOf(prefix);
            if (inl2 == -1)
            {
                return null;
            }
            string s = subString.Substring(inl2 + prefix.Length, subString.Length - inl2 - prefix.Length);
            return s;
        }
        public static List<string> ResoveList(string html, string prefix, string subfix)
        {
            List<string> list = new List<string>();
            int index = prefix.Length * -1;
            do
            {
                index = html.IndexOf(prefix, index + prefix.Length);
                if (index == -1)
                {
                    break;
                }
                index += prefix.Length;
                int index4 = html.IndexOf(subfix, index);
                string s78 = html.Substring(index, index4 - index);
                list.Add(s78);
            }
            while (index > -1);
            return list;
        }


		public static string GetHtml(string urlpath)
		{
			string html = string.Empty;
			try
			{
				WebRequest request = WebRequest.Create(urlpath);
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
				html = reader.ReadToEnd();
			}
			catch (Exception ex)
			{

			}
			return html;
		}


		public static string ReplaceFontFamily(string html)
		{
			// 使用正则表达式匹配 font-family 属性
			string pattern = @"font-family\s*:\s*[^;]+;";
			string replacedHtml = Regex.Replace(html, pattern, "font-family: 宋体;", RegexOptions.IgnoreCase);
			return replacedHtml;
		}
	}
}
