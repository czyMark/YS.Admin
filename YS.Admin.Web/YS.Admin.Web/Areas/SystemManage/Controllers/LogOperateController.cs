using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Web.Controllers;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Model;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Util.Model;

namespace YS.Admin.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class LogOperateController : BaseController
    {
        private LogOperateBLL logOperateBLL = new LogOperateBLL();

        #region 视图功能
        [AuthorizeFilter("system:logoperate:view")]
        public IActionResult LogOperateIndex()
        {
            return View();
        }

        public IActionResult LogOperateDetail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:logoperate:search")]
        public async Task<IActionResult> GetListJson(LogOperateListParam param)
        {
            TData<List<LogOperateEntity>> obj = await logOperateBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:logoperate:search")]
        public async Task<IActionResult> GetPageListJson(LogOperateListParam param, Pagination pagination)
        {
            TData<List<LogOperateEntity>> obj = await logOperateBLL.GetPageList(param, pagination);
            object objs = Json(obj);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<LogOperateEntity> obj = await logOperateBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:logoperate:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await logOperateBLL.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:logoperate:delete")]
        public async Task<IActionResult> RemoveAllFormJson()
        {
            TData obj = await logOperateBLL.RemoveAllForm();
            return Json(obj);
        }
        #endregion
    }
}