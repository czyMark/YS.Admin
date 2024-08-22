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
    /// 创 建：xiaoyu
    /// 日 期：2024-05-13 22:33
    /// 描 述：系统编号设置业务类
    /// </summary>
    public class NumberSetBLL
    {
        private NumberSetService numberSetService = new NumberSetService();

        #region 获取数据
        public async Task<TData<List<NumberSetEntity>>> GetList(NumberSetListParam param)
        {
            TData<List<NumberSetEntity>> obj = new TData<List<NumberSetEntity>>();
            obj.Data = await numberSetService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NumberSetEntity>>> GetPageList(NumberSetListParam param, Pagination pagination)
        {
            TData<List<NumberSetEntity>> obj = new TData<List<NumberSetEntity>>();
            obj.Data = await numberSetService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<NumberSetEntity>> GetEntity(long id)
        {
            TData<NumberSetEntity> obj = new TData<NumberSetEntity>();
            obj.Data = await numberSetService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(NumberSetEntity entity)
        {
            TData<string> obj = new TData<string>();
            await numberSetService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<NumberSetEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await numberSetService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await numberSetService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(NumberSetEntity entity, Expression<Func<NumberSetEntity, NumberSetEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await numberSetService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
