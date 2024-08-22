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
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Web.Code;

namespace YS.Admin.Service.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 07:27
    /// 描 述：系统公告及消息服务类
    /// </summary>
    public class MessagesService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<MessagesEntity>> GetList(MessagesListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }
        public async Task<List<MessagesEntity>> GetMyMessagesJson(MessagesListParam param)
        {
            string sql = @"select msg.*,umsg.UserId,umsg.ReadStatus from sys_messages as msg left join
                sys_user_messages as umsg on msg.id=umsg.MessagesId
where (UserId is null or UserId=" + param.Id + ") and msg.BaseIsDelete=0 and (ReadStatus is null or ReadStatus!=2)";
            var list = await this.BaseRepository().FindList<MessagesEntity>(sql);
            return list.ToList();
        }

        public async Task<List<MessagesEntity>> GetPageList(MessagesListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<MessagesEntity> GetEntity(long id)
        {
            var expression = LinqExtensions.True<UserMessagesEntity>();
            expression = expression.And(d => d.MessagesId == id);
            var list = await this.BaseRepository().FindList<UserMessagesEntity>(expression);


            MessagesEntity messagesEntity = await this.BaseRepository().FindEntity<MessagesEntity>(id);


            var um = list.FirstOrDefault();
            //查询 sys_user_messages 这个表有没有数据。
            if (um == null)
            {
                UserMessagesEntity userMessagesEntity = new UserMessagesEntity();
                OperatorInfo user = await Operator.Instance.Current();
                userMessagesEntity.ReadStatus = 1;
                userMessagesEntity.MessagesId = id;
                userMessagesEntity.ReadTime = DateTime.Now;
                userMessagesEntity.Create();
                userMessagesEntity.UserId= user.UserId;
                await this.BaseRepository().Insert(userMessagesEntity);
                //如果有就将数据变成1
            }
            else
            {
                //如果没有就添加一条数据变成1
                um.ReadTime = DateTime.Now;
                um.ReadStatus = 1;
                await this.BaseRepository().Update(um);
            }
            messagesEntity.ViewTimes = messagesEntity.ViewTimes + 1;
            await this.BaseRepository().Update(messagesEntity);
            return messagesEntity;
        }

        public async Task<List<MessagesEntity>> GetAllList(MessagesListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(MessagesEntity entity)
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

        public async Task SaveForms(List<MessagesEntity> entities)
        {
            await this.BaseRepository().Update(entities);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            foreach (long id in idArr)
            {
                MessagesEntity entity = await this.BaseRepository().FindEntity<MessagesEntity>(id);
                entity.BaseIsDelete = 1;
                await this.BaseRepository().Update(entity);
            }
        }
        public async Task<int> Update(Expression<Func<MessagesEntity, bool>> whereLambda, Expression<Func<MessagesEntity, MessagesEntity>> entity)
        {
            return await this.BaseRepository().Update(whereLambda, entity);
        }
        #endregion

        #region 私有方法
        private Expression<Func<MessagesEntity, bool>> ListFilter(MessagesListParam param)
        {
            var expression = LinqExtensions.True<MessagesEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                // 标签
                if (!string.IsNullOrEmpty(param.MessagesTag) && param.MessagesTag != "-1")
                {
                    expression = expression.And(t => t.MessagesTag.Contains(param.MessagesTag));
                }
                // 消息标题
                if (!string.IsNullOrEmpty(param.Title))
                {
                    expression = expression.And(t => t.Title.Contains(param.Title));
                }
                // 消息类型
                if (!string.IsNullOrEmpty(param.Type) && param.Type != "-1")
                {
                    expression = expression.And(t => t.Type == param.Type);
                }
            }
            return expression;
        }
        #endregion
    }
}
