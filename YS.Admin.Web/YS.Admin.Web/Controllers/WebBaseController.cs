using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Model.Result;

namespace YS.Admin.Web.Controllers
{
    public class WebBaseController : Controller
    {
        public WebBaseController(UrlParam param)
        {
            ViewBag.UrlParam = param;
        }
    }
}
