using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.OSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Business.OrganizationManage;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Service.SiteManage;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;

namespace YS.Admin.Web.Controllers
{
    public class FileController : BaseController
    {
        #region 上传单个文件
        [HttpPost]
        public async Task<TDataF<string>> UploadFile(int fileModule, IFormCollection fileList)
        {
            bool ischunk = HttpContext.Request.Form.Any(m => m.Key == "chunk");
            TDataF<string> obj = new TDataF<string>();
            if (ischunk)
            {
                string chunk = HttpContext.Request.Form["chunk"];
                string chunks = HttpContext.Request.Form["chunks"];
                string guid = HttpContext.Request.Form["guid"];
                obj = await FileHelper.UploadFile(chunk, chunks, guid, fileModule, fileList.Files);
            }
            else
            {
                obj = await FileHelper.UploadFile(fileModule, fileList.Files);
            }
            return obj;
        }


        private SharedFileBLL sharedFileBLL = new SharedFileBLL();
        [HttpPost]
        public async Task<TDataF<string>> UploadShardFile(int fileModule, IFormCollection fileList)
        {
            bool ischunk = HttpContext.Request.Form.Any(m => m.Key == "chunk");
            TDataF<string> obj = new TDataF<string>();
            if (ischunk)
            {
                string chunk = HttpContext.Request.Form["chunk"];
                string chunks = HttpContext.Request.Form["chunks"];
                string guid = HttpContext.Request.Form["guid"];
                obj = await FileHelper.UploadFile(chunk, chunks, guid, fileModule, fileList.Files);
            }
            else
            {
                obj = await FileHelper.UploadFile(fileModule, fileList.Files);
            }
            SharedFileEntity sharedFileEntity = new SharedFileEntity();
            sharedFileEntity.FilePath = obj.Data;
            sharedFileEntity.FileName = obj.filename;
            sharedFileEntity.FileType = obj.f_ext;
            //如果是图片直接就是 上传路径 
            if (
                obj.f_ext.ToLower().Contains("jpg") ||
                obj.f_ext.ToLower().Contains("jpeg") ||
                obj.f_ext.ToLower().Contains("gif") ||
                obj.f_ext.ToLower().Contains("png") ||
                obj.f_ext.ToLower().Contains("png") 
                )
            {
                sharedFileEntity.ThumbImage = obj.Data;
            }
            else
            {
                sharedFileEntity.ThumbImage = "/images/file/1.jpg"; 
            }
            await sharedFileBLL.SaveForm(sharedFileEntity);

            return obj;
        }

        #endregion
        #region 分片文件合成
        public async Task<TDataF<string>> FileCombine(string guid, string fileExt, string uploadpath)
        {
            TDataF<string> obj = new TDataF<string>();
            obj = await FileHelper.FileCombine(guid, fileExt, uploadpath);
            return obj;
        }
        public async Task<TDataF<string>> FileCombine2(string guid, string fileExt, string uploadpath, string filename)
        {
            TDataF<string> obj = new TDataF<string>();
            obj = await FileHelper.FileCombine2(guid, fileExt, uploadpath, filename);
            return obj;
        }
        #endregion

        #region 删除单个文件
        [HttpPost]
        public TData<string> DeleteFile(int fileModule, string filePath)
        {
            TData<string> obj = FileHelper.DeleteFile(fileModule, filePath);
            return obj;
        }
        #endregion

        #region 下载文件
        [HttpGet]
        public FileContentResult DownloadFile(string filePath, int delete = 1)
        {
            TData<FileContentResult> obj = FileHelper.DownloadFile(filePath, delete);
            if (obj.Tag == 1)
            {
                return obj.Data;
            }
            else
            {
                throw new Exception("下载失败：" + obj.Message);
            }
        }
        #endregion

        /// <summary>
        /// 单/多文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<object>> uploadImagesProc(int fileModule, IFormFileCollection fileList)
        {

            TData<object> obj = new TData<object>();
            string accessKeyId = GlobalContext.SystemConfig.OSSAccessKeyId;//   siteConfig.FilesStorageAccessKeyId;
            string accessKeySecret = GlobalContext.SystemConfig.OSSAccessKeySecret;// siteConfig.FilesStorageAccessKeySecret;
            string endpoint = "https://" + GlobalContext.SystemConfig.OSSEndpoint;// siteConfig.FilesStorageAliYunEndpoint;
            string bucketName = GlobalContext.SystemConfig.OSSBucketName;// siteConfig.FilesStorageAliYunBucketName;
            OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

            obj = await FileHelper.UploadFileProc(fileModule, fileList, bucketName, client);
            if (obj.Tag == 1)
            {
                List<object> resultNewObj = new List<object>();
                List<object> resultObj = (List<object>)obj.Data;
                foreach (var item in resultObj)
                {
                    resultNewObj.Add(GlobalContext.SystemConfig.OSSUrl + "/" + item);
                }
                obj.Data = resultNewObj;
            }

            return obj;

        }
        [HttpPost]
        public async Task<TData<object>> uploadImagesBaseProc(int fileModule, IFormFileCollection fileList)
        {

            TData<object> obj = new TData<object>();


            obj = await FileHelper.UploadBaseFile(fileModule, fileList);
            if (obj.Tag == 1)
            {
                List<object> resultNewObj = new List<object>();
                List<object> resultObj = (List<object>)obj.Data;
                foreach (var item in resultObj)
                {
                    resultNewObj.Add(item);//GlobalContext.SystemConfig.OSSUrl + "/" +
                }
                obj.Data = resultNewObj;
            }

            return obj;

        }


        public static string ConvertDirectoryToHttp(string directory)
        {
            directory = directory.ToString();
            directory = directory.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return directory;
        }


        public static TData CheckFileExtension(string fileExtension, string allowExtension)
        {
            TData obj = new TData();
            string[] allowArr = TextHelper.SplitToArray<string>(allowExtension.ToLower(), '|');
            if (allowArr.Where(p => p.Trim() == fileExtension.ToString().ToLower()).Any())
            {
                obj.Tag = 1;
            }
            else
            {
                obj.Message = "只有文件扩展名是 " + allowExtension + " 的文件才能上传";
            }
            return obj;
        }

        /// <summary>
        /// 删除文件夹及其内容
        /// </summary>
        /// <param name="dir"></param>
    }
}