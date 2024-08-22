using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Model.Param.OrganizationManage;
using YS.Admin.Service.OrganizationManage;
using System.Linq.Expressions;

namespace YS.Admin.Business.OrganizationManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 15:06
    /// 描 述：共享文件业务类
    /// </summary>
    public class SharedFileBLL
    {
        private SharedFileService sharedFileService = new SharedFileService();

        #region 获取数据
        public async Task<TData<List<SharedFileEntity>>> GetList(SharedFileListParam param)
        {
            TData<List<SharedFileEntity>> obj = new TData<List<SharedFileEntity>>();
            obj.Data = await sharedFileService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SharedFileEntity>>> GetPageList(SharedFileListParam param, Pagination pagination)
        {
            TData<List<SharedFileEntity>> obj = new TData<List<SharedFileEntity>>();
            obj.Data = await sharedFileService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SharedFileEntity>> GetEntity(long id)
        {
            TData<SharedFileEntity> obj = new TData<SharedFileEntity>();
            obj.Data = await sharedFileService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(SharedFileEntity entity)
        {
            TData<string> obj = new TData<string>();
            await sharedFileService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<SharedFileEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await sharedFileService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await sharedFileService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(SharedFileEntity entity, Expression<Func<SharedFileEntity, SharedFileEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await sharedFileService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
