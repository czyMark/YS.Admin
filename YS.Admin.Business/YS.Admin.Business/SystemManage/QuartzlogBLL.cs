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
    /// 日 期：2024-08-11 23:06
    /// 描 述：自动执行任务日志业务类
    /// </summary>
    public class QuartzlogBLL
    {
        private QuartzlogService quartzlogService = new QuartzlogService();

        #region 获取数据
        public async Task<TData<List<QuartzlogEntity>>> GetList(QuartzlogListParam param)
        {
            TData<List<QuartzlogEntity>> obj = new TData<List<QuartzlogEntity>>();
            obj.Data = await quartzlogService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<QuartzlogEntity>>> GetPageList(QuartzlogListParam param, Pagination pagination)
        {
            TData<List<QuartzlogEntity>> obj = new TData<List<QuartzlogEntity>>();
            obj.Data = await quartzlogService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<QuartzlogEntity>> GetEntity(long id)
        {
            TData<QuartzlogEntity> obj = new TData<QuartzlogEntity>();
            obj.Data = await quartzlogService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(QuartzlogEntity entity)
        {
            TData<string> obj = new TData<string>();
            await quartzlogService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<QuartzlogEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await quartzlogService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await quartzlogService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> RemoveAllForm()
        {
            TData obj = new TData();
            await quartzlogService.RemoveAllForm();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForms(QuartzlogEntity entity, Expression<Func<QuartzlogEntity, QuartzlogEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await quartzlogService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
