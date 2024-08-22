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
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;
using YS.Admin.Service.OrganizationManage;
using YS.Admin.Entity.OrganizationManage;

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-24 11:29
    /// 描 述：评分信息服务类
    /// </summary>
    public class ScoreService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ScoreEntity>> GetList(ScoreListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ScoreEntity>> GetPageList(ScoreListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ScoreEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<ScoreEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ScoreEntity entity)
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

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<ScoreEntity>(idArr);
        }
        #endregion

        #region 私有方法
        public bool ExistScore(ScoreEntity entity)
        {
            var expression = LinqExtensions.True<ScoreEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Score == entity.Score && t.ScoreType== entity.ScoreType);
            }
            else
            {
                expression = expression.And(t => t.Score == entity.Score && t.ScoreType == entity.ScoreType && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        private Expression<Func<ScoreEntity, bool>> ListFilter(ScoreListParam param)
        { 
            var expression = LinqExtensions.True<ScoreEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Score))
                {
                    expression = expression.And(t => t.Score.Contains(param.Score));
                }
                if (param.ScoreType>=0)
                {
                    expression = expression.And(t => t.ScoreType==param.ScoreType);
                }
            }
            return expression;
        }
        #endregion
    }
}
