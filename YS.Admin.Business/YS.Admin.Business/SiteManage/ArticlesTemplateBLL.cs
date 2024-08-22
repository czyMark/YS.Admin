using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Service.SiteManage;

namespace YS.Admin.Business.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-05 13:59
    /// 描 述：栏目对应模版业务类
    /// </summary>
    public class ArticlesTemplateBLL
    {
        private ArticlesTemplateService articlesTemplateService = new ArticlesTemplateService();

        #region 获取数据
        public async Task<TData<List<ArticlesTemplateEntity>>> GetList(ArticlesTemplateListParam param)
        {
            TData<List<ArticlesTemplateEntity>> obj = new TData<List<ArticlesTemplateEntity>>();
            obj.Data = await articlesTemplateService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ArticlesTemplateEntity>>> GetPageList(ArticlesTemplateListParam param, Pagination pagination)
        {
            TData<List<ArticlesTemplateEntity>> obj = new TData<List<ArticlesTemplateEntity>>();
            obj.Data = await articlesTemplateService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ArticlesTemplateEntity>> GetEntity(long id)
        {
            TData<ArticlesTemplateEntity> obj = new TData<ArticlesTemplateEntity>();
            obj.Data = await articlesTemplateService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ArticlesTemplateEntity entity)
        {
            TData<string> obj = new TData<string>();
            await articlesTemplateService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await articlesTemplateService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
