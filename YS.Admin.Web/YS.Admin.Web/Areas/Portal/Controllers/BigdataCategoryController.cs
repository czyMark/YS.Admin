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
using YS.Admin.Entity.Portal;
using YS.Admin.Business.Portal;
using YS.Admin.Model.Param.Portal;

namespace YS.Admin.Web.Areas.Portal.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:23
    /// 描 述：大数据栏目控制器类
    /// </summary>
    [Area("Portal")]
    public class BigdataCategoryController : BaseController
    {
        private BigdataCategoryBLL bigdataCategoryBLL = new BigdataCategoryBLL();

        #region 视图功能
        [AuthorizeFilter("portal:bigdatacategory:view")]
        public ActionResult BigdataCategoryIndex()
        {
            return View();
        }

        public ActionResult BigdataCategoryForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("portal:bigdatacategory:search")]
        public async Task<ActionResult> GetListJson(BigdataCategoryListParam param)
        {
            TData<List<BigdataCategoryEntity>> obj = await bigdataCategoryBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("portal:bigdatacategory:search")]
        public async Task<ActionResult> GetPageListJson(BigdataCategoryListParam param, Pagination pagination)
        {
            TData<List<BigdataCategoryEntity>> obj = await bigdataCategoryBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<BigdataCategoryEntity> obj = await bigdataCategoryBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("portal:bigdatacategory:add,portal:bigdatacategory:edit")]
        public async Task<ActionResult> SaveFormJson(BigdataCategoryEntity entity)
        {
            TData<string> obj = await bigdataCategoryBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("portal:bigdatacategory:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await bigdataCategoryBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
