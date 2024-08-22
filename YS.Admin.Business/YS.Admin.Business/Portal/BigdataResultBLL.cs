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
    /// 日 期：2024-08-08 15:14
    /// 描 述：评分结果大数据业务类
    /// </summary>
    public class BigdataResultBLL
    {
        private BigdataResultService bigdataResultService = new BigdataResultService();

        #region 获取数据
        public async Task<TData<List<BigdataResultEntity>>> GetList(BigdataResultListParam param)
        {
            TData<List<BigdataResultEntity>> obj = new TData<List<BigdataResultEntity>>();
            obj.Data = await bigdataResultService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BigdataResultEntity>>> GetPageList(BigdataResultListParam param, Pagination pagination)
        {
            TData<List<BigdataResultEntity>> obj = new TData<List<BigdataResultEntity>>();
            obj.Data = await bigdataResultService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BigdataResultEntity>> GetEntity(long id)
        {
            TData<BigdataResultEntity> obj = new TData<BigdataResultEntity>();
            obj.Data = await bigdataResultService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BigdataResultEntity entity)
        {
            TData<string> obj = new TData<string>();
            await bigdataResultService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BigdataResultEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await bigdataResultService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await bigdataResultService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BigdataResultEntity entity, Expression<Func<BigdataResultEntity, BigdataResultEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await bigdataResultService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
