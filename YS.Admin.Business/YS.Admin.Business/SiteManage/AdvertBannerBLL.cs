using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Service.SiteManage;

namespace YS.Admin.Business.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-20 23:03
    /// 描 述：业务类
    /// </summary>
    public class AdvertBannerBLL
    {
        private AdvertBannerService advertBannerService = new AdvertBannerService();

        #region 获取数据
        public async Task<TData<List<AdvertBannerEntity>>> GetList(AdvertBannerListParam param)
        {
            TData<List<AdvertBannerEntity>> obj = new TData<List<AdvertBannerEntity>>();
            obj.Data = await advertBannerService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AdvertBannerEntity>>> GetPageList(AdvertBannerListParam param, Pagination pagination)
        {
            TData<List<AdvertBannerEntity>> obj = new TData<List<AdvertBannerEntity>>();
            obj.Data = await advertBannerService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AdvertBannerEntity>> GetEntity(long id)
        {
            TData<AdvertBannerEntity> obj = new TData<AdvertBannerEntity>();
            obj.Data = await advertBannerService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AdvertBannerEntity entity)
        {
            TData<string> obj = new TData<string>();
            await advertBannerService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await advertBannerService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
