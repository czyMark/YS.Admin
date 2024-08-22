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
    /// 日 期：2024-07-17 16:48
    /// 描 述：分表数据业务类
    /// </summary>
    public class PringdatamaintagBLL
    {
        private PringdatamaintagService pringdatamaintagService = new PringdatamaintagService();

        #region 获取数据
        public async Task<TData<List<PringdatamaintagEntity>>> GetList(PringdatamaintagListParam param)
        {
            TData<List<PringdatamaintagEntity>> obj = new TData<List<PringdatamaintagEntity>>();
            obj.Data = await pringdatamaintagService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PringdatamaintagEntity>>> GetPageList(PringdatamaintagListParam param, Pagination pagination)
        {
            TData<List<PringdatamaintagEntity>> obj = new TData<List<PringdatamaintagEntity>>();
            obj.Data = await pringdatamaintagService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PringdatamaintagEntity>> GetEntity(long id)
        {
            TData<PringdatamaintagEntity> obj = new TData<PringdatamaintagEntity>();
            obj.Data = await pringdatamaintagService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(PringdatamaintagEntity entity)
        {
            TData<string> obj = new TData<string>();
            await pringdatamaintagService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData> UpdataTotal(string ids)
        {
            TData obj = new TData();
            await pringdatamaintagService.UpdataTotal(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await pringdatamaintagService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
