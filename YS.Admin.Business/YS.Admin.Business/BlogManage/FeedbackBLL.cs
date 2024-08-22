using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Model.Param.BlogManage;
using YS.Admin.Service.BlogManage;
using System.Linq.Expressions;

namespace YS.Admin.Business.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:07
    /// 描 述：博客留言业务类
    /// </summary>
    public class FeedbackBLL
    {
        private FeedbackService feedbackService = new FeedbackService();

        #region 获取数据
        public async Task<TData<List<FeedbackEntity>>> GetList(FeedbackListParam param)
        {
            TData<List<FeedbackEntity>> obj = new TData<List<FeedbackEntity>>();
            obj.Data = await feedbackService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<FeedbackInfo>>> GetPageList(FeedbackListParam param, Pagination pagination)
        {
            TData<List<FeedbackInfo>> obj = new TData<List<FeedbackInfo>>();
            obj.Data = await feedbackService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<FeedbackInfo>> GetEntity(long id)
        {
            TData<FeedbackInfo> obj = new TData<FeedbackInfo>();
            obj.Data = await feedbackService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(FeedbackEntity entity)
        {
            TData<string> obj = new TData<string>();
            await feedbackService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<FeedbackEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await feedbackService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await feedbackService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(FeedbackEntity entity, Expression<Func<FeedbackEntity, FeedbackEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await feedbackService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
