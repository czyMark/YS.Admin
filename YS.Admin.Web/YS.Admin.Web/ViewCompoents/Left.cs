using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Business.SiteManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Model.Result;

namespace YS.Admin.Web.ViewCompoents
{
    public class Left : ViewComponent
    {
        private ArticleCategoryBLL articleCategoryBLL = new ArticleCategoryBLL();
        private DataDictDetailBLL dataDictDetailBLL = new DataDictDetailBLL();
        public async Task<IViewComponentResult> InvokeAsync(UrlParam urlparam)
        {
            string nodeName = string.Empty;
            ArticleCategoryListParam param = new ArticleCategoryListParam();
            DataDictDetailListParam dataDictDetailListParam = new DataDictDetailListParam();
            List<WebTreeList> webTreeLists = new List<WebTreeList>();
            dataDictDetailListParam.DictType = "sysWebModel";
            List<DataDictDetailEntity> dataDictDetailEntities = await dataDictDetailBLL.GetWebList(dataDictDetailListParam);
            //param.ParentId = "0";
            //param.IsLock = "0";
            var tArtcieCategoryModel = await articleCategoryBLL.GetEntity((long)urlparam.Cid);//获取当前传递参数模型
            ArticleCategoryEntity cateModel = tArtcieCategoryModel.Data;
            if (cateModel != null)
            {
                string[] classListArr = cateModel.ClassList.Split(',');
                urlparam.nid =long.Parse(classListArr[1].ToString());
                urlparam.pid = classListArr.Length > 3 ? long.Parse(classListArr[2].ToString()) : 0;
                urlparam.tid = classListArr.Length > 4 ? long.Parse(classListArr[3].ToString()) : 0;
                param.ClassList = classListArr[1].ToString();
                List<ArticleCategoryEntity> articleCategories = await articleCategoryBLL.GetWebList(param);
                if (articleCategories.Count > 0)
                {
                    var FirstModel = articleCategories.Where(p => p.ClassLayer == 1).FirstOrDefault();
                    nodeName = FirstModel.Title;
                    long nodeId = (long)FirstModel.Id;
                    IEnumerable<ArticleCategoryEntity> menuList = articleCategories.Where(p => p.ParentId.Equals(nodeId) && p.IsLock == 0).OrderBy(p=>p.SortId);
                    foreach (ArticleCategoryEntity articleCategory in menuList)
                    {
                        var Cid = 0L;
                        List<WebTreeList> webChildTreeLists = new List<WebTreeList>();
                        var ModelUrl = string.Empty;
                        var childarticleCategory = GetCategoryChild(articleCategories, articleCategory, (long)articleCategory.Id);
                        if (childarticleCategory != null)
                        {
                            Cid = (long)childarticleCategory.Id;
                            if (childarticleCategory.ModelId != 0)
                            {
                                ModelUrl = dataDictDetailEntities.Where(p => p.DictKey == (long)childarticleCategory.ModelId).FirstOrDefault().DictUrl;
                            }
                            IEnumerable<ArticleCategoryEntity> menuChildList = articleCategories.Where(p => p.ParentId.Equals(articleCategory.Id) && p.IsLock == 0).OrderBy(p => p.SortId);
                            foreach (var itemChild in menuChildList)
                            {
                                var childModelUrl = dataDictDetailEntities.Where(p => p.DictKey == (long)itemChild.ModelId).FirstOrDefault().DictUrl;

                                webChildTreeLists.Add(new WebTreeList
                                {
                                    Id = (long)itemChild.Id,
                                    ParentId = (long)itemChild.ParentId,
                                    ModelId = (int)itemChild.ModelId,
                                    ClassList=itemChild.ClassList,
                                    ClassLayer= (int)itemChild.ClassLayer,
                                    Title = itemChild.Title,
                                    ImgUrl = itemChild.ImgUrl,
                                    ModelUrl = childModelUrl,
                                    Cid = Cid,
                                });
                            }
                        }
                        webTreeLists.Add(new WebTreeList
                        {
                            Id = (long)articleCategory.Id,
                            ParentId = (long)articleCategory.ParentId,
                            ModelId = (int)articleCategory.ModelId,
                            ClassList = articleCategory.ClassList,
                            ClassLayer = (int)articleCategory.ClassLayer,
                            Title = articleCategory.Title,
                            ImgUrl = articleCategory.ImgUrl,
                            ModelUrl = ModelUrl,
                            Cid = Cid,
                            WebChildList=webChildTreeLists,
                        });
                    }
                }
            }
            ViewBag.nodeName = nodeName;
            ViewBag.UrlParam = urlparam;
            ViewBag.Cid = "123456";
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
