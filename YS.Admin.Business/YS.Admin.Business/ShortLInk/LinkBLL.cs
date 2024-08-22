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
    /// 日 期：2024-08-15 18:31
    /// 描 述：短链接管理业务类
    /// </summary>
    public class LinkBLL
    {
        private LinkService linkService = new LinkService();

        #region 获取数据
        public async Task<TData<List<LinkEntity>>> GetList(LinkListParam param)
        {
            TData<List<LinkEntity>> obj = new TData<List<LinkEntity>>();
            obj.Data = await linkService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LinkEntity>>> GetPageList(LinkListParam param, Pagination pagination)
        {
            TData<List<LinkEntity>> obj = new TData<List<LinkEntity>>();
            obj.Data = await linkService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LinkEntity>> GetEntity(long id)
        {
            TData<LinkEntity> obj = new TData<LinkEntity>();
            obj.Data = await linkService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(LinkEntity entity)
        {
            TData<string> obj = new TData<string>();
            await linkService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }
         

        public async Task<TData<string>> SaveForms(List<LinkEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await linkService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await linkService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(LinkEntity entity, Expression<Func<LinkEntity, LinkEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await linkService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
