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
using YS.Admin.Entity.Portal;
using YS.Admin.Model.Param.Portal;

namespace YS.Admin.Service.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-08 15:13
    /// 描 述：版别大数据服务类
    /// </summary>
    public class BigdataEditionService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BigdataEditionEntity>> GetList(BigdataEditionListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BigdataEditionEntity>> GetPageList(BigdataEditionListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BigdataEditionEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<BigdataEditionEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(BigdataEditionEntity entity)
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

        public async Task SaveForms(List<BigdataEditionEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<BigdataEditionEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<BigdataEditionEntity, bool>> whereLambda, Expression<Func<BigdataEditionEntity, BigdataEditionEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<BigdataEditionEntity, bool>> ListFilter(BigdataEditionListParam param)
        {
            var expression = LinqExtensions.True<BigdataEditionEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.BigDataTag))
                {
                    expression = expression.And(t => t.BigDataTag == param.BigDataTag);
                }
            }
            return expression;
        }
        #endregion
    }
}
