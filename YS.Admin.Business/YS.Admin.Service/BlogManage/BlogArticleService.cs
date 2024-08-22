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
using TagLib.Riff;

namespace YS.Admin.Service.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-17 08:02
    /// 描 述：博客文章服务类
    /// </summary>
    public class BlogArticleService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<BlogArticleEntity>> GetList(BlogArticleListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }
        public async Task<List<BlogArticleEntity>> GetRandomArticleList(BlogArticleListParam param)
        {
            string str = @"select * from blog_article where 
 Id >= (
        (
            SELECT MAX(Id) FROM blog_article
        ) - (
            SELECT MIN(Id) FROM blog_article
        )
    ) * RAND() + (
        SELECT MIN(Id) FROM blog_article
    )
LIMIT 2;";
            var list = await this.BaseRepository().FindList<BlogArticleEntity>(str);
            return list.ToList();
        }

        public async Task<List<BlogArticleEntity>> GetPageList(BlogArticleListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BlogArticleEntity> GetEntity(long id)
        {
            var t = await this.BaseRepository().FindEntity<BlogArticleEntity>(id);
            if (t != null)
            {
                //检查是否被当前IP访问过

                if (await VerfiyArticleAccess(id, NetHelper.Ip))
                {
                    t.ReadNum++;
                    await t.Modify();
                    await this.BaseRepository().Update(t);
                }
            }
            return t;
        }
        #endregion

        private async Task<bool> VerfiyArticleAccess(long id, string tag)
        {
            var expression = LinqExtensions.True<BlogArticleAccessEntity>();
            expression = expression.And(d => d.ArticleId == id && d.Tag == tag);
            var t = await this.BaseRepository().FindList<BlogArticleAccessEntity>(expression);
            if (t.FirstOrDefault() == null)
            {
                //插入数据
                BlogArticleAccessEntity entity = new BlogArticleAccessEntity();
                entity.ArticleId = id;
                entity.Tag = tag;
                await entity.Create();
                await this.BaseRepository().Insert(entity);
                return true;
            }
            return false;
        }

        #region 提交数据
        public async Task SaveForm(BlogArticleEntity entity)
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

        public async Task SaveForms(List<BlogArticleEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<BlogArticleEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<BlogArticleEntity, bool>> whereLambda, Expression<Func<BlogArticleEntity, BlogArticleEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<BlogArticleEntity, bool>> ListFilter(BlogArticleListParam param)
        {
            var expression = LinqExtensions.True<BlogArticleEntity>();
            if (param != null)
            {
                // 分类ID
                if (param.ClassId != null && param.ClassId > 0)
                {
                    expression = expression.And(t => t.ClassId == param.ClassId);
                }
                // 类型ID
                if (param.TypeId != null && param.TypeId > 0)
                {
                    expression = expression.And(t => t.TypeId == param.TypeId);
                }
                // 摘要
                if (!string.IsNullOrEmpty(param.Title))
                {
                    expression = expression.And(t => t.Title.Contains(param.Title));
                }
            }
            return expression;
        }
        #endregion
    }
}
