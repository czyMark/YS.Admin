using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Model.Result.SystemManage
{
    public class UserRoleInfo
    {
        /// <summary>
        /// 用户角色ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? RoleId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? UserId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
