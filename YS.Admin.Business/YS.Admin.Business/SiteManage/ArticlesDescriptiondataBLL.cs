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
    /// 日 期：2024-08-05 14:00
    /// 描 述：栏目文字数据业务类
    /// </summary>
    public class ArticlesDescriptiondataBLL
    {
        private ArticlesDescriptiondataService articlesDescriptiondataService = new ArticlesDescriptiondataService();

        #region 获取数据
        public async Task<TData<List<ArticlesDescriptiondataEntity>>> GetList(ArticlesDescriptiondataListParam param)
        {
            TData<List<ArticlesDescriptiondataEntity>> obj = new TData<List<ArticlesDescriptiondataEntity>>();
            obj.Data = await articlesDescriptiondataService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ArticlesDescriptiondataEntity>>> GetPageList(ArticlesDescriptiondataListParam param, Pagination pagination)
        {
            TData<List<ArticlesDescriptiondataEntity>> obj = new TData<List<ArticlesDescriptiondataEntity>>();
            obj.Data = await articlesDescriptiondataService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ArticlesDescriptiondataEntity>> GetEntity(long id)
        {
            TData<ArticlesDescriptiondataEntity> obj = new TData<ArticlesDescriptiondataEntity>();
            obj.Data = await articlesDescriptiondataService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ArticlesDescriptiondataEntity entity)
        {
            TData<string> obj = new TData<string>();
            await articlesDescriptiondataService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> SaveIDCOdeCreateForm(ArticlesDescriptiondataEntity entity)
        {
            TData<string> obj = new TData<string>();
           string msg=  await articlesDescriptiondataService.SaveIDCOdeCreateForm(entity);
            if (string.IsNullOrEmpty(msg))
            {
                obj.Tag = 1;
            }
            else
            {
                obj.Tag = 0;
            }
            obj.Message = msg;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await articlesDescriptiondataService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
