using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-20 23:03
    /// 描 述：实体类
    /// </summary>
    [Table("site_advert_banner")]
    public class AdvertBannerEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 广告位id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? Aid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        /// <returns></returns>
        public string SubTitle { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 上传文件路径
        /// </summary>
        /// <returns></returns>
        public string FilePath { get; set; }
        /// <summary>
        /// 上传文件路径
        /// </summary>
        /// <returns></returns>
        public string PcFilePath { get; set; } = string.Empty;
        /// <summary>
        /// 链接地址
        /// </summary>
        /// <returns></returns>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 内容描述
        /// </summary>
        /// <returns></returns>
        public string Contents { get; set; }
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

    }
}
