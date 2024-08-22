using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-20 22:57
    /// 描 述：实体类
    /// </summary>
    [Table("site_advert")]
    public class AdvertEntity : BaseExtensionEntity
    {
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


    }
}
