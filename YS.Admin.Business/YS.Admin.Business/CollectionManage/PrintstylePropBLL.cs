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
    /// 日 期：2024-07-17 16:47
    /// 描 述：样式属性业务类
    /// </summary>
    public class PrintstylePropBLL
    {
        private PrintstylePropService printstylePropService = new PrintstylePropService();

        #region 获取数据
        public async Task<TData<List<PrintstylePropEntity>>> GetList(PrintstylePropListParam param)
        {
            TData<List<PrintstylePropEntity>> obj = new TData<List<PrintstylePropEntity>>();
            obj.Data = await printstylePropService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PrintstylePropEntity>>> GetPageList(PrintstylePropListParam param, Pagination pagination)
        {
            TData<List<PrintstylePropEntity>> obj = new TData<List<PrintstylePropEntity>>();
            obj.Data = await printstylePropService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PrintstylePropEntity>>> GetAllList(PrintstylePropListParam param)
        {
            TData<List<PrintstylePropEntity>> obj = new TData<List<PrintstylePropEntity>>();
            obj.Data = await printstylePropService.GetAllList(param);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PrintstylePropEntity>> GetEntity(long id)
        {
            TData<PrintstylePropEntity> obj = new TData<PrintstylePropEntity>();
            obj.Data = await printstylePropService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(PrintstylePropEntity entity)
        {
            TData<string> obj = new TData<string>();
            await printstylePropService.SaveForm(entity);
            if (entity.Token == "元素列已存在")
            {
                obj.Tag = 0;
                obj.Message = entity.Token;
            }
            else
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
            }
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await printstylePropService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
