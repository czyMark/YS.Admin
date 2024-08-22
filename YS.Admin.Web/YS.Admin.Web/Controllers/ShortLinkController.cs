using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Result;
using YS.Admin.Util.Model;
using YS.Admin.Util;
using System.Net.Http;
using System.IO;
using System.Net;
using NPOI.HPSF;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Html;
using Org.BouncyCastle.Utilities.Net;
using Microsoft.AspNetCore.Authorization;
using YS.Admin.Cache.Factory;
using YS.Admin.Business.ShortLInk;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using YS.Admin.Entity.ShortLInk;
using Microsoft.AspNetCore.Http;
using YS.Admin.Util.Extension;
using YS.Admin.Web.Hubs;

namespace YS.Admin.Web.ShortLink
{
    public class ShortLinkController : Controller
    {
        LinkLogBLL linkLog = new LinkLogBLL();
        LinkBLL link = new LinkBLL();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShortLinkController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        //public async Task<IActionResult> DemoSend(string key)
        //{
        //    //SignalRHup signalRHup = new SignalRHup();
        //    //await  signalRHup.SendMessage("1", key);
        //    ViewBag.Message = "没有找到界面";
        //    return View("~/Areas/System/Views/Home/Error.cshtml");
        //}

        public async Task<IActionResult> Demo()
        { 
            return View();
        }


        #region 短链重定向服务器

        public async Task<IActionResult> LoadPage(string key)
        {
            ViewBag.Url = key;
            return View();
        }

        [HttpGet("{key?}")]
        [AllowAnonymous]
        public async Task<IActionResult> Access(string key)
        {

            if (key == null)
                return Redirect("/System/Home/Index");

            var url = CacheFactory.Cache.GetCache<string>(key);
            if (!string.IsNullOrWhiteSpace(url))
            {

                //记录日志及对应的访问量+1
                var linklist = await link.GetList(new Model.Param.ShortLInk.LinkListParam()
                {
                    ShortUrl = GlobalContext.SystemConfig.SiteWeb + "/" + key,
                    OriginUrl = url
                });
                linklist.Data.FirstOrDefault().AccessCount = linklist.Data.FirstOrDefault().AccessCount + 1;
                await link.SaveForm(linklist.Data.FirstOrDefault());

                // 获取HttpContext
                var model = new LinkLogEntity
                {
                    LinkId = linklist.Data.FirstOrDefault().Id,
                    Ip = _httpContextAccessor.HttpContext.GetIpAddress(),
                    OsType = (int)_httpContextAccessor.HttpContext.GetSystemType(),
                    BrowserType = (int)_httpContextAccessor.HttpContext.GetBrowserType()
                };
                await linkLog.SaveForm(model);



                if (url.Contains(GlobalContext.SystemConfig.SiteWeb))
                {
                    //如果是站内
                    return Redirect("/ShortLink/LoadPage?key=" + url);
                }
                else
                {
                    //如果是站外 未了解决跨域的问题 所以暂时就直接跳转
                    return Redirect(url);
                }

            }
            else
            {
                //检查数据库里有没有
                var linklist = await link.GetList(new Model.Param.ShortLInk.LinkListParam()
                {
                    ShortUrl = GlobalContext.SystemConfig.SiteWeb + "/" + key
                });
                if (linklist.Data.FirstOrDefault() != null)
                {
                    //添加key
                    CacheFactory.Cache.SetCache(linklist.Data.FirstOrDefault().OriginUrl, linklist.Data.FirstOrDefault().ShortUrl);
                    CacheFactory.Cache.SetCache(linklist.Data.FirstOrDefault().ShortUrl, linklist.Data.FirstOrDefault().OriginUrl);
                    return Redirect(linklist.Data.FirstOrDefault().ShortUrl);
                }
            }

            //防止缓存击穿
            CacheFactory.Cache.SetCache(key, string.Empty);

            ViewBag.Message = "没有找到界面";
            return View("~/Areas/System/Views/Home/Error.cshtml");
        }

        #endregion
    }
}
