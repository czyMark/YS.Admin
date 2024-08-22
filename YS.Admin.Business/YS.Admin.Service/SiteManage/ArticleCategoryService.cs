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
    /// 日 期：2022-01-02 18:06
    /// 描 述：服务类
    /// </summary>
    public class ArticleCategoryService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ArticleCategoryEntity>> GetList(ArticleCategoryListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.OrderBy(p => p.SortId).ToList();
        }

        public async Task<List<ArticleCategoryEntity>> GetPeersList(long id)
        {
            ArticleCategoryEntity nowEntity= await this.BaseRepository().FindEntity<ArticleCategoryEntity>(id);

            var expression = LinqExtensions.True<ArticleCategoryEntity>();

            expression = expression.And(p => p.ParentId.Equals(nowEntity.ParentId));

            var list = await this.BaseRepository().FindList(expression);

            return list.OrderBy(p => p.SortId).ToList();
        }
        public async Task<List<ArticleCategoryEntity>> GetSubordinateLevelList(long id)
        {
            var expression = LinqExtensions.True<ArticleCategoryEntity>();
            expression = expression.And(p => p.ParentId.Equals(id));
            var list = await this.BaseRepository().FindList(expression);

            return list.OrderBy(p => p.SortId).ToList();
        }
        public async Task<List<ArticleCategoryEntity>> GetPageList(ArticleCategoryListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ArticleCategoryEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<ArticleCategoryEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ArticleCategoryEntity entity)
        {


            bool isAdd = false;

            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                isAdd = true;
            }
            else
            {
                await entity.Modify();
            }
            if (entity.ParentId.IsNullOrZero())
            {
                entity.ClassList = "," + entity.Id + ",";
                entity.ClassLayer = 1;
            }
            else
            {
                long parentId = (long)entity.ParentId;
                ArticleCategoryEntity entity1 = await GetEntity(parentId);
                entity.ClassList = entity1.ClassList + entity.Id + ",";
                entity.ClassLayer = entity1.ClassLayer + 1;
            }
            if (isAdd)
            {
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
            await this.BaseRepository().Delete<ArticleCategoryEntity>(idArr);
        }
        #endregion

        #region 前台获取数据
        public async Task<List<ArticleCategoryEntity>> GetWebList(ArticleCategoryListParam param)
        {
            //var expression = ListWebFilter(param);
            //var list = await this.BaseRepository().FindList(expression);
            //return list.OrderBy(p => p.SortId).ToList();
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListWebFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ArticleCategoryEntity>(strSql.ToString(), filter.ToArray());
            return list.OrderBy(p => p.SortId).ToList();
        }
        #endregion
        #region 私有方法
        private Expression<Func<ArticleCategoryEntity, bool>> ListFilter(ArticleCategoryListParam param)
        {
            var expression = LinqExtensions.True<ArticleCategoryEntity>();
            if (param != null)
            {
                if (param.ParentId > -1)
                {
                    expression = expression.And(p => p.ParentId.Equals(param.ParentId));
                }
                //expression=expression.And()
                //expression = expression.And(t => t.ParentId.Equals(param.ParentId));
                //expression = expression.And(t => t.IsLock.Equals(param.IsLock));
            }
            return expression;
        }
        private Expression<Func<ArticleCategoryEntity, bool>> ListWebFilter(ArticleCategoryListParam param)
        {
            var expression = LinqExtensions.True<ArticleCategoryEntity>();
            if (param != null)
            {
                //expression=expression.And()
                //if (!string.IsNullOrEmpty(param.ParentId)) { 
                expression = expression.And(t => t.ParentId.Equals(2));
                //}
                //if (!string.IsNullOrEmpty(param.IsLock))
                //{
                //    expression = expression.And(t => t.IsLock.Equals(Convert.ToInt32(param.IsLock)));
                //}
            }
            return expression;
        }
        private List<DbParameter> ListWebFilter(ArticleCategoryListParam param, StringBuilder strSql, bool bNewsContent = false)
        {
            strSql.Append(@"SELECT  
                                a.Id,
                                a.ParentId,
                                a.Title,
                                a.ModelId,
                                a.ClassList,
                                a.ClassLayer,
                                a.SortId,
                                a.LinkUrl,
                                a.ImgUrl,
                                a.Remarks,
                                a.Contents,
                                a.Status,
                                a.IsLock,
                                a.SeoTitle,
                                a.SeoKeywords,
                                a.SeoDescription,
                                a.BaseIsDelete,
                                a.BaseCreateTime,
                                a.BaseModifyTime,
                                a.BaseCreatorId,
                                a.BaseModifierId,
                                a.BaseVersion
                                ");

            strSql.Append(@"         FROM    sys_article_category a
                            WHERE   1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {

                if (!string.IsNullOrEmpty(param.ClassList))
                {
                    strSql.Append(" AND locate(@ClassList,a.ClassList) ");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ClassList", param.ClassList));

                }
                //if (!string.IsNullOrEmpty(param.ParentId))
                //{
                //    strSql.Append(" AND a.ParentId = @ParentId");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@ParentId", Convert.ToInt32(param.ParentId)));
                //}
                //if (!string.IsNullOrEmpty(param.IsLock))
                //{
                //    strSql.Append(" AND a.IsLock = @IsLock");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@IsLock", Convert.ToInt32(param.IsLock)));
                //}
                //if (param.NewsType > 0)
                //{
                //    strSql.Append(" AND a.NewsType = @NewsType");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@NewsType", param.NewsType));
                //}
                //if (!string.IsNullOrEmpty(param.NewsTag))
                //{
                //    strSql.Append(" AND a.NewsTag like @NewsTag");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@NewsTag", '%' + param.NewsTag + '%'));
                //}
                //if (param.ProvinceId > 0)
                //{
                //    strSql.Append(" AND a.ProvinceId = @ProvinceId");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@ProvinceId", param.ProvinceId));
                //}
                //if (param.CityId > 0)
                //{
                //    strSql.Append(" AND a.CityId = @CityId");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@CityId", param.CityId));
                //}
                //if (param.CountyId > 0)
                //{
                //    strSql.Append(" AND a.CountId = @CountId");
                //    parameter.Add(DbParameterExtension.CreateDbParameter("@CountyId", param.CountyId));
                //}
            }
            return parameter;
        }
        #endregion
    }
}
