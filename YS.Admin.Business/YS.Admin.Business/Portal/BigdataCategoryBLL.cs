using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Service.CollectionManage;
using System.Linq.Expressions;
using YS.Admin.Entity.Portal;
using YS.Admin.Model.Param.Portal;
using YS.Admin.Service.Portal;

namespace YS.Admin.Business.Portal
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:23
    /// 描 述：大数据栏目业务类
    /// </summary>
    public class BigdataCategoryBLL
    {
        private BigdataCategoryService bigdataCategoryService = new BigdataCategoryService();

        #region 获取数据
        public async Task<TData<List<BigdataCategoryEntity>>> GetList(BigdataCategoryListParam param)
        {
            TData<List<BigdataCategoryEntity>> obj = new TData<List<BigdataCategoryEntity>>();
            obj.Data = await bigdataCategoryService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BigdataCategoryEntity>>> GetPageList(BigdataCategoryListParam param, Pagination pagination)
        {
            TData<List<BigdataCategoryEntity>> obj = new TData<List<BigdataCategoryEntity>>();
            obj.Data = await bigdataCategoryService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BigdataCategoryEntity>> GetEntity(long id)
        {
            TData<BigdataCategoryEntity> obj = new TData<BigdataCategoryEntity>();
            obj.Data = await bigdataCategoryService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BigdataCategoryEntity entity)
        {
            TData<string> obj = new TData<string>();
            await bigdataCategoryService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BigdataCategoryEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await bigdataCategoryService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await bigdataCategoryService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BigdataCategoryEntity entity, Expression<Func<BigdataCategoryEntity, BigdataCategoryEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
                obj.Tag = 0;
                obj.Message = "请选择操作数据";
                return obj;
            }

            await bigdataCategoryService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
