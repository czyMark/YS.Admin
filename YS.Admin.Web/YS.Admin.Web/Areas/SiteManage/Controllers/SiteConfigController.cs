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
using YS.Admin.Business.Cache;

namespace YS.Admin.Web.Areas.SiteManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-02-01 03:31
    /// 描 述：控制器类
    /// </summary>
    [Area("SiteManage")]
    public class SiteConfigController :  BaseController
    {
        private SiteConfigBLL siteConfigBLL = new SiteConfigBLL();

        #region 视图功能
        [AuthorizeFilter("site:siteconfig:view")]
        public ActionResult SiteConfigIndex()
        {
            return View();
        }

        public ActionResult SiteConfigForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:siteconfig:search")]
        public async Task<ActionResult> GetListJson(SiteConfigListParam param)
        {
            TData<List<SiteConfigEntity>> obj = await siteConfigBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:siteconfig:search")]
        public async Task<ActionResult> GetPageListJson(SiteConfigListParam param, Pagination pagination)
        {
            TData<List<SiteConfigEntity>> obj = await siteConfigBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<SiteConfigEntity> obj = await siteConfigBLL.GetEntity(id);

           

			return Json(obj);
        }  
        [HttpGet]
        public async Task<ActionResult> GetDefaultFormJson()
        {
            TData<SiteConfigEntity> obj = await siteConfigBLL.GetDefaultList();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:siteconfig:add,site:siteconfig:edit")]
        public async Task<ActionResult> SaveFormJson(SiteConfigEntity entity)
        {
            TData<string> obj = await siteConfigBLL.SaveForm(entity);
			new SiteConfigCache().Remove();
			return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:siteconfig:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await siteConfigBLL.DeleteForm(ids);
			new SiteConfigCache().Remove();
			return Json(obj);
        }
        #endregion
    }
}
