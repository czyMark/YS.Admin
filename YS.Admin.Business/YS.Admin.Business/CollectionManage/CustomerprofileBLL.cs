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
    /// 日 期：2024-07-17 17:04
    /// 描 述：客户档案业务类
    /// </summary>
    public class CustomerprofileBLL
    {
        private CustomerprofileService customerprofileService = new CustomerprofileService();

        #region 获取数据
        public async Task<TData<List<CustomerprofileEntity>>> GetList(CustomerprofileListParam param)
        {
            TData<List<CustomerprofileEntity>> obj = new TData<List<CustomerprofileEntity>>();
            obj.Data = await customerprofileService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CustomerprofileEntity>>> GetPageList(CustomerprofileListParam param, Pagination pagination)
        {
            TData<List<CustomerprofileEntity>> obj = new TData<List<CustomerprofileEntity>>();
            obj.Data = await customerprofileService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CustomerprofileEntity>> GetEntity(long id)
        {
            TData<CustomerprofileEntity> obj = new TData<CustomerprofileEntity>();
            obj.Data = await customerprofileService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(CustomerprofileEntity entity)
        {
            TData<string> obj = new TData<string>();
            await customerprofileService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await customerprofileService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
