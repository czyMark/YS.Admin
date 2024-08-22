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
    /// 日 期：2024-08-13 22:29
    /// 描 述：自动执行任务业务类
    /// </summary>
    public class QuartzoptionsBLL
    {
        private QuartzoptionsService quartzoptionsService = new QuartzoptionsService();

        #region 获取数据
        public async Task<TData<List<QuartzoptionsEntity>>> GetList(QuartzoptionsListParam param)
        {
            TData<List<QuartzoptionsEntity>> obj = new TData<List<QuartzoptionsEntity>>();
            obj.Data = await quartzoptionsService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<QuartzoptionsEntity>>> GetPageList(QuartzoptionsListParam param, Pagination pagination)
        {
            TData<List<QuartzoptionsEntity>> obj = new TData<List<QuartzoptionsEntity>>();
            obj.Data = await quartzoptionsService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<QuartzoptionsEntity>> GetEntity(long id)
        {
            TData<QuartzoptionsEntity> obj = new TData<QuartzoptionsEntity>();
            obj.Data = await quartzoptionsService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(QuartzoptionsEntity entity)
        {
            TData<string> obj = new TData<string>();
            await quartzoptionsService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<QuartzoptionsEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await quartzoptionsService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await quartzoptionsService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(QuartzoptionsEntity entity, Expression<Func<QuartzoptionsEntity, QuartzoptionsEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await quartzoptionsService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData> ChangeTaskStatusJson(QuartzoptionsEntity entity)
        {
            TData obj = new TData();
            await quartzoptionsService.SaveForm(entity);
            obj.Tag = 1;

            //暂停正在执行的任务

            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
