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
    /// 日 期：2024-08-16 22:08
    /// 描 述：友情链接业务类
    /// </summary>
    public class LinksBLL
    {
        private LinksService linksService = new LinksService();

        #region 获取数据
        public async Task<TData<List<LinksEntity>>> GetList(LinksListParam param)
        {
            TData<List<LinksEntity>> obj = new TData<List<LinksEntity>>();
            obj.Data = await linksService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LinksEntity>>> GetPageList(LinksListParam param, Pagination pagination)
        {
            TData<List<LinksEntity>> obj = new TData<List<LinksEntity>>();
            obj.Data = await linksService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LinksEntity>> GetEntity(long id)
        {
            TData<LinksEntity> obj = new TData<LinksEntity>();
            obj.Data = await linksService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(LinksEntity entity)
        {
            TData<string> obj = new TData<string>();
            await linksService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<LinksEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await linksService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await linksService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(LinksEntity entity, Expression<Func<LinksEntity, LinksEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await linksService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
