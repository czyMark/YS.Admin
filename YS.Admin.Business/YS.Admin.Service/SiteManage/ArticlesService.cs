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
using System.Net.NetworkInformation;

namespace YS.Admin.Service.SiteManage
{
	/// <summary>
	/// 创 建：admin
	/// 日 期：2022-01-02 18:05
	/// 描 述：服务类
	/// </summary>
	public class ArticlesService : RepositoryFactory
	{
		#region 获取数据
		public async Task<List<ArticlesEntity>> GetList(ArticlesListParam param)
		{
			var expression = ListFilter(param);
			var list = await this.BaseRepository().FindList(expression);
			return list.ToList();
        }
        public async Task<ArticlesEntity> GetNewsEntity(ArticlesListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression,new Pagination { PageIndex=1,PageSize=1,Sort= "SortId" });
            return list.ToList().FirstOrDefault();
        }

        public async Task<List<ArticlesEntity>> GetPageList(ArticlesListParam param, Pagination pagination)
		{
			var expression = ListFilter(param);
			var list = await this.BaseRepository().FindList(expression, pagination);
			return list.ToList();
		}

		public async Task<ArticlesEntity> GetEntity(long id)
		{
			return await this.BaseRepository().FindEntity<ArticlesEntity>(id);
		}
		#endregion

		#region 提交数据
		public async Task SaveForm(ArticlesEntity entity)
		{
			if (entity.Id.IsNullOrZero())
			{
				entity.Create();
				await this.BaseRepository().Insert(entity);
			}
			else
			{
				//await entity.Modify();
				await this.BaseRepository().Update(entity);
			}
		}

		public async Task DeleteForm(string ids)
		{
			long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
			await this.BaseRepository().Delete<ArticlesEntity>(idArr);
		}
		#endregion

		#region 私有方法
		private Expression<Func<ArticlesEntity, bool>> ListFilter(ArticlesListParam param)
		{
			var expression = LinqExtensions.True<ArticlesEntity>();
			if (param != null)
			{
				if (param.CategoryId > 0)
				{
					expression = expression.And(t => t.CategoryId.Equals(param.CategoryId));
				}
				if (param.Status > 0)
				{
					expression = expression.And(t => t.Status == param.Status);
				}
			}
			return expression;
		}
		#endregion
	}
}
