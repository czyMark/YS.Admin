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
    /// 日 期：2024-08-14 21:10
    /// 描 述：ip地址访问黑名单服务类
    /// </summary>
    public class IpBlockService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<IpBlockEntity>> GetList(IpBlockListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<IpBlockEntity>> GetPageList(IpBlockListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<IpBlockEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<IpBlockEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(IpBlockEntity entity)
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

        public async Task SaveForms(List<IpBlockEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<IpBlockEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<IpBlockEntity, bool>> whereLambda, Expression<Func<IpBlockEntity, IpBlockEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<IpBlockEntity, bool>> ListFilter(IpBlockListParam param)
        {
            var expression = LinqExtensions.True<IpBlockEntity>();
            if (param != null)
            {
        // ip地址
                        if (!string.IsNullOrEmpty(param.IpAddr))
                        {
                                expression = expression.And(t => t.IpAddr.Contains(param.IpAddr)); 
                        }
        // 备注
                        if (!string.IsNullOrEmpty(param.Remark))
                        {
                                expression = expression.And(t => t.Remark.Contains(param.Remark)); 
                        }
            }
            return expression;
        }
        #endregion
    }
}
