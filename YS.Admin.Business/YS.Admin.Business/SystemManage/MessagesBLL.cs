using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Service.SystemManage;
using System.Linq.Expressions;

namespace YS.Admin.Business.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 07:27
    /// 描 述：系统公告及消息业务类
    /// </summary>
    public class MessagesBLL
    {
        private MessagesService messagesService = new MessagesService();

        #region 获取数据
        public async Task<TData<List<MessagesEntity>>> GetList(MessagesListParam param)
        {
            TData<List<MessagesEntity>> obj = new TData<List<MessagesEntity>>();
            obj.Data = await messagesService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MessagesEntity>>> GetMyMessagesJson(MessagesListParam param)
        {
            TData<List<MessagesEntity>> obj = new TData<List<MessagesEntity>>();
            obj.Data = await messagesService.GetMyMessagesJson(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<MessagesEntity>>> GetPageList(MessagesListParam param, Pagination pagination)
        {
            TData<List<MessagesEntity>> obj = new TData<List<MessagesEntity>>();
            obj.Data = await messagesService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<MessagesEntity>> GetEntity(long id)
        {
            TData<MessagesEntity> obj = new TData<MessagesEntity>();
            obj.Data = await messagesService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(MessagesEntity entity)
        {
            TData<string> obj = new TData<string>();
            await messagesService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<MessagesEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await messagesService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await messagesService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(MessagesEntity entity, Expression<Func<MessagesEntity, MessagesEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await messagesService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
