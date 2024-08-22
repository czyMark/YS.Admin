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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using YS.Admin.Entity.Portal;
using YS.Admin.Model.Param.Portal;

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:24
    /// 描 述：大数据统计标志服务类
    /// </summary>
    public class BigdataTagService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BigdataTagEntity>> GetList(BigdataTagListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }
        public async Task<List<BigdataTagEntity>> VerifyDataTag(BigdataTagListParam param)
        {
            var expression = LinqExtensions.True<BigdataTagEntity>();
            if (!string.IsNullOrEmpty(param.TagName))
            {
                expression = expression.And(t => t.TagName1 + t.TagName2 == param.TagName);
            }
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BigdataTagEntity>> GetPageList(BigdataTagListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BigdataTagEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<BigdataTagEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(BigdataTagEntity entity)
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
            string sql = "update collection_bigdata_tag set DataTag=(select datatag from collection_bigdata_category where collection_bigdata_tag.CategoryId =collection_bigdata_category.id  limit 1) ";
            await this.BaseRepository().ExecuteBySql(sql);
        }

        public async Task SaveForms(List<BigdataTagEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<BigdataTagEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<BigdataTagEntity, bool>> whereLambda, Expression<Func<BigdataTagEntity, BigdataTagEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<BigdataTagEntity, bool>> ListFilter(BigdataTagListParam param)
        {
            var expression = LinqExtensions.True<BigdataTagEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.CategoryId))
                {
                    expression = expression.And(t => t.CategoryId == param.CategoryId.ParseToLong());
                }
                if (!string.IsNullOrEmpty(param.TagName))
                {
                    expression = expression.And(t => t.TagName1.Equals(param.TagName) || t.TagName2.Equals(param.TagName));
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
