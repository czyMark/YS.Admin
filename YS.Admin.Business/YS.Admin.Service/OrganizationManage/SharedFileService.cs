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
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Model.Param.OrganizationManage;

namespace YS.Admin.Service.OrganizationManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 15:06
    /// 描 述：共享文件服务类
    /// </summary>
    public class SharedFileService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SharedFileEntity>> GetList(SharedFileListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SharedFileEntity>> GetPageList(SharedFileListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SharedFileEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SharedFileEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(SharedFileEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();

                var user = await this.BaseRepository().FindEntity<UserEntity>(entity.BaseCreatorId.ParseToLong());
                entity.CreateName = user.RealName;
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task SaveForms(List<SharedFileEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<SharedFileEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<SharedFileEntity, bool>> whereLambda, Expression<Func<SharedFileEntity, SharedFileEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<SharedFileEntity, bool>> ListFilter(SharedFileListParam param)
        {
            var expression = LinqExtensions.True<SharedFileEntity>();
            if (param != null)
            {
                // 上传人
                if (!string.IsNullOrEmpty(param.CreateName))
                {
                    expression = expression.And(t => t.CreateName.Contains(param.CreateName));
                }
                // 文件名称
                if (!string.IsNullOrEmpty(param.FileName))
                {
                    expression = expression.And(t => t.FileName.Contains(param.FileName));
                }
            }
            return expression;
        }
        #endregion
    }
}
