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
    /// 日 期：2024-08-16 22:07
    /// 描 述：博客留言控制器类
    /// </summary>
    [Area("BlogManage")]
    public class FeedbackController :  BaseController
    {
        private FeedbackBLL feedbackBLL = new FeedbackBLL();

        #region 视图功能
        [AuthorizeFilter("blog:feedback:view")]
        public ActionResult FeedbackIndex()
        {
            return View();
        }

        public ActionResult FeedbackForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:feedback:search")]
        public async Task<ActionResult> GetListJson(FeedbackListParam param)
        {
            TData<List<FeedbackEntity>> obj = await feedbackBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:feedback:search")]
        public async Task<ActionResult> GetPageListJson(FeedbackListParam param, Pagination pagination)
        {
            TData<List<FeedbackInfo>> obj = await feedbackBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<FeedbackInfo> obj = await feedbackBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:feedback:add,blog:feedback:edit")]
        public async Task<ActionResult> SaveFormJson(FeedbackEntity entity)
        {
            TData<string> obj = await feedbackBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:feedback:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await feedbackBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
