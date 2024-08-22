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
using YS.Admin.Business.SystemManage;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Entity.SiteManage;

namespace YS.Admin.Web.Areas.SiteManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-03-03 15:00
    /// 描 述：留言控制器类
    /// </summary>
    [Area("SiteManage")]
    public class FeedbackController :  BaseController
    {
        private FeedbackBLL feedbackBLL = new FeedbackBLL();

        #region 视图功能
        [AuthorizeFilter("system:feedback:view")]
        public ActionResult FeedbackIndex()
        {
            return View();
        }

        public ActionResult FeedbackForm()
        {
            return View();
        }
        public ActionResult FeedbackContent()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:feedback:search")]
        public async Task<ActionResult> GetListJson(FeedbackListParam param)
        {
            TData<List<FeedbackEntity>> obj = await feedbackBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:feedback:search")]
        public async Task<ActionResult> GetPageListJson(FeedbackListParam param, Pagination pagination)
        {
            TData<List<FeedbackEntity>> obj = await feedbackBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<FeedbackEntity> obj = await feedbackBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:feedback:add,system:feedback:edit")]
        public async Task<ActionResult> SaveFormJson(FeedbackEntity entity)
        {
            TData<string> obj = await feedbackBLL.SaveForm(entity);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("system:feedback:status")]
        public async Task<ActionResult> FeedbackStatusChange(string id,bool processingStatus)
        {
            TData<string> obj = await feedbackBLL.FeedbackStatusChange(id, processingStatus);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:feedback:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await feedbackBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
