using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Web.Hubs
{
    public class SignalRHup : Hub
    {  


        public async Task SendMessage(string user, string message)
        {
           await GlobalContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
       

        public async Task SendAnnouncement(string user, string announcementId)
        {
           await GlobalContext.Clients.All.SendAsync("ReceiveAnnouncement", user, announcementId);
        }

        //public async Task SendMessageToUser(string userId, string message)
        //{
        //    await GlobalContext.Clients.Group(userId).SendAsync("ReceiveMessage", message);
        //}

        /// <summary>
        /// 加入连接的事件 连接成功
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            GlobalContext.Clients = Clients;
            await base.OnConnectedAsync();
            await Clients.All.SendAsync("Connected", "连接成功[来至服务器的信息]");
        }
    }
}
