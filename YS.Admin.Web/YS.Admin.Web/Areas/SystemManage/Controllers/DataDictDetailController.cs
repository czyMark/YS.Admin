﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Entity;
using YS.Admin.Model;
using YS.Admin.Web.Controllers;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Model.Param.SystemManage;

namespace YS.Admin.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class DataDictDetailController : BaseController
    {
        private DataDictDetailBLL dataDictDetailBLL = new DataDictDetailBLL();

        #region 视图功能
        [AuthorizeFilter("system:datadict:view")]
        public IActionResult DataDictDetailIndex()
        {
            return View();
        }
        public IActionResult DataDictDetailForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:datadict:search")]
        public async Task<IActionResult> GetListJson(DataDictDetailListParam param)
        {
            TData<List<DataDictDetailEntity>> obj = await dataDictDetailBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:datadict:search")]
        public async Task<IActionResult> GetPageListJson(DataDictDetailListParam param, Pagination pagination)
        {
            TData<List<DataDictDetailEntity>> obj = await dataDictDetailBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DataDictDetailEntity> obj = await dataDictDetailBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await dataDictDetailBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:datadict:add,system:datadict:edit")]
        public async Task<IActionResult> SaveFormJson(DataDictDetailEntity entity)
        {
            TData<string> obj = await dataDictDetailBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:datadict:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await dataDictDetailBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
