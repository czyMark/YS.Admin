using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Admin.Model.Param.WebApi.Member
{
    public class UserParam
	{
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string CaptchaCode { get; set; }
        /// <summary>
        /// 验证码对应UUID
        /// </summary>
        public string Uuid { get; set; }

       /// <summary>
       /// 登录手机号
       /// </summary>
        public string Mobile { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string RealName { get; set; }
		/// <summary>
		/// 公司名称
		/// </summary>
		public string Company { get; set; }
		/// <summary>
		/// 手机验证码
		/// </summary>
		public string SmsCode { get; set; }
	}
}
