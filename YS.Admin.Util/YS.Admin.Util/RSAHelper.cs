using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YS.Admin.Util
{

	public static class RSAHelper
	{
		private static Encoding Encoding_UTF8 = Encoding.UTF8;
		/// <summary>
		/// 生成密钥
		/// <param name="PrivateKey">私钥</param>
		/// <param name="PublicKey">公钥</param>
		/// <param name="KeySize">密钥长度：512,1024,2048，4096，8192</param>
		/// </summary>
		public static void Generator(out string PrivateKey, out string PublicKey, int KeySize = 2048)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KeySize);
			//byte[] hash;
			//using (SHA256 sha256 = SHA256.Create())
			//{
			//    byte[] data = new byte[] { 59, 4, 248, 102, 77, 97, 142, 201, 210, 12, 224, 93, 25, 41, 100, 197, 213, 134, 130, 135 };
			//    hash = sha256.ComputeHash(data);
			//}

			////Create an RSASignatureFormatter object and pass it the 
			////RSACryptoServiceProvider to transfer the key information.
			//RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(rsa);

			////Set the hash algorithm to SHA256.
			//RSAFormatter.SetHashAlgorithm("SHA256");

			////Create a signature for HashValue and return it.
			//byte[] SignedHash = RSAFormatter.CreateSignature(hash);
			RSAPKCS1SignatureFormatter sign = new RSAPKCS1SignatureFormatter(rsa);
			sign.SetHashAlgorithm("SHA256");

			string strPrivateXML = rsa.ToXmlString(true);
			string strPublicXML = rsa.ToXmlString(false);

			//string strPrivate = RSAPrivateKeyDotNet2Java(strPrivateXML);
			//string strPublic = RSAPublicKeyDotNet2Java(strPublicXML);


			PrivateKey = RSAPrivateKeyDotNet2Java(strPrivateXML);
			PublicKey = RSAPublicKeyDotNet2Java(strPublicXML);
			//PrivateKey = rsa.ToXmlString(true); //将RSA算法的私钥导出到字符串PrivateKey中 参数为true表示导出私钥 true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。
			//PublicKey = rsa.ToXmlString(false); //将RSA算法的公钥导出到字符串PublicKey中 参数为false表示不导出私钥 true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。
		}
		/// <summary>
		/// RSA加密 将公钥导入到RSA对象中，准备加密
		/// </summary>
		/// <param name="PublicKey">公钥</param>
		/// <param name="encryptstring">待加密的字符串</param>
		public static string RSAEncrypt(string PublicKey, string encryptstring)
		{
			byte[] PlainTextBArray;
			byte[] CypherTextBArray;
			string Result;
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(PublicKey);
			PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptstring);
			CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
			Result = Convert.ToBase64String(CypherTextBArray);
			return Result;
		}
		/// <summary>
		/// RSA解密 将私钥导入RSA中，准备解密
		/// </summary>
		/// <param name="PrivateKey">私钥</param>
		/// <param name="decryptstring">待解密的字符串</param>
		public static string RSADecrypt(string PrivateKey, string decryptstring)
		{
			byte[] PlainTextBArray;
			byte[] DypherTextBArray;
			string Result;
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(PrivateKey);
			PlainTextBArray = Convert.FromBase64String(decryptstring);
			DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
			Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
			return Result;
		}


		///// <summary>
		///// RSA私钥格式转换，.net->java
		///// </summary>
		///// <param name="privateKey">.net生成的私钥</param>
		///// <returns></returns>
		//public static byte[] RSAPrivateKeyDotNet2Java(string privateKey)
		//{
		//    XmlDocument doc = new XmlDocument();
		//    doc.LoadXml(privateKey);
		//    BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
		//    BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
		//    BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
		//    BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
		//    BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
		//    BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
		//    BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
		//    BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));

		//    RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);

		//    PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
		//    byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
		//    return serializedPrivateBytes;
		//    //return Convert.ToBase64String(serializedPrivateBytes);
		//}
		//public static string RSAPrivateKeyJava2DotNet(string privateKey)
		//{
		//    RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

		//    return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
		//        Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
		//        Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
		//}


		/// <summary>  
		/// RSA私钥格式转换，.net->java  
		/// </summary>  
		/// <param name="privateKey">.net生成的私钥</param>  
		/// <returns></returns>  
		public static string RSAPrivateKeyDotNet2Java(string privateKey)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(privateKey);
			BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
			BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
			BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
			BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
			BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
			BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
			BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
			BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));

			RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);

			PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
			byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
			return Convert.ToBase64String(serializedPrivateBytes);
		}
		/// <summary>  
		/// RSA公钥格式转换，.net->java  
		/// </summary>  
		/// <param name="publicKey">.net生成的公钥</param>  
		/// <returns></returns>  
		public static string RSAPublicKeyDotNet2Java(string publicKey)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(publicKey);
			BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
			BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
			RsaKeyParameters pub = new RsaKeyParameters(false, m, p);

			SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
			byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
			return Convert.ToBase64String(serializedPublicBytes);
		}


		/// <summary>
		/// 私钥加密
		/// </summary>
		/// <param name="data">加密内容</param>
		/// <param name="privateKey">私钥（Base64后的）</param>
		/// <returns>返回Base64内容</returns>
		public static string EncryptByPrivateKey(string data, string privateKey)
		{
			//非对称加密算法，加解密用  
			IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());


			//加密  

			try
			{
				engine.Init(true, GetPrivateKeyParameter(privateKey));
				byte[] byteData = Encoding_UTF8.GetBytes(data);
				var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
				return Convert.ToBase64String(ResultData);
				//Console.WriteLine("密文（base64编码）:" + Convert.ToBase64String(testData) + Environment.NewLine);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 私钥解密
		/// </summary>
		/// <param name="data">待解密的内容</param>
		/// <param name="privateKey">私钥（Base64编码后的）</param>
		/// <returns>返回明文</returns>
		public static string DecryptByPrivateKey(string data, string privateKey)
		{
			data = data.Replace("\r", "").Replace("\n", "").Replace(" ", "");
			//非对称加密算法，加解密用  
			IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

			//解密  
			try
			{
				engine.Init(false, GetPrivateKeyParameter(privateKey));
				byte[] byteData = Convert.FromBase64String(data);
				var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
				return Encoding_UTF8.GetString(ResultData);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 公钥加密
		/// </summary>
		/// <param name="data">加密内容</param>
		/// <param name="publicKey">公钥（Base64编码后的）</param>
		/// <returns>返回Base64内容</returns>
		public static string EncryptByPublicKey(string data, string publicKey)
		{
			//非对称加密算法，加解密用  
			IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

			//加密  
			try
			{
				engine.Init(true, GetPublicKeyParameter(publicKey));
				byte[] byteData = Encoding_UTF8.GetBytes(data);
				var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
				return Convert.ToBase64String(ResultData);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 公钥解密
		/// </summary>
		/// <param name="data">待解密的内容</param>
		/// <param name="publicKey">公钥（Base64编码后的）</param>
		/// <returns>返回明文</returns>
		public static string DecryptByPublicKey(string data, string publicKey)
		{
			data = data.Replace("\r", "").Replace("\n", "").Replace(" ", "");
			//非对称加密算法，加解密用  
			IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
			//解密  
			try
			{
				engine.Init(false, GetPublicKeyParameter(publicKey));
				byte[] byteData = Convert.FromBase64String(data);
				var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
				return Encoding_UTF8.GetString(ResultData);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private static AsymmetricKeyParameter GetPublicKeyParameter(string keyBase64)
		{
			keyBase64 = keyBase64.Replace("\r", "").Replace("\n", "").Replace(" ", "");
			byte[] publicInfoByte = Convert.FromBase64String(keyBase64);
			Asn1Object pubKeyObj = Asn1Object.FromByteArray(publicInfoByte);//这里也可以从流中读取，从本地导入   
			AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(publicInfoByte);
			return pubKey;
		}

		private static AsymmetricKeyParameter GetPrivateKeyParameter(string keyBase64)
		{
			keyBase64 = keyBase64.Replace("\r", "").Replace("\n", "").Replace(" ", "");
			byte[] privateInfoByte = Convert.FromBase64String(keyBase64);
			// Asn1Object priKeyObj = Asn1Object.FromByteArray(privateInfoByte);//这里也可以从流中读取，从本地导入   
			// PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
			AsymmetricKeyParameter priKey = PrivateKeyFactory.CreateKey(privateInfoByte);
			return priKey;
		}
	}
}
