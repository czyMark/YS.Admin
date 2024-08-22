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
    /// 日 期：2024-08-14 21:10
    /// 描 述：ip地址访问黑名单控制器类
    /// </summary>
    [Area("SystemManage")]
    public class IpBlockController :  BaseController
    {
        private IpBlockBLL ipBlockBLL = new IpBlockBLL();

        #region 视图功能
        [AuthorizeFilter("system:ipblock:view")]
        public ActionResult IpBlockIndex()
        {
            return View();
        }

        public ActionResult IpBlockForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:ipblock:search")]
        public async Task<ActionResult> GetListJson(IpBlockListParam param)
        {
            TData<List<IpBlockEntity>> obj = await ipBlockBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:ipblock:search")]
        public async Task<ActionResult> GetPageListJson(IpBlockListParam param, Pagination pagination)
        {
            TData<List<IpBlockEntity>> obj = await ipBlockBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<IpBlockEntity> obj = await ipBlockBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:ipblock:add,system:ipblock:edit")]
        public async Task<ActionResult> SaveFormJson(IpBlockEntity entity)
        {
            TData<string> obj = await ipBlockBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:ipblock:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await ipBlockBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
