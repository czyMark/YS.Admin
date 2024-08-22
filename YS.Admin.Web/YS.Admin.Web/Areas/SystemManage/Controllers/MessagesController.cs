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
using YS.Admin.Web.Hubs;

namespace YS.Admin.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 07:27
    /// 描 述：系统公告及消息控制器类
    /// </summary>
    [Area("SystemManage")]
    public class MessagesController : BaseController
    {
        private MessagesBLL messagesBLL = new MessagesBLL();

        #region 视图功能
        [AuthorizeFilter("system:messages:view")]
        public ActionResult MessagesIndex()
        {
            return View();
        }

        public ActionResult MessagesForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:messages:search")]
        public async Task<ActionResult> GetListJson(MessagesListParam param)
        {
            TData<List<MessagesEntity>> obj = await messagesBLL.GetList(param);
            return Json(obj);
        }


        [HttpGet]
        public async Task<ActionResult> GetMyMessagesJson(MessagesListParam param)
        {
            TData<List<MessagesEntity>> obj = await messagesBLL.GetMyMessagesJson(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:messages:search")]
        public async Task<ActionResult> GetPageListJson(MessagesListParam param, Pagination pagination)
        {
            TData<List<MessagesEntity>> obj = await messagesBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<MessagesEntity> obj = await messagesBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:messages:add,system:messages:edit")]
        public async Task<ActionResult> SaveFormJson(MessagesEntity entity)
        {
            bool send = false;
            //如果保存的是公告就发送出去
            if (entity.Id == 0 && entity.Type=="2")
            {
                send=true;
            }
            TData<string> obj = await messagesBLL.SaveForm(entity);

            if (send)
            {
                await new SignalRHup().SendAnnouncement("1", obj.Data);
            }

            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:messages:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await messagesBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
