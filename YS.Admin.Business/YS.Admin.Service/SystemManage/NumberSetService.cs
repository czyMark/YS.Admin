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
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param.SystemManage;

namespace YS.Admin.Service.SystemManage
{
    /// <summary>
    /// 创 建：xiaoyu
    /// 日 期：2024-05-13 22:33
    /// 描 述：系统编号设置服务类
    /// </summary>
    public class NumberSetService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<NumberSetEntity>> GetList(NumberSetListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NumberSetEntity>> GetPageList(NumberSetListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<NumberSetEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<NumberSetEntity>(id);
        }

		public async Task<long?> GetNumber(int type)
		{
			long? numbers = 0;
			var numberSet = await GetList(new NumberSetListParam() { Type = type });
			NumberSetEntity entity1 = numberSet.FirstOrDefault();
			entity1.Number = entity1.Number + 1;
			numbers = entity1.Number;
			await entity1.Modify();
			await this.BaseRepository().Update(entity1);
			return numbers;
		}
		#endregion

		#region 提交数据
		public async Task SaveForm(NumberSetEntity entity)
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

        public async Task SaveForms(List<NumberSetEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<NumberSetEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<NumberSetEntity, bool>> whereLambda, Expression<Func<NumberSetEntity, NumberSetEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<NumberSetEntity, bool>> ListFilter(NumberSetListParam param)
        {
            var expression = LinqExtensions.True<NumberSetEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
