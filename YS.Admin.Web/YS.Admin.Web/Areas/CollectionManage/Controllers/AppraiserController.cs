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
    /// 日 期：2024-07-17 16:28
    /// 描 述：鉴定师控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class AppraiserController :  BaseController
    {
        private AppraiserBLL appraiserBLL = new AppraiserBLL();

        #region 视图功能
        [AuthorizeFilter("collection:appraiser:view")]
        public ActionResult AppraiserIndex()
        {
            return View();
        }

        public ActionResult AppraiserForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:appraiser:search")]
        public async Task<ActionResult> GetListJson(AppraiserListParam param)
        {
            TData<List<AppraiserEntity>> obj = await appraiserBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:appraiser:search")]
        public async Task<ActionResult> GetPageListJson(AppraiserListParam param, Pagination pagination)
        {
            TData<List<AppraiserEntity>> obj = await appraiserBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<AppraiserEntity> obj = await appraiserBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:appraiser:add,collection:appraiser:edit")]
        public async Task<ActionResult> SaveFormJson(AppraiserEntity entity)
        {
            TData<string> obj = await appraiserBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("collection:appraiser:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await appraiserBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
