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
    /// 日 期：2022-02-01 03:31
    /// 描 述：服务类
    /// </summary>
    public class SiteConfigService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SiteConfigEntity>> GetList(SiteConfigListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SiteConfigEntity>> GetPageList(SiteConfigListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SiteConfigEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SiteConfigEntity>(id);
        }

        public async Task<SiteConfigEntity> GetDefaultList()
        {
            var expression = ListFilter(null);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList().FirstOrDefault();
        }
		public async Task<SiteConfigEntity> GetDefault()
		{
			var expression = ListFilter(null);
			var list = await this.BaseRepository().FindList(expression);
			return list.ToList().FirstOrDefault();
		}
		#endregion

		#region 提交数据
		public async Task SaveForm(SiteConfigEntity entity)
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
            await this.BaseRepository().Delete<SiteConfigEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<SiteConfigEntity, bool>> ListFilter(SiteConfigListParam param)
        {
            var expression = LinqExtensions.True<SiteConfigEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
