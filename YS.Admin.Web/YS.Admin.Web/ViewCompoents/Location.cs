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
    public class Location : ViewComponent
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
                string[] classListArr = cateModel.ClassList.TrimEnd(',').TrimStart(',').Split(',');
                param.ClassList = classListArr[0].ToString();
                List<ArticleCategoryEntity> articleCategories = await articleCategoryBLL.GetWebList(param);
                if (articleCategories.Count > 0)
                {
                    foreach (var item in classListArr)
                    {
                        ArticleCategoryEntity model = articleCategories.Where(p => p.Id == long.Parse(item)).FirstOrDefault();
                        var ModelUrl = string.Empty;
                        var childarticleCategory = GetCategoryChild(articleCategories, model, (long)model.Id);
                        if (childarticleCategory != null)
                        {

                            if (childarticleCategory.ModelId != 0)
                            {
                                ModelUrl = dataDictDetailEntities.Where(p => p.DictKey == (long)childarticleCategory.ModelId).FirstOrDefault().DictUrl;
                            }
                        }
                        webTreeLists.Add(new WebTreeList
                        {
                            Id = (long)model.Id,
                            ParentId = (long)model.ParentId,
                            ModelId = (int)model.ModelId,
                            ClassList = model.ClassList,
                            ClassLayer = (int)model.ClassLayer,
                            Title = model.Title,
                            ImgUrl = model.ImgUrl,
                            ModelUrl = ModelUrl,
                            Cid = (long)childarticleCategory.Id,
                        });
                    }

                }

            }
            ViewBag.UrlParam = urlparam;
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
