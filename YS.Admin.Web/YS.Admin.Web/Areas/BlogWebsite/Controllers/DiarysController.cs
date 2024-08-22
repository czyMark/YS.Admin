
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YS.Admin.Business.BlogManage;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class DiarysController : BaseController
    {
        private DiarysBLL diarysBLL = new DiarysBLL();
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        // GET: Diarys
        public async Task<ActionResult> Index()
        {
            var t = await diarysBLL.GetPageList(new Model.Param.BlogManage.DiarysListParam(), new Util.Model.Pagination() { PageIndex = 1, Sort = "BaseCreateTime", PageSize = int.MaxValue });
           await  GetGlobalInfo();
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