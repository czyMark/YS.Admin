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
    /// 日 期：2022-01-20 23:03
    /// 描 述：控制器类
    /// </summary>
    [Area("SiteManage")]
    public class AdvertBannerController :  BaseController
    {
        private AdvertBannerBLL advertBannerBLL = new AdvertBannerBLL();

        #region 视图功能
        [AuthorizeFilter("site:advertbanner:view")]
        public ActionResult AdvertBannerIndex()
        {
            return View();
        }

        public ActionResult AdvertBannerForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:advertbanner:search")]
        public async Task<ActionResult> GetListJson(AdvertBannerListParam param)
        {
            TData<List<AdvertBannerEntity>> obj = await advertBannerBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:advertbanner:search")]
        public async Task<ActionResult> GetPageListJson(AdvertBannerListParam param, Pagination pagination)
        {
            TData<List<AdvertBannerEntity>> obj = await advertBannerBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<AdvertBannerEntity> obj = await advertBannerBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:advertbanner:add,site:advertbanner:edit")]
        public async Task<ActionResult> SaveFormJson(AdvertBannerEntity entity)
        {
            TData<string> obj = await advertBannerBLL.SaveForm(entity);


			return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:advertbanner:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await advertBannerBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
