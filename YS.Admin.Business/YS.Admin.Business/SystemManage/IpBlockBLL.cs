using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Service.SystemManage;
using System.Linq.Expressions;
using YS.Admin.Business.Cache;

namespace YS.Admin.Business.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-14 21:10
    /// 描 述：ip地址访问黑名单业务类
    /// </summary>
    public class IpBlockBLL
    {
        private IpBlockService ipBlockService = new IpBlockService();
		private IpBlockCache cache = new IpBlockCache();


		#region 获取数据
		public async Task<TData<List<IpBlockEntity>>> GetList(IpBlockListParam param)
		{
			TData<List<IpBlockEntity>> obj = new TData<List<IpBlockEntity>>();
			List<IpBlockEntity> itemList = await cache.GetList();
			if (param != null)
			{
				if (!param.IpAddr.IsEmpty())
				{
					itemList = itemList.Where(t => t.IpAddr.Contains(param.IpAddr)).ToList();
				}
				if (!param.Remark.IsEmpty())
				{
					itemList = itemList.Where(t => t.Remark.Contains(param.Remark)).ToList();
				}
			}
			obj.Data = itemList;
			obj.Tag = 1;
			return obj;
             
        }

        public async Task<TData<List<IpBlockEntity>>> GetPageList(IpBlockListParam param, Pagination pagination)
        {
            TData<List<IpBlockEntity>> obj = new TData<List<IpBlockEntity>>();
            obj.Data = await ipBlockService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<IpBlockEntity>> GetEntity(long id)
        {
            TData<IpBlockEntity> obj = new TData<IpBlockEntity>();
            obj.Data = await ipBlockService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(IpBlockEntity entity)
        {
            TData<string> obj = new TData<string>();
            await ipBlockService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
			cache.Remove();
			return obj;
        }

        public async Task<TData<string>> SaveForms(List<IpBlockEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await ipBlockService.SaveForms(entities);
            obj.Tag = 1;
			cache.Remove();
			return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await ipBlockService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(IpBlockEntity entity, Expression<Func<IpBlockEntity, IpBlockEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await ipBlockService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
