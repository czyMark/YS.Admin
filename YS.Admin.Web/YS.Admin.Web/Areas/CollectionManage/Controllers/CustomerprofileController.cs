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
	/// 日 期：2024-07-17 17:04
	/// 描 述：客户档案控制器类Customerprofile
	/// </summary>
	[Area("CollectionManage")]
    public class CustomerprofileController :  BaseController
    {
        private CustomerprofileBLL customerprofileBLL = new CustomerprofileBLL();

        #region 视图功能
        [AuthorizeFilter("collection:customerprofile:view")]
        public ActionResult CustomerprofileIndex()
        {
            return View();
        }

        public ActionResult CustomerprofileForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:customerprofile:search")]
        public async Task<ActionResult> GetListJson(CustomerprofileListParam param)
        {
            TData<List<CustomerprofileEntity>> obj = await customerprofileBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:customerprofile:search")]
        public async Task<ActionResult> GetPageListJson(CustomerprofileListParam param, Pagination pagination)
        {
            TData<List<CustomerprofileEntity>> obj = await customerprofileBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<CustomerprofileEntity> obj = await customerprofileBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:customerprofile:add")]
        public async Task<ActionResult> SaveFormJson(CustomerprofileEntity entity)
        {
            TData<string> obj = await customerprofileBLL.SaveForm(entity);
            return Json(obj);
        }
         
        #endregion
    }
}
