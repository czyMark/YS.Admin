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
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Business.OrganizationManage;
using YS.Admin.Model.Param.OrganizationManage;

namespace YS.Admin.Web.Areas.OrganizationManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 15:06
    /// 描 述：共享文件控制器类
    /// </summary>
    [Area("OrganizationManage")]
    public class SharedFileController :  BaseController
    {
        private SharedFileBLL sharedFileBLL = new SharedFileBLL();

        #region 视图功能
        [AuthorizeFilter("organization:sharedfile:view")]
        public ActionResult SharedFileIndex()
        {
            return View();
        }

        public ActionResult SharedFileForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("organization:sharedfile:search")]
        public async Task<ActionResult> GetListJson(SharedFileListParam param)
        {
            TData<List<SharedFileEntity>> obj = await sharedFileBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("organization:sharedfile:search")]
        public async Task<ActionResult> GetPageListJson(SharedFileListParam param, Pagination pagination)
        {
            TData<List<SharedFileEntity>> obj = await sharedFileBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<SharedFileEntity> obj = await sharedFileBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("organization:sharedfile:add,organization:sharedfile:edit")]
        public async Task<ActionResult> SaveFormJson(SharedFileEntity entity)
        {
            TData<string> obj = await sharedFileBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("organization:sharedfile:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await sharedFileBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
