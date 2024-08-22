using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YS.Admin.Util.Browser;

namespace YS.Admin.Util.Extension
{
    public enum BrowserType
    {
        [Description("其他浏览器")]
        Default,
        [Description("Internet Explorer浏览器")]
        IE,
        [Description("火狐浏览器")]
        Firefox,
        [Description("谷歌浏览器")]
        Chrome,
        [Description("Safari浏览器")]
        Safari,
        [Description("微信内置浏览器")]
        WeChat,
        [Description("QQ浏览器")]
        QQ
    }

    public enum SystemType
    {
        [Description("其他系统")]
        Default,
        [Description("Windows系统")]
        Windows,
        [Description("MacOS系统")]
        MacOS,
        [Description("Linux系统")]
        Linux,
        [Description("安卓系统")]
        Android,
        [Description("IOS系统")]
        IOS
    }

    public static partial class Extensions 
    {
        public static string GetHttpParameters(this HttpContext context)
        {
            string result = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(context.Request.ContentType) && context.Request.ContentType.Contains("multipart/form-data"))
                {
                    IFormFileCollection files = context.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        string text = string.Join(",", files.Select((IFormFile a) => a.FileName));
                        return result = "上传文件：" + text;
                    }
                }

                NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(context.Request.QueryString.ToString());
                HttpRequest request = context.Request;
                string method = request.Method;
                if (!(method == "POST"))
                {
                    if (method == "GET")
                    {
                        IDictionary<string, string> dictionary = new Dictionary<string, string>();
                        for (int i = 0; i < nameValueCollection.Count; i++)
                        {
                            string text2 = nameValueCollection.Keys[i];
                            dictionary.Add(text2, nameValueCollection[text2]);
                        }

                        IEnumerator<KeyValuePair<string, string>> enumerator = ((IEnumerable<KeyValuePair<string, string>>)new SortedDictionary<string, string>(dictionary)).GetEnumerator();
                        StringBuilder stringBuilder = new StringBuilder();
                        while (enumerator.MoveNext())
                        {
                            string key = enumerator.Current.Key;
                            string value = enumerator.Current.Value;
                            if (!string.IsNullOrEmpty(key))
                            {
                                stringBuilder.Append(key).Append("=").Append(value)
                                    .Append("&");
                            }
                        }

                        result = stringBuilder.ToString().TrimEnd('&');
                    }
                    else
                    {
                        result = string.Empty;
                    }
                }
                else
                {
                    using MemoryStream memoryStream = new MemoryStream();
                    using StreamReader streamReader = new StreamReader(memoryStream);
                    request.Body.Seek(0L, SeekOrigin.Begin);
                    request.Body.CopyTo(memoryStream);
                    memoryStream.Seek(0L, SeekOrigin.Begin);
                    result = streamReader.ReadToEnd();
                    request.Body.Position = 0L;
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string GetIpAddress(this HttpContext context)
        {
            string text = string.Empty;
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                text = context.Request.Headers["X-Forwarded-For"];
            }

            if (string.IsNullOrEmpty(text))
            {
                text = context.Connection.RemoteIpAddress.ToString();
            }

            return IPAddress.Parse(text).ToString();
        }

        public static BrowserType GetBrowserType(this HttpContext context)
        {
            string text = context.Request.Headers["User-Agent"];
            if (text != null)
            {
                if (text.Contains("MSIE"))
                {
                    return BrowserType.IE;
                }

                if (text.Contains("QQBrowser"))
                {
                    return BrowserType.QQ;
                }

                if (text.Contains("MicroMessenger"))
                {
                    return BrowserType.WeChat;
                }

                if (text.Contains("Firefox"))
                {
                    return BrowserType.Firefox;
                }

                if (text.Contains("Safari"))
                {
                    return BrowserType.Safari;
                }

                if (text.Contains("Chrome"))
                {
                    return BrowserType.Chrome;
                }
            }

            return BrowserType.Default;
        }

        public static SystemType GetSystemType(this HttpContext context)
        {
            string text = context.Request.Headers["User-Agent"];
            if (text != null)
            {
                if (text.Contains("Windows NT"))
                {
                    return SystemType.Windows;
                }

                if (text.Contains("Macintosh"))
                {
                    return SystemType.MacOS;
                }

                if (text.Contains("Linux"))
                {
                    return SystemType.Linux;
                }

                if (text.Contains("Android"))
                {
                    return SystemType.Android;
                }

                string text2 = text;
                if (text2.Contains("iPhone") || text2.Contains("iPad"))
                {
                    return SystemType.IOS;
                }
            }

            return SystemType.Default;
        }
    }
}
