using System;
using System.IO;
using System.Web;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Enum;
using YS.Admin.Util.Model;
using YS.Admin.Util.Extension;
using Aliyun.OSS;

namespace YS.Admin.Util
{
	public class FileHelper
	{
		#region 创建文本文件
		/// <summary>
		/// 创建文件
		/// </summary>
		/// <param name="path"></param>
		/// <param name="content"></param>
		public static void CreateFile(string path, string content)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
			{
				sw.Write(content);
			}
		}
		#endregion

		#region 上传单个文件
		/// <summary>
		/// 上传单个文件
		/// </summary>
		/// <param name="fileModule"></param>
		/// <param name="fileCollection"></param>
		/// <returns></returns>
		public async static Task<TDataF<string>> UploadFile(int fileModule, IFormFileCollection files)
		{

			string dirModule = string.Empty;
			TDataF<string> obj = new TDataF<string>();
			if (files == null || files.Count == 0)
			{
				obj.Message = "请先选择文件！";
				return obj;
			}
			if (files.Count > 1)
			{
				obj.Message = "一次只能上传一个文件！";
				return obj;
			}
			TData objCheck = null;
			IFormFile file = files[0];
			switch (fileModule)
			{
				case (int)UploadFileType.Portrait:
					objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png");
					if (objCheck.Tag != 1)
					{
						obj.Message = objCheck.Message;
						return obj;
					}
					dirModule = UploadFileType.Portrait.ToString();
					break;

				case (int)UploadFileType.News:


					if (file.Length > 5 * 1024 * 1024) // 5MB
					{
						obj.Message = "文件最大限制为 5MB";
						return obj;
					}
					objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png|.rar");
					if (objCheck.Tag != 1)
					{
						obj.Message = objCheck.Message;
						return obj;
					}
					dirModule = UploadFileType.News.ToString();
					break;
				case (int)UploadFileType.Pdf:


					if (file.Length > 5 * 1024 * 1024) // 5MB
					{
						obj.Message = "文件最大限制为 5MB";
						return obj;
					}
					objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".pdf");
					if (objCheck.Tag != 1)
					{
						obj.Message = objCheck.Message;
						return obj;
					}
					dirModule = UploadFileType.Pdf.ToString();
					break;
				case (int)UploadFileType.Mp4:
					if (file.Length > 5 * 1024 * 1024) // 5MB
					{
						obj.Message = "文件最大限制为 5MB";
						return obj;
					}
					objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".mp4");
					if (objCheck.Tag != 1)
					{
						obj.Message = objCheck.Message;
						return obj;
					}
					dirModule = UploadFileType.Mp4.ToString();
					break;
				case (int)UploadFileType.Apk:
					if (file.Length > 5 * 1024 * 1024) // 5MB
					{
						obj.Message = "文件最大限制为 5MB";
						return obj;
					}
					objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".apk");
					if (objCheck.Tag != 1)
					{
						obj.Message = objCheck.Message;
						return obj;
					}
					dirModule = UploadFileType.Apk.ToString();
					break;
				case (int)UploadFileType.Import:
					objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".xls|.xlsx");
					if (objCheck.Tag != 1)
					{
						obj.Message = objCheck.Message;
						return obj;
					}
					dirModule = UploadFileType.Import.ToString();
					break;

                case (int)UploadFileType.Share:
                    objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".doc|.docx|.mp4|.mp3|.pdf|.apk|.xls|.xlsx|.jpg|.jpeg|.gif|.png|.rar");
                    if (objCheck.Tag != 1)
                    {
                        obj.Message = objCheck.Message;
                        return obj;
                    }
                    dirModule = UploadFileType.Share.ToString();
                    break;

                default:
					obj.Message = "请指定上传到的模块";
					return obj;
			}

			string fileExtension = TextHelper.GetCustomValue(Path.GetExtension(file.FileName), ".png");

			string newFileName = SecurityHelper.GetGuid(true) + fileExtension;
			//if (fileModule.Equals(3))
			//{
			//	newFileName = file.FileName;
			//}
			string dir = "upload" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

			string absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, dir);
			string absoluteFileName = string.Empty;
			if (!Directory.Exists(absoluteDir))
			{
				Directory.CreateDirectory(absoluteDir);
			}
			absoluteFileName = absoluteDir + newFileName;
			try
			{
				using (FileStream fs = File.Create(absoluteFileName))
				{
					await file.CopyToAsync(fs);
					fs.Flush();
				}
				obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
				obj.filename = file.FileName;
				obj.f_ext = fileExtension;
                obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
				obj.Description = (file.Length / 1024).ToString(); // KB
				obj.Tag = 1;
			}
			catch (Exception ex)
			{
				obj.Message = ex.Message;
			}
			return obj;
		}
		#endregion

		#region 分片上传
		/// <summary>
		/// 分片上传
		/// </summary>
		/// <param name="fileModule"></param>
		/// <param name="fileCollection"></param>
		/// <returns></returns>
		public async static Task<TDataF<string>> UploadFile(string chunk, string chunks, string guid, int fileModule, IFormFileCollection files)
		{

			string dirModule = string.Empty;
			TDataF<string> obj = new TDataF<string>();
			if (files == null || files.Count == 0)
			{
				obj.Message = "请先选择文件！";
				return obj;
			}
			if (files.Count > 1)
			{
				obj.Message = "一次只能上传一个文件！";
				return obj;
			}
			TData objCheck = null;
			IFormFile file = files[0];
			switch (fileModule)
			{
				case (int)UploadFileType.Portrait:

					dirModule = UploadFileType.Portrait.ToString();
					break;

				case (int)UploadFileType.News:
					dirModule = UploadFileType.News.ToString();
					break;
				case (int)UploadFileType.Pdf:
					dirModule = UploadFileType.Pdf.ToString();
					break;
				case (int)UploadFileType.Mp4:
					dirModule = UploadFileType.Mp4.ToString();
					break;
				case (int)UploadFileType.Apk:
					dirModule = UploadFileType.Apk.ToString();
					break;
				case (int)UploadFileType.Import:
					dirModule = UploadFileType.Import.ToString();
					break;

				default:
					dirModule = UploadFileType.News.ToString();
					return obj;
			}

			string fileExtension = TextHelper.GetCustomValue(Path.GetExtension(file.FileName), ".png");

			string newFileName = SecurityHelper.GetGuid(true);// + fileExtension;
			string dir = "upload" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

			string absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, dir + guid);
			string absoluteFileName = string.Empty;
			if (!Directory.Exists(absoluteDir))
			{
				Directory.CreateDirectory(absoluteDir);
			}
			absoluteFileName = absoluteDir + "/" + chunk;
			try
			{
				using (FileStream fs = File.Create(absoluteFileName))
				{
					await file.CopyToAsync(fs);
					fs.Flush();
				}
				obj.chunked = true;
				obj.uploadpath = dir;
				obj.f_ext = fileExtension;
				//obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
				obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
				//obj.Description = (file.Length / 1024).ToString(); // KB
				obj.Tag = 1;
			}
			catch (Exception ex)
			{
				obj.Message = ex.Message;
			}
			return obj;
		}
		#endregion
		#region 分片合并

		public async static Task<TDataF<string>> FileCombine(string guid, string fileExt, string uploadpath)
		{
			TDataF<string> obj = new TDataF<string>();
			string newFileName = SecurityHelper.GetGuid(true) + fileExt;
			uploadpath = Path.DirectorySeparatorChar + uploadpath;
			string newFilePath =  uploadpath + newFileName; //上传后的路径
			string root = GlobalContext.HostingEnvironment.ContentRootPath +  uploadpath;
			string sourcePath = Path.Combine(root, guid +"\\");//源数据文件夹
			string targetPath = Path.Combine(root, newFileName);//合并后的文件


			DirectoryInfo dicInfo = new DirectoryInfo(sourcePath);
			if (Directory.Exists(Path.GetDirectoryName(sourcePath)))
			{
				FileInfo[] files = dicInfo.GetFiles();
				foreach (FileInfo file2 in files.OrderBy(f => int.Parse(f.Name)))
				{
					FileStream addFile2 = new FileStream(targetPath, FileMode.Append, FileAccess.Write);
					BinaryWriter AddWriter2 = new BinaryWriter(addFile2);

					//获得上传的分片数据流
					Stream stream2 = file2.Open(FileMode.Open);
					BinaryReader TempReader2 = new BinaryReader(stream2);
					//将上传的分片追加到临时文件末尾
					AddWriter2.Write(TempReader2.ReadBytes((int)stream2.Length));
					//关闭BinaryReader文件阅读器
					TempReader2.Close();
					stream2.Close();
					AddWriter2.Close();
					addFile2.Close();

					TempReader2.Dispose();
					stream2.Dispose();
					AddWriter2.Dispose();
					addFile2.Dispose();
				}
				DeleteDirectory(sourcePath);

				newFilePath = ConvertDirectoryToHttp(newFilePath);
				obj.filepath = newFilePath;
				obj.Data = newFilePath;
				//obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
				//obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
				obj.Tag = 1;


				//context.Response.Write("{\"chunked\" : true, \"hasError\" : false, \"savePath\" :\"" + System.Web.HttpUtility.UrlEncode(targetPath) + "\"}");
				//msg = "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
				//      + newFileName + "\", \"path\": \"" + newFilePath + "\", \"ext\": \"" + fileExt + "\"}";
				//context.Response.Write("{\"hasError\" : false}");
			}
			else
			{
				obj.Message = "";
				obj.Tag = 0;
			}

			return obj;
		}
		public async static Task<TDataF<string>> FileCombine2(string guid, string fileExt, string uploadpath, string filename)
		{
			TDataF<string> obj = new TDataF<string>();
			string newFileName = filename;// SecurityHelper.GetGuid(true) + fileExt;
			uploadpath = Path.DirectorySeparatorChar + uploadpath;
			string newFilePath = uploadpath + newFileName; //上传后的路径
			string root = GlobalContext.HostingEnvironment.ContentRootPath + uploadpath;
			string sourcePath = Path.Combine(root, guid + "/");//源数据文件夹
			string targetPath = Path.Combine(root, newFileName);//合并后的文件


			DirectoryInfo dicInfo = new DirectoryInfo(sourcePath);
			if (Directory.Exists(Path.GetDirectoryName(sourcePath)))
			{
				FileInfo[] files = dicInfo.GetFiles();
				foreach (FileInfo file2 in files.OrderBy(f => int.Parse(f.Name)))
				{
					FileStream addFile2 = new FileStream(targetPath, FileMode.Append, FileAccess.Write);
					BinaryWriter AddWriter2 = new BinaryWriter(addFile2);

					//获得上传的分片数据流
					Stream stream2 = file2.Open(FileMode.Open);
					BinaryReader TempReader2 = new BinaryReader(stream2);
					//将上传的分片追加到临时文件末尾
					AddWriter2.Write(TempReader2.ReadBytes((int)stream2.Length));
					//关闭BinaryReader文件阅读器
					TempReader2.Close();
					stream2.Close();
					AddWriter2.Close();
					addFile2.Close();

					TempReader2.Dispose();
					stream2.Dispose();
					AddWriter2.Dispose();
					addFile2.Dispose();
				}
				DeleteDirectory(sourcePath);
				newFilePath = ConvertDirectoryToHttp(newFilePath);
				obj.filepath = newFilePath;
				obj.Data = newFilePath;
				//obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
				//obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
				obj.Tag = 1;


				//context.Response.Write("{\"chunked\" : true, \"hasError\" : false, \"savePath\" :\"" + System.Web.HttpUtility.UrlEncode(targetPath) + "\"}");
				//msg = "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
				//      + newFileName + "\", \"path\": \"" + newFilePath + "\", \"ext\": \"" + fileExt + "\"}";
				//context.Response.Write("{\"hasError\" : false}");
			}
			else
			{

				obj.Message = "";
				obj.Tag = 0;
			}

			return obj;
		}
		#endregion
		#region 删除单个文件
		/// <summary>
		/// 删除单个文件
		/// </summary>
		/// <param name="fileModule"></param>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static TData<string> DeleteFile(int fileModule, string filePath)
		{
			TData<string> obj = new TData<string>();
			string dirModule = fileModule.GetDescriptionByEnum<UploadFileType>();

			if (string.IsNullOrEmpty(filePath))
			{
				obj.Message = "请先选择文件！";
				return obj;
			}
			filePath = "Resource" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + filePath;
			string absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, filePath);
			try
			{
				if (File.Exists(absoluteDir))
				{
					File.Delete(absoluteDir);
				}
				else
				{
					obj.Message = "文件不存在";
				}
				obj.Tag = 1;
			}
			catch (Exception ex)
			{
				obj.Message = ex.Message;
			}
			return obj;
		}
		#endregion

		#region 下载文件
		/// <summary>
		/// 下载文件
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="delete"></param>
		/// <returns></returns>
		public static TData<FileContentResult> DownloadFile(string filePath, int delete)
		{
			TData<FileContentResult> obj = new TData<FileContentResult>();
			string absoluteFilePath = GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			byte[] fileBytes = File.ReadAllBytes(absoluteFilePath);
			if (delete == 1)
			{
				File.Delete(absoluteFilePath);
			}
			string fileNamePrefix = DateTime.Now.ToString("yyyyMMddHHmmss");
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
			string title = string.Empty;
			if (fileNameWithoutExtension.Contains("_"))
			{
				title = fileNameWithoutExtension.Split('_')[1].Trim();
			}
			string fileExtensionName = Path.GetExtension(filePath);
			obj.Data = new FileContentResult(fileBytes, "application/octet-stream")
			{
				FileDownloadName = string.Format("{0}_{1}{2}", fileNamePrefix, title, fileExtensionName)
			};
			obj.Tag = 1;
			return obj;
		}
		#endregion

		#region GetContentType
		public static string GetContentType(string path)
		{
			var types = GetMimeTypes();
			var ext = Path.GetExtension(path).ToLowerInvariant();
			var contentType = types[ext];
			if (string.IsNullOrEmpty(contentType))
			{
				contentType = "application/octet-stream";
			}
			return contentType;
		}
		#endregion

		#region GetMimeTypes
		public static Dictionary<string, string> GetMimeTypes()
		{
			return new Dictionary<string, string>
			{
				{".txt", "text/plain"},
				{".pdf", "application/pdf"},
				{".doc", "application/vnd.ms-word"},
				{".docx", "application/vnd.ms-word"},
				{".xls", "application/vnd.ms-excel"},
				{".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
				{".png", "image/png"},
				{".jpg", "image/jpeg"},
				{".jpeg", "image/jpeg"},
				{".gif", "image/gif"},
				{".csv", "text/csv"}
			};
		}
		#endregion

		public static void CreateDirectory(string directory)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}

		public static void DeleteDirectory(string filePath)
		{
			try
			{
				if (Directory.Exists(filePath)) //如果存在这个文件夹删除之 
				{
					foreach (string d in Directory.GetFileSystemEntries(filePath))
					{
						if (File.Exists(d))
							File.Delete(d); //直接删除其中的文件                        
						else
							DeleteDirectory(d); //递归删除子文件夹 
					}
					Directory.Delete(filePath, true); //删除已空文件夹                 
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex);
			}
		}

		public static string ConvertDirectoryToHttp(string directory)
		{
			directory = directory.ToString();
			directory = directory.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			return directory;
		}

		public static string ConvertHttpToDirectory(string http)
		{
			http = http.ToString();
			http = http.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			return http;
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

		#region 上传多个文件
		/// <summary>
		/// 上传多个文件
		/// </summary>
		/// <param name="fileModule"></param>
		/// <param name="files">文件流集合</param>
		/// <returns></returns>
		public async static Task<TData<object>> UploadFileProc(int fileModule, IFormFileCollection files, string bucketName, OssClient client)
		{
			string dirModule = string.Empty;
			TData<object> obj = new TData<object>();
			if (files == null || files.Count == 0)
			{
				obj.Message = "请先选择文件！";
				return obj;
			}
			//IFormFile file = files[0];
			List<object> resultObj = new List<object>();
			foreach (IFormFile file in files)
			{
				TData objCheck = null;
				objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png");
				if (objCheck.Tag != 1)
				{
					obj.Tag = 0;
					obj.Message = objCheck.Message;
					return obj;
				}
				switch (fileModule)
				{
					case (int)UploadFileType.Portrait:
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png|.jfif");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Portrait.ToString();
						break;
					case (int)UploadFileType.News:
						if (file.Length > 50 * 1024 * 1024) // 5MB
						{
							obj.Message = "文件最大限制为 50MB";
							return obj;
						}
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.jfif|.gif|.png|.tif|.jpeg|.rar|.pdf|.doc|.docx|.txt|.ppt|.pptx|.zip|.mp4");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.News.ToString();
						break;
					case (int)UploadFileType.Pdf:


						if (file.Length > 5 * 1024 * 1024) // 5MB
						{
							obj.Message = "文件最大限制为 5MB";
							return obj;
						}
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".pdf");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Pdf.ToString();
						break;
					case (int)UploadFileType.Mp4:


						if (file.Length > 5 * 1024 * 1024) // 5MB
						{
							obj.Message = "文件最大限制为 5MB";
							return obj;
						}
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".mp4");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Mp4.ToString();
						break;
					case (int)UploadFileType.Import:
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".xls|.xlsx");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Import.ToString();
						break;
					default:
						obj.Message = "请指定上传到的模块";
						return obj;
				}
				string fileExtension = TextHelper.GetCustomValue(Path.GetExtension(file.FileName), ".png");

				string newFileName = SecurityHelper.GetGuid(true) + fileExtension;
				string dir = dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

				string FilePath = Path.Combine(dir.Replace("\\", "/"), newFileName);
				try
				{
					//Stream fileStream = new MemoryStream(byteData)

					MemoryStream ms = new MemoryStream();
					file.CopyTo(ms);
					byte[] byteData = ms.ToArray();

					Stream fileStream = new MemoryStream(byteData);
					string md5 = Aliyun.OSS.Util.OssUtils.ComputeContentMd5(fileStream, fileStream.Length);
					//将文件md5值赋值给meat头信息，服务器验证文件MD5  
					var objectMeta = new Aliyun.OSS.ObjectMetadata
					{
						ContentMd5 = md5,
					};
					if (client != null)
					{
						var c = client.PutObject(bucketName, FilePath, fileStream, objectMeta);
					}
					//obj.Data = FilePath;
					resultObj.Add(FilePath);
					//obj.filepath = FilePath;
					//obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
					//obj.filename = file.FileName;
					//obj.Description = (file.Length / 1024).ToString(); // KB
					//obj.fileModule = fileModule;
					obj.Tag = 1;
				}
				catch (Exception ex)
				{
					obj.Tag = 0;
					obj.Message = ex.Message;

				}
			}
			if (resultObj.Count > 0)
			{
				obj.Tag = 1;
				obj.Message = "上传成功";
				obj.Data = resultObj;
			}
			return obj;
		}
		/// <summary>
		/// 上传多个文件
		/// </summary>
		/// <param name="fileModule"></param>
		/// <param name="files">文件流集合</param>
		/// <returns></returns>
		public async static Task<TData<object>> UploadBaseFile(int fileModule, IFormFileCollection files)
		{
			string dirModule = string.Empty;
			TData<object> obj = new TData<object>();
			if (files == null || files.Count == 0)
			{
				obj.Message = "请先选择文件！";
				return obj;
			}
			//IFormFile file = files[0];
			List<object> resultObj = new List<object>();
			foreach (IFormFile file in files)
			{
				TData objCheck = null;
				objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png");
				if (objCheck.Tag != 1)
				{
					obj.Tag = 0;
					obj.Message = objCheck.Message;
					return obj;
				}
				switch (fileModule)
				{
					case (int)UploadFileType.Portrait:
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png|.jfif");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Portrait.ToString();
						break;
					case (int)UploadFileType.News:
						if (file.Length > 50 * 1024 * 1024) // 5MB
						{
							obj.Message = "文件最大限制为 50MB";
							return obj;
						}
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.jfif|.gif|.png|.tif|.jpeg|.rar|.pdf|.doc|.docx|.txt|.ppt|.pptx|.zip|.mp4");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.News.ToString();
						break;
					case (int)UploadFileType.Pdf:


						if (file.Length > 5 * 1024 * 1024) // 5MB
						{
							obj.Message = "文件最大限制为 5MB";
							return obj;
						}
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".pdf");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Pdf.ToString();
						break;
					case (int)UploadFileType.Mp4:


						if (file.Length > 5 * 1024 * 1024) // 5MB
						{
							obj.Message = "文件最大限制为 5MB";
							return obj;
						}
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".mp4");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Mp4.ToString();
						break;
					case (int)UploadFileType.Import:
						objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".xls|.xlsx");
						if (objCheck.Tag != 1)
						{
							obj.Message = objCheck.Message;
							return obj;
						}
						dirModule = UploadFileType.Import.ToString();
						break;
					default:
						obj.Message = "请指定上传到的模块";
						return obj;
				}
				string fileExtension = TextHelper.GetCustomValue(Path.GetExtension(file.FileName), ".png");

				string newFileName = SecurityHelper.GetGuid(true) + fileExtension;
				//if (fileModule.Equals(3))
				//{
				//	newFileName = file.FileName;
				//}
				string dir = "upload" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

				string absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, dir);
				string absoluteFileName = string.Empty;
				if (!Directory.Exists(absoluteDir))
				{
					Directory.CreateDirectory(absoluteDir);
				}
				absoluteFileName = absoluteDir + newFileName;
				try
				{
					//Stream fileStream = new MemoryStream(byteData)

					MemoryStream ms = new MemoryStream();
					using (FileStream fs = File.Create(absoluteFileName))
					{
						await file.CopyToAsync(fs);
						fs.Flush();
					}
					string FilePath = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
					obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));

					obj.Tag = 1;

					resultObj.Add(FilePath);

					obj.Tag = 1;
				}
				catch (Exception ex)
				{
					obj.Tag = 0;
					obj.Message = ex.Message;

				}
			}
			if (resultObj.Count > 0)
			{
				obj.Tag = 1;
				obj.Message = "上传成功";
				obj.Data = resultObj;
			}
			return obj;
		}
		#endregion
	}
}
