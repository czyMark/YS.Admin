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
    /// 日 期：2024-08-16 22:43
    /// 描 述：网站设置业务类
    /// </summary>
    public class BlogConfigBLL
    {
        private BlogConfigService blogConfigService = new BlogConfigService();

        #region 获取数据
        public async Task<TData<List<BlogConfigEntity>>> GetList(BlogConfigListParam param)
        {
            TData<List<BlogConfigEntity>> obj = new TData<List<BlogConfigEntity>>();
            obj.Data = await blogConfigService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BlogConfigEntity>>> GetPageList(BlogConfigListParam param, Pagination pagination)
        {
            TData<List<BlogConfigEntity>> obj = new TData<List<BlogConfigEntity>>();
            obj.Data = await blogConfigService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BlogConfigEntity>> GetEntity(long id)
        {
            TData<BlogConfigEntity> obj = new TData<BlogConfigEntity>();
            obj.Data = await blogConfigService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BlogConfigEntity entity)
        {
            TData<string> obj = new TData<string>();
            await blogConfigService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BlogConfigEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await blogConfigService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await blogConfigService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BlogConfigEntity entity, Expression<Func<BlogConfigEntity, BlogConfigEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await blogConfigService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
