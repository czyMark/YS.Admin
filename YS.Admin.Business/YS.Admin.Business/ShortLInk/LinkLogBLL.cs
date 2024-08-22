using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.ShortLInk;
using YS.Admin.Model.Param.ShortLInk;
using YS.Admin.Service.ShortLInk;
using System.Linq.Expressions;

namespace YS.Admin.Business.ShortLInk
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 19:39
    /// 描 述：短链接访问日志业务类
    /// </summary>
    public class LinkLogBLL
    {
        private LinkLogService linkLogService = new LinkLogService();

        #region 获取数据
        public async Task<TData<List<LinkLogEntity>>> GetList(LinkLogListParam param)
        {
            TData<List<LinkLogEntity>> obj = new TData<List<LinkLogEntity>>();
            obj.Data = await linkLogService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LinkLogEntity>>> GetPageList(LinkLogListParam param, Pagination pagination)
        {
            TData<List<LinkLogEntity>> obj = new TData<List<LinkLogEntity>>();
            obj.Data = await linkLogService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LinkLogEntity>> GetEntity(long id)
        {
            TData<LinkLogEntity> obj = new TData<LinkLogEntity>();
            obj.Data = await linkLogService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(LinkLogEntity entity)
        {
            TData<string> obj = new TData<string>();
            await linkLogService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<LinkLogEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await linkLogService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await linkLogService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(LinkLogEntity entity, Expression<Func<LinkLogEntity, LinkLogEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await linkLogService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
