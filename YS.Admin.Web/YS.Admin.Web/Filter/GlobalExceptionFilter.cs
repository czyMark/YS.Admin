﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YS.Admin.Util.Model;
using YS.Admin.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using YS.Admin.Util;

namespace YS.Admin.Web.Controllers
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            LogHelper.Error(context.Exception);
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                TData obj = new TData();
                obj.Message = context.Exception.GetOriginalException().Message;
                if (string.IsNullOrEmpty(obj.Message))
                {
                    obj.Message = "抱歉，系统错误，请联系管理员！";
                }
                context.Result = new JsonResult(obj);
                context.ExceptionHandled = true;
            }
            else
            {
                string errorMessage = context.Exception.GetOriginalException().Message;
                context.Result = new RedirectResult("~/Web/Error?message=" + HttpUtility.UrlEncode(errorMessage));
                context.ExceptionHandled = true;
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}