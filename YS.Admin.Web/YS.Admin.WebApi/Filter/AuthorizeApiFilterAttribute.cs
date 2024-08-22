using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Enum;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Web.Code;

namespace YS.Admin.WebApi.Controllers
{
	/// <summary>
	/// 验证token和记录日志
	/// </summary>
	public class AuthorizeApiFilterAttribute : ActionFilterAttribute
	{

		/// <summary>
		/// 异步接口日志
		/// </summary>
		/// <param name="context"></param>
		/// <param name="next"></param>
		/// <returns></returns>
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			string actionName = context.RouteData.Values["Action"].ToString();
			OperatorInfo user = null;

			#region 判断用户Token状态

			TData obj = new TData();
			string token = context.HttpContext.Request.Headers["ApiToken"].ToString();
			if (string.IsNullOrEmpty(token))
			{
				obj.Tag = 2;
				obj.Message = "Token不能为空";
				context.Result = new JsonResult(obj);
				return;
			}
			if (!GlobalContext.SystemConfig.ApiToken.Equals(token))
			{
				obj.Tag = 2;
				obj.Message = "验证失败";
				context.Result = new JsonResult(obj);
				return;
			}
			#endregion
			var resultContext = await next();
			sw.Stop();

			#region 保存日志
			LogApiEntity logApiEntity = new LogApiEntity();
			logApiEntity.ExecuteUrl = context.HttpContext.Request.Path;
			logApiEntity.LogStatus = OperateStatusEnum.Success.ParseToInt();

			#region 获取Post参数
			switch (context.HttpContext.Request.Method.ToUpper())
			{
				case "GET":
					logApiEntity.ExecuteParam = context.HttpContext.Request.QueryString.Value.ToString();
					break;

				case "POST":
					if (context.ActionArguments?.Count > 0)
					{
						logApiEntity.ExecuteUrl += context.HttpContext.Request.QueryString.Value.ToString();
						logApiEntity.ExecuteParam = TextHelper.GetSubString(JsonConvert.SerializeObject(context.ActionArguments), 4000);
					}
					else
					{
						logApiEntity.ExecuteParam = context.HttpContext.Request.QueryString.Value.ToString();
					}
					break;
			}
			#endregion

			if (resultContext.Exception != null)
			{
				#region 异常获取
				StringBuilder sbException = new StringBuilder();
				Exception exception = resultContext.Exception;
				sbException.AppendLine(exception.Message);
				while (exception.InnerException != null)
				{
					sbException.AppendLine(exception.InnerException.Message);
					exception = exception.InnerException;
				}
				sbException.AppendLine(TextHelper.GetSubString(resultContext.Exception.StackTrace, 8000));
				#endregion

				logApiEntity.ExecuteResult = sbException.ToString();
				logApiEntity.LogStatus = OperateStatusEnum.Fail.ParseToInt();
			}
			else
			{
				ObjectResult result = context.Result as ObjectResult;
				if (result != null)
				{
					logApiEntity.ExecuteResult = JsonConvert.SerializeObject(result.Value);
					logApiEntity.LogStatus = OperateStatusEnum.Success.ParseToInt();
				}
			}
			if (user != null)
			{
				logApiEntity.BaseCreatorId = user.UserId;
			}
			logApiEntity.ExecuteParam = TextHelper.GetSubString(logApiEntity.ExecuteParam, 4000);
			logApiEntity.ExecuteResult = TextHelper.GetSubString(logApiEntity.ExecuteResult, 4000);
			logApiEntity.ExecuteTime = sw.ElapsedMilliseconds.ParseToInt();

			Action taskAction = async () =>
			{
				// 让底层不用获取HttpContext
				logApiEntity.BaseCreatorId = logApiEntity.BaseCreatorId ?? 0;

				await new LogApiBLL().SaveForm(logApiEntity);
			};
			AsyncTaskHelper.StartTask(taskAction);
			#endregion
		}
	}
}
