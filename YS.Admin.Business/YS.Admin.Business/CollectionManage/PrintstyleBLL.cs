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
    /// 描 述：打印样式业务类
    /// </summary>
    public class PrintstyleBLL
    {
        private PrintstyleService printstyleService = new PrintstyleService();

        #region 获取数据
        public async Task<TData<List<PrintstyleEntity>>> GetList(PrintstyleListParam param)
        {
            TData<List<PrintstyleEntity>> obj = new TData<List<PrintstyleEntity>>();
            obj.Data = await printstyleService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PrintstyleEntity>>> GetPageList(PrintstyleListParam param, Pagination pagination)
        {
            TData<List<PrintstyleEntity>> obj = new TData<List<PrintstyleEntity>>();
            obj.Data = await printstyleService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PrintstyleEntity>> GetEntity(long id)
        {
            TData<PrintstyleEntity> obj = new TData<PrintstyleEntity>();
            obj.Data = await printstyleService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(PrintstyleEntity entity)
        {
            TData<string> obj = new TData<string>();
            await printstyleService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await printstyleService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
