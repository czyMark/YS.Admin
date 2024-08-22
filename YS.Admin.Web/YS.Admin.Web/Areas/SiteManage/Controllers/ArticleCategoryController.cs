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
using YS.Admin.Model.Result;
using YS.Admin.Business.Cache;

namespace YS.Admin.Web.Areas.SiteManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-02 18:06
    /// 描 述：控制器类
    /// </summary>
    [Area("SiteManage")]
    public class ArticleCategoryController :  BaseController
    {
        private ArticleCategoryBLL articleCategoryBLL = new ArticleCategoryBLL();

        #region 视图功能
        [AuthorizeFilter("site:articlecategory:view")]
        public ActionResult ArticleCategoryIndex()
        {
            return View();
        }

        public ActionResult ArticleCategoryForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:articlecategory:search")]
        public async Task<ActionResult> GetListJson(ArticleCategoryListParam param)
        {
            TData<List<ArticleCategoryEntity>> obj = await articleCategoryBLL.GetList(param);
            return Json(obj);
        }
        //[HttpGet]
        //[AuthorizeFilter("site:articlecategory:search")]
        //public async Task<IActionResult> GetArticleCategoryTreeListJson(ArticleCategoryListParam param)
        //{
        //    TData<List<ZtreeInfo>> obj = await articleCategoryBLL.GetZtreeArticleCategoryList(param);
        //    return Json(obj);
        //}
        [HttpGet]
        [AuthorizeFilter("site:articlecategory:search")]
        public async Task<ActionResult> GetPageListJson(ArticleCategoryListParam param, Pagination pagination)
        {
            TData<List<ArticleCategoryEntity>> obj = await articleCategoryBLL.GetPageList(param, pagination);
            object o = Json(obj);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("site:articlecategory:search")]
        public async Task<IActionResult> GetArticleCategoryTreeListJson(ArticleCategoryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await articleCategoryBLL.GetZtreeArticleCategorytList(param);
            return Json(obj);
        }
        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<ArticleCategoryEntity> obj = await articleCategoryBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:articlecategory:add,site:articlecategory:edit")]
        public async Task<ActionResult> SaveFormJson(ArticleCategoryEntity entity)
        {
            TData<string> obj = await articleCategoryBLL.SaveForm(entity);
             new ArticleCategoryCache().Remove();

			return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:articlecategory:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await articleCategoryBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
