using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Util;

namespace YS.Admin.Web.Code
{
	public class UserInfo
	{
		public long? UserId { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string Mobile { get; set; }
		/// <summary>
		/// 登录类型  1 系统管理员  2 报名用户
		/// </summary>
		public int? RoleType { get; set; }
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 职位
		/// </summary>
		public string UserPost { get; set; }
		/// <summary>
		/// 登录凭据
		/// </summary>
		public string UToken { get; set; }

		/// <summary>
		/// 公司ID/
		/// </summary>
		[JsonConverter(typeof(StringJsonConverter))]
		public long? Mid { get; set; } = 0;

		/// <summary>
		/// 公司名称
		/// </summary>
		public string CompanyName { get; set; }
	}
}
