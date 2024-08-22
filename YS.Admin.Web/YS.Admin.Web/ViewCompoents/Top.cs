using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Business.SiteManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Model.Result;

namespace YS.Admin.Web.ViewCompoents
{
    public class Top : ViewComponent
    {
        private ArticleCategoryBLL articleCategoryBLL = new ArticleCategoryBLL();
        private DataDictDetailBLL dataDictDetailBLL = new DataDictDetailBLL();
        private SiteConfigBLL siteConfigBLL = new SiteConfigBLL();
        public async Task<IViewComponentResult> InvokeAsync(UrlParam urlparam)
        {
            ArticleCategoryListParam param = new ArticleCategoryListParam();
            DataDictDetailListParam dataDictDetailListParam = new DataDictDetailListParam();
            dataDictDetailListParam.DictType = "sysWebModel";
            List<DataDictDetailEntity> dataDictDetailEntities = await dataDictDetailBLL.GetWebList(dataDictDetailListParam);
            //param.ParentId = "0";
            //param.IsLock = "0";
            List<ArticleCategoryEntity> articleCategories = await articleCategoryBLL.GetWebList(param);

            IEnumerable<ArticleCategoryEntity> menuList = articleCategories.Where(p => p.ParentId == 0 && p.IsLock == 0);

            List<WebTreeList> webTreeLists = new List<WebTreeList>();

            foreach (ArticleCategoryEntity articleCategory in menuList)
            {
                var Cid = 0L;
                var ModelUrl = string.Empty;
                var childarticleCategory = GetCategoryChild(articleCategories, articleCategory,(long)articleCategory.Id);
                if (childarticleCategory != null)
                {
                    Cid = (long)childarticleCategory.Id;
                    if (childarticleCategory.ModelId != 0)
                    {
                        ModelUrl = dataDictDetailEntities.Where(p => p.DictKey == (long)childarticleCategory.ModelId).FirstOrDefault().DictUrl;
                    }
                }
                //else
                //{
                //    Cid = (int)articleCategory.Id;
                //    if (articleCategory.ModelId != 0)
                //    {
                //        ModelUrl = dataDictDetailEntities.Where(p => p.DictKey == (int)articleCategory.ModelId).FirstOrDefault().DictUrl;
                //    }
                //}
                webTreeLists.Add(new WebTreeList
                {
                    Id = (long)articleCategory.Id,
                    ParentId = (long)articleCategory.ParentId,
                    ModelId = (int)articleCategory.ModelId,

                    Title = articleCategory.Title,
                    ImgUrl = articleCategory.ImgUrl,
                    ModelUrl = ModelUrl,
                    Cid = Cid,

                });
            }


            var siteConfig = await siteConfigBLL.GetDefaultList();
            SiteConfigEntity siteConfigEntity = siteConfig.Data;//获取版权模型

            //var items;
            ViewBag.UrlParam = urlparam;
            //ViewBag.Cid = _Cid;
            ViewBag.TreeList = "tiandao";
            ViewBag.SiteConfig = siteConfigEntity;
            return View(webTreeLists);
        }

        private ArticleCategoryEntity GetCategoryChild(List<ArticleCategoryEntity> articleCategories, ArticleCategoryEntity nowArticleCategoryEntity, long Id)
        {
            ArticleCategoryEntity model = null;

            model = articleCategories.Where(p => p.ParentId == Id).OrderBy(p => p.SortId).FirstOrDefault();
            if (model != null)
            {
               return GetCategoryChild(articleCategories, model, (long)model.Id);
            }
            else
            {
                return nowArticleCategoryEntity; 
            }
        }


    }
}
