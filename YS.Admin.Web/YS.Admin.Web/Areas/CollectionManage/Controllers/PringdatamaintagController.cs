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
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Business.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;

namespace YS.Admin.Web.Areas.CollectionManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:48
    /// 描 述：分表数据控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class PringdatamaintagController :  BaseController
    {
        private PringdatamaintagBLL pringdatamaintagBLL = new PringdatamaintagBLL();

        #region 视图功能
        [AuthorizeFilter("collection:pringdatamaintag:view")]
        public ActionResult PringdatamaintagIndex()
        {
            return View();
        }

        public ActionResult PringdatamaintagForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:pringdatamaintag:search")]
        public async Task<ActionResult> GetListJson(PringdatamaintagListParam param)
        {
            TData<List<PringdatamaintagEntity>> obj = await pringdatamaintagBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:pringdatamaintag:search")]
        public async Task<ActionResult> GetPageListJson(PringdatamaintagListParam param, Pagination pagination)
        {
            TData<List<PringdatamaintagEntity>> obj = await pringdatamaintagBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<PringdatamaintagEntity> obj = await pringdatamaintagBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:pringdatamaintag:add,collection:pringdatamaintag:edit")]
        public async Task<ActionResult> SaveFormJson(PringdatamaintagEntity entity)
        {
            //
            TData<string> obj = await pringdatamaintagBLL.SaveForm(entity);
            return Json(obj);
        }
         
        [HttpPost]
        [AuthorizeFilter("collection:pringdatamaintag:updatatotal")]
        public async Task<ActionResult> UpdataTotal(string ids)
        {
            TData obj = await pringdatamaintagBLL.UpdataTotal(ids);
            return Json(obj);
        }
        #endregion
    }
}
