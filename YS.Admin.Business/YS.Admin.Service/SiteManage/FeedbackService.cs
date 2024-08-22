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
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Entity.SiteManage;
using NPOI.SS.Formula.Functions; 

namespace YS.Admin.Service.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-03-03 15:00
    /// 描 述：留言服务类
    /// </summary>
    public class FeedbackService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<FeedbackEntity>> GetList(FeedbackListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.OrderByDescending(d => d.Id).ToList();
        }

        public async Task<List<FeedbackEntity>> GetPageList(FeedbackListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.OrderByDescending(d => d.Id).ToList();
        }

        public async Task<FeedbackEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<FeedbackEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(FeedbackEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                entity.Create();
                entity = await BaseRepository().Insert2(entity);
            }
            else
            {

                await BaseRepository().Update(entity);
            }
        }
        public async Task<bool> FeedbackStatusChange(string id, bool processingStatus)
        { 
            var list = await this.BaseRepository().FindEntity<FeedbackEntity>(id.ParseToLong()); ;

            FeedbackEntity fb =list;
            if (fb != null)
            {
                fb.isLock = processingStatus ? 1 : 0;
                await BaseRepository().Update(fb);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<FeedbackEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<FeedbackEntity, bool>> ListFilter(FeedbackListParam param)
        {
            var expression = LinqExtensions.True<FeedbackEntity>();
            if (param != null)
            {
                if (string.IsNullOrEmpty(param.Key))
                {
                    expression = expression.And(d => d.title.Contains(param.Key) || d.content.Contains(param.Key));
                }
            }
            return expression;
        }
        #endregion
    }
}
