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
using TagLib.Ape;
using YS.Admin.Enum;
using YS.Admin.Service.SystemManage;
using YS.Admin.Business.AutoJob;
using YS.Admin.Util.Extension;

namespace YS.Admin.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-13 22:29
    /// 描 述：自动执行任务控制器类
    /// </summary>
    [Area("SystemManage")]
    public class QuartzoptionsController : BaseController
    {
        private QuartzoptionsBLL quartzoptionsBLL = new QuartzoptionsBLL();

        #region 视图功能
        [AuthorizeFilter("system:quartzoptions:view")]
        public ActionResult QuartzoptionsIndex()
        {
            return View();
        }

        public ActionResult QuartzoptionsForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:quartzoptions:search")]
        public async Task<ActionResult> GetListJson(QuartzoptionsListParam param)
        {
            TData<List<QuartzoptionsEntity>> obj = await quartzoptionsBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:quartzoptions:search")]
        public async Task<ActionResult> GetPageListJson(QuartzoptionsListParam param, Pagination pagination)
        {
            TData<List<QuartzoptionsEntity>> obj = await quartzoptionsBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<QuartzoptionsEntity> obj = await quartzoptionsBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:quartzoptions:add,system:quartzoptions:edit")]
        public async Task<ActionResult> SaveFormJson(QuartzoptionsEntity entity)
        {

            //修改状态 
            int c = entity.TaskStatus.ParseToInt();
            StatusEnum enumValueParsed = (StatusEnum)StatusEnum.Parse(typeof(StatusEnum), c.ToString());
            switch (enumValueParsed)
            {
                case StatusEnum.Yes:
                    //开始任务
                    new JobCenter().AddScheduleJob(entity);
                    break;
            }

            TData<string> obj = await quartzoptionsBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeTaskStatusJson(QuartzoptionsEntity entity)
        {
            //修改状态 
            int c = entity.TaskStatus.ParseToInt();
            StatusEnum enumValueParsed = (StatusEnum)StatusEnum.Parse(typeof(StatusEnum), c.ToString());

            var task = await quartzoptionsBLL.GetEntity(entity.Id.ParseToLong());
            switch (enumValueParsed)
            {
                case StatusEnum.Yes:
                    //开始任务
                    new JobCenter().AddScheduleJob(task.Data);
                    break;
                case StatusEnum.No:
                    //取消任务
                    await new JobCenter().StopJob(task.Data);
                    break;
                default:
                    break;
            }
            task.Data.TaskStatus = entity.TaskStatus;
            TData obj = await quartzoptionsBLL.ChangeTaskStatusJson(task.Data);
            return Json(obj);
        }
        [HttpPost]
        [AuthorizeFilter("system:quartzoptions:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await quartzoptionsBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
