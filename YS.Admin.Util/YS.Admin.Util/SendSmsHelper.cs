using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tea;

namespace YS.Admin.Util
{
	public class SendSmsHelper
	{
		//public static string Sendsms(string PhoneNumbers, string SignName, string TemplateCode, string TemplateParam)
		//{
		//	//IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAI5tMGeMFpPHRsB3sG9fUH", "uge6tRGZS60HCfkLrooBPGmZWqgETK");
		//	IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAIYyEuEFGkMNqn", "aMxT8GfjYCSpGP07fZNUjZbcpDoAvx");
		//	DefaultAcsClient client = new DefaultAcsClient(profile);
		//	CommonRequest request = new CommonRequest();
		//	request.Method = MethodType.POST;
		//	request.Domain = "dysmsapi.aliyuncs.com";
		//	request.Version = "2017-05-25";
		//	request.Action = "SendSms";
		//	// request.Protocol = ProtocolType.HTTP;
		//	request.AddQueryParameters("PhoneNumbers", "" + PhoneNumbers + "");
		//	request.AddQueryParameters("SignName", "" + SignName + "");
		//	request.AddQueryParameters("TemplateCode", "" + TemplateCode + "");
		//	//request.AddQueryParameters("TemplateParam", "{\"code\":\"" + code + "\"} ");
		//	request.AddQueryParameters("TemplateParam", TemplateParam);
		//	try
		//	{
		//		CommonResponse response = client.GetCommonResponse(request);
		//		//response.HttpResponse.ContentType( //Type("text/html;cahrset=UTF-8");
		//		response.HttpResponse.Encoding = "utf-8";
		//		string kk = System.Text.Encoding.Default.GetString(response.HttpResponse.Content);
		//		return kk;
		//	}
		//	catch
		//	(ServerException e)
		//	{
		//		return e.ToString();
		//		//Console.WriteLine(e);
		//	}
		//	catch (ClientException e)
		//	{
		//		return e.ToString();
		//		//Console.WriteLine(e);
		//	}
		//}



		public static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient()
		{
			// 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
			// 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378671.html。
			AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
			{
				// 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
				AccessKeyId = "LTAI5t6ND4esp6unRbkNTqdA",// Environment.GetEnvironmentVariable("LTAI5t6ND4esp6unRbkNTqdA"),
				// 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
				AccessKeySecret = "7D1dL6K4J0IrrTzK1qXavogUs4exuw",// Environment.GetEnvironmentVariable("7D1dL6K4J0IrrTzK1qXavogUs4exuw"),
			};
			config.RegionId = "cn-beijing";
			// Endpoint 请参考 https://api.aliyun.com/product/Dysmsapi
			config.Endpoint = "dysmsapi.aliyuncs.com";
			return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
		}

		public static void Sendsms(string PhoneNumbers, string SignName, string TemplateCode, string TemplateParam)
		{
			AlibabaCloud.SDK.Dysmsapi20170525.Client client = CreateClient();
			AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
			{
				PhoneNumbers = PhoneNumbers,
				SignName = SignName,
				TemplateCode = TemplateCode,
				TemplateParam = TemplateParam
			};
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				client.SendSmsWithOptions(sendSmsRequest, new AlibabaCloud.TeaUtil.Models.RuntimeOptions());
			}
			catch (TeaException error)
			{
				// 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
				// 错误 message
				//Console.WriteLine(error.Message);
				// 诊断地址
				//Console.WriteLine(error.Data["Recommend"]);
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
				// 错误 message
				//Console.WriteLine(error.Message);
				// 诊断地址
				//Console.WriteLine(error.Data["Recommend"]);
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
			}
		}

	}
}
