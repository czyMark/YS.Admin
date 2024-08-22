using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YS.Admin.Business.SystemManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Enum;
using YS.Admin.Util.Extension;

namespace YS.Admin.Web.Middleware
{
    /// <summary>
    /// 阻止特定访问
    /// </summary>
    public class AccessBlocklist
    {
        private readonly RequestDelegate _next;
        private readonly string _redirectUrl;
        private IpBlockBLL ipBlockBLL = new IpBlockBLL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="redirectUrl">指定的重定向URL</param>
        public AccessBlocklist(RequestDelegate next, string redirectUrl = "")
        {
            _next = next;
            _redirectUrl= redirectUrl;
        }
        public async Task Invoke(HttpContext context)
        {
            //if (context.Request.Method != "GET")
            {
                var remoteIp = context.Connection.RemoteIpAddress;    //获取远程访问IP
                var blockList = await ipBlockBLL.GetList(new Model.Param.SystemManage.IpBlockListParam());
                if (blockList.Tag == 0)
                {
                    //到错误界面
                    //context.Response.Redirect("/");
                    //await _next.Invoke(context);
                    return;
                }
                List<IpBlockEntity> AccessBlockIpList = blockList.Data;
                var bytes = remoteIp.GetAddressBytes();
                var badIp = false;
                foreach (var address in AccessBlockIpList)
                {
                    int c = address.Method.ParseToInt();
                    IpBlockEnum enumValueParsed = (IpBlockEnum)IpBlockEnum.Parse(typeof(IpBlockEnum), c.ToString());
                    if (enumValueParsed.GetDescription().ToLower() == "none" || context.Request.Method.ToLower() != enumValueParsed.GetDescription().ToLower())
                    {
                        var testIp = IPAddress.Parse(address.IpAddr);
                        if (testIp.GetAddressBytes().SequenceEqual(bytes))
                        {
                            badIp = true;
                            break;    //直接跳出ForEach循环
                        }
                    }
                }
                if (badIp)//黑白名单中
                {
                    if (string.IsNullOrEmpty(_redirectUrl))
                        context.Response.StatusCode = 401;
                    else
                        context.Response.Redirect(_redirectUrl);
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
