using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.Portal;
using YS.Admin.Model.Param.Portal;
using YS.Admin.Service.Portal;
using System.Linq.Expressions;

namespace YS.Admin.Business.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-08 15:13
    /// 描 述：版别大数据业务类
    /// </summary>
    public class BigdataEditionBLL
    {
        private BigdataEditionService bigdataEditionService = new BigdataEditionService();

        #region 获取数据
        public async Task<TData<List<BigdataEditionEntity>>> GetList(BigdataEditionListParam param)
        {
            TData<List<BigdataEditionEntity>> obj = new TData<List<BigdataEditionEntity>>();
            obj.Data = await bigdataEditionService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BigdataEditionEntity>>> GetPageList(BigdataEditionListParam param, Pagination pagination)
        {
            TData<List<BigdataEditionEntity>> obj = new TData<List<BigdataEditionEntity>>();
            obj.Data = await bigdataEditionService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BigdataEditionEntity>> GetEntity(long id)
        {
            TData<BigdataEditionEntity> obj = new TData<BigdataEditionEntity>();
            obj.Data = await bigdataEditionService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BigdataEditionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await bigdataEditionService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BigdataEditionEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await bigdataEditionService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await bigdataEditionService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BigdataEditionEntity entity, Expression<Func<BigdataEditionEntity, BigdataEditionEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await bigdataEditionService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
