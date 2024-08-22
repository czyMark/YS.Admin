using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Model.Param.BlogManage;
using YS.Admin.Service.BlogManage;
using System.Linq.Expressions;

namespace YS.Admin.Business.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-17 08:02
    /// 描 述：博客文章业务类
    /// </summary>
    public class BlogArticleBLL
    {
        private BlogArticleService blogArticleService = new BlogArticleService();

        #region 获取数据
        public async Task<TData<List<BlogArticleEntity>>> GetList(BlogArticleListParam param)
        {
            TData<List<BlogArticleEntity>> obj = new TData<List<BlogArticleEntity>>();
            obj.Data = await blogArticleService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BlogArticleEntity>>> GetRandomArticleList(BlogArticleListParam param)
        {
            TData<List<BlogArticleEntity>> obj = new TData<List<BlogArticleEntity>>();
            obj.Data = await blogArticleService.GetRandomArticleList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<BlogArticleEntity>>> GetPageList(BlogArticleListParam param, Pagination pagination)
        {
            TData<List<BlogArticleEntity>> obj = new TData<List<BlogArticleEntity>>();
            obj.Data = await blogArticleService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BlogArticleEntity>> GetEntity(long id)
        {
            TData<BlogArticleEntity> obj = new TData<BlogArticleEntity>();
            obj.Data = await blogArticleService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BlogArticleEntity entity)
        {
            TData<string> obj = new TData<string>();
            await blogArticleService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BlogArticleEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await blogArticleService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await blogArticleService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BlogArticleEntity entity, Expression<Func<BlogArticleEntity, BlogArticleEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await blogArticleService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
