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
    /// 日 期：2024-08-08 15:14
    /// 描 述：评分大数据服务类
    /// </summary>
    public class BigdataRatingService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BigdataRatingEntity>> GetList(BigdataRatingListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BigdataRatingEntity>> GetPageList(BigdataRatingListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BigdataRatingEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<BigdataRatingEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(BigdataRatingEntity entity)
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

        public async Task SaveForms(List<BigdataRatingEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<BigdataRatingEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<BigdataRatingEntity, bool>> whereLambda, Expression<Func<BigdataRatingEntity, BigdataRatingEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<BigdataRatingEntity, bool>> ListFilter(BigdataRatingListParam param)
        {
            var expression = LinqExtensions.True<BigdataRatingEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.BigDataTag))
                {
                    expression = expression.And(t => t.BigDataTag==param.BigDataTag);
                }
            }
            return expression;
        }
        #endregion
    }
}
