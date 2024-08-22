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
    /// 日 期：2022-01-02 18:05
    /// 描 述：业务类
    /// </summary>
    public class ArticlesBLL
    {
        private ArticlesService articlesService = new ArticlesService();

        #region 获取数据
        public async Task<TData<List<ArticlesEntity>>> GetList(ArticlesListParam param)
        {
            TData<List<ArticlesEntity>> obj = new TData<List<ArticlesEntity>>();
            obj.Data = await articlesService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<ArticlesEntity>> GetNewsEntity(ArticlesListParam param)
        {
            TData<ArticlesEntity> obj = new TData<ArticlesEntity>();
            obj.Data = await articlesService.GetNewsEntity(param);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }

        public async Task<TData<List<ArticlesEntity>>> GetPageList(ArticlesListParam param, Pagination pagination)
        {
            TData<List<ArticlesEntity>> obj = new TData<List<ArticlesEntity>>();
            obj.Data = await articlesService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ArticlesEntity>> GetEntity(long id)
        {
            TData<ArticlesEntity> obj = new TData<ArticlesEntity>();
            obj.Data = await articlesService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ArticlesEntity entity)
        {
            TData<string> obj = new TData<string>();
            await articlesService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await articlesService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
