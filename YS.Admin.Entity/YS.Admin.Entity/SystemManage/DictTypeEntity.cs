using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;
using System.Threading.Tasks;

namespace YS.Admin.Entity.SystemManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2021-10-15 13:19
    /// 描 述：字典管理实体类
    /// </summary>
    [Table("sys_dict_type")]
    public class DictTypeEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        /// <returns></returns>
        public string DictName { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        /// <returns></returns>
        public string DictType { get; set; }
        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        /// <returns></returns>
        public string Status { get; set; }
      
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
         
    }
}
