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
    /// 日 期：2022-01-02 18:05
    /// 描 述：控制器类
    /// </summary>
    [Area("SiteManage")]
    public class ArticlesController :  BaseController
    {
        private ArticlesBLL articlesBLL = new ArticlesBLL();

        #region 视图功能
        [AuthorizeFilter("site:articles:view")]
        public ActionResult ArticlesIndex()
        {
            return View();
        }

        public ActionResult ArticlesForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:articles:search")]
        public async Task<ActionResult> GetListJson(ArticlesListParam param)
        {
            TData<List<ArticlesEntity>> obj = await articlesBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:articles:search")]
        public async Task<ActionResult> GetPageListJson(ArticlesListParam param, Pagination pagination)
        {
            TData<List<ArticlesEntity>> obj = await articlesBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<ArticlesEntity> obj = await articlesBLL.GetEntity(id);
            return Json(obj);
        }
        [HttpGet]
    
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:articles:add,site:articles:edit")] 
        public async Task<ActionResult> SaveFormJson(ArticlesEntity entity)
        {
            TData<string> obj = await articlesBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:articles:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await articlesBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
