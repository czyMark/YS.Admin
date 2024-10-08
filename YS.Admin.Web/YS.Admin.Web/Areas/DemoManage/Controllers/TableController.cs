﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Model.Param.OrganizationManage;
using YS.Admin.Util;
using YS.Admin.Util.Model;

namespace YS.Admin.Web.Areas.DemoManage.Controllers
{
    [Area("DemoManage")]
    public class TableController : Controller
    {
        #region 视图功能
        public IActionResult Editable()
        {
            return View();
        }

        public IActionResult Image()
        {
            return View();
        }

        public IActionResult Footer()
        {
            return View();
        }

        public IActionResult GroupHeader()
        {
            return View();
        }

        public IActionResult MultiToolbar()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        public IActionResult GetPageListJson(UserListParam param, Pagination pagination)
        {
            // 测试总共23条数据
            int total = 23;
            TData<List<UserEntity>> obj = new TData<List<UserEntity>>();
            obj.Total = total;
            obj.Data =  new List<UserEntity>();
            int id = (pagination.PageIndex - 1) * pagination.PageSize + 1;
            for (int i = id; i <= pagination.PageIndex * pagination.PageSize; i++)
            {
                if (i > total)
                {
                    break;
                }
                obj.Data.Add(new UserEntity
                {
                    Id = i,
                    RealName = "用户" + i,
                    Mobile = "15612345678",
                    Email = "test@163.com",
                    Birthday = DateTime.Now.ToString("yyyy-MM-dd"),
                    LoginCount = new Random().Next(1, 100),
                    UserStatus = i % 2
                });
            }
            obj.Tag = 1;
            return Json(obj);
        }
        #endregion
    }
}