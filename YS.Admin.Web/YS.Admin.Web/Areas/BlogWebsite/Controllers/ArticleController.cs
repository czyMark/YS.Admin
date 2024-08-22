using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NPOI.POIFS.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Business.BlogManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param.BlogManage;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Web.Controllers;
using YS.Admin.Web.ViewCompoents;
using FeedbackBLL = YS.Admin.Business.BlogManage.FeedbackBLL;


namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class ArticleController : BaseController
    {
        private BlogConfigBLL blogConfigBLL = new BlogConfigBLL();
        private BlogArticleBLL blogArticleBLL = new BlogArticleBLL();
        private DataDictDetailBLL dataDictDetailBLL = new DataDictDetailBLL();
        private CommentBLL commentBLL = new CommentBLL();
        // GET: Article
        public async Task<ActionResult> Index(int? Id = 0)
        {
           await GetGlobalInfo();

            var t = await blogArticleBLL.GetList(new Model.Param.BlogManage.BlogArticleListParam() { ClassId = Id });
            var temp = await dataDictDetailBLL.GetList(new Model.Param.SystemManage.DataDictDetailListParam() { DictType = "BlogClass" });
            ViewBag.ClassList = temp.Data;

            ViewBag.HotList = t.Data.OrderByDescending(x => x.ReadNum).Take(5).ToList();
            ViewBag.DingList = t.Data.OrderByDescending(x => x.Ding).Take(3).ToList();

            //ViewBag.UserList = userService.GetBySkip(0, 12, null, null, null, "ORDER BY LastLogin Desc");


            //每页显示数目
            int PageSize = 10;
            ViewBag.PageSize = PageSize;
            //总条数
            int Count = t.Total;
            ViewBag.Count = Count;
            //总页数
            int PageCount = (Count + PageSize - 1) / PageSize;
            ViewBag.PageCount = PageCount;
            //文章分类Id
            ViewBag.ClassId = Id;
            return View();
        }


        // GET: Article
        public async Task<ActionResult> Send(int? Id = 0)
        {
          await  GetGlobalInfo();
             
            return View();
        }

        public async Task GetGlobalInfo()
        {
            //todo:可以优化去缓存中读
            //查询网站全局信息
            

            var siteInfo = await blogConfigBLL.GetEntity(744688942032359424);
            ViewBag.Site = siteInfo.Data;


            string? userSessionCookie;
            // 尝试读取名为"UserSession"的Cookie
            if (Request.Cookies.TryGetValue("UserSession", out userSessionCookie))
            {
                // 将Cookie值传递给视图
                ViewBag.UserSession = userSessionCookie;
            }
            else
            {
                // 如果Cookie不存在，可以设置一个默认值或进行其他操作
                ViewBag.UserSession = "登录";
            }
        }


        public async Task<ActionResult> Detail(long Id)
        {
           await GetGlobalInfo();
             

            //浏览次数+1
            var t = await blogArticleBLL.GetEntity(Id);
            var other = await blogArticleBLL.GetRandomArticleList(new Model.Param.BlogManage.BlogArticleListParam());
            if (t.Data != null)
            {
                {

                    var itemList = await dataDictDetailBLL.GetWebList(new Model.Param.SystemManage.DataDictDetailListParam() { DictKey = t.Data.TypeId, DictType = "BlogType" });
                    t.Data.TypeName = itemList.FirstOrDefault()?.DictValue;
                }
                {

                    var itemList = await dataDictDetailBLL.GetWebList(new Model.Param.SystemManage.DataDictDetailListParam() { DictKey = t.Data.ClassId, DictType = "BlogClass" });
                    t.Data.ClassName = itemList.FirstOrDefault()?.DictValue;
                }
            }
            //延伸阅读
            ViewBag.OtherList = other.Data;
            //每页显示数目
            int PageSize = 10;
            ViewBag.PageSize = PageSize;

            var temp = await (commentBLL.GetList(new Model.Param.BlogManage.CommentListParam() { ParentId = false, ArticleId = Id }));
            //总条数
            int Count = temp.Total;
            ViewBag.Count = Count;
            //总页数
            int PageCount = (Count + PageSize - 1) / PageSize;
            ViewBag.PageCount = PageCount;
            return View(t.Data);
        }
        [HttpPost]
        public async Task<JsonResult> SearchResult(string context)
        {
            var result = await blogArticleBLL.GetList(new Model.Param.BlogManage.BlogArticleListParam() { Title = context });
            return Json(result.Data);
            //return Json("");
        }
        [HttpPost]
        public async Task<ContentResult> LoadArticleByClass(int classId, int page, int pagesize)
        {
            var result = await blogArticleBLL.GetPageList(
                new Model.Param.BlogManage.BlogArticleListParam() { ClassId = classId },
                new Util.Model.Pagination() { PageIndex = page, PageSize = pagesize, Sort = "BaseCreateTime", SortType = "" });
            string content = CreateArticleHtml(result.Data);

            return Content(content);
        }


        private string CreateArticleHtml(IEnumerable<BlogArticleEntity> list)
        {
            List<DataDictDetailEntity> dataDicts = CacheFactory.Cache.GetCache<List<DataDictDetailEntity>>(GlobalContext.SystemConfig.ProCode + "DataDictDetailCache");
            StringBuilder sb = new StringBuilder();
            if (list != null)
            {
                foreach (BlogArticleEntity item in list)
                {
                    string links = "/BlogWebsite/Article/Detail/" + item.Id;
                    sb.AppendFormat(@"<section class='article-item zoomIn article'>
                                        {0}
                                        <h5 class='title'>
                                            <span class='fc-blue'>【{1}】</span>
                                            <a href='{11}'>{2}</a>
                                        </h5>
                                        <div class='time'>
                                            <span class='day'>{3}</span>
                                            <span class='month fs-18'>{4}<span class='fs-14'>月</span></span>
                                            <span class='year fs-18 ml10'>{5}</span>
                                        </div>
                                        <div class='content'>
                                            <a href='{12}' class='cover img-light'>
                                                <img src='{6}' >
                                            </a>
                                            {7}
                                        </div>
                                        <div class='read-more'>
                                            <a href='{13}' class='fc-black f-fwb'>继续阅读</a>
                                        </div>
                                        <aside class='f-oh footer'>
                                            <div class='f-fl tags'>
                                                <span class='fa fa-tags fs-16'></span>
                                                <a class='tag'>{8}</a>
                                            </div>
                                            <div class='f-fr'>
                                                <span class='read'>
                                                    <i class='fa fa-eye fs-16'></i>
                                                    <i class='num'>{9}</i>
                                                </span>
                                                <span class='ml20'>
                                                    <i class='fa fa-comments fs-16'></i>
                                                    <a href = 'javascript:void(0)' class='num fc-grey'>{10}</a>
                                                </span>
                                            </div>
                                        </aside>
                                    </section>", item.Ding == 1 ? "<div class='fc-flag'>置顶</div>" : "", dataDicts.Where(d => d.DictKey == item.TypeId && d.DictType == "BlogType").FirstOrDefault()?.DictValue, item.Title, ((DateTime)item.BaseCreateTime).Day, ((DateTime)item.BaseCreateTime).Month, ((DateTime)item.BaseCreateTime).Year, item.ImgUrl, item.ZhaiYao, dataDicts.Where(d => d.DictKey == item.ClassId && d.DictType == "BlogClass").FirstOrDefault()?.DictValue, item.ReadNum, item.CommentNum, links, links, links);

                }
            }
            return sb.ToString();
        }



        [HttpPost]
        public async Task<ContentResult> LoadArticleComment(long articleId, int page, int pagesize)
        {
            var tp = await commentBLL.GetPageList(new Model.Param.BlogManage.CommentListParam() { ArticleId = articleId, ParentId = false }, new Util.Model.Pagination() { PageIndex = page, PageSize = pagesize, Sort = "BaseCreateTime", SortType = "Desc" });
            var tc = await commentBLL.GetPageList(new Model.Param.BlogManage.CommentListParam() { ArticleId = articleId, ParentId = true }, new Util.Model.Pagination() { PageIndex = page, PageSize = pagesize, Sort = "BaseCreateTime", SortType = "Desc" });
            string str = await CreateArticleCommentHtml(tp.Data.ToList(), tc.Data.ToList());
            return Content(str);
        }


        private async Task<string> CreateArticleCommentHtml(List<CommentListInfo> parentList, List<CommentListInfo> list)
        {
            var siteInfo = await blogConfigBLL.GetEntity(744688942032359424);
            string OpenComment = siteInfo.Data.CommentStatus;
            string replyBtn = "<a href='javascript:;' class='btn-reply' data-targetid='{0}' data-targetname='{1}'>回复</a>";
            StringBuilder sb = new StringBuilder();
            if (parentList != null && list != null)
            {
                foreach (CommentListInfo item in parentList)
                {
                    sb.AppendFormat(@"<li>
                                <div class='comment-parent'>
                                    <a name='remark-{0}'></a><div class='circle'>{1}</div>
                                    <div class='info'>
                                        <span class='username'>{2}</span>
                                    </div>
                                    <div class='comment-content'>{3}</div>
                                    <p class='info info-footer'>
                                        <span class='comment-time'>{4}</span>{5}
                                    </p>

                                </div>
                                <hr>", item.Id, item.HeadShot, item.SendNickName, item.Content, item.BaseCreateTime, OpenComment == "1" ? string.Format(replyBtn, item.SendId, item.SendNickName) : "回复已关闭");
                    foreach (CommentListInfo model in list)
                    {
                        if (item.Id == model.ParentId)
                        {
                            sb.AppendFormat(@"<div class='comment-child'>
                                            <a name='reply-{0}'></a><div class='circle'>{1}</div>
                                            <div class='info'>
                                                <span class='username'>{2}</span><span style='padding-right:0;margin-left:-5px;'> 回复 </span><span class='username'>{3}</span><span>{4}</span>
                                            </div>
                                            <p class='info'>
                                                <span class='comment-time'>{5}</span>{6}
                                            </p>
                                        </div>", model.Id, model.HeadShot, model.SendNickName, model.AcceptNickName, model.Content, model.BaseCreateTime, OpenComment == "1" ? string.Format(replyBtn, model.SendId, model.SendNickName) : "回复已关闭");
                        }
                    }
                    sb.AppendFormat(@"<div class='replycontainer layui-hide'>
                                    <form class='layui-form' action=''>
                                        <input type='hidden' name='remarkId' value='{0}'><input type='hidden' name='targetUserId' value='0'><input type='hidden' name='articleId' value='{1}'>
                                        <div class='layui-form-item'>
                                            <textarea name='replyContent' lay-verify='replyContent' placeholder='请输入回复内容' class='layui-textarea' style='min-height:80px;'></textarea>
                                        </div>
                                        <div class='layui-form-item'>
                                            <button class='layui-btn layui-btn-xs' lay-submit='formReply' lay-filter='formReply'>提交</button>
                                        </div>
                                    </form>
                                </div>
                            </li>", item.Id, item.ArticleId);
                }
            }
            return sb.ToString();
        }

    }
}