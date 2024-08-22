using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Business.BlogManage;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class FeedbackController : BaseController
    {
        private FeedbackBLL diarysBLL = new FeedbackBLL();
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        // GET: Feedback
        public async Task<ActionResult> Index()
        {
           await GetGlobalInfo();

            return View();
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