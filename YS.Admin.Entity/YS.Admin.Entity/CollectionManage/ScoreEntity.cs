using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YS.Admin.Util;

namespace YS.Admin.Entity.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-24 11:29
    /// 描 述：评分信息实体类
    /// </summary>
    [Table("collection_score")]
    public class ScoreEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 分数
        /// </summary>
        /// <returns></returns>
        public string Score { get; set; }
        /// <summary>
        /// 英文释义
        /// </summary>
        /// <returns></returns>
        public string EnglishInterpretation { get; set; }
        /// <summary>
        /// 分数类别  0是纸币 1是邮票  
        /// </summary>
        /// <returns></returns>
        public int ScoreType { get; set; }
    }
}
