using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using YS.Admin.Business.BlogManage;
using YS.Admin.Business.OrganizationManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Enum;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class AccountController : BaseController
    {
        // GET: About
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        private LogLoginBLL logLoginBLL = new LogLoginBLL();

        private QqUserBLL qqUserBLL = new QqUserBLL();



        public ActionResult Index()
        {
            //验证码
            GetGlobalInfo();
            return View();
        }


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
            TData<QqUserEntity> userObj = await qqUserBLL.CheckLogin(userName, password, (int)PlatformEnum.Web);
            if (userObj.Tag == 0)
            {
                obj.Message = userObj.Message;
                return Json(obj);
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
            SetLoginCookie(userName, userObj.Data.OpenId);

            obj.Tag = userObj.Tag;
            obj.Message = userObj.Message;
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CryptoLoginJson(string Crypto)
        {
            TData obj = new TData();
            if (string.IsNullOrEmpty(Crypto))
            {
                obj.Message = "无法识别";
                return Json(obj);
            }
            TData<QqUserEntity> userObj = await qqUserBLL.CheckLogin(Crypto);

             //await qqUserBLL.CheckLogin(userName, password, (int)PlatformEnum.Web);
            if (userObj.Tag == 0)
            {
                obj.Message = userObj.Message;
                return Json(obj);
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
            SetLoginCookie(userObj.Data.NickName, userObj.Data.OpenId);

            obj.Tag = userObj.Tag;
            obj.Message = userObj.Message;
            return Json(obj);
        }
        /// <summary>
        /// 第三方登录
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<JsonResult> OtherLogin(string type)
        //{
        //    //验证验证码是否正确


        //    //1 验证账户是否可用
        //    //验证账户用户名密码是否正确
        //string state = DateTime.Now.ToString("yyyyMMddHHmmssffff");  //client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。
        //string url = QQLoginHelper.CreateAuthorizeUrl(state);
        //RouteValueDictionary routeValue = RouteData.Route.GetRouteData(this.HttpContext).Values;
        //Session["QQLoginState"] = state;    //记录client端状态值
        //    Session["BeforeLoginUrl"] = Request.UrlReferrer;    //记录登陆之前的URL，登陆成功后返回
        //    SetLoginCookie("");
        //    GetGlobalInfo(); TData<string> result = new TData<string>();
        //    result.Tag = 1;
        //    result.Message = "登录成功";
        //    return Json(result.Data);
        //}


        [HttpPost]
        public async Task<JsonResult> LogoutJson()
        {
            HttpContext.Response.Cookies.Delete("UserSession");
            HttpContext.Response.Cookies.Delete("openid");
            TData<string> result = new TData<string>();
            result.Tag = 1;
            result.Message = "退出登录成功";
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> RegisterJson(string userName, string password, string captchaCode)
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

            QqUserEntity qqUserEntity = new QqUserEntity();
            qqUserEntity.NickName = userName;
            qqUserEntity.Password = password; 



            await qqUserBLL.SaveForm(qqUserEntity, true);

            //1 查询博客关于信息
            //验证账户名是否唯一
            // 


            obj.Tag = 1;
            obj.Message = "注册成功";
            return Json(obj);
        }

        public async void GetGlobalInfo()
        {
            // 尝试读取名为"UserSession"的Cookie
            if (Request.Cookies.TryGetValue("UserSession", out string userSessionCookie))
            {
                // 将Cookie值传递给视图
                ViewBag.UserSession = userSessionCookie;
            }
            else
            {
                // 如果Cookie不存在，可以设置一个默认值或进行其他操作
                ViewBag.UserSession = "登录";
            }

            //todo:可以优化去缓存中读
            //查询网站全局信息
            var siteInfo = await blogConfigBLL.GetEntity(744688942032359424);
            ViewBag.Site = siteInfo.Data;
        }

        private void SetLoginCookie(string userName, string openId)
        {

            // 设置cookie的值
            var cookieValue = userName;
            var cookieopenIdValue = openId;

            // 设置cookie的选项
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1), // Cookie有效期为1天
                HttpOnly = false, // 仅服务器可以访问cookie
                Secure = false, // 仅通过HTTPS传输cookie
                IsEssential = true, // Cookie是否是必需的
                SameSite = SameSiteMode.Strict // 同站策略，严格模式
            };

            // 将cookie添加到响应中
            HttpContext.Response.Cookies.Append("UserSession", cookieValue, cookieOptions);
            HttpContext.Response.Cookies.Append("openid", cookieopenIdValue, cookieOptions);
        }

    }
}