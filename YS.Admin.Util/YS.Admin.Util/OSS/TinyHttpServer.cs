using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Newtonsoft.Json;
using System.Globalization;

namespace callbackserver
{
	public class PolicyConfig
	{
		public string expiration { get; set; }
		public List<List<Object>> conditions { get; set; }
	}

	public class PolicyToken
	{
		public string accessid { get; set; }
		public string policy { get; set; }
		public string signature { get; set; }
		public string dir { get; set; }
		public string host { get; set; }
		public string expire { get; set; }
		public string callback { get; set; }
	}

	public class CallbackParam
	{
		public string callbackUrl { get; set; }
		public string callbackBody { get; set; }
		public string callbackBodyType { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class TinyHttpServer
	{
		public static IPAddress ipaddress = IPAddress.Parse("127.0.0.1");
		public static int port = 81;
		private const int MAX_POST_SIZE = 1024 * 1024 * 1024;  // 10MB 
		private const int BUF_SIZE = 4096;

		private TcpClient tcpClient;
		private Stream streamRequest;
		private StreamWriter swResponse;
		private StreamReader srPostData;

		private String httpMethod;
		private String httpURL;
		private String httpProtocolVersionString;
		private Hashtable httpHeadersDict = new Hashtable();
		private String httpBody;

		private String strAuthorizationRequestBase64 = "";
		private byte[] byteAuthorizationRequest;

		private String strPublicKeyURLBase64 = "";
		private String strPublicKeyBase64 = "";
		private String strPublicKeyContentBase64 = "";
		private String strPublicKeyContentXML = "";
		private String strAuthSourceForMD5 = "";

		// 请填写您的AccessKeyId。
		public static string accessKeyId = "LTAI5t6ND4esp6unRbkNTqdA";
		// 请填写您的AccessKeySecret。
		public static string accessKeySecret = "7D1dL6K4J0IrrTzK1qXavogUs4exuw";
		// host的格式为 bucketname.endpoint ，请替换为您的真实信息。
		public static string host = "";
		// callbackUrl为 上传回调服务器的URL，请将下面的IP和Port配置为您自己的真实信息。
		public static string callbackUrl = "http://88.88.88.88:8888";
		// 用户上传文件时指定的前缀。
		public static string uploadDir = "user-dir-prefix/";
		public static int expireTime = 30;

		public TinyHttpServer(TcpClient clientRequest)
		{
			this.tcpClient = clientRequest;
		}

		public static void ListenAndServe()
		{
			Console.WriteLine("HttpServer is running : IP={0} , PORT={1}", TinyHttpServer.ipaddress, TinyHttpServer.port);
			TcpListener listenerTinyHttpServer = new TcpListener(TinyHttpServer.ipaddress, TinyHttpServer.port);
			try
			{
				listenerTinyHttpServer.Start();
			}
			catch (System.Net.Sockets.SocketException e)
			{
				Console.WriteLine("HttpServer Listener Exception : {0}. ", e.ToString());
				return;
			}

			while (true)
			{
				TcpClient tcpclientFromSingleRequest = listenerTinyHttpServer.AcceptTcpClient();

				TinyHttpServer singleProcessor = new TinyHttpServer(tcpclientFromSingleRequest);
				Thread threadServer = new Thread(new ThreadStart(singleProcessor.ProcessSingleRequest));
				threadServer.Start();
				Thread.Sleep(1);
			}
		}

		public void ProcessSingleRequest()
		{
			this.streamRequest = new BufferedStream(this.tcpClient.GetStream());
			this.swResponse = new StreamWriter(new BufferedStream(this.tcpClient.GetStream()));

			try
			{
				this.ParseRequest();
				this.ParseHeaders();

				if (this.httpMethod.Equals("GET"))
				{
					this.DoGet();
				}
				else if (this.httpMethod.Equals("POST"))
				{
					this.DoPost();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.ToString());
				this.HttpResponseFailure();
			}

			this.swResponse.Flush();
			this.streamRequest = null;
			this.swResponse = null;
			this.tcpClient.Close();
		}

		private void ParseRequest()
		{
			String strRequest = StreamReadLine(this.streamRequest);
			string[] tokens = strRequest.Split(' ');
			if (tokens.Length != 3)
			{
				throw new Exception("invalid http request line");
			}
			this.httpMethod = tokens[0].ToUpper();
			this.httpURL = tokens[1];
			this.httpProtocolVersionString = tokens[2];

			Console.WriteLine("starting: " + strRequest);
		}

		private string StreamReadLine(Stream inputStream)
		{
			int next_char;
			string data = "";
			while (true)
			{
				next_char = inputStream.ReadByte();
				if (next_char == '\n') { break; }
				if (next_char == '\r') { continue; }
				if (next_char == -1) { Thread.Sleep(1); continue; };
				data += Convert.ToChar(next_char);
			}
			return data;
		}

		private void ParseHeaders()
		{
			String line;
			while ((line = StreamReadLine(this.streamRequest)) != null)
			{
				if (line.Equals(""))
				{
					Console.WriteLine("got headers ... ");
					break;
				}

				int separator = line.IndexOf(':');
				if (separator == -1)
				{
					throw new Exception("invalid http header line: " + line);
				}
				String name = line.Substring(0, separator);
				int pos = separator + 1;
				while ((pos < line.Length) && (line[pos] == ' '))
				{
					pos++;  //Strip spaces
				}

				string value = line.Substring(pos, line.Length - pos);
				Console.WriteLine("httpHeadersDict[{0}]={1}", name, value);
				httpHeadersDict[name] = value;
			}
		}

		private void GetPostBody()
		{
			int contentLength = 0;
			MemoryStream ms = new MemoryStream();
			if (this.httpHeadersDict.ContainsKey("Content-Length"))
			{
				contentLength = Convert.ToInt32(this.httpHeadersDict["Content-Length"]);
				if (contentLength > MAX_POST_SIZE)
				{
					throw new Exception(String.Format("POST Content-Length({0}) too big for this simple server", contentLength));
				}
				byte[] buf = new byte[BUF_SIZE];
				int toReadLen = contentLength;
				while (toReadLen > 0)
				{
					//Console.WriteLine("starting Read, toReadLen={0}", toReadLen);
					int numRead = this.streamRequest.Read(buf, 0, Math.Min(BUF_SIZE, toReadLen));
					//Console.WriteLine("read finished, numRead={0}", numRead);
					if (numRead == 0)
					{
						if (toReadLen == 0)
						{
							break;
						}
						else
						{
							throw new Exception("client disconnected during post");
						}
					}
					else
					{
						string strBuf = System.Text.Encoding.Default.GetString(buf);
						//Console.WriteLine("strBuf=[{0}]", strBuf);
					}
					toReadLen -= numRead;
					ms.Write(buf, 0, numRead);
				}
				ms.Seek(0, SeekOrigin.Begin);
			} // Get post data end

			this.srPostData = new StreamReader(ms);
			this.httpBody = this.srPostData.ReadToEnd();
			Console.WriteLine("POST_BODY=[{0}]", this.httpBody);
			this.srPostData.Close();
		}

		public void DoPost()
		{
			this.GetPostBody();

			// Verify Signature
			try
			{
				if (this.VerifySignature())
				{
					Console.WriteLine("\nVerifySignature Successful . \n");

					// do something accoding to callback_body ... 

					this.HttpResponseSuccess();
				}
				else
				{
					Console.WriteLine("\nVerifySignature Failed . \n");
					this.HttpResponseFailure();
				}
			}
			catch
			{
				Console.WriteLine("\nVerifySignature Failed . \n");
				this.HttpResponseFailure();
			}
		}

		public bool VerifySignature()
		{
			// Get the Authorization Base64 from Request
			if (this.httpHeadersDict["authorization"] != null)
			{
				this.strAuthorizationRequestBase64 = this.httpHeadersDict["authorization"].ToString();
			}
			else if (this.httpHeadersDict["Authorization"] != null)
			{
				this.strAuthorizationRequestBase64 = this.httpHeadersDict["Authorization"].ToString();
			}
			if (this.strAuthorizationRequestBase64 == "")
			{
				Console.WriteLine("authorization property in the http request header is null. ");
				return false;
			}

			// Decode the Authorization from Request
			this.byteAuthorizationRequest = Convert.FromBase64String(this.strAuthorizationRequestBase64);

			// Decode the URL of PublicKey
			this.strPublicKeyURLBase64 = this.httpHeadersDict["x-oss-pub-key-url"].ToString();
			var bytePublicKeyURL = Convert.FromBase64String(this.strPublicKeyURLBase64);
			var strAsciiPublickeyURL = System.Text.Encoding.ASCII.GetString(bytePublicKeyURL);

			// Get PublicKey from the URL
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(validateServerCertificate);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strAsciiPublickeyURL);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader srPublicKey = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			this.strPublicKeyBase64 = srPublicKey.ReadToEnd();
			response.Close();
			srPublicKey.Close();
			this.strPublicKeyContentBase64 = this.strPublicKeyBase64.Replace("-----BEGIN PUBLIC KEY-----\n", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "");
			this.strPublicKeyContentXML = this.RSAPublicKeyString2XML(this.strPublicKeyContentBase64);

			// Generate the New Authorization String according to the HttpRequest
			String[] arrURL;
			if (this.httpURL.Contains('?'))
			{
				arrURL = this.httpURL.Split('?');
				this.strAuthSourceForMD5 = String.Format("{0}?{1}\n{2}", System.Web.HttpUtility.UrlDecode(arrURL[0]), arrURL[1], this.httpBody);
			}
			else
			{
				this.strAuthSourceForMD5 = String.Format("{0}\n{1}", System.Web.HttpUtility.UrlDecode(this.httpURL), this.httpBody);
			}

			// MD5 hash bytes from the New Authorization String 
			var byteAuthMD5 = byteMD5Encrypt32(this.strAuthSourceForMD5);

			// Verify Signature
			System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
			try
			{
				RSA.FromXmlString(this.strPublicKeyContentXML);
			}
			catch (System.ArgumentNullException e)
			{
				throw new ArgumentNullException(String.Format("VerifySignature Failed : RSADeformatter.VerifySignature get null argument : {0} .", e));
			}
			catch (System.Security.Cryptography.CryptographicException e)
			{
				throw new System.Security.Cryptography.CryptographicException(String.Format("VerifySignature Failed : RSA.FromXmlString Exception : {0} .", e));
			}
			System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
			RSADeformatter.SetHashAlgorithm("MD5");

			var bVerifyResult = false;
			try
			{
				bVerifyResult = RSADeformatter.VerifySignature(byteAuthMD5, this.byteAuthorizationRequest);
			}
			catch (System.ArgumentNullException e)
			{
				throw new ArgumentNullException(String.Format("VerifySignature Failed : RSADeformatter.VerifySignature get null argument : {0} .", e));
			}
			catch (System.Security.Cryptography.CryptographicUnexpectedOperationException e)
			{
				throw new System.Security.Cryptography.CryptographicUnexpectedOperationException(String.Format("VerifySignature Failed : RSADeformatter.VerifySignature Exception : {0} .", e));
			}

			return bVerifyResult;
		}

		public void HttpResponseSuccess()
		{
			this.swResponse.WriteLine("HTTP/1.0 200 OK");
			this.swResponse.WriteLine("Content-Type: application/json"); //Not "Content-Type: text/html");
			this.swResponse.WriteLine("Content-Length: 15");
			this.swResponse.WriteLine("Connection: close");
			this.swResponse.WriteLine("");
			string strResponseBody = "{\"Status\":\"OK\"}";
			this.swResponse.WriteLine(strResponseBody);
		}

		public void HttpResponseFailure()
		{
			this.swResponse.WriteLine("HTTP/1.0 404 File not found");
			this.swResponse.WriteLine("Connection: close");
			this.swResponse.WriteLine("");
		}

		public void DoGet()
		{
			Console.WriteLine("DoGet request: {0}", this.httpURL);
			var content = GetPolicyToken();
			this.swResponse.WriteLine("HTTP/1.0 200 OK");
			this.swResponse.WriteLine("Content-Type: application/json");
			this.swResponse.WriteLine("Access-Control-Allow-Origin: *");
			this.swResponse.WriteLine("Access-Control-Allow-Method: GET, POST");
			this.swResponse.WriteLine($"Content-Length: {content.Length.ToString()}");
			this.swResponse.WriteLine("Connection: close");
			this.swResponse.WriteLine("");
			this.swResponse.WriteLine(content);
		}

		public static byte[] byteMD5Encrypt32(string password)
		{
			string cl = password;
			MD5 md5 = MD5.Create();
			byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
			return s;
		}

		public static bool validateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public string RSAPublicKeyString2XML(string publicKey)
		{
			RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
			return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
				Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
				Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
		}

		public void PrintByteArray(byte[] byteArr, String strArraryName = "", bool isUpper = false)
		{
			Console.WriteLine("PrintByteArray : {0} , Length={1}", strArraryName, byteArr.Length);
			foreach (byte b in byteArr)
			{
				if (isUpper)
				{
					Console.Write(b.ToString("X2"));
				}
				else
				{
					Console.Write(b.ToString("x2"));
				}
			}
			Console.WriteLine("\nPrintByteArray : {0} , End", strArraryName);
		}

		private static string GetPolicyToken()
		{
			//expireTime
			var expireDateTime = DateTime.Now.AddSeconds(expireTime);

			// example of policy
			//{
			//  "expiration": "2020-05-01T12:00:00.000Z",
			//  "conditions": [
			//    ["content-length-range", 0, 1048576000]
			//    ["starts-with", "$key", "user-dir-prefix/"]
			//  ]
			//}

			//policy
			var config = new PolicyConfig();
			config.expiration = FormatIso8601Date(expireDateTime);
			config.conditions = new List<List<Object>>();
			config.conditions.Add(new List<Object>());
			config.conditions[0].Add("content-length-range");
			config.conditions[0].Add(0);
			config.conditions[0].Add(1048576000);
			config.conditions.Add(new List<Object>());
			config.conditions[1].Add("starts-with");
			config.conditions[1].Add("$key");
			config.conditions[1].Add(uploadDir);

			var policy = JsonConvert.SerializeObject(config);
			var policy_base64 = EncodeBase64("utf-8", policy);
			var signature = ComputeSignature(accessKeySecret, policy_base64);

			//callback
			var callback = new CallbackParam();
			callback.callbackUrl = callbackUrl;
			callback.callbackBody = "filename=${object}&size=${size}&mimeType=${mimeType}&height=${imageInfo.height}&width=${imageInfo.width}";
			callback.callbackBodyType = "application/x-www-form-urlencoded";

			var callback_string = JsonConvert.SerializeObject(callback);
			var callback_string_base64 = EncodeBase64("utf-8", callback_string);

			var policyToken = new PolicyToken();

			policyToken.accessid = accessKeyId;
			policyToken.host = host;
			policyToken.policy = policy_base64;
			policyToken.signature = signature;
			policyToken.expire = ToUnixTime(expireDateTime);
			policyToken.callback = callback_string_base64;
			policyToken.dir = uploadDir;

			return JsonConvert.SerializeObject(policyToken);
		}

		public static string FormatIso8601Date(DateTime dtime)
		{
			return dtime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
							   CultureInfo.CurrentCulture);
		}

