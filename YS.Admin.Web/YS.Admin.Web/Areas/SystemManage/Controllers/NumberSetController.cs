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
    /// 创 建：xiaoyu
    /// 日 期：2024-05-13 22:33
    /// 描 述：系统编号设置控制器类
    /// </summary>
    [Area("SystemManage")]
    public class NumberSetController :  BaseController
    {
        private NumberSetBLL numberSetBLL = new NumberSetBLL();

        #region 视图功能
        [AuthorizeFilter("system:numberset:view")]
        public ActionResult NumberSetIndex()
        {
            return View();
        }

        public ActionResult NumberSetForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:numberset:search")]
        public async Task<ActionResult> GetListJson(NumberSetListParam param)
        {
            TData<List<NumberSetEntity>> obj = await numberSetBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:numberset:search")]
        public async Task<ActionResult> GetPageListJson(NumberSetListParam param, Pagination pagination)
        {
            TData<List<NumberSetEntity>> obj = await numberSetBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<NumberSetEntity> obj = await numberSetBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:numberset:add,system:numberset:edit")]
        public async Task<ActionResult> SaveFormJson(NumberSetEntity entity)
        {
            TData<string> obj = await numberSetBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:numberset:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await numberSetBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
