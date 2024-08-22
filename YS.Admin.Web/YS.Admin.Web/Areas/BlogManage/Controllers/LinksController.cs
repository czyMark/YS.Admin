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
using YS.Admin.Entity.BlogManage;
using YS.Admin.Business.BlogManage;
using YS.Admin.Model.Param.BlogManage;

namespace YS.Admin.Web.Areas.BlogManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:08
    /// 描 述：友情链接控制器类
    /// </summary>
    [Area("BlogManage")]
    public class LinksController :  BaseController
    {
        private LinksBLL linksBLL = new LinksBLL();

        #region 视图功能
        [AuthorizeFilter("blog:links:view")]
        public ActionResult LinksIndex()
        {
            return View();
        }

        public ActionResult LinksForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:links:search")]
        public async Task<ActionResult> GetListJson(LinksListParam param)
        {
            TData<List<LinksEntity>> obj = await linksBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:links:search")]
        public async Task<ActionResult> GetPageListJson(LinksListParam param, Pagination pagination)
        {
            TData<List<LinksEntity>> obj = await linksBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<LinksEntity> obj = await linksBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:links:add,blog:links:edit")]
        public async Task<ActionResult> SaveFormJson(LinksEntity entity)
        {
            TData<string> obj = await linksBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:links:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await linksBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
