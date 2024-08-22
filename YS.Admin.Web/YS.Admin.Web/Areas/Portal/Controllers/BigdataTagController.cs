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
using YS.Admin.Entity.Portal;
using YS.Admin.Business.Portal;
using YS.Admin.Model.Param.Portal;

namespace YS.Admin.Web.Areas.Portal.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-07 16:24
    /// 描 述：大数据统计标志控制器类
    /// </summary>
    [Area("Portal")]
    public class BigdataTagController : BaseController
    {
        private BigdataTagBLL bigdataTagBLL = new BigdataTagBLL();

        #region 视图功能
        [AuthorizeFilter("portal:bigdatatag:view")]
        public ActionResult BigdataTagIndex()
        {
            return View();
        }

        public ActionResult BigdataTagForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("portal:bigdatatag:search")]
        public async Task<ActionResult> GetListJson(BigdataTagListParam param)
        {
            TData<List<BigdataTagEntity>> obj = await bigdataTagBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("portal:bigdatatag:search")]
        public async Task<ActionResult> GetPageListJson(BigdataTagListParam param, Pagination pagination)
        {
            TData<List<BigdataTagEntity>> obj = await bigdataTagBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<BigdataTagEntity> obj = await bigdataTagBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("portal:bigdatatag:add,portal:bigdatatag:edit")]
        public async Task<ActionResult> SaveFormJson(BigdataTagEntity entity)
        {
            TData<string> obj = await bigdataTagBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("portal:bigdatatag:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await bigdataTagBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
