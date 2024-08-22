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
using YS.Admin.Business;

namespace YS.Admin.Business.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表业务类
    /// </summary>
    public class PrintdataBaseBLL
    {
        private PrintdataBaseService printdataBaseService = new PrintdataBaseService();

        #region 获取数据 

        public async Task<TData<List<PrintdataBaseEntity>>> GetPageList(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PrintdataBaseEntity>>> GetPageListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPageListCount(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetUnLockTempPageList(PrintdataTempListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetUnLockTempPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetPrintTempPageListJson(PrintdataTempListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPrintTempPageListJson(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetPrintPageList(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPrintPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetPrintPageListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPrintPageListCount(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetPrintTypeDataList(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPrintTypeDataList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetPrintTypeDataListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetPrintTypeDataListCount(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetDatabaseTypeDataListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetDatabaseTypeDataListCount(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<PrintdataBaseEntity>>> GetDatabaseTypeDataList(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = new TData<List<PrintdataBaseEntity>>();
            obj.Data = await printdataBaseService.GetDatabaseTypeDataList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PrintdataBaseEntity>> GetEntity(long id)
        {
            TData<PrintdataBaseEntity> obj = new TData<PrintdataBaseEntity>();
            obj.Data = await printdataBaseService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(PrintdataBaseSaveParam entity)
        {
            
            TData<string> obj = new TData<string>();
            if (entity == null)
            {
                obj.Message = "发送数据无法解析";
                obj.Tag = 0;
            } else
            {

                await printdataBaseService.SaveForm(entity);
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
            }
            return obj;
        }
        public async Task<TData<string>> SaveTempForm(PrintdataBaseSaveParam entity)
        {
            TData<string> obj = new TData<string>();
            await printdataBaseService.SaveTempForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> LockSaveForm(PrintdataBaseSaveParam entity)
        {
            TData<string> obj = new TData<string>();
            await printdataBaseService.LockSaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UnLockSaveForm(PrintdataBaseSaveParam entity)
        {
            TData<string> obj = new TData<string>();
            await printdataBaseService.UnLockSaveForm(entity);
            if (entity.State)
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = entity.ParamMsg;
            }
            else
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 0;
                obj.Message = entity.ParamMsg;
            }
            return obj;
        }
        public async Task<TData<string>> SaveAndPrintForm(PrintdataBaseSaveParam entity)
        {
            TData<string> obj = new TData<string>();
            await printdataBaseService.SaveAndPrintForm(entity);
            if (entity.State)
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = entity.ParamMsg;
            }
            else
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 0;
                obj.Message = entity.ParamMsg;
            }
            return obj;
        } 
        public async Task<TData<string>> LockAndPrintForm(PrintdataBaseSaveParam entity)
        {
            TData<string> obj = new TData<string>();
            await printdataBaseService.LockAndPrintForm(entity);
            if (entity.State)
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = entity.ParamMsg;
            }
            else
            {
                obj.Data = entity.Id.ToString();
                obj.Tag = 0;
                obj.Message = entity.ParamMsg;
            }
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await printdataBaseService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
