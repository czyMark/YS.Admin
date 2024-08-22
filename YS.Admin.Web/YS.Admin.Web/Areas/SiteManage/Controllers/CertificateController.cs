using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Entity;
using YS.Admin.Model;
using YS.Admin.Web.Controllers;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Business.SiteManage;
using YS.Admin.Model.Param.SiteManage;

namespace YS.Admin.Web.Areas.SiteManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-09 13:08
    /// 描 述：证书管理控制器类
    /// </summary>
    [Area("SiteManage")]
    public class CertificateController :  BaseController
    {
        private CertificateBLL certificateBLL = new CertificateBLL();

        #region 视图功能
        [AuthorizeFilter("site:certificate:view")]
        public ActionResult CertificateIndex()
        {
            return View();
        }

        public ActionResult CertificateForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("site:certificate:search")]
        public async Task<ActionResult> GetListJson(CertificateListParam param)
        {
            TData<List<CertificateEntity>> obj = await certificateBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("site:certificate:search")]
        public async Task<ActionResult> GetPageListJson(CertificateListParam param, Pagination pagination)
        {
            TData<List<CertificateEntity>> obj = await certificateBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<CertificateEntity> obj = await certificateBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("site:certificate:add,site:certificate:edit")]
        public async Task<ActionResult> SaveFormJson(CertificateEntity entity)
        {
            TData<string> obj = await certificateBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("site:certificate:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await certificateBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
