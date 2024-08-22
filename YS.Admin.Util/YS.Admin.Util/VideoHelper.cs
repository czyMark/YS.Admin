using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Xabe.FFmpeg;
//using Xabe.FFmpeg.Downloader;

//using MediaInfoLib;
namespace YS.Admin.Util
{
	public class VideoHelper
	{

		//public static async Task<int> GetVideoDuration(string filePaths)
		//{
		//	int videoDuration = 0;
		//	try
		//	{
		//		// 下载并设置FFmpeg
		//		//await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
		//		// 设置FFmpeg可执行文件路径
		//		//string ffmpegPath = @"C:\ffmpeg\bin";
		//		string ffmpegPath = GlobalContext.HostingEnvironment.ContentRootPath + "\\ffmpeg";
		//		Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + ffmpegPath);
		//		// 指定MP4文件路径
		//		string filePath = filePaths;

		//		// 获取视频信息
		//		IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);

		//		// 获取视频流信息
		//		IVideoStream videoStream = mediaInfo.VideoStreams.FirstOrDefault();

		//		if (videoStream != null)
		//		{
		//			TimeSpan duration = videoStream.Duration;

		//			videoDuration = duration.Seconds;
		//			//Console.WriteLine($"Duration: {duration.Hours}h {duration.Minutes}m {duration.Seconds}s");
		//		}
		//		else
		//		{
		//			//Console.WriteLine("Failed to get the duration of the file.");
		//		}
		//	}
		//	catch
		//	{
		//	}
		//	return videoDuration;
		//}


		public static int GetVideoDuration(string filePaths)
		{
			int videoDuration = 0;
			try
			{
				string filePath = filePaths;
				var file = TagLib.File.Create(filePath);
				TimeSpan duration = file.Properties.Duration;
				videoDuration = UtilsHelper.StrToInt(duration.TotalSeconds.ToString(), 0);
			}
			catch
			{

			}

			return videoDuration;
		}
	}
}
