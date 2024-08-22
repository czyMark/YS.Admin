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
    /// 日 期：2024-08-05 13:59
    /// 描 述：栏目对应模版控制器类
    /// </summary>
    [Area("SiteManage")]
    public class ArticlesTemplateController :  BaseController
    {
        private ArticlesTemplateBLL articlesTemplateBLL = new ArticlesTemplateBLL();

        #region 视图功能
        [AuthorizeFilter("site:articlestemplate:view")]
        public ActionResult ArticlesTemplateIndex()
        {
            return View();
        }

        public ActionResult ArticlesTemplateForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:articlestemplate:search")]
        public async Task<ActionResult> GetListJson(ArticlesTemplateListParam param)
        {
            TData<List<ArticlesTemplateEntity>> obj = await articlesTemplateBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:articlestemplate:search")]
        public async Task<ActionResult> GetPageListJson(ArticlesTemplateListParam param, Pagination pagination)
        {
            TData<List<ArticlesTemplateEntity>> obj = await articlesTemplateBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<ArticlesTemplateEntity> obj = await articlesTemplateBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:articlestemplate:add,site:articlestemplate:edit")]
        public async Task<ActionResult> SaveFormJson(ArticlesTemplateEntity entity)
        {
            TData<string> obj = await articlesTemplateBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:articlestemplate:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await articlesTemplateBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
