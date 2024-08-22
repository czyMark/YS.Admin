using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-19 15:30
    /// 描 述：博客网站留言实体类
    /// </summary>
    [Table("blog_sitefeedback")]
    public class SitefeedbackEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserTel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserQq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string UserEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? IsLock { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
