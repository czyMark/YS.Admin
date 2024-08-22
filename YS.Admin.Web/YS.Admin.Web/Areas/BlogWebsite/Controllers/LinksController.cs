
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Business.BlogManage;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class LinksController : BaseController
    {
        // GET: Links
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        private LinksBLL linksBLL = new LinksBLL();
        public async Task<ActionResult> Index()
        {
            await GetGlobalInfo();
            var t = await linksBLL.GetList(new Model.Param.BlogManage.LinksListParam() { });

            return View(t.Data);
        }



        public async Task GetGlobalInfo()
        {
            //todo:可以优化去缓存中读
           

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