		public static string EncodeBase64(string code_type, string code)
		{
			string encode = "";
			byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
			try
			{
				encode = Convert.ToBase64String(bytes);
			}
			catch
			{
				encode = code;
			}
			return encode;
		}

		public static string ComputeSignature(string key, string data)
		{
			using (var algorithm = new HMACSHA1())
			{
				algorithm.Key = Encoding.UTF8.GetBytes(key.ToCharArray());
				return Convert.ToBase64String(
					algorithm.ComputeHash(Encoding.UTF8.GetBytes(data.ToCharArray())));
			}
		}

		private static string ToUnixTime(DateTime dtime)
		{
			const long ticksOf1970 = 621355968000000000;
			var expires = ((dtime.ToUniversalTime().Ticks - ticksOf1970) / 10000000L)
				.ToString(CultureInfo.InvariantCulture);

			return expires;
		}

		public static void PrintHelp()
		{
			string strHelp = "\n";
			strHelp += "Usage     :  callbackserver.exe   ipaddress    port  \n";
			strHelp += "Examples  :  callbackserver.exe   127.0.0.1    80    \n";
			strHelp += "             callbackserver.exe   11.22.33.44  8080    \n";
			Console.WriteLine(strHelp);
			Environment.Exit(0);
		}
	}
}
