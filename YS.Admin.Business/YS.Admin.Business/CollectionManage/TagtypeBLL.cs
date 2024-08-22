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
    /// 日 期：2024-07-17 16:45
    /// 描 述：标签类型业务类
    /// </summary>
    public class TagtypeBLL
    {
        private TagtypeService tagtypeService = new TagtypeService();

        #region 获取数据
        public async Task<TData<List<TagtypeEntity>>> GetList(TagtypeListParam param)
        {
            TData<List<TagtypeEntity>> obj = new TData<List<TagtypeEntity>>();
            obj.Data = await tagtypeService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<TagtypeEntity>>> GetPageList(TagtypeListParam param, Pagination pagination)
        {
            TData<List<TagtypeEntity>> obj = new TData<List<TagtypeEntity>>();
            obj.Data = await tagtypeService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<TagtypeEntity>> GetEntity(long id)
        {
            TData<TagtypeEntity> obj = new TData<TagtypeEntity>();
            obj.Data = await tagtypeService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(TagtypeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await tagtypeService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await tagtypeService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
