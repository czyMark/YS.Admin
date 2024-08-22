using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:47
    /// 描 述：样式属性实体类
    /// </summary>
    [Table("collection_printstyle_prop")]
    public class PrintstylePropEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 样式模版ID
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? StyleID { get; set; }
        /// <summary>
        /// 元素颜色
        /// </summary>
        /// <returns></returns>
        public string StylePropColor { get; set; }
        /// <summary>
        /// 元素列
        /// </summary>
        /// <returns></returns>
        public string StylePropElement { get; set; }
        /// <summary>
        /// 文字字体
        /// </summary>
        /// <returns></returns>
        public string StylePropFontFamily { get; set; }
        /// <summary>
        /// 文字大小
        /// </summary>
        /// <returns></returns>
        public string StylePropFontSize { get; set; }
    }
}
