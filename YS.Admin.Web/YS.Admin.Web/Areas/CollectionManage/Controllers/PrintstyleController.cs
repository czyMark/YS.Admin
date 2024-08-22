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
    /// 描 述：打印样式控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class PrintstyleController :  BaseController
    {
        private PrintstyleBLL printstyleBLL = new PrintstyleBLL();

        #region 视图功能
        [AuthorizeFilter("collection:printstyle:view")]
        public ActionResult PrintstyleIndex()
        {
            return View();
        }

        public ActionResult PrintstyleForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:printstyle:search")]
        public async Task<ActionResult> GetListJson(PrintstyleListParam param)
        {
            TData<List<PrintstyleEntity>> obj = await printstyleBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:printstyle:search")]
        public async Task<ActionResult> GetPageListJson(PrintstyleListParam param, Pagination pagination)
        {
            TData<List<PrintstyleEntity>> obj = await printstyleBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<PrintstyleEntity> obj = await printstyleBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:printstyle:add,collection:printstyle:edit")]
        public async Task<ActionResult> SaveFormJson(PrintstyleEntity entity)
        {
            TData<string> obj = await printstyleBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("collection:printstyle:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await printstyleBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
