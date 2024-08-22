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
using YS.Admin.Entity.ShortLInk;
using YS.Admin.Business.ShortLInk;
using YS.Admin.Model.Param.ShortLInk;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using YS.Admin.Cache.Factory;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Util.Extension;

namespace YS.Admin.Web.Areas.ShortLInk.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:31
    /// 描 述：短链接管理控制器类
    /// </summary>
    [Area("ShortLInk")]
    public class LinkController :  BaseController
    {
        private LinkBLL linkBLL = new LinkBLL();

        #region 视图功能
        [AuthorizeFilter("shortlink:link:view")]
        public ActionResult LinkIndex()
        {
            return View();
        }

        public ActionResult LinkForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("shortlink:link:search")]
        public async Task<ActionResult> GetListJson(LinkListParam param)
        {
            TData<List<LinkEntity>> obj = await linkBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("shortlink:link:search")]
        public async Task<ActionResult> GetPageListJson(LinkListParam param, Pagination pagination)
        {
            TData<List<LinkEntity>> obj = await linkBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<LinkEntity> obj = await linkBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("shortlink:link:add,shortlink:link:edit")]
        public async Task<ActionResult> SaveFormJson(LinkEntity entity)
        {
            TData<string> obj = await linkBLL.SaveForm(entity);
            return Json(obj);
        }

        private static bool CheckVaild(string url)
        {
            bool isValid = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult);
            return isValid && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private  ShortLinkConverterHelper shortLinkConverterHelper = new ShortLinkConverterHelper(
            
            );
        [HttpGet]
        public async Task<ActionResult> GeneratorShortLinkJson(string originUrl)
        {
            //shortLinkConverterHelper.Encode(id);
            TData<string> obj = new TData<string>();

            if (!CheckVaild(originUrl))
            {
                return Json(obj);
            }

            #region 缓存查询
            var cacheUrl = CacheFactory.Cache.GetCache<string>(originUrl);
            if (!string.IsNullOrEmpty(cacheUrl))
            {
                obj.Data = GlobalContext.SystemConfig.SiteWeb + "/" + cacheUrl;
                return Json(obj);
            } 
            #endregion

            #region 数据库查询
            var temp = await linkBLL.GetList(new LinkListParam() { OriginUrl = originUrl });
            if (temp.Data.FirstOrDefault() != null)
            {
                CacheFactory.Cache.SetCache(originUrl, temp.Data.FirstOrDefault().ShortUrl);
                CacheFactory.Cache.SetCache(temp.Data.FirstOrDefault().ShortUrl, originUrl);

                obj.Data = GlobalContext.SystemConfig.SiteWeb + "/" + temp.Data.FirstOrDefault().ShortUrl;
                return Json(obj);
            } 
            #endregion

            LinkEntity model=new LinkEntity();
            model.OriginUrl = originUrl;
            //插入数据
            TData<string> link = await linkBLL.SaveForm(model);
            //生成key
            var shortKey = shortLinkConverterHelper.Confuse(link.Data.ParseToLong());

            model.Id = link.Data.ParseToLong();
            model.ShortUrl = GlobalContext.SystemConfig.SiteWeb + "/" + shortKey;
            await linkBLL.SaveForm(model); 

            CacheFactory.Cache.SetCache(originUrl, shortKey);
            CacheFactory.Cache.SetCache(shortKey, originUrl);

            obj.Data = GlobalContext.SystemConfig.SiteWeb + "/" + shortKey;
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("shortlink:link:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await linkBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
