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
    /// 创 建：admin
    /// 日 期：2024-08-11 23:06
    /// 描 述：自动执行任务日志服务类
    /// </summary>
    public class QuartzlogService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<QuartzlogEntity>> GetList(QuartzlogListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<QuartzlogEntity>> GetPageList(QuartzlogListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<QuartzlogEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<QuartzlogEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(QuartzlogEntity entity)
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

        public async Task SaveForms(List<QuartzlogEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }


        public async Task RemoveAllForm()
        {
            await this.BaseRepository().ExecuteBySql("truncate table sys_quartzlog");
        }
        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<QuartzlogEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<QuartzlogEntity, bool>> whereLambda, Expression<Func<QuartzlogEntity, QuartzlogEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<QuartzlogEntity, bool>> ListFilter(QuartzlogListParam param)
        {
            var expression = LinqExtensions.True<QuartzlogEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
