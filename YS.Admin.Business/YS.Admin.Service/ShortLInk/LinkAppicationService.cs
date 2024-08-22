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
    /// 日 期：2024-08-15 18:32
    /// 描 述：短链接应用管理服务类
    /// </summary>
    public class LinkAppicationService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<LinkAppicationEntity>> GetList(LinkAppicationListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LinkAppicationEntity>> GetPageList(LinkAppicationListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LinkAppicationEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<LinkAppicationEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(LinkAppicationEntity entity)
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

        public async Task SaveForms(List<LinkAppicationEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<LinkAppicationEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<LinkAppicationEntity, bool>> whereLambda, Expression<Func<LinkAppicationEntity, LinkAppicationEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<LinkAppicationEntity, bool>> ListFilter(LinkAppicationListParam param)
        {
            var expression = LinqExtensions.True<LinkAppicationEntity>();
            if (param != null)
            {
        // 应用码
                        if (!string.IsNullOrEmpty(param.AppCode))
                        {
                                expression = expression.And(t => t.AppCode.Contains(param.AppCode)); 
                        }
        // 应用名称
                        if (!string.IsNullOrEmpty(param.AppName))
                        {
                                expression = expression.And(t => t.AppName.Contains(param.AppName)); 
                        }
            }
            return expression;
        }
        #endregion
    }
}
