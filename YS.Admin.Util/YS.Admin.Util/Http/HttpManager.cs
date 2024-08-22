using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS;

namespace YS.Admin.Util.Http
{
	public static class HttpManager
	{
		public static async Task<string> SendAsync(
			HttpMethod method,
			string url,
			string postData = null,
			int timeOut = 180,
			Dictionary<string, string> headers = null)
		{
			var handler = new HttpClientHandler();
			var httpclient = new HttpClient(handler);
			 
			//var content = new StringContent(postData ?? "");
			//var request = new HttpRequestMessage(method, url)
			//{
			//    Content = content
			//};

			HttpContent httpContent = new StringContent(postData, Encoding.UTF8, "application/json");

			if (headers != null)
			{
				foreach (var header in headers)
				{
					httpclient.DefaultRequestHeaders.Add(header.Key, header.Value);
				}
			}
			try
			{
				httpclient.Timeout = TimeSpan.FromSeconds(timeOut);
				HttpResponseMessage response = null;
				if (method == HttpMethod.Post)
				{
					response = await httpclient.PostAsync(url, httpContent);
				}
				else
				{
					response = await httpclient.GetAsync(url);
				}
				string result;
				if (response.StatusCode == HttpStatusCode.OK)
				{
					result = await response.Content.ReadAsStringAsync();
				}
				else
				{
					result = response.ToString();
				}
				Console.WriteLine("返回：" + result);
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ex.Message;
			}
		}
	}
}
