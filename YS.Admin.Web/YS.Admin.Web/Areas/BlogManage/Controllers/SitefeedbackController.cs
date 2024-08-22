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
using YS.Admin.Business.SystemManage;

namespace YS.Admin.Web.Areas.BlogManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-19 15:30
    /// 描 述：博客网站留言控制器类
    /// </summary>
    [Area("BlogManage")]
    public class SitefeedbackController : BaseController
    {
        private SitefeedbackBLL sitefeedbackBLL = new SitefeedbackBLL();

        #region 视图功能
        [AuthorizeFilter("blog:sitefeedback:view")]
        public ActionResult SitefeedbackIndex()
        {
            return View();
        }

        public ActionResult SitefeedbackForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:sitefeedback:search")]
        public async Task<ActionResult> GetListJson(SitefeedbackListParam param)
        {
            TData<List<SitefeedbackEntity>> obj = await sitefeedbackBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:sitefeedback:search")]
        public async Task<ActionResult> GetPageListJson(SitefeedbackListParam param, Pagination pagination)
        {
            TData<List<SitefeedbackEntity>> obj = await sitefeedbackBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<SitefeedbackEntity> obj = await sitefeedbackBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:sitefeedback:add,blog:sitefeedback:edit")]
        public async Task<ActionResult> SaveFormJson(SitefeedbackEntity entity)
        {
            TData<string> obj = await sitefeedbackBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:sitefeedback:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await sitefeedbackBLL.DeleteForm(ids);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("blog:sitefeedback:add,blog:sitefeedback:edit")]
        public async Task<ActionResult> FeedbackStatusChange(string id, bool processingStatus)
        {
            TData<string> obj = await sitefeedbackBLL.FeedbackStatusChange(id, processingStatus);
            return Json(obj);
        }



        [HttpPost]
        public async Task<ActionResult> Save(SitefeedbackEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (string.IsNullOrEmpty(entity.UserName))
            {
                obj.Message = "留下你的名字！";
                return Json(obj);
            }
            bool f = true;
            if (!string.IsNullOrEmpty(entity.UserTel))
            {
                f = false;
            }
            if (!string.IsNullOrEmpty(entity.UserQq))
            {
                f = false;
            }
            if (!string.IsNullOrEmpty(entity.UserEmail))
            {
                f = false;
            }
            if (f)
            {

                obj.Message = "留下你的联系方式！";
                return Json(obj);
            }

            obj = await sitefeedbackBLL.SaveForm(entity);
            obj.Message = "留言收到了，过会就会处理！";
            return Json(obj);
        }
        #endregion
    }
}
