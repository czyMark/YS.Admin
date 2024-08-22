using RightControl.IService;
using RightControl.WebApp.Models;
using System.Web.Mvc;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class ArchivesController : BaseController
    {
        public IArticleService service { get; set; }
        // GET: Archives
        public ActionResult Index()
        {
            WebSiteInfo siteInfo = new WebSiteInfo();
            ViewBag.Site = siteInfo.GetWebSiteInfo();
            ViewData["Year"] = service.GetYear();
            ViewData["ArticleList"] = service.GetAll();
            return View();
        }
    }
}