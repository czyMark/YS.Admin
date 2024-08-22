using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-09 13:08
    /// 描 述：证书管理实体类
    /// </summary>
    [Table("site_certificate")]
    public class CertificateEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 文本话术
        /// </summary>
        /// <returns></returns>
        public string Details { get; set; }
        public string IDCode { get; set; }
        /// <summary>
        /// 鉴定机构
        /// </summary>
        /// <returns></returns>
        public string AppraisalInstitution { get; set; }
        /// <summary>
        /// 备案名称
        /// </summary>
        /// <returns></returns>
        public string RecordName { get; set; }
        /// <summary>
        /// 鉴定结果
        /// </summary>
        /// <returns></returns>
        public string IdentificationResult { get; set; }
        /// <summary>
        /// 鉴定老师1
        /// </summary>
        /// <returns></returns>
        public string Appraisal1 { get; set; }
        /// <summary>
        /// 鉴定老师2
        /// </summary>
        /// <returns></returns>
        public string Appraisal2 { get; set; }
        /// <summary>
        /// 公司印章
        /// </summary>
        /// <returns></returns>
        public string CompanySeal { get; set; }
        /// <summary>
        /// 基础证书
        /// </summary>
        /// <returns></returns>
        public string BaseCertificate { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
