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
using YS.Admin.Model.Result;

namespace YS.Admin.Business.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-20 22:57
    /// 描 述：业务类
    /// </summary>
    public class AdvertBLL
    {
        private AdvertService advertService = new AdvertService();
        private AdvertBannerService advertBannerService = new AdvertBannerService();

        #region 获取数据
        public async Task<TData<List<AdvertEntity>>> GetList(AdvertListParam param)
        {
            TData<List<AdvertEntity>> obj = new TData<List<AdvertEntity>>();
            obj.Data = await advertService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AdvertEntity>>> GetPageList(AdvertListParam param, Pagination pagination)
        {
            TData<List<AdvertEntity>> obj = new TData<List<AdvertEntity>>();
            obj.Data = await advertService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AdvertEntity>> GetEntity(long id)
        {
            TData<AdvertEntity> obj = new TData<AdvertEntity>();
            obj.Data = await advertService.GetEntity(id);
            AdvertBannerListParam advertBannerListParam = new AdvertBannerListParam();
            //if (obj.Data != null)
            //{
            //    advertBannerListParam.Aid = obj.Data.Id;
            //    obj.Data.advertBannerEntities = await advertBannerService.GetList(advertBannerListParam);
            //}
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        public async Task<AdvertInfo> GetEntityInfo(long id)
        {
            AdvertInfo obj = new AdvertInfo();

            AdvertEntity advertEntity = await advertService.GetEntity(id);
            if (advertEntity != null)
            {
                obj.Id = advertEntity.Id;
                obj.Title = advertEntity.Title;
                obj.Price = advertEntity.Price;
                obj.Remark = advertEntity.Remark;
                obj.ViewNum = advertEntity.ViewNum;
                obj.ViewWidth = advertEntity.ViewWidth;
                obj.ViewHeight = advertEntity.ViewHeight;
                obj.Target = advertEntity.Target;
            }
            //obj.Data = ;
            AdvertBannerListParam advertBannerListParam = new AdvertBannerListParam();
            if (obj != null)
            {
                advertBannerListParam.Aid = obj.Id;
                obj.advertBannerEntities = await advertBannerService.GetList(advertBannerListParam);
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AdvertEntity entity)
        {
            TData<string> obj = new TData<string>();
            await advertService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await advertService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
