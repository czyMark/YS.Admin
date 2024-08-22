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
    /// 日 期：2021-10-15 13:19
    /// 描 述：字典管理控制器类
    /// </summary>
    [Area("SystemManage")]
    public class DictTypeController :  BaseController
    {
        private DictTypeBLL dictTypeBLL = new DictTypeBLL();

        #region 视图功能
        [AuthorizeFilter("system:dicttype:view")]
        public ActionResult DictTypeIndex()
        {
            return View();
        }

        public ActionResult DictTypeForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:dicttype:search")]
        public async Task<ActionResult> GetListJson(DictTypeListParam param)
        {
            TData<List<DictTypeEntity>> obj = await dictTypeBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:dicttype:search")]
        public async Task<ActionResult> GetPageListJson(DictTypeListParam param, Pagination pagination)
        {
            TData<List<DictTypeEntity>> obj = await dictTypeBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<DictTypeEntity> obj = await dictTypeBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:dicttype:add,system:dicttype:edit")]
        public async Task<ActionResult> SaveFormJson(DictTypeEntity entity)
        {
            TData<string> obj = await dictTypeBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:dicttype:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await dictTypeBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
