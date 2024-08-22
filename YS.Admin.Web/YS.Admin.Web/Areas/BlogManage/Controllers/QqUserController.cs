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
    /// 日 期：2024-08-16 22:09
    /// 描 述：第三方登录用户控制器类
    /// </summary>
    [Area("BlogManage")]
    public class QqUserController :  BaseController
    {
        private QqUserBLL qqUserBLL = new QqUserBLL();

        #region 视图功能
        [AuthorizeFilter("blog:qquser:view")]
        public ActionResult QqUserIndex()
        {
            return View();
        }

        public ActionResult QqUserForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:qquser:search")]
        public async Task<ActionResult> GetListJson(QqUserListParam param)
        {
            TData<List<QqUserEntity>> obj = await qqUserBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:qquser:search")]
        public async Task<ActionResult> GetPageListJson(QqUserListParam param, Pagination pagination)
        {
            TData<List<QqUserEntity>> obj = await qqUserBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<QqUserEntity> obj = await qqUserBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:qquser:add,blog:qquser:edit")]
        public async Task<ActionResult> SaveFormJson(QqUserEntity entity)
        {
            TData<string> obj = await qqUserBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:qquser:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await qqUserBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
