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
    /// 日 期：2021-10-15 13:19
    /// 描 述：字典管理服务类
    /// </summary>
    public class DictTypeService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DictTypeEntity>> GetList(DictTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<DictTypeEntity>> GetPageList(DictTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DictTypeEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DictTypeEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DictTypeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<DictTypeEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<DictTypeEntity, bool>> ListFilter(DictTypeListParam param)
        {
            var expression = LinqExtensions.True<DictTypeEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
