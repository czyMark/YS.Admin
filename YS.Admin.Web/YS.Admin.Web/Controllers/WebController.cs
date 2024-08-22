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

namespace YS.Admin.Web.Controllers
{
	public class WebController : Controller
	{

		#region 视图功能
		[HttpGet]
		public async Task<IActionResult> Index(UrlParam param)
		{
			return Redirect("/System/Home/Index");

			//return RedirectToAction("Index", "Home", new { area = "System" });
		}
        #endregion

    }
}
