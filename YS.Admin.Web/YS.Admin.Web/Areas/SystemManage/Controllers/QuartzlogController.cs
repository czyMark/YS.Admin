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
using YS.Admin.Entity.SystemManage;
using YS.Admin.Business.SystemManage;
using YS.Admin.Model.Param.SystemManage;

namespace YS.Admin.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-11 23:06
    /// 描 述：自动执行任务日志控制器类
    /// </summary>
    [Area("SystemManage")]
    public class QuartzlogController :  BaseController
    {
        private QuartzlogBLL quartzlogBLL = new QuartzlogBLL();

        #region 视图功能
        [AuthorizeFilter("system:quartzlog:view")]
        public ActionResult QuartzlogIndex()
        {
            return View();
        }

        public ActionResult QuartzlogForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:quartzlog:search")]
        public async Task<ActionResult> GetListJson(QuartzlogListParam param)
        {
            TData<List<QuartzlogEntity>> obj = await quartzlogBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:quartzlog:search")]
        public async Task<ActionResult> GetPageListJson(QuartzlogListParam param, Pagination pagination)
        {
            TData<List<QuartzlogEntity>> obj = await quartzlogBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<QuartzlogEntity> obj = await quartzlogBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:quartzlog:add,system:quartzlog:edit")]
        public async Task<ActionResult> SaveFormJson(QuartzlogEntity entity)
        {
            TData<string> obj = await quartzlogBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:quartzlog:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await quartzlogBLL.DeleteForm(ids);
            return Json(obj);
        }


        [HttpPost]
        [AuthorizeFilter("system:quartzlog:delete")]
        public async Task<IActionResult> RemoveAllFormJson()
        {
            TData obj = await quartzlogBLL.RemoveAllForm();
            return Json(obj);
        }
        #endregion
    }
}
