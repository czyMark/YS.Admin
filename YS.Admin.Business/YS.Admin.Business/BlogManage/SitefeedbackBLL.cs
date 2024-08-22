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
using YS.Admin.Service.SiteManage;

namespace YS.Admin.Business.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-19 15:30
    /// 描 述：博客网站留言业务类
    /// </summary>
    public class SitefeedbackBLL
    {
        private SitefeedbackService sitefeedbackService = new SitefeedbackService();

        #region 获取数据
        public async Task<TData<List<SitefeedbackEntity>>> GetList(SitefeedbackListParam param)
        {
            TData<List<SitefeedbackEntity>> obj = new TData<List<SitefeedbackEntity>>();
            obj.Data = await sitefeedbackService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SitefeedbackEntity>>> GetPageList(SitefeedbackListParam param, Pagination pagination)
        {
            TData<List<SitefeedbackEntity>> obj = new TData<List<SitefeedbackEntity>>();
            obj.Data = await sitefeedbackService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SitefeedbackEntity>> GetEntity(long id)
        {
            TData<SitefeedbackEntity> obj = new TData<SitefeedbackEntity>();
            obj.Data = await sitefeedbackService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(SitefeedbackEntity entity)
        {
            TData<string> obj = new TData<string>();
            await sitefeedbackService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> FeedbackStatusChange(string id, bool processingStatus)
        {
            TData<string> obj = new TData<string>();
            bool task = await sitefeedbackService.FeedbackStatusChange(id, processingStatus);
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
        public async Task<TData<string>> SaveForms(List<SitefeedbackEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await sitefeedbackService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await sitefeedbackService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(SitefeedbackEntity entity, Expression<Func<SitefeedbackEntity, SitefeedbackEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await sitefeedbackService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
