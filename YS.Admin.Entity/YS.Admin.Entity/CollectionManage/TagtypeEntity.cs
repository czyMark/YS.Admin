using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:45
    /// 描 述：标签类型实体类
    /// </summary>
    [Table("collection_tagtype")]
    public class TagtypeEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 标签类型名称
        /// </summary>
        /// <returns></returns>
        public string TagTypeName { get; set; }


        /// <summary>
        /// 标签对应的打印地址
        /// </summary>
        /// <returns></returns>
        public string TagTypeAddr { get; set; }


        /// <summary>
        /// 标签类型备注
        /// </summary>
        /// <returns></returns>
        public string TagTypeRemark { get; set; }
    }
}
