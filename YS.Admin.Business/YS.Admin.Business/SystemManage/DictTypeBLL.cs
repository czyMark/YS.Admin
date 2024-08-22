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

namespace YS.Admin.Business.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2021-10-15 13:19
    /// 描 述：字典管理业务类
    /// </summary>
    public class DictTypeBLL
    {
        private DictTypeService dictTypeService = new DictTypeService();

        #region 获取数据
        public async Task<TData<List<DictTypeEntity>>> GetList(DictTypeListParam param)
        {
            TData<List<DictTypeEntity>> obj = new TData<List<DictTypeEntity>>();
            obj.Data = await dictTypeService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DictTypeEntity>>> GetPageList(DictTypeListParam param, Pagination pagination)
        {
            TData<List<DictTypeEntity>> obj = new TData<List<DictTypeEntity>>();
            obj.Data = await dictTypeService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DictTypeEntity>> GetEntity(long id)
        {
            TData<DictTypeEntity> obj = new TData<DictTypeEntity>();
            obj.Data = await dictTypeService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(DictTypeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await dictTypeService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await dictTypeService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
