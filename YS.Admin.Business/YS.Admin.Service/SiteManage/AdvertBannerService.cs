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
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;

namespace YS.Admin.Service.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-20 23:03
    /// 描 述：服务类
    /// </summary>
    public class AdvertBannerService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AdvertBannerEntity>> GetList(AdvertBannerListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AdvertBannerEntity>> GetPageList(AdvertBannerListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AdvertBannerEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AdvertBannerEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(AdvertBannerEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<AdvertBannerEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<AdvertBannerEntity, bool>> ListFilter(AdvertBannerListParam param)
        {
            var expression = LinqExtensions.True<AdvertBannerEntity>();
            if (param != null)
            {
                expression = expression.And(t => t.Aid.Equals(param.Aid));
            }
            return expression;
        }
        #endregion
    }
}
