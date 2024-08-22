using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;
using YS.Admin.Service.CollectionManage;

namespace YS.Admin.Business.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:28
    /// 描 述：鉴定师业务类
    /// </summary>
    public class AppraiserBLL
    {
        private AppraiserService appraiserService = new AppraiserService();

        #region 获取数据
        public async Task<TData<List<AppraiserEntity>>> GetList(AppraiserListParam param)
        {
            TData<List<AppraiserEntity>> obj = new TData<List<AppraiserEntity>>();
            obj.Data = await appraiserService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AppraiserEntity>>> GetPageList(AppraiserListParam param, Pagination pagination)
        {
            TData<List<AppraiserEntity>> obj = new TData<List<AppraiserEntity>>();
            obj.Data = await appraiserService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AppraiserEntity>> GetEntity(long id)
        {
            TData<AppraiserEntity> obj = new TData<AppraiserEntity>();
            obj.Data = await appraiserService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AppraiserEntity entity)
        {
            TData<string> obj = new TData<string>();
            await appraiserService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await appraiserService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
