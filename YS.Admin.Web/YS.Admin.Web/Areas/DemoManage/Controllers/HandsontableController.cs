﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YS.Admin.Web.Areas.DemoManage.Controllers
{
    [Area("DemoManage")]
    public class HandsontableController : Controller
    {
        public IActionResult Simple()
        {
            return View();
        } 
    }
}