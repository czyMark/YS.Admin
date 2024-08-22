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
using YS.Admin.Entity.BlogManage;
using YS.Admin.Model.Param.BlogManage;

namespace YS.Admin.Service.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:06
    /// 描 述：日记服务类
    /// </summary>
    public class DiarysService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DiarysEntity>> GetList(DiarysListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<DiarysEntity>> GetPageList(DiarysListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DiarysEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DiarysEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DiarysEntity entity)
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

        public async Task SaveForms(List<DiarysEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<DiarysEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<DiarysEntity, bool>> whereLambda, Expression<Func<DiarysEntity, DiarysEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<DiarysEntity, bool>> ListFilter(DiarysListParam param)
        {
            var expression = LinqExtensions.True<DiarysEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
