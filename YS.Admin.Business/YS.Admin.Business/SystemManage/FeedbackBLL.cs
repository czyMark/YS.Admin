using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Service.SiteManage;

namespace YS.Admin.Business.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-03-03 15:00
    /// 描 述：留言业务类
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

        public async Task<TData<List<FeedbackEntity>>> GetPageList(FeedbackListParam param, Pagination pagination)
        {
            TData<List<FeedbackEntity>> obj = new TData<List<FeedbackEntity>>();
            obj.Data = await feedbackService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<FeedbackEntity>> GetEntity(long id)
        {
            TData<FeedbackEntity> obj = new TData<FeedbackEntity>();
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
        public async Task<TData<string>> FeedbackStatusChange(string id, bool processingStatus)
        {
            TData<string> obj = new TData<string>();
            bool task = await feedbackService.FeedbackStatusChange(id, processingStatus);
            if (task)
            {
                obj.Data = id;
                obj.Tag = 1;
                obj.Message = "操作成功";
            }
            else
            {
                obj.Tag = 0;
                obj.Message = "无法处理。操作错误！";
            }
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await feedbackService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
