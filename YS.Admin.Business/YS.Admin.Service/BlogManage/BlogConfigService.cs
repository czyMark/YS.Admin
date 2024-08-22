using System;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Data;
using YS.Admin.Data.Repository;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Model.Param.BlogManage;

namespace YS.Admin.Service.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:43
    /// 描 述：网站设置服务类
    /// </summary>
    public class BlogConfigService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BlogConfigEntity>> GetList(BlogConfigListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BlogConfigEntity>> GetPageList(BlogConfigListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BlogConfigEntity> GetEntity(long id)
        {

            var expression = LinqExtensions.True<BlogConfigEntity>();
            var t = await this.BaseRepository().FindList(expression);
            return t.FirstOrDefault();
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(BlogConfigEntity entity)
        { 
            var expression = LinqExtensions.True<BlogConfigEntity>();
            var t = await this.BaseRepository().FindList(expression);
            if (t.FirstOrDefault() != null)
            {
                entity.Id= t.FirstOrDefault().Id;
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task SaveForms(List<BlogConfigEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<BlogConfigEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<BlogConfigEntity, bool>> whereLambda, Expression<Func<BlogConfigEntity, BlogConfigEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<BlogConfigEntity, bool>> ListFilter(BlogConfigListParam param)
        {
            var expression = LinqExtensions.True<BlogConfigEntity>();
            if (param != null)
            {
                // 缓存时间
                if (!string.IsNullOrEmpty(param.CacheTime))
                {
                    expression = expression.And(t => t.CacheTime.Contains(param.CacheTime));
                }
                // 是否开启评论
                if (!string.IsNullOrEmpty(param.CommentStatus))
                {
                    expression = expression.And(t => t.CommentStatus.Contains(param.CommentStatus));
                }
            }
            return expression;
        }
        #endregion
    }
}
