using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 07:27
    /// 描 述：系统公告及消息实体查询类
    /// </summary>
    public class MessagesListParam
    {
        /// <summary>
        /// 标签
        /// </summary>
        /// <returns></returns>
        public string MessagesTag { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
    }
}
