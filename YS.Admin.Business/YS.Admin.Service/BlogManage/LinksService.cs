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
    /// 日 期：2024-08-16 22:08
    /// 描 述：友情链接服务类
    /// </summary>
    public class LinksService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<LinksEntity>> GetList(LinksListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LinksEntity>> GetPageList(LinksListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LinksEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<LinksEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(LinksEntity entity)
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

        public async Task SaveForms(List<LinksEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<LinksEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<LinksEntity, bool>> whereLambda, Expression<Func<LinksEntity, LinksEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<LinksEntity, bool>> ListFilter(LinksListParam param)
        {
            var expression = LinqExtensions.True<LinksEntity>();
            if (param != null)
            {
        // 网站名称
                        if (!string.IsNullOrEmpty(param.Name))
                        {
                                expression = expression.And(t => t.Name.Contains(param.Name)); 
                        }
            }
            return expression;
        }
        #endregion
    }
}
