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
using YS.Admin.Business.OrganizationManage;
using YS.Admin.Util.HtmlLabel;

namespace YS.Admin.Web.Areas.BlogManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-17 08:02
    /// 描 述：博客文章控制器类
    /// </summary>
    [Area("BlogManage")]
    public class BlogArticleController :  BaseController
    {
        private BlogArticleBLL blogArticleBLL = new BlogArticleBLL();

        #region 视图功能
        [AuthorizeFilter("blog:blogarticle:view")]
        public ActionResult BlogArticleIndex()
        {
            return View();
        }

        public ActionResult BlogArticleForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:blogarticle:search")]
        public async Task<ActionResult> GetListJson(BlogArticleListParam param)
        {
            TData<List<BlogArticleEntity>> obj = await blogArticleBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:blogarticle:search")]
        public async Task<ActionResult> GetPageListJson(BlogArticleListParam param, Pagination pagination)
        {
            TData<List<BlogArticleEntity>> obj = await blogArticleBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<BlogArticleEntity> obj = await blogArticleBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:blogarticle:add,blog:blogarticle:edit")]
        public async Task<ActionResult> SaveFormJson(BlogArticleEntity entity)
        {
            entity.UserId = 746363778358054912;
            entity.Author = "admin";
            TData<string> obj = await blogArticleBLL.SaveForm(entity);
            return Json(obj);
        }


        private QqUserBLL userBLL = new QqUserBLL();
        [HttpPost]
        public async Task<ActionResult> PublishArticle(BlogArticleEntity entity)
        {
            var t = await userBLL.GetQQUserByOpenId(entity.UserId.ToString());
            QqUserEntity userModel = t.Data;
            if (userModel == null)
            {
                t.Message = "非法提交，Openid不存在";
                t.Tag = 0;
                return Json(t);
            }
            if (userModel.Status == 1)
            {
                t.Message = "用户已被锁定，无法评论\"";
                t.Tag = 0;
                return Json(t);
            }
            entity.ImgUrl = "/images/default.jpg";
            entity.Content = HoldXSS.FilterXSS(entity.Content);
            TData<string> obj = await blogArticleBLL.SaveForm(entity);
            obj.Message = "发布成功！";
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:blogarticle:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await blogArticleBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
