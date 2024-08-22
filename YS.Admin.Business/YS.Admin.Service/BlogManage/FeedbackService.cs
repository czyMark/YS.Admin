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
    /// 日 期：2024-08-16 22:07
    /// 描 述：博客留言服务类
    /// </summary>
    public class FeedbackService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<FeedbackEntity>> GetList(FeedbackListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<FeedbackInfo>> GetPageList(FeedbackListParam param, Pagination pagination)
        {
            //需要关联  文章标题 发送人 接收人
            string sql = @"select a.*,qq1.NickName as AcceptNickName,qq2.NickName as SendNickName from blog_feedback as a
left join blog_user as qq1 on a.AcceptId=qq1.Id
left join blog_user as qq2 on a.SendId=qq2.Id ";
            var t = await this.BaseRepository().FindList<FeedbackInfo>(sql);

            return t.ToList();
        }

        public async Task<FeedbackInfo> GetEntity(long id)
        {

            //需要关联  文章标题 发送人 接收人
            string sql = @"select a.*,qq1.NickName as AcceptNickName,qq2.NickName as SendNickName  from blog_feedback as a
left join blog_user as qq1 on a.AcceptId=qq1.Id
left join blog_user as qq2 on a.SendId=qq2.Id where id=" + id + "";
            var t = await this.BaseRepository().FindList<FeedbackInfo>(sql);

            return t.FirstOrDefault();
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(FeedbackEntity entity)
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

        public async Task SaveForms(List<FeedbackEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<FeedbackEntity>(idArr);
        }
        public async Task<int> Update(Expression<Func<FeedbackEntity, bool>> whereLambda, Expression<Func<FeedbackEntity, FeedbackEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<FeedbackEntity, bool>> ListFilter(FeedbackListParam param)
        {
            var expression = LinqExtensions.True<FeedbackEntity>();
            if (param != null)
            {
            }
            return expression;
        }
        #endregion
    }
}
