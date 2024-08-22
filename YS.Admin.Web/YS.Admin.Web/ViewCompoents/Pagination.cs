using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YS.Admin.Web.ViewCompoents
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(string pageUrl)
        {
            ViewBag.pageUrl = pageUrl;
            return View();
        }
    }
}
