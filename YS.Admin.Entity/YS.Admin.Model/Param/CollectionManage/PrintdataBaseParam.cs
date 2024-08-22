using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YS.Admin.Util;

namespace YS.Admin.Model.Param.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表实体查询类
    /// </summary>
    public class PrintdataBaseListParam
    {
        /// <summary>
        /// 档案id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 查询的类型 纸币 邮票 硬币
        /// </summary>
        public int DataTag { get; set; }

        /// <summary>
        /// 查询的打印类型
        /// </summary>
        public string TagtypeName { get; set; }

        /// <summary>
        /// 查询的数据表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 返回的数据表名
        /// </summary>
        public string MainTagName { get; set; }

        /// <summary>
        /// 查询的客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 鉴定编号
        /// </summary>
        public string IDCode { get; set; }
        /// <summary>
        /// 藏品名称
        /// </summary>
        public string CollectionName { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string CollectionYear { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public string CollectionValue { get; set; }
    }

    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表实体查询类
    /// </summary>
    public class PrintdataTempListParam
    {
        /// <summary>
        /// 临时数据表编号
        /// </summary>
        public string Id { get; set; }
    }
}
