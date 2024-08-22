using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.OrganizationManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-15 15:06
    /// 描 述：共享文件实体类
    /// </summary>
    [Table("shared_file")]
    public class SharedFileEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 上传人
        /// </summary>
        /// <returns></returns>
        public string CreateName { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        /// <returns></returns>
        public string FileName { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        /// <returns></returns>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        public string FileType { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        /// <returns></returns>
        public string ThumbImage { get; set; }
        /// <summary>
        /// ids
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string ids { get; set; } = string.Empty;
    }
}
