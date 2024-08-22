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
using NPOI.POIFS.Properties;

namespace YS.Admin.Service.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:05
    /// 描 述：博客文章评论服务类
    /// </summary>
    public class CommentService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<CommentEntity>> GetList(CommentListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CommentListInfo>> GetPageList(CommentListParam param, Pagination pagination)
        {
            //需要关联  文章标题 发送人 接收人
            string sql = @"select a.*,qq1.NickName as AcceptNickName,qq2.NickName as SendNickName,art.Title as ArticleTitle,qq2.HeadShot as HeadShot from blog_comment as a
left join blog_user as qq1 on a.AcceptId=qq1.Id
left join blog_user as qq2 on a.SendId=qq2.Id 
left join blog_article as art on a.ArticleId=art.Id";
            string where = "  WHERE 1=1  ";
            if (param.ParentId)
            {
                where += " and a.ParentId!=0 ";
            }
            if (param.ArticleId != null )
            {
                where += "and a.ArticleId=" + param.ArticleId;
            }


            string paginationstr = GetPaginationStr(pagination);
            List<CommentListInfo> t = new List<CommentListInfo>();
            try
            {

                var temp = await this.BaseRepository().FindList<CommentListInfo>(sql + where + paginationstr);
                t=temp.ToList();    
            }
            catch (Exception ex)
            {

                throw;
            }

            return t;
        }

        public async Task<CommentListInfo> GetEntity(long id)
        {

            //需要关联  文章标题 发送人 接收人
            string sql = @"select a.*,qq1.NickName as AcceptNickName,qq2.NickName as SendNickName,art.Title as ArticleTitle from blog_comment as a
left join blog_user as qq1 on a.AcceptId=qq1.Id
left join blog_user as qq2 on a.SendId=qq2.Id
left join blog_article as art on a.ArticleId=art.Id where id=" + id + "";
            var t = await this.BaseRepository().FindList<CommentListInfo>(sql);

            return t.FirstOrDefault();
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(CommentEntity entity)
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

        public async Task SaveForms(List<CommentEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<CommentEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<CommentEntity, bool>> whereLambda, Expression<Func<CommentEntity, CommentEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法

        public string GetPaginationStr(Pagination pagination)
        {
            StringBuilder sb = new StringBuilder();
            if (pagination.PageIndex == 0)
            {
                pagination.PageIndex = 1;
            }
            int num = (pagination.PageIndex - 1) * pagination.PageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(pagination.Sort))
            {
                if (pagination.SortType.ToUpper().IndexOf("ASC") + pagination.SortType.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + pagination.Sort;
                }
                else
                {
                    orderBy = " ORDER BY " + pagination.Sort + " DESC";
                }
            }
            sb.Append(orderBy);
            sb.Append(" LIMIT " + num + "," + pagination.PageSize + "");
            return sb.ToString();
        }

        private Expression<Func<CommentEntity, bool>> ListFilter(CommentListParam param)
        {
            var expression = LinqExtensions.True<CommentEntity>();
            if (param != null)
            {
                if (param.ArticleId != null && param.ArticleId > 0)
                    expression.And(d => d.ArticleId == param.ArticleId);
                if (param.ParentId == true)
                    expression.And(d => d.ParentId > 0);
            }
            return expression;
        }
        #endregion
    }
}
