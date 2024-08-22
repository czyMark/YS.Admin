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
    /// 日 期：2024-07-17 16:45
    /// 描 述：标签类型控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class TagtypeController :  BaseController
    {
        private TagtypeBLL tagtypeBLL = new TagtypeBLL();

        #region 视图功能
        [AuthorizeFilter("collection:tagtype:view")]
        public ActionResult TagtypeIndex()
        {
            return View();
        }

        public ActionResult TagtypeForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:tagtype:search")]
        public async Task<ActionResult> GetListJson(TagtypeListParam param)
        {
            TData<List<TagtypeEntity>> obj = await tagtypeBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:tagtype:search")]
        public async Task<ActionResult> GetPageListJson(TagtypeListParam param, Pagination pagination)
        {
            TData<List<TagtypeEntity>> obj = await tagtypeBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<TagtypeEntity> obj = await tagtypeBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:tagtype:add,collection:tagtype:edit")]
        public async Task<ActionResult> SaveFormJson(TagtypeEntity entity)
        {
            TData<string> obj = await tagtypeBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("collection:tagtype:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await tagtypeBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
