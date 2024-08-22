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
using YS.Admin.Entity.BlogManage;
using YS.Admin.Business.BlogManage;
using YS.Admin.Model.Param.BlogManage;

namespace YS.Admin.Web.Areas.BlogManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:06
    /// 描 述：日记控制器类
    /// </summary>
    [Area("BlogManage")]
    public class DiarysController :  BaseController
    {
        private DiarysBLL diarysBLL = new DiarysBLL();

        #region 视图功能
        [AuthorizeFilter("blog:diarys:view")]
        public ActionResult DiarysIndex()
        {
            return View();
        }

        public ActionResult DiarysForm()
        {
            return View();
        }

        public IActionResult IconForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("blog:diarys:search")]
        public async Task<ActionResult> GetListJson(DiarysListParam param)
        {
            TData<List<DiarysEntity>> obj = await diarysBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("blog:diarys:search")]
        public async Task<ActionResult> GetPageListJson(DiarysListParam param, Pagination pagination)
        {
            TData<List<DiarysEntity>> obj = await diarysBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<DiarysEntity> obj = await diarysBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("blog:diarys:add,blog:diarys:edit")]
        public async Task<ActionResult> SaveFormJson(DiarysEntity entity)
        {
            TData<string> obj = await diarysBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("blog:diarys:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await diarysBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
