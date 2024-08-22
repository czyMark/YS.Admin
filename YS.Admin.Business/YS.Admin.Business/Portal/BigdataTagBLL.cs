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
    /// 日 期：2024-08-07 16:24
    /// 描 述：大数据统计标志业务类
    /// </summary>
    public class BigdataTagBLL
    {
        private BigdataTagService bigdataTagService = new BigdataTagService();

        #region 获取数据
        public async Task<TData<List<BigdataTagEntity>>> GetList(BigdataTagListParam param)
        {
            TData<List<BigdataTagEntity>> obj = new TData<List<BigdataTagEntity>>();
            obj.Data = await bigdataTagService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<BigdataTagEntity>>> VerifyDataTag(BigdataTagListParam param)
        {
            TData<List<BigdataTagEntity>> obj = new TData<List<BigdataTagEntity>>();
            obj.Data = await bigdataTagService.VerifyDataTag(param);
            obj.Total = obj.Data.Count;
            if (obj.Total > 0)
            {
                obj.Tag = 1;
            }
            else
            {
                obj.Tag = 0;
                obj.Message = "没有找到对应的大数据统计项";
            }
            return obj;
        }

        public async Task<TData<List<BigdataTagEntity>>> GetPageList(BigdataTagListParam param, Pagination pagination)
        {
            TData<List<BigdataTagEntity>> obj = new TData<List<BigdataTagEntity>>();
            obj.Data = await bigdataTagService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BigdataTagEntity>> GetEntity(long id)
        {
            TData<BigdataTagEntity> obj = new TData<BigdataTagEntity>();
            obj.Data = await bigdataTagService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BigdataTagEntity entity)
        {
            TData<string> obj = new TData<string>();
            await bigdataTagService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BigdataTagEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await bigdataTagService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await bigdataTagService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BigdataTagEntity entity, Expression<Func<BigdataTagEntity, BigdataTagEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
                obj.Tag = 0;
                obj.Message = "请选择操作数据";
                return obj;
            }

            await bigdataTagService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
