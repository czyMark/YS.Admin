using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表实体查询类
    /// </summary>
    public class PrintdataBaseSaveParam
    {
        /// <summary>
        /// 档案号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 1纸币 2邮票 3硬币 
        /// </summary>
        public int DataTag { get; set; }
        /// <summary>
        /// 要保存的数据
        /// </summary>
        public List<PrintdataBaseEntity> SaveData { get; set; }

        public string SaveDataString { get; set; }

        public bool State { get; set; }
        public string ParamMsg { get; set; }
    }
}
