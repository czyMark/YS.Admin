using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Enum
{
	public enum CourseStatusEnum
	{
		[Description("进行中")]
		JinXingIng = 0,

		[Description("已授课")]
		YiShouKe = 1,

		[Description("已取消")]
		YiQuXiao = 2
	}

	public enum ContractStatusEnum
	{
		[Description("未签")]
		WeiQian = 0,
		[Description("已签")]
		YIQian = 1,
	}

	public enum PaymentStatusEnum
	{
		[Description("未付款")]
		WeiFu = 0,
		[Description("已付款")]
		YIFu = 1,
	}


}
