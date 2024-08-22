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
    /// 日 期：2024-08-13 22:29
    /// 描 述：自动执行任务服务类
    /// </summary>
    public class QuartzoptionsService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<QuartzoptionsEntity>> GetList(QuartzoptionsListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<QuartzoptionsEntity>> GetPageList(QuartzoptionsListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<QuartzoptionsEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<QuartzoptionsEntity>(id);
        }



        #endregion


        #region 执行任务

        public async Task<string> ExecuteBySql(string sql)
        {
            try
            {
                int c = await this.BaseRepository().ExecuteBySql(sql);
                return c.ToString();
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                    return ex.ToString();
				return ex.Message.ToString();
			}
        } 
        #endregion


        #region 提交数据
        public async Task SaveForm(QuartzoptionsEntity entity)
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

        public async Task SaveForms(List<QuartzoptionsEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<QuartzoptionsEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<QuartzoptionsEntity, bool>> whereLambda, Expression<Func<QuartzoptionsEntity, QuartzoptionsEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<QuartzoptionsEntity, bool>> ListFilter(QuartzoptionsListParam param)
        {
            var expression = LinqExtensions.True<QuartzoptionsEntity>();
            if (param != null)
            {
        // 任务名称
                        if (!string.IsNullOrEmpty(param.TaskName))
                        {
                                expression = expression.And(t => t.TaskName.Contains(param.TaskName)); 
                        }
        // 任务分组
                        if (!string.IsNullOrEmpty(param.GroupName))
                        {
                                expression = expression.And(t => t.GroupName.Contains(param.GroupName)); 
                        }
            }
            return expression;
        }
        #endregion
    }
}
