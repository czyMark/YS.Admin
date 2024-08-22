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
    /// 日 期：2024-08-15 18:32
    /// 描 述：短链接应用管理业务类
    /// </summary>
    public class LinkAppicationBLL
    {
        private LinkAppicationService linkAppicationService = new LinkAppicationService();

        #region 获取数据
        public async Task<TData<List<LinkAppicationEntity>>> GetList(LinkAppicationListParam param)
        {
            TData<List<LinkAppicationEntity>> obj = new TData<List<LinkAppicationEntity>>();
            obj.Data = await linkAppicationService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LinkAppicationEntity>>> GetPageList(LinkAppicationListParam param, Pagination pagination)
        {
            TData<List<LinkAppicationEntity>> obj = new TData<List<LinkAppicationEntity>>();
            obj.Data = await linkAppicationService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LinkAppicationEntity>> GetEntity(long id)
        {
            TData<LinkAppicationEntity> obj = new TData<LinkAppicationEntity>();
            obj.Data = await linkAppicationService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(LinkAppicationEntity entity)
        {
            TData<string> obj = new TData<string>();
            await linkAppicationService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<LinkAppicationEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await linkAppicationService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await linkAppicationService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(LinkAppicationEntity entity, Expression<Func<LinkAppicationEntity, LinkAppicationEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await linkAppicationService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
