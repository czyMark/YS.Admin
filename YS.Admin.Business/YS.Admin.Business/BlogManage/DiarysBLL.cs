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

namespace YS.Admin.Business.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:06
    /// 描 述：日记业务类
    /// </summary>
    public class DiarysBLL
    {
        private DiarysService diarysService = new DiarysService();

        #region 获取数据
        public async Task<TData<List<DiarysEntity>>> GetList(DiarysListParam param)
        {
            TData<List<DiarysEntity>> obj = new TData<List<DiarysEntity>>();
            obj.Data = await diarysService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DiarysEntity>>> GetPageList(DiarysListParam param, Pagination pagination)
        {
            TData<List<DiarysEntity>> obj = new TData<List<DiarysEntity>>();
            obj.Data = await diarysService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DiarysEntity>> GetEntity(long id)
        {
            TData<DiarysEntity> obj = new TData<DiarysEntity>();
            obj.Data = await diarysService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(DiarysEntity entity)
        {
            TData<string> obj = new TData<string>();
            await diarysService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<DiarysEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await diarysService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await diarysService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(DiarysEntity entity, Expression<Func<DiarysEntity, DiarysEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await diarysService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
