using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Web.Code
{
	public class WeChatOperatorInfo
	{
	
		public string Openid { get; set; }
		public string NickName { get; set; }
		public string Sex { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Country { get; set; }

		public string HeadImgurl { get; set; }

		public string Privilege { get; set; }
		public string Unionid { get; set; }


	}


}
