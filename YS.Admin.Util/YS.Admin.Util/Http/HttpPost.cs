﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace YS.Admin.Util.Http
{
    public static class HttpPost
    {
        //Accept
        public static string Accept = "text/html, application/xhtml+xml, */*";
        //发送内容类型
        public static string ContentType = "application/json";
        //传递的字符集
        public static Encoding Encoding = Encoding.UTF8;
        //证书路径
        public static string CertFileName;
        private static bool Certificate;

        public static void SetPostCertFileName(this string filename)
        {
            CertFileName = filename;
        }
        /// <summary>
        /// 是否启动证书验证
        /// 证书需要对操作系统进行配置才能验证通过
        ///  Windows xp/2003
        ///  1. 单击 开始 ，单击 运行 ，键入 mmc ，然后单击 确定 。
        ///  2. 在 文件 菜单上单击 添加/删除管理单元 。
        ///  3. 在 添加/删除管理单元 对话框中，单击 添加 。
        ///  4. 在 添加独立管理单元 对话框单击 证书 ，然后单击 添加 。
        ///  5. 在在 证书管理单元中 对话框中单击 计算机帐户 ，然后单击 下一步
        ///  6. 在 选择计算机 对话框中，单击 完成 。
        ///  7. 在 添加独立管理单元 对话框单击 关闭 ，然后单击 确定 。
        ///  8. 展开 证书 （本地计算机） ，展开 个人 ，然后单击 证书 。
        ///  9. 右键 -》 所有任务-》导入 选择你的证书导入
        ///  Windows 7及以上
        ///  1. 单击 开始 ，单击 运行 ，键入 mmc ，然后单击 确定 。
        ///  2. 在 文件 菜单上单击 添加/删除管理单元 。
        ///  3. 在 可用的管理单元 列表中选择 证书 ，点击 添加 。
        ///  4. 在 证书管理 对话框中选择 计算机账户 ，然后单击 下一步
        ///  5. 在 选择计算机 对话框中，单击 完成 。
        ///  6. 在 添加或删除管理单元 对话框单击 确定 。
        ///  7. 展开 证书 （本地计算机） ，展开 个人 ，然后单击 证书 。
        ///  8. 右键 -》 所有任务-》导入 选择你的证书导入
        /// </summary>
        /// <param name="status">true:后续的请求启动证书验证，false后续的请求关闭证书验证</param>
        public static void SetPostCertificate(this bool status)
        {
            if (status == true && string.IsNullOrEmpty(CertFileName))
            {
                throw new System.SystemException("需要设置证书路径【CertFileName】后才能启用证书");
            }
            Certificate = status;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
        }
        // 证书验证

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (Certificate == true)//认证证书
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                    return true;
                return false;
            }
            else
            {
                //取消证书验证
                return true;//证书一直都通过
            }

        }

        /// <summary>
        /// 字符串以Post方式请求网页
        /// </summary>
        /// <param name="url">post请求地址</param>
        /// <param name="body">
        /// ContentType="application/json"  参数格式:  {username:"admin",password:"123} 如果参数不是json类型会报错
        /// ContentType="application/x-www-form-urlencoded" 参数格式:  username=admin&password=123 如果参数是json格式或者参数写错不会报错的
        /// </param>
        /// <returns></returns>
        public static string WebRequestPost(this string url, string body)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST"; //Post请求方式
                request.Accept = Accept;
                // 内容类型
                request.ContentType = ContentType;

                //组织参数
                byte[] buffer = Encoding.GetBytes(body);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);

                //是否启动证书认证
                if (Certificate == true)
                {
                    request.ClientCertificates.Add(X509Certificate.CreateFromCertFile(CertFileName));
                    request.KeepAlive = true;
                }


                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string ReturnXml = reader.ReadToEnd();
                reader.Close();
                response.Close();
                return ReturnXml;
            }
            catch (System.Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }



        /// <summary>
        ///字符串以Post方式请求网页
        /// </summary>
        public static async Task<string>  HttpClientPost(this string url, Dictionary<string, string> para)
        {
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
                using (var httpclient = new HttpClient(handler))
                {
                    httpclient.BaseAddress = new Uri(url);
                    var content = new FormUrlEncodedContent(para);
                    var response = await httpclient.PostAsync(url, content);
                    string responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }

        /// <summary>
        ///字符串以Post方式请求网页
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="body">传递的参数</param>
        /// <param name="headers">
        /// 默认key是Content-Type
        /// 默认value是application/x-www-form-urlencoded
        /// </param>
        /// <returns></returns>
        public static string WebClientPost(this string url, string body, Dictionary<string, string> headers = null)
        {
            try
            {
                byte[] postData = Encoding.GetBytes(body);//编码，尤其是汉字，事先要看下抓取网页的编码方式
                WebClient webClient = new WebClient();
                if (headers == null)
                {
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                }
                else
                {
                    foreach (var item in headers)
                    {

                        webClient.Headers.Add(item.Key, item.Value);
                    }
                }
                byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流 
                string srcString = Encoding.GetString(responseData);//解码
                return srcString;
            }
            catch (System.Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }
    }

}
