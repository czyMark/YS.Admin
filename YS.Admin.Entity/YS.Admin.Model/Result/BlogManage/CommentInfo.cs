using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Newtonsoft.Json;
using YS.Admin.Util;
using YS.Admin.Entity;

namespace YS.Admin.Model.Param.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:05
    /// 描 述：博客文章评论实体返回类
    /// </summary>
    public class CommentListInfo : BaseExtensionEntity
    {

        /// <summary>
        /// 目标人员ID
        /// </summary>
        /// <returns></returns>
        public long? AcceptId { get; set; }
        public string AcceptNickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <returns></returns>
        public string HeadShot { get; set; }
        /// <summary>
        /// 文章ID
        /// </summary>
        /// <returns></returns>
        public long? ArticleId { get; set; }

        public string ArticleTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        /// <returns></returns>
        public long? ParentId { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        /// <returns></returns>
        public long? SendId { get; set; }

        public string SendNickName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public bool? Status { get; set; }
    }
}
