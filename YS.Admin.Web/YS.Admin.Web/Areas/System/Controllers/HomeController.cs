using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Business.OrganizationManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Enum;
using YS.Admin.IdGenerator;
using YS.Admin.Model.Result;
using YS.Admin.Util.Extension;
using YS.Admin.Web.Code;
using YS.Admin.Util.Model;
using YS.Admin.Util;
using YS.Admin.Entity.OrganizationManage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using YS.Admin.Web.Controllers;
using Aliyun.OSS;
using callbackserver;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace YS.Admin.Web.Areas.System.Controllers
{
	[Area("System")]
	public class HomeController : BaseController
	{
		private MenuBLL menuBLL = new MenuBLL();
		private UserBLL userBLL = new UserBLL();
		private LogLoginBLL logLoginBLL = new LogLoginBLL();
		private MenuAuthorizeBLL menuAuthorizeBLL = new MenuAuthorizeBLL();

		#region 视图功能
		[HttpGet]
		[AuthorizeFilter]
		public async Task<IActionResult> Index()
		{
			OperatorInfo operatorInfo = await Operator.Instance.Current();

			TData<List<MenuEntity>> objMenu = await menuBLL.GetList(null);
			List<MenuEntity> menuList = objMenu.Data;
			menuList = menuList.Where(p => p.MenuStatus == StatusEnum.Yes.ParseToInt()).ToList();

			if (operatorInfo.IsSystem != 1)
			{
				TData<List<MenuAuthorizeInfo>> objMenuAuthorize = await menuAuthorizeBLL.GetAuthorizeList(operatorInfo);
				List<long?> authorizeMenuIdList = objMenuAuthorize.Data.Select(p => p.MenuId).ToList();
				menuList = menuList.Where(p => authorizeMenuIdList.Contains(p.Id)).ToList();
			}

			ViewBag.MenuList = menuList;
			ViewBag.OperatorInfo = operatorInfo;
			return View();
		}

		[HttpGet]
		public IActionResult Welcome()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (GlobalContext.SystemConfig.Demo)
			{
				ViewBag.UserName = "admin";
				ViewBag.Password = "123456";
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> LoginOff()
		{
			#region 退出系统
			OperatorInfo user = await Operator.Instance.Current();
			if (user != null)
			{
				// 如果不允许同一个用户多次登录，当用户登出的时候，就不在线了
				if (!GlobalContext.SystemConfig.LoginMultiple)
				{
					await userBLL.UpdateUser(new UserEntity { Id = user.UserId, IsOnline = 0 });
				}

				// 登出日志
				await logLoginBLL.SaveForm(new LogLoginEntity
				{
					LogStatus = OperateStatusEnum.Success.ParseToInt(),
					Remark = "退出系统",
					IpAddress = NetHelper.Ip,
					IpLocation = string.Empty,
					Browser = NetHelper.Browser,
					OS = NetHelper.GetOSVersion(),
					ExtraRemark = NetHelper.UserAgent,
					BaseCreatorId = user.UserId
				});

				Operator.Instance.RemoveCurrent();
				new CookieHelper().RemoveCookie("RememberMe");
			}
			#endregion
			return View(nameof(Login));
		}

		[HttpGet]
		public IActionResult NoPermission()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Error(string message)
		{
			ViewBag.Message = message;
			return View();
		}

        [HttpGet]
        public IActionResult Announcement()
        {
            return View();
        }
        [HttpGet]
		public IActionResult Skin()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> LockScreen()
		{
            OperatorInfo operatorInfo = await Operator.Instance.Current();
			ViewBag.OperatorInfo = operatorInfo;


			await userBLL.LockJson();


            return View();
        }
		#endregion

		#region 获取数据
		public IActionResult GetCaptchaImage()
		{
			string sessionId = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>().HttpContext.Session.Id;

			Tuple<string, int> captchaCode = CaptchaHelper.GetCaptchaCode();
			byte[] bytes = CaptchaHelper2.CreateCaptchaImage(captchaCode.Item1);
			new SessionHelper().WriteSession("CaptchaCode", captchaCode.Item2);
			return File(bytes, @"image/jpeg");
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cid"></param>
		/// <param name="type">1 imgage,2 mp4, 3,mp3 </param>
		/// <returns></returns>
		public IActionResult GetPolicyToken(long cid, int filetype)
		{
			string dirpath = "images/";
			if (filetype == 2)
			{
				dirpath = "mp4/";
			}
			else if (filetype == 3)
			{
				dirpath = "mp3/";
			}
			var expireDateTime = DateTime.Now.AddDays(1);
			string accessKeyId = GlobalContext.SystemConfig.OSSAccessKeyId;// "LTAI5t6ND4esp6unRbkNTqdA";
			string accessKeySecret = GlobalContext.SystemConfig.OSSAccessKeySecret;// "7D1dL6K4J0IrrTzK1qXavogUs4exuw";
			string host = "https://" + GlobalContext.SystemConfig.OSSBucketName + "." + GlobalContext.SystemConfig.OSSEndpoint + "";

			#region 2024-04-03
			var ossClient = new OssClient("http://" + GlobalContext.SystemConfig.OSSEndpoint, accessKeyId, accessKeySecret);
			var config = new PolicyConditions();
			config.AddConditionItem(PolicyConditions.CondContentLengthRange, 1, 1024L * 1024 * 1024 * 5);// 文件大小范围：单位byte
			config.AddConditionItem(MatchMode.StartWith, PolicyConditions.CondKey, dirpath);
			var expire = DateTimeOffset.Now.AddMinutes(30);// 过期时间
														   // 生成 Policy，并进行 Base64 编码
			var policy = ossClient.GeneratePostPolicy(expire.LocalDateTime, config);
			var policyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(policy));

			var callbackUrl = GlobalContext.SystemConfig.SiteWeb + "/Web/PostCallback";

			var callback = new CallbackParam();
			callback.callbackUrl = callbackUrl;
			callback.callbackBody = "filename=${object}&size=${size}&mimeType=${mimeType}&height=${imageInfo.height}&width=${imageInfo.width}&cid=" + cid + "&filetype=" + filetype;
			callback.callbackBodyType = "application/x-www-form-urlencoded";

			var callback_string = JsonConvert.SerializeObject(callback);
			var callback_string_base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(callback_string));// EncodeBase64("utf-8", callback_string);



			// 计算签名
			var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(accessKeySecret));
			var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(policyBase64));
			var sign = Convert.ToBase64String(bytes);
			#endregion
			var policyToken = new PolicyToken();

			policyToken.accessid = accessKeyId;
			policyToken.host = host;
			policyToken.policy = policyBase64;
			policyToken.signature = sign;
			policyToken.expire = DateTimeHelper.GetUnixTimeStamp2(expireDateTime);
			policyToken.callback = callback_string_base64;
			policyToken.dir = dirpath;


			return Json(policyToken);
		}
		#endregion

		#region 提交数据
		[HttpPost]
		public async Task<IActionResult> LoginJson(string userName, string password, string captchaCode)
		{
			TData obj = new TData();
			if (string.IsNullOrEmpty(captchaCode))
			{
				obj.Message = "验证码不能为空";
				return Json(obj);
			}
			if (captchaCode != new SessionHelper().GetSession("CaptchaCode").ToString())
			{
				obj.Message = "验证码错误，请重新输入";
				return Json(obj);
			}
			TData<UserEntity> userObj = await userBLL.CheckLogin(userName, password, (int)PlatformEnum.Web);
			if (userObj.Tag == 1)
			{
				await new UserBLL().UpdateUser(userObj.Data);
				await Operator.Instance.AddCurrent(userObj.Data.WebToken);
			}

			string ip = NetHelper.Ip;
			string browser = NetHelper.Browser;
			string os = NetHelper.GetOSVersion();
			string userAgent = NetHelper.UserAgent;

			Action taskAction = async () =>
			{
				LogLoginEntity logLoginEntity = new LogLoginEntity
				{
					LogStatus = userObj.Tag == 1 ? OperateStatusEnum.Success.ParseToInt() : OperateStatusEnum.Fail.ParseToInt(),
					Remark = userObj.Message,
					IpAddress = ip,
					IpLocation = IpLocationHelper.GetIpLocation(ip),
					Browser = browser,
					OS = os,
					ExtraRemark = userAgent,
					BaseCreatorId = userObj.Data?.Id
				};

				// 让底层不用获取HttpContext
				logLoginEntity.BaseCreatorId = logLoginEntity.BaseCreatorId ?? 0;

				await logLoginBLL.SaveForm(logLoginEntity);
			};
			AsyncTaskHelper.StartTask(taskAction);

			obj.Tag = userObj.Tag;
			obj.Message = userObj.Message;
			return Json(obj);
		}
		#endregion
	}
}
