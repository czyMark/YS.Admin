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
    /// 日 期：2024-08-16 22:05
    /// 描 述：博客文章评论控制器类
    /// </summary>
    [Area("BlogManage")]
    public class CommentController :  BaseController
    {
        private CommentBLL commentBLL = new CommentBLL();

        #region 视图功能
        [AuthorizeFilter("blog:comment:view")]
        public ActionResult CommentIndex()
        {
            return View();
        }

        public ActionResult CommentForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:comment:search")]
        public async Task<ActionResult> GetListJson(CommentListParam param)
        {
            TData<List<CommentEntity>> obj = await commentBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:comment:search")]
        public async Task<ActionResult> GetPageListJson(CommentListParam param, Pagination pagination)
        {
            TData<List<CommentListInfo>> obj = await commentBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<CommentListInfo> obj = await commentBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:comment:add,blog:comment:edit")]
        public async Task<ActionResult> SaveFormJson(CommentEntity entity)
        {
            TData<string> obj = await commentBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:comment:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await commentBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
