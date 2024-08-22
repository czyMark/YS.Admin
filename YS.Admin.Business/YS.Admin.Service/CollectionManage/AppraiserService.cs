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

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:28
    /// 描 述：鉴定师服务类
    /// </summary>
    public class AppraiserService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AppraiserEntity>> GetList(AppraiserListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AppraiserEntity>> GetPageList(AppraiserListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AppraiserEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AppraiserEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(AppraiserEntity entity)
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
            await this.BaseRepository().Delete<AppraiserEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<AppraiserEntity, bool>> ListFilter(AppraiserListParam param)
        {
            var expression = LinqExtensions.True<AppraiserEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AppraiserName))
                {
                    expression = expression.And(t => t.AppraiserName.Contains(param.AppraiserName));
                }
            }
            return expression;
        }
        #endregion
    }
}
