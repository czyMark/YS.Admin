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
    /// 日 期：2022-02-01 03:31
    /// 描 述：业务类
    /// </summary>
    public class SiteConfigBLL
    {
        private SiteConfigService siteConfigService = new SiteConfigService();

        #region 获取数据
        public async Task<TData<List<SiteConfigEntity>>> GetList(SiteConfigListParam param)
        {
            TData<List<SiteConfigEntity>> obj = new TData<List<SiteConfigEntity>>();
            obj.Data = await siteConfigService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SiteConfigEntity>>> GetPageList(SiteConfigListParam param, Pagination pagination)
        {
            TData<List<SiteConfigEntity>> obj = new TData<List<SiteConfigEntity>>();
            obj.Data = await siteConfigService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SiteConfigEntity>> GetEntity(long id)
        {
            TData<SiteConfigEntity> obj = new TData<SiteConfigEntity>();
            obj.Data = await siteConfigService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }

        public async Task<TData<SiteConfigEntity>> GetDefaultList()
        {
            TData<SiteConfigEntity> obj = new TData<SiteConfigEntity>();
            obj.Data = await siteConfigService.GetDefaultList();
            obj.Total = 1;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(SiteConfigEntity entity)
        {
            TData<string> obj = new TData<string>();
            await siteConfigService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await siteConfigService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
