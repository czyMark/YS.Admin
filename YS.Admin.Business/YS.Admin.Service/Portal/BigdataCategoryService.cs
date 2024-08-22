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

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:23
    /// 描 述：大数据栏目服务类
    /// </summary>
    public class BigdataCategoryService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BigdataCategoryEntity>> GetList(BigdataCategoryListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BigdataCategoryEntity>> GetPageList(BigdataCategoryListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BigdataCategoryEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<BigdataCategoryEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(BigdataCategoryEntity entity)
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

        public async Task SaveForms(List<BigdataCategoryEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<BigdataCategoryEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<BigdataCategoryEntity, bool>> whereLambda, Expression<Func<BigdataCategoryEntity, BigdataCategoryEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<BigdataCategoryEntity, bool>> ListFilter(BigdataCategoryListParam param)
        {
            var expression = LinqExtensions.True<BigdataCategoryEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.CategoryName))
                {
                    expression = expression.And(t => t.CategoryName.Equals(param.CategoryName));
                }
                if (!string.IsNullOrEmpty(param.DataTag))
                {
                    expression = expression.And(t => t.DataTag == param.DataTag);
                }
            }
            return expression;
        }
        #endregion
    }
}
