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
    /// 日 期：2024-08-05 14:00
    /// 描 述：栏目文字数据控制器类
    /// </summary>
    [Area("SiteManage")]
    public class ArticlesDescriptiondataController :  BaseController
    {
        private ArticlesDescriptiondataBLL articlesDescriptiondataBLL = new ArticlesDescriptiondataBLL();

        #region 视图功能
        [AuthorizeFilter("site:articlesdescriptiondata:view")]
        public ActionResult ArticlesDescriptiondataIndex(string categoryId,string id)
        {
            ViewBag.categoryId = categoryId;
            ViewBag.id = id;
			return View();
        }

        public ActionResult ArticlesDescriptiondataForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:articlesdescriptiondata:search")]
        public async Task<ActionResult> GetListJson(ArticlesDescriptiondataListParam param)
        {
            TData<List<ArticlesDescriptiondataEntity>> obj = await articlesDescriptiondataBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:articlesdescriptiondata:search")]
        public async Task<ActionResult> GetPageListJson(ArticlesDescriptiondataListParam param, Pagination pagination)
        {
            TData<List<ArticlesDescriptiondataEntity>> obj = await articlesDescriptiondataBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<ArticlesDescriptiondataEntity> obj = await articlesDescriptiondataBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:articlesdescriptiondata:add,site:articlesdescriptiondata:edit")]
        public async Task<ActionResult> SaveFormJson(ArticlesDescriptiondataEntity entity)
        {
            TData<string> obj = await articlesDescriptiondataBLL.SaveForm(entity);
            return Json(obj);
        }


        [HttpPost]
        [AuthorizeFilter("site:articlesdescriptiondata:add")]
        public async Task<ActionResult> IDCOdeCreateFormJson(ArticlesDescriptiondataEntity entity)
        {
            TData<string> obj = await articlesDescriptiondataBLL.SaveIDCOdeCreateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:articlesdescriptiondata:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await articlesDescriptiondataBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
