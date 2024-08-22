using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Model.Result;
using YS.Admin.Business.SiteManage;
using YS.Admin.Entity.SiteManage;
namespace YS.Admin.Web.ViewCompoents
{
    public class Foot : ViewComponent
    {
        private SiteConfigBLL siteConfigBLL = new SiteConfigBLL();
        public async Task<IViewComponentResult> InvokeAsync(UrlParam urlparam)
        {
            var siteConfig = await siteConfigBLL.GetDefaultList();
            SiteConfigEntity siteConfigEntity = siteConfig.Data;
            return View(siteConfigEntity);
        }
    }
}
