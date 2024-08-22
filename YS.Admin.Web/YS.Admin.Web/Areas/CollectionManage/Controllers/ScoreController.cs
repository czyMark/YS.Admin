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
    /// 日 期：2024-07-24 11:29
    /// 描 述：评分信息控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class ScoreController :  BaseController
    {
        private ScoreBLL scoreBLL = new ScoreBLL();

        #region 视图功能
        [AuthorizeFilter("collection:score:view")]
        public ActionResult ScoreIndex()
        {
            return View();
        }

        public ActionResult ScoreForm()
        {
            return View();
        }


        [AuthorizeFilter("collection:scorestamp:view")]
        public ActionResult ScoreStampIndex()
        {
            return View();
        }

        public ActionResult ScoreStampForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("collection:score:search,collection:scorestamp:search")]
        public async Task<ActionResult> GetListJson(ScoreListParam param)
        {
            TData<List<ScoreEntity>> obj = await scoreBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("collection:score:search,collection:scorestamp:search")]
        public async Task<ActionResult> GetPageListJson(ScoreListParam param, Pagination pagination)
        {
            TData<List<ScoreEntity>> obj = await scoreBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<ScoreEntity> obj = await scoreBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("collection:score:add,collection:score:edit")]
        public async Task<ActionResult> SaveFormJson(ScoreEntity entity)
        {
            entity.ScoreType = 0;
            TData<string> obj = await scoreBLL.SaveForm(entity);
            return Json(obj);
        }



        [HttpPost]
        [AuthorizeFilter("collection:scorestamp:add,collection:scorestamp:edit")]
        public async Task<ActionResult> SaveStampFormJson(ScoreEntity entity)
        {
            entity.ScoreType = 1;
            TData<string> obj = await scoreBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("collection:score:delete,collection:scorestamp:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await scoreBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
