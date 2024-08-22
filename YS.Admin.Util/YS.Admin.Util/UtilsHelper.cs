using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace YS.Admin.Util
{
	public static class UtilsHelper
	{


		#region 对象转换处理
		/// <summary>
		/// 判断对象是否为Int32类型的数字
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsNumeric(object expression)
		{
			if (expression != null)
				return IsNumeric(expression.ToString());

			return false;

		}

		/// <summary>
		/// 判断对象是否为Int32类型的数字
		/// </summary>
		/// <param name="Expression"></param>
		/// <returns></returns>
		public static bool IsNumeric(string expression)
		{
			if (expression != null)
			{
				string str = expression;
				if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
				{
					if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 是否为Double类型
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static bool IsDouble(object expression)
		{
			if (expression != null)
				return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");

			return false;
		}

		/// <summary>
		/// 检测是否符合email格式
		/// </summary>
		/// <param name="strEmail">要判断的email字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsValidEmail(string strEmail)
		{
			return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
		}
		public static bool IsValidDoEmail(string strEmail)
		{
			return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		/// <summary>
		/// 检测是否是正确的Url
		/// </summary>
		/// <param name="strUrl">要验证的Url</param>
		/// <returns>判断结果</returns>
		public static bool IsURL(string strUrl)
		{
			return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
		}

		/// <summary>
		/// 将字符串转换为数组
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns>字符串数组</returns>
		public static string[] GetStrArray(string str)
		{
			return str.Split(new char[',']);
		}

		/// <summary>
		/// 将数组转换为字符串
		/// </summary>
		/// <param name="list">List</param>
		/// <param name="speater">分隔符</param>
		/// <returns>String</returns>
		public static string GetArrayStr(List<string> list, string speater)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				if (i == list.Count - 1)
				{
					sb.Append(list[i]);
				}
				else
				{
					sb.Append(list[i]);
					sb.Append(speater);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// object型转换为bool型
		/// </summary>
		/// <param name="strValue">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的bool类型结果</returns>
		public static bool StrToBool(object expression, bool defValue)
		{
			if (expression != null)
				return StrToBool(expression, defValue);

			return defValue;
		}

		/// <summary>
		/// string型转换为bool型
		/// </summary>
		/// <param name="strValue">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的bool类型结果</returns>
		public static bool StrToBool(string expression, bool defValue)
		{
			if (expression != null)
			{
				if (string.Compare(expression, "true", true) == 0)
					return true;
				else if (string.Compare(expression, "false", true) == 0)
					return false;
			}
			return defValue;
		}

		/// <summary>
		/// 将对象转换为Int32类型
		/// </summary>
		/// <param name="expression">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的int类型结果</returns>
		public static int ObjToInt(object expression, int defValue)
		{
			if (expression != null)
				return StrToInt(expression.ToString(), defValue);

			return defValue;
		}

		/// <summary>
		/// 将字符串转换为Int32类型
		/// </summary>
		/// <param name="expression">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的int类型结果</returns>
		public static int StrToInt(string expression, int defValue)
		{
			if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
				return defValue;

			int rv;
			if (Int32.TryParse(expression, out rv))
				return rv;

			return Convert.ToInt32(StrToFloat(expression, defValue));
		}

		/// <summary>
		/// Object型转换为decimal型
		/// </summary>
		/// <param name="strValue">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的decimal类型结果</returns>
		public static decimal ObjToDecimal(object expression, decimal defValue)
		{
			if (expression != null)
				return StrToDecimal(expression.ToString(), defValue);

			return defValue;
		}

		/// <summary>
		/// string型转换为decimal型
		/// </summary>
		/// <param name="strValue">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的decimal类型结果</returns>
		public static decimal StrToDecimal(string expression, decimal defValue)
		{
			if ((expression == null) || (expression.Length > 10))
				return defValue;

			decimal intValue = defValue;
			if (expression != null)
			{
				bool IsDecimal = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
				if (IsDecimal)
					decimal.TryParse(expression, out intValue);
			}
			return intValue;
		}

		/// <summary>
		/// Object型转换为float型
		/// </summary>
		/// <param name="strValue">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的int类型结果</returns>
		public static float ObjToFloat(object expression, float defValue)
		{
			if (expression != null)
				return StrToFloat(expression.ToString(), defValue);

			return defValue;
		}

		/// <summary>
		/// string型转换为float型
		/// </summary>
		/// <param name="strValue">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的int类型结果</returns>
		public static float StrToFloat(string expression, float defValue)
		{
			if ((expression == null) || (expression.Length > 10))
				return defValue;

			float intValue = defValue;
			if (expression != null)
			{
				bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
				if (IsFloat)
					float.TryParse(expression, out intValue);
			}
			return intValue;
		}

		/// <summary>
		/// 将对象转换为日期时间类型
		/// </summary>
		/// <param name="str">要转换的字符串</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的int类型结果</returns>
		public static DateTime StrToDateTime(string str, DateTime defValue)
		{
			if (!string.IsNullOrEmpty(str))
			{
				DateTime dateTime;
				if (DateTime.TryParse(str, out dateTime))
					return dateTime;
			}
			return defValue;
		}

		/// <summary>
		/// 将对象转换为日期时间类型
		/// </summary>
		/// <param name="str">要转换的字符串</param>
		/// <returns>转换后的int类型结果</returns>
		public static DateTime StrToDateTime(string str)
		{
			return StrToDateTime(str, DateTime.Now);
		}

		/// <summary>
		/// 将对象转换为日期时间类型
		/// </summary>
		/// <param name="obj">要转换的对象</param>
		/// <returns>转换后的int类型结果</returns>
		public static DateTime ObjectToDateTime(object obj)
		{
			return StrToDateTime(obj.ToString());
		}

		/// <summary>
		/// 将对象转换为日期时间类型
		/// </summary>
		/// <param name="obj">要转换的对象</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>转换后的int类型结果</returns>
		public static DateTime ObjectToDateTime(object obj, DateTime defValue)
		{
			return StrToDateTime(obj.ToString(), defValue);
		}

		/// <summary>
		/// 将对象转换为字符串
		/// </summary>
		/// <param name="obj">要转换的对象</param>
		/// <returns>转换后的string类型结果</returns>
		public static string ObjectToStr(object obj)
		{
			if (obj == null)
				return "";
			return obj.ToString().Trim();
		}
		#endregion

		#region 分割字符串
		/// <summary>
		/// 分割字符串
		/// </summary>
		public static string[] SplitString(string strContent, string strSplit)
		{
			if (!string.IsNullOrEmpty(strContent))
			{
				if (strContent.IndexOf(strSplit) < 0)
					return new string[] { strContent };

				return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
			}
			else
				return new string[0] { };
		}

		/// <summary>
		/// 分割字符串
		/// </summary>
		/// <returns></returns>
		public static string[] SplitString(string strContent, string strSplit, int count)
		{
			string[] result = new string[count];
			string[] splited = SplitString(strContent, strSplit);

			for (int i = 0; i < count; i++)
			{
				if (i < splited.Length)
					result[i] = splited[i];
				else
					result[i] = string.Empty;
			}

			return result;
		}
		#endregion

		#region 删除最后结尾的一个逗号
		/// <summary>
		/// 删除最后结尾的一个逗号
		/// </summary>
		public static string DelLastComma(string str)
		{
			if (str.Length < 1)
			{
				return "";
			}
			return str.Substring(0, str.LastIndexOf(","));
		}
		#endregion

		#region 删除最后结尾的指定字符后的字符
		/// <summary>
		/// 删除最后结尾的指定字符后的字符
		/// </summary>
		public static string DelLastChar(string str, string strchar)
		{
			if (string.IsNullOrEmpty(str))
				return "";
			if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
			{
				return str.Substring(0, str.LastIndexOf(strchar));
			}
			return str;
		}
		#endregion

		#region 生成指定长度的字符串
		/// <summary>
		/// 生成指定长度的字符串,即生成strLong个str字符串
		/// </summary>
		/// <param name="strLong">生成的长度</param>
		/// <param name="str">以str生成字符串</param>
		/// <returns></returns>
		public static string StringOfChar(int strLong, string str)
		{
			string ReturnStr = "";
			for (int i = 0; i < strLong; i++)
			{
				ReturnStr += str;
			}

			return ReturnStr;
		}
		#endregion

		#region 生成日期随机码
		/// <summary>
		/// 生成日期随机码
		/// </summary>
		/// <returns></returns>
		public static string GetRamCode()
		{
			#region
			return DateTime.Now.ToString("yyyyMMddHHmmssffff");
			#endregion
		}
		#endregion

		#region 生成随机字母或数字
		/// <summary>
		/// 生成随机数字
		/// </summary>
		/// <param name="length">生成长度</param>
		/// <returns></returns>
		public static string Number(int Length)
		{
			return Number(Length, false);
		}

		/// <summary>
		/// 生成随机数字
		/// </summary>
		/// <param name="Length">生成长度</param>
		/// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
		/// <returns></returns>
		public static string Number(int Length, bool Sleep)
		{
			if (Sleep)
				System.Threading.Thread.Sleep(3);
			string result = "";
			System.Random random = new Random();
			for (int i = 0; i < Length; i++)
			{
				result += random.Next(10).ToString();
			}
			return result;
		}
		/// <summary>
		/// 生成随机字母字符串(数字字母混和)
		/// </summary>
		/// <param name="codeCount">待生成的位数</param>
		public static string GetCheckCode(int codeCount)
		{
			string str = string.Empty;
			int rep = 0;
			long num2 = DateTime.Now.Ticks + rep;
			rep++;
			Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
			for (int i = 0; i < codeCount; i++)
			{
				char ch;
				int num = random.Next();
				if ((num % 2) == 0)
				{
					ch = (char)(0x30 + ((ushort)(num % 10)));
				}
				else
				{
					ch = (char)(0x41 + ((ushort)(num % 0x1a)));
				}
				str = str + ch.ToString();
			}
			return str;
		}
		/// <summary>
		/// 根据日期和随机码生成订单号
		/// </summary>
		/// <returns></returns>
		public static string GetOrderNumber()
		{
			string num = DateTime.Now.ToString("yyMMddHHmmss");//yyyyMMddHHmmssms
			return num + Number(4).ToString();
		}
		private static int Next(int numSeeds, int length)
		{
			byte[] buffer = new byte[length];
			System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();
			Gen.GetBytes(buffer);
			uint randomResult = 0x0;//这里用uint作为生成的随机数  
			for (int i = 0; i < length; i++)
			{
				randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
			}
			return (int)(randomResult % numSeeds);
		}
		#endregion







		#region TXT代码转换成HTML格式
		/// <summary>
		/// 字符串字符处理
		/// </summary>
		/// <param name="chr">等待处理的字符串</param>
		/// <returns>处理后的字符串</returns>
		/// //把TXT代码转换成HTML格式
		public static String ToHtml(string Input)
		{
			StringBuilder sb = new StringBuilder(Input);
			sb.Replace("&", "&amp;");
			sb.Replace("<", "&lt;");
			sb.Replace(">", "&gt;");
			sb.Replace("\r\n", "<br />");
			sb.Replace("\n", "<br />");
			sb.Replace("\t", " ");
			//sb.Replace(" ", "&nbsp;");
			return sb.ToString();
		}
		#endregion

		#region HTML代码转换成TXT格式
		/// <summary>
		/// 字符串字符处理
		/// </summary>
		/// <param name="chr">等待处理的字符串</param>
		/// <returns>处理后的字符串</returns>
		/// //把HTML代码转换成TXT格式
		public static String ToTxt(String Input)
		{
			StringBuilder sb = new StringBuilder(Input);
			sb.Replace("&nbsp;", " ");
			sb.Replace("<br>", "\r\n");
			sb.Replace("<br>", "\n");
			sb.Replace("<br />", "\n");
			sb.Replace("<br />", "\r\n");
			sb.Replace("&lt;", "<");
			sb.Replace("&gt;", ">");
			sb.Replace("&amp;", "&");
			return sb.ToString();
		}
		#endregion

		#region 检测是否有Sql危险字符
		/// <summary>
		/// 检测是否有Sql危险字符
		/// </summary>
		/// <param name="str">要判断字符串</param>
		/// <returns>判断结果</returns>
		public static bool IsSafeSqlString(string str)
		{
			return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
		}

		/// <summary>
		/// 检查危险字符
		/// </summary>
		/// <param name="Input"></param>
		/// <returns></returns>
		public static string Filter(string sInput)
		{
			if (sInput == null || sInput == "")
				return null;
			string sInput1 = sInput.ToLower();
			string output = sInput;
			string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
			if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
			{
				throw new Exception("字符串中含有非法字符!");
			}
			else
			{
				output = output.Replace("'", "''");
			}
			return output;
		}

		/// <summary> 
		/// 检查过滤设定的危险字符
		/// </summary> 
		/// <param name="InText">要过滤的字符串 </param> 
		/// <returns>如果参数存在不安全字符，则返回true </returns> 
		public static bool SqlFilter(string word, string InText)
		{
			if (InText == null)
				return false;
			foreach (string i in word.Split('|'))
			{
				if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
				{
					return true;
				}
			}
			return false;
		}
		#endregion

		#region 过滤特殊字符
		/// <summary>
		/// 过滤特殊字符
		/// </summary>
		/// <param name="Input"></param>
		/// <returns></returns>
		public static string Htmls(string Input)
		{
			if (Input != string.Empty && Input != null)
			{
				string ihtml = Input.ToLower();
				ihtml = ihtml.Replace("<script", "&lt;script");
				ihtml = ihtml.Replace("script>", "script&gt;");
				ihtml = ihtml.Replace("<%", "&lt;%");
				ihtml = ihtml.Replace("%>", "%&gt;");
				ihtml = ihtml.Replace("<$", "&lt;$");
				ihtml = ihtml.Replace("$>", "$&gt;");
				return ihtml;
			}
			else
			{
				return string.Empty;
			}
		}
		public static string FilterString(string Input)
		{
			if (Input != string.Empty && Input != null)
			{
				string ihtml = Input.ToLower();
				ihtml = ihtml.Replace("/", "");
				return ihtml;
			}
			else
			{
				return string.Empty;
			}
		}
		#endregion

		#region 检查是否为IP地址
		/// <summary>
		/// 是否为ip
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIP(string ip)
		{
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
		}
		#endregion








		#region 替换指定的字符串
		/// <summary>
		/// 替换指定的字符串
		/// </summary>
		/// <param name="originalStr">原字符串</param>
		/// <param name="oldStr">旧字符串</param>
		/// <param name="newStr">新字符串</param>
		/// <returns></returns>
		public static string ReplaceStr(string originalStr, string oldStr, string newStr)
		{
			if (string.IsNullOrEmpty(oldStr))
			{
				return "";
			}
			return originalStr.Replace(oldStr, newStr);
		}
		#endregion

		#region 显示分页
		/// <summary>
		/// 返回分页页码
		/// </summary>
		/// <param name="pageSize">页面大小</param>
		/// <param name="pageIndex">当前页</param>
		/// <param name="totalCount">总记录数</param>
		/// <param name="linkUrl">链接地址，__id__代表页码</param>
		/// <param name="centSize">中间页码数量</param>
		/// <returns></returns>
		public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
		{

			//计算页数
			if (totalCount < 1 || pageSize < 1)
			{
				return "";
			}
			int pageCount = totalCount / pageSize;
			if (pageCount < 1)
			{
				return "";
			}
			if (totalCount % pageSize > 0)
			{
				pageCount += 1;
			}
			if (pageCount <= 1)
			{
				return "";
			}
			StringBuilder pageStr = new StringBuilder();
			string pageId = "__id__";
			string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
			string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";
			string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
			string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

			if (pageIndex <= 1)
			{
				firstBtn = "<span class=\"disabled\">«上一页</span>";
			}
			if (pageIndex >= pageCount)
			{
				lastBtn = "<span class=\"disabled\">下一页»</span>";
			}
			if (pageIndex == 1)
			{
				firstStr = "<span class=\"current\">1</span>";
			}
			if (pageIndex == pageCount)
			{
				lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
			}
			int firstNum = pageIndex - (centSize / 2); //中间开始的页码
			if (pageIndex < centSize)
				firstNum = 2;
			int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
			if (lastNum >= pageCount)
				lastNum = pageCount - 1;
			pageStr.Append("<span>共" + totalCount + "记录</span>");
			pageStr.Append(firstBtn + firstStr);
			if (pageIndex >= centSize)
			{
				pageStr.Append("<span>...</span>\n");
			}
			for (int i = firstNum; i <= lastNum; i++)
			{
				if (i == pageIndex)
				{
					pageStr.Append("<span class=\"current\">" + i + "</span>");
				}
				else
				{
					pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
				}
			}
			if (pageCount - pageIndex > centSize - ((centSize / 2)))
			{
				pageStr.Append("<span>...</span>");
			}
			pageStr.Append(lastStr + lastBtn);
			return pageStr.ToString();
		}
		#endregion





		#region 操作权限菜单
		/// <summary>
		/// 获取操作权限
		/// </summary>
		/// <returns>Dictionary</returns>
		public static Dictionary<string, string> ActionType()
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("Show", "显示");
			dic.Add("View", "查看");
			dic.Add("Add", "添加");
			dic.Add("Edit", "修改");
			dic.Add("Delete", "删除");
			dic.Add("Audit", "审核");
			dic.Add("Reply", "回复");
			dic.Add("Confirm", "确认");
			dic.Add("Cancel", "取消");
			dic.Add("Invalid", "作废");
			dic.Add("Build", "生成");
			dic.Add("Instal", "安装");
			dic.Add("Unload", "卸载");
			dic.Add("Back", "备份");
			dic.Add("Restore", "还原");
			dic.Add("Replace", "替换");
			return dic;
		}
		public static Dictionary<string, string> ActionType2()
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("Show", "显示");
			dic.Add("View", "查看");
			dic.Add("Add", "添加");
			dic.Add("Edit", "修改");
			dic.Add("Delete", "删除");
			//dic.Add("Audit", "审核");
			//dic.Add("Reply", "回复");
			//dic.Add("Confirm", "确认");
			//dic.Add("Cancel", "取消");
			//dic.Add("Invalid", "作废");
			//dic.Add("Build", "生成");
			//dic.Add("Instal", "安装");
			//dic.Add("Unload", "卸载");
			//dic.Add("Back", "备份");
			//dic.Add("Restore", "还原");
			//dic.Add("Replace", "替换");
			return dic;
		}
		#endregion

		#region 替换URL
		/// <summary>
		/// 替换扩展名
		/// </summary>
		public static string GetUrlExtension(string urlPage, string staticExtension)
		{
			int indexNum = urlPage.LastIndexOf('.');
			if (indexNum > 0)
			{
				return urlPage.Replace(urlPage.Substring(indexNum), "." + staticExtension);
			}
			return urlPage;
		}
		/// <summary>
		/// 替换扩展名，如没有扩展名替换默认首页
		/// </summary>
		public static string GetUrlExtension(string urlPage, string staticExtension, bool defaultVal)
		{
			int indexNum = urlPage.LastIndexOf('.');
			if (indexNum > 0)
			{
				return urlPage.Replace(urlPage.Substring(indexNum), "." + staticExtension);
			}
			if (defaultVal)
			{
				if (urlPage.EndsWith("/"))
				{
					return urlPage + "index." + staticExtension;
				}
				else
				{
					return urlPage + "/index." + staticExtension;
				}
			}
			return urlPage;
		}
		#endregion

		/// <summary>
		/// 将对象转换为Int类型
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static int ObjToInt(object obj)
		{
			if (isNumber(obj))
			{
				return int.Parse(obj.ToString());
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// 判断对象是否可以转成int型
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool isNumber(object o)
		{
			int tmpInt;
			if (o == null)
			{
				return false;
			}
			if (o.ToString().Trim().Length == 0)
			{
				return false;
			}
			if (!int.TryParse(o.ToString(), out tmpInt))
			{
				return false;
			}
			else
			{
				return true;
			}
		}



		#region 时分秒转秒
		public static int DatetimeToSecond(string date)
		{
			int s = 0;
			try
			{
				string[] arrs = date.Split(':');
				if (arrs.Length == 3)
				{
					s = Convert.ToInt32(arrs[0]) * 3600 + Convert.ToInt32(arrs[1]) * 60 + Convert.ToInt32(arrs[2]);
				}
				else if (arrs.Length == 2)
				{
					s = Convert.ToInt32(arrs[0]) * 60 + Convert.ToInt32(arrs[1]);
				}
				else if (arrs.Length == 1)
				{
					s = Convert.ToInt32(arrs[0]);
				}
			}
			catch
			{

			}


			return s;
		}
		#endregion
		#region 秒转时分秒
		public static string SecondToDatetime(int theTime)
		{
			string s = string.Empty;
			int middle = 0; // 分
			int hour = 0; // 小时
			try
			{
				if (theTime > 60)
				{
					middle = Convert.ToInt32(theTime / 60);
					theTime = Convert.ToInt32(theTime % 60);
					if (middle > 60)
					{
						hour = Convert.ToInt32(middle / 60);
						middle = Convert.ToInt32(middle % 60);
					}
				}

				s = hour.ToString().PadLeft(2, '0') + ":" + middle.ToString().PadLeft(2, '0') + ":" + theTime.ToString().PadLeft(2, '0');

			}
			catch
			{

			}


			return s;
		}
		#endregion
		public static string[] ZiMuArr()
		{
			string ZimuStr = "A,B,C,D,E,G,H,I,G,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
			return ZimuStr.Split(',');
		}

		public static int ChkShangXinStatus(int NewTime, DateTime dTime)
		{
			int status = 0;
			try
			{
				DateTime nowtime = DateTime.Now.AddHours(-NewTime);
				if (DateTime.Compare(dTime, nowtime) > 0)
				{
					status = 1;
				}
			}
			catch
			{
				status = 0;
			}

			return status;

		}


		public static string ChkFileExt(string filepath)
		{
			string files = filepath.ToString().ToLower();
			string ext = "e_doc";
			if (files.Contains(".doc") || files.Contains(".docx"))
			{
				ext = "e_doc";
			}
			else if (files.Contains(".pdf"))
			{
				ext = "e_pdf";
			}
			else if (files.Contains(".xls") || files.Contains(".xlsx"))
			{
				ext = "e_xls";
			}
			else if (files.Contains(".ppt") || files.Contains(".pptx"))
			{
				ext = "e_ppt";
			}
			else if (files.Contains(".rar") || files.Contains(".zip"))
			{
				ext = "e_rar";
			}
			return ext;
		}



		public static decimal GetArrMedian(decimal[] list)
		{
			decimal mediam = 0;
			if (list.Length > 0)
			{

				var len = list.Length;
				if (len == 1)
				{
					mediam = list[0];
				}
				else
				{
					var r = len / 2;

					if (len % 2 == 0)
					{
						mediam = (list[r - 1] + list[r]) / 2;
					}
					else if (len % 2 != 0)
					{
						mediam = list[r];
					}
					else
					{
						mediam = 0;
					}
				}
			}
			return mediam;
		}

		#region 截取字符长度
		/// <summary>
		/// 截取字符长度
		/// </summary>
		/// <param name="inputString">字符</param>
		/// <param name="len">长度</param>
		/// <returns></returns>
		public static string CutString(string inputString, int len)
		{
			if (string.IsNullOrEmpty(inputString))
				return "";
			inputString = DropHTML(inputString);
			ASCIIEncoding ascii = new ASCIIEncoding();
			int tempLen = 0;
			string tempString = "";
			byte[] s = ascii.GetBytes(inputString);
			for (int i = 0; i < s.Length; i++)
			{
				if ((int)s[i] == 63)
				{
					tempLen += 2;
				}
				else
				{
					tempLen += 1;
				}

				try
				{
					tempString += inputString.Substring(i, 1);
				}
				catch
				{
					break;
				}

				if (tempLen > len)
					break;
			}
			//如果截过则加上半个省略号 
			byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
			if (mybyte.Length > len)
				tempString += "";
			return tempString;
		}
		#endregion

		#region 清除HTML标记
		public static string DropHTML(string Htmlstring)
		{
			if (string.IsNullOrEmpty(Htmlstring)) return "";
			//删除脚本  
			Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			//删除HTML  
			Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(emsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace("<", "");
			Htmlstring.Replace(">", "");
			Htmlstring.Replace("\r\n", "");
			//Htmlstring = Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}
		public static string DropHTML2
		  (string Htmlstring)
		{
			if (string.IsNullOrEmpty(Htmlstring)) return "";
			//删除脚本  
			Htmlstring = Regex.Replace(Htmlstring, @"<span[^>]*?>.*?</span>", "", RegexOptions.IgnoreCase);
			//Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}
		public static string DropHTMLP(string Htmlstring)
		{
			if (string.IsNullOrEmpty(Htmlstring)) return "";
			//删除脚本  
			//Htmlstring = Regex.Replace(Htmlstring, "\\sclass=[\'\"][^\'\"]*[\'\"]", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			//Htmlstring = Regex.Replace(Htmlstring, "\\sstyle=[\'\"][^\'\"]*[\'\"]", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"</?[span|p|P|br][^>]*>", "", RegexOptions.IgnoreCase);
			//Htmlstring = Regex.Replace(Htmlstring, @"<(?!(p)\s+)[^<>]*?>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			//Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
			//Htmlstring.Replace("<", "");
			//Htmlstring.Replace(">", "");
			//Htmlstring.Replace("\r\n", "");
			//Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}

		public static string noTagHtml(string html)
		{
			if(string.IsNullOrEmpty(html))
			{
				return "";
			}	
			string pattern = @"<(?!p\b)([^>]+)>(?:[^<]|<(?!\/?\1\b))*<\/\1>|<[^>]+>";

			return Regex.Replace(html, pattern, string.Empty);

		}

		#endregion

		#region 清除HTML标记且返回相应的长度
		public static string DropHTML(string Htmlstring, int strLen)
		{
			return CutString(DropHTML(Htmlstring), strLen);
		}
		#endregion


		public static bool IsMorning(string timeString)
		{
			try
			{
				// 定义时间格式
				string format = "HH:mm";

				// 解析时间字符串
				DateTime time = DateTime.ParseExact(timeString, format, null);

				// 获取小时部分
				int hours = time.Hour;

				// 判断是上午还是下午
				return hours < 12;
			}
			catch
			{
				return false;
			}

		}


		/// <summary>
		/// 密码MD5处理
		/// </summary>
		/// <param name="password"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string EncryptUserPassword(string password, string salt)
		{
			string md5Password = SecurityHelper.MD5ToHex(password);
			string encryptPassword = SecurityHelper.MD5ToHex(md5Password.ToLower() + salt).ToLower();
			return encryptPassword;
		}

		/// <summary>
		/// 密码盐
		/// </summary>
		/// <returns></returns>
		public static string GetPasswordSalt()
		{
			return new Random().Next(1, 100000).ToString();
		}
	}

}
