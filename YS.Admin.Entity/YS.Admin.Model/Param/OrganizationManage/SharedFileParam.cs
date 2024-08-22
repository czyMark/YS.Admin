using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.OrganizationManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 15:06
    /// 描 述：共享文件实体查询类
    /// </summary>
    public class SharedFileListParam
    {
        /// <summary>
        /// 上传人
        /// </summary>
        /// <returns></returns>
        public string CreateName { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        /// <returns></returns>
        public string FileName { get; set; }
    }
}
