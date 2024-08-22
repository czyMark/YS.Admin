using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:06
    /// 描 述：日记实体类
    /// </summary>
    [Table("blog_diarys")]
    public class DiarysEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public string Icon { get; set; }
        /// <summary>
        /// 日记内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
