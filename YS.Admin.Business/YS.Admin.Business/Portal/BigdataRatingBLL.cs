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
    /// 描 述：评分大数据业务类
    /// </summary>
    public class BigdataRatingBLL
    {
        private BigdataRatingService bigdataRatingService = new BigdataRatingService();

        #region 获取数据
        public async Task<TData<List<BigdataRatingEntity>>> GetList(BigdataRatingListParam param)
        {
            TData<List<BigdataRatingEntity>> obj = new TData<List<BigdataRatingEntity>>();
            obj.Data = await bigdataRatingService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<BigdataRatingEntity>>> GetPageList(BigdataRatingListParam param, Pagination pagination)
        {
            TData<List<BigdataRatingEntity>> obj = new TData<List<BigdataRatingEntity>>();
            obj.Data = await bigdataRatingService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<BigdataRatingEntity>> GetEntity(long id)
        {
            TData<BigdataRatingEntity> obj = new TData<BigdataRatingEntity>();
            obj.Data = await bigdataRatingService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(BigdataRatingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await bigdataRatingService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<BigdataRatingEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await bigdataRatingService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await bigdataRatingService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(BigdataRatingEntity entity, Expression<Func<BigdataRatingEntity, BigdataRatingEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await bigdataRatingService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
