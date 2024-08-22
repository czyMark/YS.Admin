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
    /// 日 期：2022-01-20 22:57
    /// 描 述：服务类
    /// </summary>
    public class AdvertService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AdvertEntity>> GetList(AdvertListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AdvertEntity>> GetPageList(AdvertListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AdvertEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AdvertEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(AdvertEntity entity)
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
            await this.BaseRepository().Delete<AdvertEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<AdvertEntity, bool>> ListFilter(AdvertListParam param)
        {
            var expression = LinqExtensions.True<AdvertEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
