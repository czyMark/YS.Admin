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
using YS.Admin.Entity.ShortLInk;
using YS.Admin.Model.Param.ShortLInk;
using YS.Admin.Cache.Factory;

namespace YS.Admin.Service.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:31
    /// 描 述：短链接管理服务类
    /// </summary>
    public class LinkService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<LinkEntity>> GetList(LinkListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LinkEntity>> GetPageList(LinkListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LinkEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<LinkEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(LinkEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task SaveForms(List<LinkEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            
            #region 清楚缓存

            foreach (long id in idArr)
            {
                LinkEntity linkEntity = await this.BaseRepository().FindEntity<LinkEntity>(id);
                CacheFactory.Cache.SetCache(linkEntity.OriginUrl, linkEntity.ShortUrl);
                CacheFactory.Cache.SetCache(linkEntity.ShortUrl, linkEntity.OriginUrl);
            } 
            
            #endregion

            await this.BaseRepository().Delete<LinkEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<LinkEntity, bool>> whereLambda, Expression<Func<LinkEntity, LinkEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<LinkEntity, bool>> ListFilter(LinkListParam param)
        {
            var expression = LinqExtensions.True<LinkEntity>();
            if (param != null)
            {
                // 原链接
                if (!string.IsNullOrEmpty(param.OriginUrl))
                {
                    expression = expression.And(t => t.OriginUrl.Contains(param.OriginUrl));
                }
                // 短链接
                if (!string.IsNullOrEmpty(param.ShortUrl))
                {
                    expression = expression.And(t => t.ShortUrl.Contains(param.ShortUrl));
                }
            }
            return expression;
        }
        #endregion
    }
}
