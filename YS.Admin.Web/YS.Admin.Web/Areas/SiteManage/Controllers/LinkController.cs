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
using YS.Admin.Entity.SiteManage;
using YS.Admin.Business.SiteManage;
using YS.Admin.Model.Param.SiteManage;

namespace YS.Admin.Web.Areas.SiteManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-22 15:11
    /// 描 述：控制器类
    /// </summary>
    [Area("SiteManage")]
    public class LinkController :  BaseController
    {
        private LinkBLL linkBLL = new LinkBLL();

        #region 视图功能
        [AuthorizeFilter("site:link:view")]
        public ActionResult LinkIndex()
        {
            return View();
        }

        public ActionResult LinkForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:link:search")]
        public async Task<ActionResult> GetListJson(LinkListParam param)
        {
            TData<List<LinkEntity>> obj = await linkBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:link:search")]
        public async Task<ActionResult> GetPageListJson(LinkListParam param, Pagination pagination)
        {
            TData<List<LinkEntity>> obj = await linkBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<LinkEntity> obj = await linkBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:link:add,site:link:edit")]
        public async Task<ActionResult> SaveFormJson(LinkEntity entity)
        {
            TData<string> obj = await linkBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:link:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await linkBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
