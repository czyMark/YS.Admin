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
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;

namespace YS.Admin.Service.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-05 13:59
    /// 描 述：栏目对应模版服务类
    /// </summary>
    public class ArticlesTemplateService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ArticlesTemplateEntity>> GetList(ArticlesTemplateListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ArticlesTemplateEntity>> GetPageList(ArticlesTemplateListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ArticlesTemplateEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ArticlesTemplateEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ArticlesTemplateEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ArticlesTemplateEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<ArticlesTemplateEntity, bool>> ListFilter(ArticlesTemplateListParam param)
        {
            var expression = LinqExtensions.True<ArticlesTemplateEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.TemplateName))
                {
                    expression = expression.And(t => t.TemplateName.Contains(param.TemplateName));
                }
            }
            return expression;
        }
        #endregion
    }
}
