using System;
using System.Collections.Generic;
using System.Text;
using YS.Admin.Entity.SiteManage;

namespace YS.Admin.Model.Result
{
    public class AdvertInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///   [JsonConverter(typeof(StringJsonConverter))]
        public long? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? ViewNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? ViewWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? ViewHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Target { get; set; }


        public List<AdvertBannerEntity> advertBannerEntities { get; set; }
    }
}
