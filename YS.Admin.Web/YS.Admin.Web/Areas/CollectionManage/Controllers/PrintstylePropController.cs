using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Entity;
using YS.Admin.Model;
using YS.Admin.Web.Controllers;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Business.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;

namespace YS.Admin.Web.Areas.CollectionManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:47
    /// 描 述：样式属性控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class PrintstylePropController :  BaseController
    {
        private PrintstylePropBLL printstylePropBLL = new PrintstylePropBLL();

        #region 视图功能
        [AuthorizeFilter("collection:printstyleprop:view")]
        public ActionResult PrintstylePropIndex()
        {
            return View();
        }

        public ActionResult PrintstylePropForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:printstyleprop:search")]
        public async Task<ActionResult> GetListJson(PrintstylePropListParam param)
        {
            TData<List<PrintstylePropEntity>> obj = await printstylePropBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:printstyleprop:search")]
        public async Task<ActionResult> GetPageListJson(PrintstylePropListParam param, Pagination pagination)
        {
            TData<List<PrintstylePropEntity>> obj = await printstylePropBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<PrintstylePropEntity> obj = await printstylePropBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:printstyleprop:add,collection:printstyleprop:edit")]
        public async Task<ActionResult> SaveFormJson(PrintstylePropEntity entity)
        {
            TData<string> obj = await printstylePropBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("collection:printstyleprop:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await printstylePropBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
