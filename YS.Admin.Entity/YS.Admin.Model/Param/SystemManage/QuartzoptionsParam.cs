using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-13 22:29
    /// 描 述：自动执行任务实体查询类
    /// </summary>
    public class QuartzoptionsListParam
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        /// <returns></returns>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        /// <returns></returns>
        public string GroupName { get; set; }
    }
}
