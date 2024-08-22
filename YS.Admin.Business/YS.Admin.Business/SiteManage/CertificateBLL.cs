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
using System.Linq.Expressions;

namespace YS.Admin.Business.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-09 13:08
    /// 描 述：证书管理业务类
    /// </summary>
    public class CertificateBLL
    {
        private CertificateService certificateService = new CertificateService();

        #region 获取数据
        public async Task<TData<List<CertificateEntity>>> GetList(CertificateListParam param)
        {
            TData<List<CertificateEntity>> obj = new TData<List<CertificateEntity>>();
            obj.Data = await certificateService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CertificateEntity>>> GetPageList(CertificateListParam param, Pagination pagination)
        {
            TData<List<CertificateEntity>> obj = new TData<List<CertificateEntity>>();
            obj.Data = await certificateService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CertificateEntity>> GetEntity(long id)
        {
            TData<CertificateEntity> obj = new TData<CertificateEntity>();
            obj.Data = await certificateService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }

        public async Task<TData<CertificateEntity>> IDCodeGetEntity(string IDCode)
        {
            TData<CertificateEntity> obj = new TData<CertificateEntity>();
            obj.Data = await certificateService.IDCodeGetEntity(IDCode);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            else
            {
                obj.Message = "没有该证书信息";
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(CertificateEntity entity)
        {
            TData<string> obj = new TData<string>();
            await certificateService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<CertificateEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await certificateService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await certificateService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(CertificateEntity entity, Expression<Func<CertificateEntity, CertificateEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await certificateService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
