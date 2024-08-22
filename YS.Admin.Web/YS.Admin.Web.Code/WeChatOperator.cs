using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Cache.Factory;

namespace YS.Admin.Web.Code
{
	public class WeChatOperator
	{
		public static WeChatOperator Instance
		{
			get { return new WeChatOperator(); }
		}

		private string LoginProvider = GlobalContext.Configuration.GetSection("SystemConfig:LoginProvider").Value;
		private string TokenName = "WeChatToken"; //用户openid

		public async Task AddCurrent(string token)
		{
			switch (LoginProvider)
			{
				case "Cookie":
					new CookieHelper().WriteCookie(TokenName, token, 1440);
					break;

				case "Session":
					new SessionHelper().WriteSession(TokenName, token);
					break;

				case "WebApi":
					OperatorInfo user = await new DataRepository().GetUserByToken(token);
					if (user != null)
					{
						CacheFactory.Cache.SetCache(token, user);
					}
					break;

				default:
					throw new Exception("未找到LoginProvider配置");
			}
		}

		/// <summary>
		/// Api接口需要传入apiToken
		/// </summary>
		/// <param name="apiToken"></param>
		public void RemoveCurrent(string apiToken = "")
		{
			switch (LoginProvider)
			{
				case "Cookie":
					new CookieHelper().RemoveCookie(TokenName);
					break;

				case "Session":
					new SessionHelper().RemoveSession(TokenName);
					break;

				case "WebApi":
					CacheFactory.Cache.RemoveCache(apiToken);
					break;

				default:
					throw new Exception("未找到LoginProvider配置");
			}
		}

		/// <summary>
		/// Api接口需要传入apiToken
		/// </summary>
		/// <param name="apiToken"></param>
		/// <returns></returns>
		public async Task<WeChatOperatorInfo> Current(string apiToken = "")
		{
			IHttpContextAccessor hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
			WeChatOperatorInfo user = null;
			string token = string.Empty;
			switch (LoginProvider)
			{
				case "Cookie":
					if (hca.HttpContext != null)
					{
						token = new CookieHelper().GetCookie(TokenName);
					}
					break;

				case "Session":
					if (hca.HttpContext != null)
					{
						token = new SessionHelper().GetSession(TokenName);
					}
					break;

				case "WebApi":
					token = apiToken;
					break;
			}
			if (string.IsNullOrEmpty(token))
			{
				return user;
			}
			token = token.Trim('"');
			user = CacheFactory.Cache.GetCache<WeChatOperatorInfo>(token);
			if (user == null)
			{
				user = await new DataRepository().GetWeChatUserByToken(token);
				if (user != null)
				{
					CacheFactory.Cache.SetCache(token, user, DateTime.Now.AddDays(1));
				}
			}
			return user;
		}



		/// <summary>
		/// Api接口需要传入用户
		/// </summary>
		/// <param name="UToken"></param>
		/// <returns></returns>
		public async Task<WeChatOperatorInfo> CurrentUser(string UToken = "")
		{
			IHttpContextAccessor hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
			WeChatOperatorInfo user = null;
			string token = string.Empty;
			token = UToken;

			if (string.IsNullOrEmpty(token))
			{
				return user;
			}
			token = token.Trim('"');
			user = CacheFactory.Cache.GetCache<WeChatOperatorInfo>(token);
			if (user == null)
			{
				user = await new DataRepository().GetWeChatUserByToken(token);
				if (user != null)
				{
					CacheFactory.Cache.SetCache(token, user, DateTime.Now.AddDays(1));
				}
			}
			return user;
		}

	}
}
