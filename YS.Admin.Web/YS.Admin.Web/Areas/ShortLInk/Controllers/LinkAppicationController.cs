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
using YS.Admin.Entity.ShortLInk;
using YS.Admin.Business.ShortLInk;
using YS.Admin.Model.Param.ShortLInk;

namespace YS.Admin.Web.Areas.ShortLInk.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 18:32
    /// 描 述：短链接应用管理控制器类
    /// </summary>
    [Area("ShortLInk")]
    public class LinkAppicationController :  BaseController
    {
        private LinkAppicationBLL linkAppicationBLL = new LinkAppicationBLL();

        #region 视图功能
        [AuthorizeFilter("shortlink:linkappication:view")]
        public ActionResult LinkAppicationIndex()
        {
            return View();
        }

        public ActionResult LinkAppicationForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("shortlink:linkappication:search")]
        public async Task<ActionResult> GetListJson(LinkAppicationListParam param)
        {
            TData<List<LinkAppicationEntity>> obj = await linkAppicationBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("shortlink:linkappication:search")]
        public async Task<ActionResult> GetPageListJson(LinkAppicationListParam param, Pagination pagination)
        {
            TData<List<LinkAppicationEntity>> obj = await linkAppicationBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<LinkAppicationEntity> obj = await linkAppicationBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("shortlink:linkappication:add,shortlink:linkappication:edit")]
        public async Task<ActionResult> SaveFormJson(LinkAppicationEntity entity)
        {
            TData<string> obj = await linkAppicationBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("shortlink:linkappication:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await linkAppicationBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
