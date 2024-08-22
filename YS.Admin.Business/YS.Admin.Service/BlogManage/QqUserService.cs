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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TagLib.Ogg;
using YS.Admin.Entity.OrganizationManage;

namespace YS.Admin.Service.BlogManage
{
    public class Temp
    {
        public int CommentNum { get; set; }
    }
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:09
    /// 描 述：第三方登录用户服务类
    /// </summary>
    public class QqUserService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<QqUserEntity>> GetList(QqUserListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<QqUserEntity>> GetPageList(QqUserListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<QqUserEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<QqUserEntity>(id);
        }
        public async Task<QqUserEntity> GetQQUserByOpenId(string id)
        {
            var expression = LinqExtensions.True<QqUserEntity>();
            expression.And(d => d.OpenId == id);
            var list = await this.BaseRepository().FindList(expression);
            return list.FirstOrDefault();
        }
        #endregion

        public bool ExistUserName(QqUserEntity entity)
        {
            var expression = LinqExtensions.True<QqUserEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.NickName == entity.NickName);
            }
            else
            {
                expression = expression.And(t => t.NickName == entity.NickName && t.OpenId != entity.Id.ToString());
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public async Task<int> GetTodayCommentNum(string id)
        {
            var sql = @"SELECT count(1) as commentNum FROM blog_comment a
                    INNER JOIN blog_user b on a.SendId=b.Id
                    WHERE DATE_FORMAT(a.BaseCreateTime,'%y-%m-%d')=DATE_FORMAT(NOW(),'%y-%m-%d')
                    and b.OpenId=" + id;

            var list = await this.BaseRepository().FindList<Temp>(sql);
            return list.FirstOrDefault().CommentNum;
        }


        public async Task<QqUserEntity> CheckLogin(string userName)
        {
            var expression = LinqExtensions.True<QqUserEntity>();
            expression = expression.And(t => t.NickName == userName);
            expression = expression.Or(t => t.Email == userName);
            return await this.BaseRepository().FindEntity(expression);
        }
        public async Task<QqUserEntity> cryptoCheckLogin(string crypto)
        {
            var expression = LinqExtensions.True<QqUserEntity>();
            expression = expression.And(t => t.Crypto == crypto);
            return await this.BaseRepository().FindEntity(expression);
        }


        #region 提交数据
        public async Task SaveForm(QqUserEntity entity)
        {

            await entity.Create();
            await this.BaseRepository().Insert(entity);

            entity.OpenId = entity.Id.ToString();
            await entity.Modify();
            await this.BaseRepository().Update(entity);

        }

        public async Task SaveForms(List<QqUserEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<QqUserEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<QqUserEntity, bool>> whereLambda, Expression<Func<QqUserEntity, QqUserEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<QqUserEntity, bool>> ListFilter(QqUserListParam param)
        {
            var expression = LinqExtensions.True<QqUserEntity>();
            if (param != null)
            {
                // 昵称
                if (!string.IsNullOrEmpty(param.NickName))
                {
                    expression = expression.And(t => t.NickName.Contains(param.NickName));
                }
            }
            return expression;
        }
        #endregion
    }
}
