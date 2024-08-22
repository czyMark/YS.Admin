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
    /// 日 期：2024-08-19 15:30
    /// 描 述：博客网站留言服务类
    /// </summary>
    public class SitefeedbackService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SitefeedbackEntity>> GetList(SitefeedbackListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SitefeedbackEntity>> GetPageList(SitefeedbackListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SitefeedbackEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SitefeedbackEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(SitefeedbackEntity entity)
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

        public async Task<bool> FeedbackStatusChange(string id, bool processingStatus)
        {
            var list = await this.BaseRepository().FindEntity<SitefeedbackEntity>(id.ParseToLong());

            SitefeedbackEntity fb = list;
            if (fb != null)
            {
                fb.IsLock = processingStatus ? 1 : 0;
                await BaseRepository().Update(fb);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task SaveForms(List<SitefeedbackEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<SitefeedbackEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<SitefeedbackEntity, bool>> whereLambda, Expression<Func<SitefeedbackEntity, SitefeedbackEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<SitefeedbackEntity, bool>> ListFilter(SitefeedbackListParam param)
        {
            var expression = LinqExtensions.True<SitefeedbackEntity>();
            if (param != null)
            {
                // 
                if (!string.IsNullOrEmpty(param.Content))
                {
                    expression = expression.And(t => t.Content.Contains(param.Content));
                }
            }
            return expression;
        }
        #endregion
    }
}
