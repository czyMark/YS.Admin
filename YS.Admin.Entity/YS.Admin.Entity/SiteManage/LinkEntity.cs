using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-22 15:11
    /// 描 述：实体类
    /// </summary>
    [Table("site_link")]
    public class LinkEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 链接标题
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        public string UserTel { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        /// <returns></returns>
        public string SiteUrl { get; set; }
        /// <summary>
        /// 链接图片
        /// </summary>
        /// <returns></returns>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public int? SortId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? IsLock { get; set; }
        /// <summary>
        /// 链接类型
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? LinkType { get; set; } 
    }
}
