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
using YS.Admin.Entity.BlogManage;
using YS.Admin.Business.BlogManage;
using YS.Admin.Model.Param.BlogManage;

namespace YS.Admin.Web.Areas.BlogManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:43
    /// 描 述：网站设置控制器类
    /// </summary>
    [Area("BlogManage")]
    public class BlogConfigController :  BaseController
    {
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();

        #region 视图功能
        [AuthorizeFilter("blog:blogconfig:view")] 
        public ActionResult BlogConfigForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:blogconfig:search")]
        public async Task<ActionResult> GetListJson(BlogConfigListParam param)
        {
            TData<List<BlogConfigEntity>> obj = await blogConfigBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:blogconfig:search")]
        public async Task<ActionResult> GetPageListJson(BlogConfigListParam param, Pagination pagination)
        {
            TData<List<BlogConfigEntity>> obj = await blogConfigBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<BlogConfigEntity> obj = await blogConfigBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:blogconfig:add,blog:blogconfig:edit")]
        public async Task<ActionResult> SaveFormJson(BlogConfigEntity entity)
        {
            TData<string> obj = await blogConfigBLL.SaveForm(entity);
            return Json(obj);
        }
         
        #endregion
    }
}
