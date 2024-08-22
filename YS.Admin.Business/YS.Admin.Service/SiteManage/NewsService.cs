using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using System.Text;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Data;
using YS.Admin.Data.Repository;
using YS.Admin.Model.Param.OrganizationManage;
using YS.Admin.Entity.SiteManage;

namespace YS.Admin.Service.SiteManage
{
    public class NewsService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<NewsEntity>> GetList(NewsListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<NewsEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<NewsEntity>> GetPageList(NewsListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<NewsEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<NewsEntity>> GetPageContentList(NewsListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await BaseRepository().FindList<NewsEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<NewsEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<NewsEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(NewsSort) FROM Site_News");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(NewsEntity entity)
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
            await BaseRepository().Delete<NewsEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(NewsListParam param, StringBuilder strSql, bool bNewsContent = false)
        {
            strSql.Append(@"SELECT  a.Id,
                                    a.BaseModifyTime,
                                    a.BaseModifierId,
                                    a.NewsTitle,
                                    a.ThumbImage,
                                    a.NewsTag,
                                    a.NewsAuthor,
                                    a.NewsSort,
                                    a.NewsDate,
                                    a.NewsType,
                                    a.ProvinceId,
                                    a.CityId,
                                    a.CountyId,
                                    a.ViewTimes");
            if (bNewsContent)
            {
                strSql.Append(",a.NewsContent");
            }
            strSql.Append(@"         FROM    Site_News a
                            WHERE   1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.NewsTitle))
                {
                    strSql.Append(" AND a.NewsTitle like @NewsTitle");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@NewsTitle", '%' + param.NewsTitle + '%'));
                }
                if (param.NewsType > 0)
                {
                    strSql.Append(" AND a.NewsType = @NewsType");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@NewsType", param.NewsType));
                }
                if (!string.IsNullOrEmpty(param.NewsTag))
                {
                    strSql.Append(" AND a.NewsTag like @NewsTag");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@NewsTag", '%' + param.NewsTag + '%'));
                }
                if (param.ProvinceId > 0)
                {
                    strSql.Append(" AND a.ProvinceId = @ProvinceId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ProvinceId", param.ProvinceId));
                }
                if (param.CityId > 0)
                {
                    strSql.Append(" AND a.CityId = @CityId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@CityId", param.CityId));
                }
                if (param.CountyId > 0)
                {
                    strSql.Append(" AND a.CountId = @CountId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@CountyId", param.CountyId));
                }
            }
            return parameter;
        }
        #endregion
    }
}
