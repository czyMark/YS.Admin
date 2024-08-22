
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using YS.Admin.Business.BlogManage;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Util.Extension;
using YS.Admin.Util.HtmlLabel;
using YS.Admin.Web.Controllers;

namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class CommentController : BaseController
    {
        private CommentBLL commentBLL = new CommentBLL();
        private QqUserBLL userBLL = new QqUserBLL();
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        // GET: Comment
        [HttpPost]
        public async Task<JsonResult> AddComment(string openid, string articleid, string editorContent)
        {
            var t = await userBLL.GetQQUserByOpenId(openid);
            QqUserEntity userModel = t.Data;
            if (userModel == null)
            {
                t.Message = "非法提交，Openid不存在";
                t.Tag = 0;
                return Json(t);
            }
            if (userModel.Status==1)
            {
                t.Message = "用户已被锁定，无法评论\"";
                t.Tag = 0;
                return Json(t); 
            }


            var siteInfo = await blogConfigBLL.GetEntity(744688942032359424);
            int maxCommentNum = Convert.ToInt32(siteInfo.Data.MaxCommentCount);
            var CommentNum=await userBLL.GetTodayCommentNum(openid);
            int todayCommentNum = CommentNum.Data;
            if (todayCommentNum >= maxCommentNum)
            {
                t.Message = "评论提交失败，已超出每日最大提交数量";
                t.Tag = 0;
                return Json(t);
            }
            CommentEntity model = new CommentEntity()
            {
                SendId = userModel.Id,
                AcceptId = 0,
                Content = HoldXSS.FilterXSS(editorContent),
                Status = true,
                ParentId = 0,
                ArticleId = articleid.ParseToLong()
            };
            var task = await commentBLL.SaveForm(model);
            if (task.Tag > 1)
            {
                return Json(task);
            }
            else
            {
                return Json(task);
            }
        }
        [HttpPost]
        public async Task<JsonResult> ReplyComment(string openid, string remarkId, string targetUserId, string articleid, string editorContent)
        {
            var t = await userBLL.GetQQUserByOpenId(openid);
            QqUserEntity userModel = t.Data;
            if (userModel == null)
            {
                t.Message = "非法提交，Openid不存在";
                t.Tag = 0;
                return Json(t);
            }
            if (userModel.Status == 1)
            {
                t.Message = "QQ用户已被锁定，无法评论";
                t.Tag = 0;
                return Json(t);
            }

            var siteInfo = await blogConfigBLL.GetEntity(744688942032359424);
            int maxCommentNum = Convert.ToInt32(siteInfo.Data.MaxCommentCount);
            var CommentNum = await userBLL.GetTodayCommentNum(openid);
            int todayCommentNum = CommentNum.Data;
            if (todayCommentNum >= maxCommentNum)
            {
                t.Message = "评论提交失败，已超出每日最大提交数量";
                t.Tag = 0;
                return Json(t);
            }

            CommentEntity model = new CommentEntity()
            {
                SendId = userModel.Id,
                AcceptId =targetUserId.ParseToLong(),
                Content = HoldXSS.FilterXSS(editorContent),
                Status = true,
                ParentId = remarkId.ParseToLong(),
                ArticleId = articleid.ParseToLong()
            };
             
            var task = await commentBLL.SaveForm(model);
            if (task.Tag == 1)
            {
                return Json(task);
            }
            else
            {
                return Json(task);
            }
        }
    }
}