using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YS.Admin.Business.BlogManage;
using YS.Admin.Web.Controllers;
namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{

    [Area("BlogWebsite")]
    public class HomeController : BaseController
    {
        private BlogArticleBLL blogArticleBLL = new BlogArticleBLL();
        // GET: Home
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        public async Task<ActionResult> Index()
        {
            //查询出网站信息
           await GetGlobalInfo();


            var t = await blogArticleBLL.GetList(new Model.Param.BlogManage.BlogArticleListParam() { });
            ViewBag.HotArtileList = t.Data.OrderByDescending(x => x.ReadNum).Take(6).ToList();
            //model.GetWebSiteInfo()
            return View();
        }


        public async Task GetGlobalInfo()
        {

            //查询网站全局信息
            var siteInfo = await blogConfigBLL.GetEntity(744688942032359424);
            ViewBag.Site = siteInfo.Data;




            string? userSessionCookie;
            // 尝试读取名为"UserSession"的Cookie
            if (Request.Cookies.TryGetValue("UserSession", out userSessionCookie))
            {
                // 将Cookie值传递给视图
                ViewBag.UserSession = userSessionCookie;
            }
            else
            {
                // 如果Cookie不存在，可以设置一个默认值或进行其他操作
                ViewBag.UserSession = "登录";
            }
        }
    }
}