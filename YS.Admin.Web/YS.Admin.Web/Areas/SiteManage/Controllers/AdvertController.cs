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
    /// 日 期：2022-01-20 22:57
    /// 描 述：控制器类
    /// </summary>
    [Area("SiteManage")]
    public class AdvertController :  BaseController
    {
        private AdvertBLL advertBLL = new AdvertBLL();

        #region 视图功能
        [AuthorizeFilter("site:advert:view")]
        public ActionResult AdvertIndex()
        {
            return View();
        }

        public ActionResult AdvertForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:advert:search")]
        public async Task<ActionResult> GetListJson(AdvertListParam param)
        {
            TData<List<AdvertEntity>> obj = await advertBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:advert:search")]
        public async Task<ActionResult> GetPageListJson(AdvertListParam param, Pagination pagination)
        {
            TData<List<AdvertEntity>> obj = await advertBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<AdvertEntity> obj = await advertBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:advert:add,site:advert:edit")]
        public async Task<ActionResult> SaveFormJson(AdvertEntity entity)
        {
            TData<string> obj = await advertBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:advert:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await advertBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
