using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Enum
{
	public enum StatusEnum
	{
		[Description("启用")]
		Yes = 1,

		[Description("禁用")]
		No = 0
	}

    public enum ProcessingStatusEnum
    {
        [Description("已处理")]
        Yes = 1,

        [Description("未处理")]
        No = 0
    }

    public enum IsEnum
	{
		[Description("是")]
		Yes = 1,

		[Description("否")]
		No = 0
	}
	public enum YesOrNo
	{
		[Description("正确")]
		Yes = 1,

		[Description("错误")]
		No = 0
	}
	public enum NeedEnum
	{
		[Description("不需要")]
		NotNeed = 0,

		[Description("需要")]
		Need = 1
	}

	public enum OperateStatusEnum
	{
		[Description("失败")]
		Fail = 0,

		[Description("成功")]
		Success = 1
	}



    public enum IpBlockEnum
    {
        [Description("None")]
        None = 0,
        [Description("Get")]
        Get = 1,

        [Description("Post")]
        Post = 2,

    }

    public enum UploadFileType
	{
		[Description("头像")]
		Portrait = 1,

		[Description("新闻图片")]
		News = 2,
		[Description("PDF文件")]
		Pdf = 3,
		[Description("视频文件")]
		Mp4 = 5,
		[Description("APK文件")]
		Apk = 6,
		[Description("导入的文件")]
		Import = 10,
        [Description("共享文件")]
        Share = 12,
    }

	public enum PlatformEnum
	{
		[Description("Web后台")]
		Web = 1,

		[Description("WebApi")]
		WebApi = 2
	}

	public enum PayStatusEnum
	{
		[Description("未知")]
		Unknown = 0,

		[Description("已支付")]
		Success = 1,

		[Description("转入退款")]
		Refund = 2,

		[Description("未支付")]
		NotPay = 3,

		[Description("已关闭")]
		Closed = 4,

		[Description("已撤销（付款码支付）")]
		Revoked = 5,

		[Description("用户支付中（付款码支付）")]
		UserPaying = 6,

		[Description("支付失败(其他原因，如银行返回失败)")]
		PayError = 7
	}
	public enum ShowEmum
	{
		[Description("显示")]
		Yes = 1,

		[Description("隐藏")]
		No = 0
	}
	public enum TargetEnum
	{
		[Description("新窗口")]
		Yes = 1,

		[Description("原窗口")]
		No = 0
	}

	public enum UserRoleId : long
	{
		[Description("后台管理员")]
		UserAdmin = 16508640061130146,
		[Description("注册用户")]
		UserRegister = 713351748688809984,
	}


	public enum UserBenefitsEnum
	{
		[Description("折扣")]
		ZheKou = 1,

		[Description("协议")]
		YiKouJia = 2
	}
	/// <summary>
	/// 订单类型 1新增 2续费(弃用) 3天数补偿
	/// </summary>
	public enum OpenClassEnum
	{
		[Description("新增续费")]
		XinZeng = 1,
		//[Description("续费")]
		//XueFei = 2,
		[Description("天数补偿")]
		BuCang = 3
	}
	/// <summary>
	/// 订单类型 1新增 2续费(弃用) 3天数补偿
	/// </summary>
	public enum MemberOrderStatusEnum
	{
		[Description("待确认")]
		DaiQueRen = 0,


		[Description("已确认")]
		YiQueRen = 1
	}
	/// <summary>
	/// 订单类型 1新增 2续费 3延期
	/// </summary>
	public enum LimitedTimeEnum
	{
		[Description("限时")]
		XianShi = 1,

		[Description("不限时")]
		BuXianShi = 2
	}
	/// <summary>
	/// 项目类型 公开课 内训 其他
	/// </summary>
	public enum ClassTypeEnum
	{
		[Description("公开课")]
		GongKaiKe = 1,

		[Description("内训")]
		NeiXun = 2,

		[Description("其他")]
		QiTa = 3
	}
	/// <summary>
	/// 图书 购买渠道  1赛尼尔甄选，2小鹅通 3微信
	/// </summary>
	public enum ChannelTypeEnum
	{
		[Description("赛尼尔甄选")]
		Senior = 1,

		[Description("小鹅通")]
		Xiaoe = 2,

		[Description("微信")]
		WeiXin = 3
	}
	/// <summary>
	///  订单类别  1 会员  2课程
	/// </summary>
	public enum OrderTypeEnum
	{
		[Description("会员")]
		User = 1,

		[Description("课程")]
		Course = 2
	}
	/// <summary>
	///  合规师级别（1首席 2高级 3企业）
	/// </summary>
	public enum ComplianceLevelEnum
	{

		[Description("首席")]
		ShouXi = 1,

		[Description("高级")]
		GaoJi = 2,

		[Description("企业")]
		QiYe = 3
	}
	/// <summary>
	///  证书状态（0未制作/1制作中/2待颁发/3已颁发）
	/// </summary>
	public enum ComplianceStatusEnum
	{
		[Description("未制作")]
		WeiZhiZuo = 0,

		[Description("制作中")]
		ZhiZuoZhong = 1,

		[Description("待颁发")]
		DaiBanFa = 2,

		[Description("已颁发")]
		YiBanFa = 3
	}
	/// <summary>
	///  上课形式（1线上 2线下）
	/// </summary>
	public enum ProTyoeEnum
	{

		[Description("线上")]
		Up = 1,

		[Description("线下")]
		Down = 2,

	}
	/// <summary>
	/// 项目类型  1公开课-主题，2内训-项目名称，3合规师-单位，4网课-订单名称，5图书
	/// </summary>
	public enum ProjectTypeEnum
	{

		[Description("公开课")]
		OpenClass = 1,

		[Description("内训")]
		NeiXun = 2,
		[Description("合规师")]
		HeGui = 3,
		[Description("网课")]
		WangKe = 4,
		[Description("图书")]
		TuShu = 5,

	}
	/// <summary>
	///  发票（1纸质专票，2纸质普票，3电子普票，4无发票）
	/// </summary>
	public enum InvoiceTypeEnum
	{
		[Description("纸质专票")]
		ZhiZhiZhuan = 1,
		[Description("纸质普票")]
		ZhiZhiPu = 2,
		[Description("电子普票")]
		DianZiPu = 3,
		[Description("无发票")]
		DiKou = 4,

	}
	/// <summary>
	///  发票类型 1交通 2酒店 3餐饮 4资料 5办公用品 6会场 7课酬 8物流 9其他
	/// </summary>
	public enum ExpendTypeEnum
	{

		[Description("交通")]
		JiaoTong = 1,
		[Description("酒店")]
		JiuDian = 2,
		[Description("餐饮")]
		CanYin = 3,
		[Description("资料")]
		ZiLiao = 4,
		[Description("办公用品")]
		BanGong = 5,
		[Description("会场")]
		HuiChang = 6,
		[Description("课酬")]
		KeChou = 7,
		[Description("物流")]
		WuLiu = 8,
		[Description("其他")]
		QiTa = 9,

	}

	public enum IsAutoOrderEnum
	{
		[Description("创建")]
		Yes = 1,
		[Description("不创建")]
		No = 0
	}
	public enum OpenClassStatusEnum
	{
		[Description("招生中")]
		ZhaoShengIng = 1,

		[Description("未开始")]
		WeiKaishi = 2,

		[Description("已结束")]
		YIjieShu = 3
	}

    public enum TaskMethodEnum
    {
        [Description("Get请求")]
        Get = 0,

        [Description("Post请求")]
        Post = 1,

        [Description("本地程序执行")]
        LocalExe = 2,

        [Description("数据库存储过程")]
        Sql = 3,

        [Description("消息队列")]
        Other = 4
    }


    public enum PointsEnum
	{
		[Description("花费积分")]
		HuaFei = 1,
		[Description("奖励积分")]
		JiangLi = 0
	}
}
