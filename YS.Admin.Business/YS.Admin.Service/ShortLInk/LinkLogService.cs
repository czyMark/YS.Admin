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
using YS.Admin.Entity.ShortLInk;
using YS.Admin.Model.Param.ShortLInk;

namespace YS.Admin.Service.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 19:39
    /// 描 述：短链接访问日志服务类
    /// </summary>
    public class LinkLogService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<LinkLogEntity>> GetList(LinkLogListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LinkLogEntity>> GetPageList(LinkLogListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LinkLogEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<LinkLogEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(LinkLogEntity entity)
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

        public async Task SaveForms(List<LinkLogEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<LinkLogEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<LinkLogEntity, bool>> whereLambda, Expression<Func<LinkLogEntity, LinkLogEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<LinkLogEntity, bool>> ListFilter(LinkLogListParam param)
        {
            var expression = LinqExtensions.True<LinkLogEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
